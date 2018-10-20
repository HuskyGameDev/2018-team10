#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GenShadowColliders : EditorWindow {

	bool whitelist = false;
	List<string> tags = new List<string>();

	[MenuItem ("Window/Generate Shadow Colliders")]
	public static void ShowWindow(){
		EditorWindow.GetWindow(typeof(GenShadowColliders));
	}

	void OnGUI(){
		//GUILayout.Label("Generate Shadow Colliders", EditorStyles.boldLabel);
		GUILayout.Label("Tags", EditorStyles.boldLabel);
		
		whitelist = EditorGUILayout.Toggle("Whitelist?", whitelist);
		
		int newSize = EditorGUILayout.IntField("Number of Tags", tags.Count);
		ChangeCount<string>(tags, "", newSize);
		
		for(int i = 0; i < tags.Count; i++){
			tags[i] = EditorGUILayout.TagField("Tag " + i, tags[i]);
		}

		if(GUILayout.Button("Generate Shadow Colliders")){
			GenerateShadowColliders();
		}

	}

	void GenerateShadowColliders(){
		//Get the cube mesh
		Material darkMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/DarkMaterial.mat");;
		
		//Get all game objects, find box colliders,
		//Then attach a cube mesh to them that will generate shadows
		//If they have/don't have one of the tags in our tags array,
		//Depending on whether this is a blacklist or whitelist.
		Scene scene = EditorSceneManager.GetActiveScene();
		GameObject[] rootGameObjects = scene.GetRootGameObjects();
		for(int i = 0; i < rootGameObjects.Length; i++){
			//Get all BoxCollider components from this root gameobject.
			BoxCollider2D[] childComponents = rootGameObjects[i].GetComponentsInChildren<BoxCollider2D>();
			List<BoxCollider2D> allComponents = new List<BoxCollider2D>(childComponents);
			
			bool dirty = false;
			//For every box collider, check it's tag, then
			//if that checks out, copy our cube into it. 
			foreach(BoxCollider2D boxCollider in allComponents){
				if(CanAddShadowCaster(boxCollider)){
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.name = "ShadowCaster";
					DestroyImmediate(cube.GetComponent<BoxCollider>());

					//Parent object will be the object with the box collider
					Undo.RegisterCreatedObjectUndo(cube, "Created ShadowCaster");
					//Undo.SetTransformParent(cube.transform, boxCollider.gameObject.transform, "Set Shadow Caster Parent");
					GameObjectUtility.SetParentAndAlign(cube, boxCollider.gameObject);
					cube.layer = 9; // Set to ShadowCaster layer!
					cube.isStatic = false;

					MeshRenderer meshRenderer = cube.GetComponent<MeshRenderer>();

					meshRenderer.allowOcclusionWhenDynamic = false;
					meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
					meshRenderer.receiveShadows = false;
					Material[] materials = { darkMaterial };
					meshRenderer.materials = materials;

					//Set it to have the same length/width and offset as the boxCollider,
					//with a z position of 1.5, and a z scale of 3

					cube.transform.localPosition = new Vector3(boxCollider.offset.x, boxCollider.offset.y, 1.5f);
					cube.transform.localScale = new Vector3(boxCollider.size.x, boxCollider.size.y, 3f);
					cube.transform.localEulerAngles = new Vector3(0, 0, 0);

					dirty = true;
				}
			}

			//Mark the current scene as dirty, if any gameobjects changed
			if(dirty){ 
				EditorSceneManager.MarkSceneDirty(scene);
				//Set the name of all the undo operations we just created.
				Undo.SetCurrentGroupName("Generate Shadow Casters");
			}
		}
	}

	public bool CanAddShadowCaster(BoxCollider2D boxCollider){
		//if it is in the list and whitelisted, return true.
		//If it's not in the list and the list is a blacklist, return true.
		//Else return false.
		return tags.Contains(boxCollider.tag) == whitelist &&
			boxCollider.gameObject.transform.Find("ShadowCaster") == null;
	}

	//Mutates the list to have the given count; Truncates if it's less than the given
	//count, otherwise grows it by adding the default value until it has count.
	//Returns the mutated list.
	public static List<T> ChangeCount<T>(List<T> list, T defaultValue, int count){
		int deltaCount = count - list.Count;
		if(deltaCount < 0){
			list.RemoveRange(list.Count + deltaCount, -deltaCount);
		}else if(deltaCount > 0){
			list.AddRange(System.Linq.Enumerable.Repeat<T>(defaultValue, deltaCount));
		}

		return list;
	}
}

#endif

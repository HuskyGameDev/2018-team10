using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class EmployeesMenu : MonoBehaviour {
    public RectTransform doug;
    public RectTransform brandon;
    public RectTransform pengwin;
    public static bool complete = false;

    // Use this for initialization
    void Start () {

        // Update positions if completed
        if (complete)
        {
            doug.position = new Vector3(doug.position.x + 90, doug.position.y, doug.position.z);
            brandon.position = new Vector3(brandon.position.x - 70, brandon.position.y, brandon.position.z);
            pengwin.position = new Vector3(pengwin.position.x, pengwin.position.y + 300, pengwin.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trigger : MonoBehaviour {

    public bool isLit = true;
    public Torch_Toggle torch;

    void Update()
    {
        LightCheck();
    }

    //check to see if nearby light is on
    void LightCheck()
    {
        isLit = torch.isLit;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cheats : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.L))
            Globals.NextLevel();
    }
}

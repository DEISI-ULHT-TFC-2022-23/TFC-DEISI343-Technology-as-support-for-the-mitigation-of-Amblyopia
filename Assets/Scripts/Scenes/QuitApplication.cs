using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)){
            Debug.Log("Escape Pressed");
            Application.Quit();
        }
    }
}

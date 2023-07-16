using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] int MenuSide = 0;
    // Start is called before the first frame update
    void Start()
    {
        // check which side should be visible, depending on amblyopic eye
        if (Globals.amblyopicEye == MenuSide){
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

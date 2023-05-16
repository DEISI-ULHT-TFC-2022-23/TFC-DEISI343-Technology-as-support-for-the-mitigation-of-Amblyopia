using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnvironmentVariables : MonoBehaviour
{
    [SerializeField] public Globals.SceneType sceneType = Globals.SceneType.Invalid ; 
    [SerializeField] public Material DominantEyeMaterial ;
    [SerializeField] public Material WeakEyeMaterial ;
    
    // Start is called before the first frame update
    void Start()
    {
        // adjust scene tracking global setting
        Globals.SetSceneType(sceneType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

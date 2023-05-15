/* 
    Manage scene's projection, depending on level definition
        Perspective : a pure, realistic 3D approach
        Orthogonal  : an adjusted view as if vision perspective would not exist (good to simulate 2D)
    https://www.youtube.com/watch?v=eCZw0j_AunM
    https://docs.unity3d.com/Manual/class-Camera.html
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneProjection : MonoBehaviour
{
    [SerializeField] Globals.ProjectionType ProjectionType;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        SetProjectionType();
        Debug.Log(cam.orthographic );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // establish camera projection setting, based on scene config
    void SetProjectionType(){
        if ( ProjectionType == Globals.ProjectionType.Perspective ){
            cam.orthographic = false ;
        } else {
            cam.orthographic = true ;
        }
    }
}

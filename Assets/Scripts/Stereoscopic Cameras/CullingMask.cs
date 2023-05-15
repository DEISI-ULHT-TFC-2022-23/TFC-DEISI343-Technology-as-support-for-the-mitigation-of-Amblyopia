using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class CullingMask : MonoBehaviour
{
    
    // Culling Mask
    [SerializeField] public int CameraID    ;
    
    private Camera cm;
    
    void Start()
    {
        cm = GetComponent<Camera>();
        setCullingMask();
    }

    void Update()
    {
        if (GetUCullingMaskpdateFlag(CameraID)) {setCullingMask();}
    }

    void setCullingMask(){
        cm.cullingMask =  Globals.GetCullingMask( (Side)CameraID, (Side)amblyopicEye);
        ClearCullingMaskUpdateFlag(CameraID);

        // Debug.Log(CameraID + ": " + cm.cullingMask);
    }


}

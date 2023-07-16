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
        cm.cullingMask += (1 << (Globals.CullingMaskLeftCameraBit -1 + CameraID) );    // Only left camera layer (13) + CameraID (left:0, right: 1)
        ClearCullingMaskUpdateFlag(CameraID);

        // Debug.Log(CameraID + ": " + cm.cullingMask);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Globals;
using lvl = LevelManager;

public class PieceSetPiece : MonoBehaviour
{
    [SerializeField] int movingPiece    ;                                       // identify piece's relative position
    [SerializeField] Rigidbody  rb      ;                                       //

    // Start is called before the first frame update
    void Start()
    {   
        SetPieceMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovementDefinition(int side, int eye){
        if ( ( side == 0 ^ eye == 0 ) ) {
            movingPiece = 0         ;                                           // dominant eye: movement == false
            rb.isKinematic = true   ;                                           // object has no physics calculations
        } else {
            movingPiece = 1         ;                                           // weak eye: movement == true
            rb.isKinematic = false  ;                                           // object is impacted by game engine physics
        }
        applyObjRestrictions()      ;                                           // base on moving definition, retrict object axis freedom
    }

    public void SetCullingLayerMask( int side, int eye){
        // requires movingPiece value already set
        if (rb.isKinematic) return;
        gameObject.SetLayer( GetCullingLayerMask( (Side) side , (Side) eye) ); // applyes to all children
    }

    public int getMoveSetting(){
        return movingPiece;
    }

    void SetPieceMaterial(){
        // get baseComponent variables
        GameObject ObjectVars = GameObject.FindGameObjectWithTag(TAGS_SCENE_ENVIRONMENT_VARS);
        SceneEnvironmentVariables SceneVars = (SceneEnvironmentVariables) ObjectVars.GetComponent(typeof(SceneEnvironmentVariables));
        Material materialDominantEye        = SceneVars.DominantEyeMaterial     ;
        Material materialWeakEye            = SceneVars.WeakEyeMaterial         ;

        if ( movingPiece == 1 ) { gameObject.ApplyMaterial(materialWeakEye)     ;}  // weak eye
        else                    { gameObject.ApplyMaterial(materialDominantEye) ;}  // dominant eye
    }

    public bool CanMove(){
        return movingPiece == 1 ;
    }

    void applyObjRestrictions(){
        switch (movingPiece){
            case 0 :
                rb.constraints = RigidbodyConstraints.FreezeAll;    // gameObject's position is fixed
                break;
            case 1 :
                rb.constraints = RigidbodyConstraints.None;
                if ( lvl.axisMovFreedom[(int) Axis.x] == 0f) { rb.constraints |= RigidbodyConstraints.FreezePositionX; }
                if ( lvl.axisMovFreedom[(int) Axis.y] == 0f) { rb.constraints |= RigidbodyConstraints.FreezePositionY; }
                if ( lvl.axisMovFreedom[(int) Axis.z] == 0f) { rb.constraints |= RigidbodyConstraints.FreezePositionZ; }
                if ( lvl.axisRotFreedom[(int) Axis.x] == 0f) { rb.constraints |= RigidbodyConstraints.FreezeRotationX; }
                if ( lvl.axisRotFreedom[(int) Axis.y] == 0f) { rb.constraints |= RigidbodyConstraints.FreezeRotationY; }
                if ( lvl.axisRotFreedom[(int) Axis.z] == 0f) { rb.constraints |= RigidbodyConstraints.FreezeRotationZ; }
                break;
            default: 
                break;
        }
    }
}

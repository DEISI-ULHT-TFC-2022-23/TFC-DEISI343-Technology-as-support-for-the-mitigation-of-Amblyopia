using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Globals;

public class PieceSetPiece : MonoBehaviour
{
    [SerializeField] int movingPiece ;                                                   // identify piece's relative position
   
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
            movingPiece = 0 ;                                                   // dominant eye: movement == false
        } else {
            movingPiece = 1 ;                                                   // weak eye: movement == true
        }   
    }

    public void SetCullingLayerMask( int side, int eye){
        // requires movingPiece value already set
        //gameObject.layer = Globals.GetCullingLayerMask( (Side) side , (Side) eye);
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
}

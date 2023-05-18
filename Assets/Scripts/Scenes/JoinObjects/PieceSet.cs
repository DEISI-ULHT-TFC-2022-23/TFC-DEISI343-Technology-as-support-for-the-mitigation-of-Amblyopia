/*
    PieceSet levels are chalenges where users must join piece sets in a defined
    position configuration. Once the level is loaded, this script will:
        a)  list pieces & identify each one's specific ContactPoint  (may evolve 
            managing multiple contact points per piece)
        b)  ContactPoint: a specific prism in the  environment with minimal mass
            to establish a contact coordinate for that specific piece
        c)  on every frame, the level will assess all piece's  ContactPoints are
            within a 3 dimentional distance  from each other. The  distance must
            be within precision setting for each of the  axis (not the euclidean
            distance)
        d)  piece  grouping must have assigned  tag "PieceSet". Always, Leftmost 
            piece must have tag "Piece00" and rightmost with  tag "Piece01" (the 
            latter depending on th existance of only 2 pieces)
        e)  proximity precision is calculated taking in account "bounds" size of
            first piece. For this, piece (or pieces) must have a collider 

    Depending on the Amblyopic eye setting (left or right eye), this class must
    be able to adjust:
        1. set fixed (dominant) and moving (weaker eye) pieces
        2. specific color definition for both pieces
        3. manage moving piece, based on user input
        4. calculate 3 axis distances and check them against config precision

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using lvl = LevelManager;

public class PieceSet : MonoBehaviour
{
    [SerializeField] Rigidbody[] rbContactPoints                ;
    
    public  Vector3[]       contactPoints                       ;               // pieces contact point's coordinates (3 axis)

    private GameObject      gmGroup                             ;               // pieces grouping object: THIS OBJECT !!  @@@ check if required
    private GameObject[]    gameObjects     = { null, null }    ;               // [0]: moving | [1]: fixed
    private Vector3         targetDistances                     ;

    private Bounds          bounds                              ;

    private void Awake() {
        getContactPoints();
        //Globals.SetCullingMaskUpdateFlag();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetDistances = DefineTargetDistances();
    }

    // Update is called once per frame
    void Update()
    {
        FinalPositionReached();
    }

    void getContactPoints(){
        gameObjects[0] = GameObject.FindGameObjectWithTag("Piece00" );
        gameObjects[1] = GameObject.FindGameObjectWithTag("Piece01" );

        if ( gameObjects[0] == null || gameObjects[1] == null ){
            Globals.Error(Globals.ERROR_CODE_PIECES_IDENTIFICATION, "Pieces not identified for PieceSet.", Globals.ErrorType.Critical);
            return;
        }

        bounds = gameObjects[0].GetComponent<Collider>().bounds;        
        
        PieceSetPiece script;
        ManageObjectSide(gameObjects[0], 0);
        script = (PieceSetPiece) gameObjects[0].GetComponent(typeof(PieceSetPiece));

        ManageObjectSide(gameObjects[1], 1);
        script = (PieceSetPiece) gameObjects[1].GetComponent(typeof(PieceSetPiece));
    }

    void ManageObjectSide(GameObject go, int side){
        PieceSetPiece script;
        
        // get the the object script
        script = (PieceSetPiece) go.GetComponent(typeof(PieceSetPiece));

        // invoke the script for movement definition settings
        script.SetMovementDefinition( side, Globals.amblyopicEye );

        // invoke corresponding script for layer maskdefinition
        script.SetCullingLayerMask( side, Globals.amblyopicEye );
    }

    Vector3 DefineTargetDistances(){
        // target distances (precision), depend on first piece's collider box size (bounds)
        return 
            Vector3.forward * /* lvl.axisMovFreedom[2] * */ lvl.precision * bounds.size.z +
            Vector3.up      * /* lvl.axisMovFreedom[1] * */ lvl.precision * bounds.size.y + 
            Vector3.right   * /* lvl.axisMovFreedom[0] * */ lvl.precision * bounds.size.x ; // Calculate distances' vector
    }

    // detect if pieces are close enough in all 3 axis
    // based on level defined precision
    bool FinalPositionReached(){
        // target distances (precision), depend on first piece's collider box size (bounds)
        // check if moving object's center is within reach of the target object
        if ( Mathf.Abs( rbContactPoints[0].position.y - rbContactPoints[1].position.y ) < targetDistances.y && 
             Mathf.Abs( rbContactPoints[0].position.x - rbContactPoints[1].position.x ) < targetDistances.x &&
             Mathf.Abs( rbContactPoints[0].position.z - rbContactPoints[1].position.z ) < targetDistances.z ){  
                Debug.Log("Completo...!!");
                lvl.Complete();                                                 // set level to complete
                return true;
             } 
             
        return false;
    }
}

/*
    PieceSet level are game chalenges where users must join pieces in a defined
    position configuration. Once the level is loaded, this script will:
        a) list pieces & identify each one's specific ContactPoint  (may evolve 
           managing multiple contact points per piece)
        b) ContactPoint: a specific prism in the  environment with minimal mass
           to establish a contact coordinate for that specific piece
        c) on every frame, the level will assess all piece's  ContactPoints are
           within a 3 dimentional distance  from each other. The  distance must
           be within precision setting for each of the  axis (not the euclidean
           distance)
        d) piece  grouping must have assigned  tag "PieceSet". Always, Leftmost 
           piece must have tag "Piece00" and rightmost with  tag "Piece01" (the 
           latter depending on th existance of only 2 pieces)

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

public class PieceSet : MonoBehaviour
{
    GameObject      gmGroup         ;                                           // pieces grouping object: THIS OBJECT !!  @@@ check if required
    GameObject[]    gameObjects     = { null, null }  ;                                           // [0]: moving | [1]: fixed
    Vector3[]       contactPoints   ;                                           // pieces contact point's coordinates (3 axis)

    private void Awake() {
        getContactPoints();     
        Globals.SetCullingMaskUpdateFlag();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getContactPoints(){
        gameObjects[0] = GameObject.FindGameObjectWithTag("Piece00"     );
        gameObjects[1] = GameObject.FindGameObjectWithTag("Piece01"    );

        if ( gameObjects[0] == null || gameObjects[1] == null ){
            Globals.Error(Globals.ERROR_CODE_PIECES_IDENTIFICATION, "Pieces not identified for PieceSet.", Globals.ErrorType.Critical);
            return;
        }
        
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
}

/* 
    Class accountable of managing game piece objects movement, for both gamepad
    and keyboard inputs (when debugging).

    Level Completion is managed by the pieces aggregator script (PieceSet.cs),
    since is is already aware of all the child objects in its hierarchy.
*/

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using static Globals;
using static Controls;
using lvl = LevelManager;

public class PieceSetPieceMovement : MonoBehaviour
{
    [SerializeField] 
    private PieceSetPiece   parent                                          ;
    private Vector3         movementAxis = new Vector3( 0, 0, 0 )           ;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // handle piece movement
        HandlePieceMovement();
    }

    void HandlePieceMovement(){
        if ( ! parent.CanMove() )   { return    ; }                                 // only some pieces are allowed to move 
        if ( ! lvl.IsActive()   )   { return    ; }                                 // pieces can only move if level is active

        // check user input reagrding axis
        movementAxis = HandleAxisInput();
        //Debug.Log($"{movementAxis[0]}, {movementAxis[1]}, {movementAxis[2]}");
        MovePiece();
        //Debug.Log($"gameObject.translation: {gameObject.transform.position}");
    }

    void MovePiece(){
        if ( ! lvl.IsActive() ){ return ; }                                     // Check if level is active

        // Scene environment variables, can restrict axis movement and rotation
        // to specific axis

        gameObject.transform.Translate( calcMovementOffset() ) ;
    }

    float axisMovement(Axis paxis){
        return movementAxis[(int) paxis];
    }

    Vector3 calcMovementOffset( ){
        Vector3 displacement = new Vector3(0, 0, 0);
        displacement = 
            (Vector3.right      * axisMovement(Axis.x) * lvl.axisScaledSpeed(Axis.x)) +                // x
            (Vector3.up         * axisMovement(Axis.y) * lvl.axisScaledSpeed(Axis.y)) +                // y
            (Vector3.forward    * axisMovement(Axis.z) * lvl.axisScaledSpeed(Axis.z))                  // z
        ;
        return displacement;
    }
}   

/*
    This is supposed to be a general movement manager for object movement based chalenges
    Some types of movement that may occur:
        Default         (advance a predefied distance per move. e.g., move an object freely in scene space)
        IntegerSteps    (predefined steps. e.g., move a Tetris piece or advance frog in level position)
        ...
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ObjectMovement : MonoBehaviour
{    
    //[SerializeField] private Globals.MovementType sceneMovementType = Globals.MovementType.Default ;
    [SerializeField] private Rigidbody rb                   ;                   // the moving component
    [SerializeField] private Rigidbody targetRb             ;                   // the target gameobject's rigidbody
    [SerializeField] private float movementFactor           ;                   // movement speed factor 
    [SerializeField] private int[] targetAxis = {1, 1, 0}   ;                   // detrmines which axis position must be compared to reach goal
    [SerializeField] private float precisionFactor = 0.025f ;                   // precision definition (should be a scale and not a distance)

    private List<int> movementVector        = new List<int>() { 0, 0 , 0, 0 } ; 
    private bool levelComplete              = false                           ; // describes if the level is already completed
    private Vector3 distances                                                 ; // calculated min distance per axis to complete challenge

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var rbBounds = GetComponent<Collider>().bounds;

        // Debug.Log(targetRb.position);

        // Calculate distances' vector
        distances = Vector3.up    * targetAxis[1] * precisionFactor * rbBounds.size.y + 
                    Vector3.right * targetAxis[0] * precisionFactor * rbBounds.size.x ;
        
        // Debug.Log(distances);
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelComplete){ handleCommands(); }
    }

    void handleCommands(){

        if (Input.GetKey (KeyCode.LeftArrow)    || GamepadUtils.isButtonPressed("Left") )   { movementVector[0] = 1 ; }
        if (Input.GetKey (KeyCode.UpArrow)      || GamepadUtils.isButtonPressed("Up") )     { movementVector[1] = 1 ; }
        if (Input.GetKey (KeyCode.RightArrow)   || GamepadUtils.isButtonPressed("Right") )  { movementVector[2] = 1 ; }
        if (Input.GetKey (KeyCode.DownArrow)    || GamepadUtils.isButtonPressed("Down") )   { movementVector[3] = 1 ; }

        
        if (Input.GetKeyUp (KeyCode.LeftArrow)  && !GamepadUtils.isButtonPressed("Left") )  { movementVector[0] = 0 ; }
        if (Input.GetKeyUp (KeyCode.UpArrow)    && !GamepadUtils.isButtonPressed("Up") )    { movementVector[1] = 0 ; }
        if (Input.GetKeyUp (KeyCode.RightArrow) && !GamepadUtils.isButtonPressed("Right") ) { movementVector[2] = 0 ; }
        if (Input.GetKeyUp (KeyCode.DownArrow)  && !GamepadUtils.isButtonPressed("Down") )  { movementVector[3] = 0 ; }

        if (movementVector.Exists( x => x != 0)) {
            CalcPosition();
            levelComplete = checkIfLevelCompleted();
        }
    }

    void CalcPosition(){
        rb.position += 
            Vector3.left    * movementVector[0] * movementFactor    +
            Vector3.up      * movementVector[1] * movementFactor    +
            Vector3.right   * movementVector[2] * movementFactor    +
            Vector3.down    * movementVector[3] * movementFactor    ;

        Debug.Log("------\nStar position: " + rb.position);
        Debug.Log("Cyl  position: ");
    }

    bool checkIfLevelCompleted(){
        // check if moving object's center is within reach of the target object
        if ( Math.Abs( rb.position.y - targetRb.position.y ) < distances.y && 
             Math.Abs( rb.position.x - targetRb.position.x ) < distances.x ){  /*&& 
             Math.Abs( rb.position.z - targetRb.z ) < distances.z ){  */
                Debug.Log("TERMINOU!!!!");
                return true;
             } 
             
        return false;
    }
}

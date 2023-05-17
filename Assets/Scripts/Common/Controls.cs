/* 
    Class to manage user inputs and corresponding implications.

    Methods in this script give  priority to  gamepad inputs, instead  of touch
    taking in accoun that, although mobile devices are the intended targets for 
    this software product, the  device will be used in a way  similar to a "VR"
    where the touh capabilities are not accessible, while on the head support. 
    
    Additionally, keyboard inputs are also handled to support development, test
    and debugging processes.

    Input priority:
        a) gamepad
        b) keyboard/mouse
        c) touch (important only while the device is not in the head support)

*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using static GamepadUtils;

public static class Controls
{

    public static Vector3 HandleAxisInput(){
        Vector3 movementVector = new Vector3( 0, 0, 0 );
        if (Input.GetKey (KeyCode.LeftArrow)    || GamepadUtils.isButtonPressed("Left") )   { movementVector[0] = -1 ; }
        if (Input.GetKey (KeyCode.UpArrow)      || GamepadUtils.isButtonPressed("Up") )     { movementVector[1] =  1 ; }
        if (Input.GetKey (KeyCode.RightArrow)   || GamepadUtils.isButtonPressed("Right") )  { movementVector[0] =  1 ; }
        if (Input.GetKey (KeyCode.DownArrow)    || GamepadUtils.isButtonPressed("Down") )   { movementVector[1] = -1 ; }

        
        if (Input.GetKeyUp (KeyCode.LeftArrow)  && !GamepadUtils.isButtonPressed("Left") )  { movementVector[0] =  0 ; }
        if (Input.GetKeyUp (KeyCode.UpArrow)    && !GamepadUtils.isButtonPressed("Up") )    { movementVector[1] =  0 ; }
        if (Input.GetKeyUp (KeyCode.RightArrow) && !GamepadUtils.isButtonPressed("Right") ) { movementVector[0] =  0 ; }
        if (Input.GetKeyUp (KeyCode.DownArrow)  && !GamepadUtils.isButtonPressed("Down") )  { movementVector[1] =  0 ; }

        return movementVector;
    }
}
using UnityEngine;

using static GamepadUtils;

public class EyesInputHandler : MonoBehaviour
{
   void Update()
    { 
        // cheat code Ctrl + Shift + Space to change amblyopic eye
        if (
            (Input.GetKey(KeyCode.LeftControl  ) || 
             Input.GetKey(KeyCode.RightControl )    ) &&
            (Input.GetKey(KeyCode.LeftShift    ) || 
             Input.GetKey(KeyCode.RightShift   )    ) &&
             Input.GetKeyDown(KeyCode.Space         )    
             ||
             ((ButtonValue("Select") != 0 && ButtonValue("Action") != 0))
             )
        {
                Globals.amblyopicEye = ( Globals.amblyopicEye + 1 ) % 2;        // cycles amblyopic eye definition
                Globals.SetCullingMaskUpdateFlag(); 
        }
    }
}

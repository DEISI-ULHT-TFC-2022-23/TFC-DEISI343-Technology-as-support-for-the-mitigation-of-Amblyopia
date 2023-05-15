using UnityEngine;

public class EyesInputHandler : MonoBehaviour
{
   void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) { Debug.Log("aaaaaa");}
        // cheat code Ctrl + Shift + Space to change amblyopic eye
        if ((Input.GetKey(KeyCode.LeftControl  ) || 
             Input.GetKey(KeyCode.RightControl )    ) &&
            (Input.GetKey(KeyCode.LeftShift    ) || 
             Input.GetKey(KeyCode.RightShift   )    ) &&
             Input.GetKeyDown(KeyCode.Space         )         ) {
                Globals.amblyopicEye = ( Globals.amblyopicEye + 1 ) % 2;        // cycles amblyopic eye definition
                Globals.SetCullingMaskUpdateFlag(); 
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadTest : MonoBehaviour
{
    TMPro.TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();
        GamepadUtils.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (GamepadUtils.gamepad == null){
            Debug.Log(GamepadUtils.gamepad == null ? "null\n" : GamepadUtils.gamepads);
            GamepadUtils.UpdateInfo();
            return;
        }
        txt.text =  "Select      : " + GamepadUtils.ButtonValue("MainSelect") + "\n" + 
                    "Alternative : " + GamepadUtils.ButtonValue("AltSelect") + "\n" +
                    "up : " + GamepadUtils.ButtonValue("Up") + "\n" + 
                    "Look : " + GamepadUtils.ButtonValue("Look");

        Debug.Log(txt.text + "\n" + "[" + GamepadUtils.gamepad.displayName + "]");
        Debug.Log(GamepadUtils.gamepad == null ? "null\n" : GamepadUtils.gamepads);
    }
}

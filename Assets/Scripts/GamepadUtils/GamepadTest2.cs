using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadTest2 : MonoBehaviour
{
    TMPro.TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (GamepadUtils.gamepadAvailable){
            txt.text =  "displayName: "      + GamepadUtils.gamepad.displayName        + "\n" + 
                        "shortDisplayName: " + GamepadUtils.gamepad.shortDisplayName   + "\n" + 
                        "name: "             + GamepadUtils.gamepad.name;
        } else {
            txt.text =  "No gamepad available...";
        }
    }
}

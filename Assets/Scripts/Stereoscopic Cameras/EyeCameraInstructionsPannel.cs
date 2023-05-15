using UnityEngine;

public class EyeCameraInstructionsPannel : MonoBehaviour
{
    [SerializeField] int cameraID;
    [SerializeField] GameObject gmObjectLeft;                                   // object on the left camera
    [SerializeField] GameObject gmObjectRight;                                  // object on the right camera

    // Update is called once per frame
    void Update()
    {
        if (Globals.amblyopicEye == 0){                                         // hide instruction components amblyopic eye
            gmObjectLeft.SetActive(false);
            gmObjectRight.SetActive(true);

        } else {
            gmObjectLeft.SetActive(true);
            gmObjectRight.SetActive(false);
        }
    }
}

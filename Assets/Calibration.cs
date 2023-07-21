using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;


using static GamepadUtils   ;
using static Globals        ;

public class Calibration : MonoBehaviour
{
    // placeholders definition

    [SerializeField] private Transform  _EyeLeft                        ;       // left eye position
    [SerializeField] private Transform  _EyeRight                       ;       // rigth eye position
    [SerializeField] private Transform  _LookAtObject                   ;       // camera's convergence point
    [SerializeField] private Transform  _LookAtPosition                 ;       // camera's convergence point

    [SerializeField] private Camera     _CameraLeft                     ;       // left camera's current position
    [SerializeField] private Camera     _CameraRight                    ;       // right camera's current position

    [SerializeField] private float      _CalibrationSpeed       = 0f    ;
    
    [SerializeField] private Vector3    _prefabRighCamPosition          ;
    [SerializeField] private Vector3    _prefabLeftCamPosition          ;

    [SerializeField] public  bool       CalibrationMode         = false ;

    [SerializeField] private CalibrationType _calibrationType   = CalibrationType.Offset;

    [SerializeField] private Vector3    _focalOffset                    ;
    [SerializeField] private Vector3    _focalOffsetSimetryFactor       ;


    enum CalibrationType { ConvergencePoint, CameraDistance, Offset}
    
    private void Awake()
    {
        // Set placeholder's position to current camera's gameObjects position in environment
        _EyeLeft = _CameraLeft.transform;
        _EyeRight = _CameraRight.transform;

        // init Gamepad
        GamepadUtils.Init();
    }

    // Update is called once per frame
    void Update()
    {
        SetCalibrationMode();
        if (CalibrationMode) { CalibrateCameras();}
    }

    void SetCalibrationMode()
    {
        // Calibration mode
        if (CalibrationMode)
        {
            if ((ButtonValue("Select") != 0 && ButtonValue("MainSelect") != 0) || (
                (Input.GetKey(KeyCode.LeftControl)) && 
                (Input.GetKey(KeyCode.LeftShift)) && 
                (Input.GetKeyDown(KeyCode.Q)) ) )
            {
                CalibrationMode = false;
            }
        } else {
            if ((ButtonValue("Select") != 0 && ButtonValue("MainSelect") != 0) || (
                (Input.GetKey(KeyCode.LeftControl)) && 
                (Input.GetKey(KeyCode.LeftShift)) && 
                (Input.GetKeyDown(KeyCode.Q)) ) )
            {
                CalibrationMode = true;
            }
        }


        // Calibration Type
        if ((ButtonValue("Select") != 0 && ButtonValue("AltSelect") != 0) || (
                (Input.GetKey(KeyCode.LeftControl)) && 
                (Input.GetKey(KeyCode.LeftShift)) && 
                (Input.GetKeyDown(KeyCode.W)) ))
        {
            _calibrationType =  (CalibrationType) ( ( (int) _calibrationType + 1 ) % 3 );
        }
    }

    // apply an offset to a transform
    // to be used on eye cameras to offset focal point (away from nasal position -> skweeing image towards nasal position)
    // => "symetry" will apply only for Symetry vector components with -1, while 1 values will keep position
    //          Example: Symetry = (-1, 1, 1 ) will only "invert" x offset (make sens between left and right eyes)
    // => right side eye will not apply Symetry vector (will keep current offset by applying a symetry vector of (1, 1, 1)
    Vector3 CameraOffset(Vector3 TargetObjectPosition, Vector3 Offset, Vector3 Simetry, Side side){
        Vector3 _simetry = side == Side.Right ? Vector3.one : Simetry;                      // calc "symetry", based on side
        Vector3 _offsetted = TargetObjectPosition;
        
        //Debug.Log("offset: " + Vector3.Scale(Offset, _simetry));
        //Debug.Log("Position -> before: " + _offsetted);
        _offsetted += Vector3.Scale(Offset, _simetry);
        //Debug.Log("Position -> after: " + _offsetted);

        
        return _offsetted;
    }


    void CalibrateCameras()
    {
        // apply position transformation, based on pressed buttons
        float       _eyeSeparation = 0.0f;
        float       _convergenceOffset = 0.0f;
        Transform   _lookAt;

        // Select which point to follow (convergence)
        if (_calibrationType == CalibrationType.ConvergencePoint)   _lookAt = _LookAtPosition   ;
        else                                                        _lookAt = _LookAtObject     ;

        // calculate eye separation
        _eyeSeparation += ButtonValue("Right") * _CalibrationSpeed / 2;
        _eyeSeparation += (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) * _CalibrationSpeed / 2;
         
        _eyeSeparation -= ButtonValue("Left") * _CalibrationSpeed / 2;
        _eyeSeparation -= (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) * _CalibrationSpeed / 2;

        // calculate convergence distance
        _convergenceOffset += ButtonValue("Up") * _CalibrationSpeed;
        _convergenceOffset += (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) * _CalibrationSpeed;
        _convergenceOffset -= ButtonValue("Down") * _CalibrationSpeed;
        _convergenceOffset -= (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) * _CalibrationSpeed;


        // apply offset in gameobjects 
        _EyeLeft.transform.Translate ( new Vector3(-_eyeSeparation, 0, 0) );    // Cameras: Left Eye
        _EyeRight.transform.Translate( new Vector3(_eyeSeparation , 0, 0) );    // Cameras: Right Eye
            
        // Convergence
        if (_calibrationType == CalibrationType.ConvergencePoint)
        {
            _lookAt.transform.Translate( new Vector3(0, 0, _convergenceOffset)); // convergence distance
        } 
        else if (_calibrationType == CalibrationType.CameraDistance)
        {
            _EyeLeft.transform.Translate ( new Vector3(0, 0, _convergenceOffset)); // Cameras: Left Eye
            _EyeRight.transform.Translate( new Vector3(0, 0, _convergenceOffset)); // Cameras: Right Eye
        }
        else if (_calibrationType == CalibrationType.Offset){
            var _offsettedRight = CameraOffset(_lookAt.position, _focalOffset, _focalOffsetSimetryFactor, Side.Right);
            var _offsettedLeft  = CameraOffset(_lookAt.position, _focalOffset, _focalOffsetSimetryFactor, Side.Left);

            // place cameras at the same position of the object
            _EyeRight.position = new Vector3(_offsettedRight.x, _offsettedRight.y, _EyeRight.position.z );
            _EyeLeft.position  = new Vector3(_offsettedLeft.x , _offsettedLeft.y , _EyeLeft.position.z  );

            // direct gaze towards offsetted object
            _EyeRight.LookAt(_offsettedRight);
            _EyeLeft.LookAt (_offsettedLeft );
            return;
        }
        
        _EyeLeft.LookAt(_lookAt);
        _EyeRight.LookAt(_lookAt);
    }
}

/* 
    Class Globals
    Describes global data (structs, types, variables, consts) to be used accorss the platform

    Inludes general purpose methods to be invoked anywhere in the code (utils) 
    
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Globals // : MonoBehaviour  // static -> doesn't derive from MonoBehaviour
{
    // enums 
    public enum Side            { Left          , Right                                 }
    public enum EyeStatus       { Dominant      , Weaker                                }
    public enum SceneType       { Invalid       , chalenge  , Splash    , Other , Exit  }
    public enum ErrorType       { Warning       , Actionable, Critical  , Abend         }
    public enum MovementType    { Default       , IntegerSteps                          }
    public enum ProjectionType  { Perspective   , Orthogonal                            }

    public const int ERROR_CODE_NO_SCENE_TYPE           = 101 ;
    public const int ERROR_CODE_PIECES_IDENTIFICATION   = 201 ;

    public const string TAGS_SCENE_ENVIRONMENT_VARS     = "SceneEnvironmentVars";

    // global data definition
    public static int currentScene                      = 0 ;                   // current scene's build index
    public static int amblyopicEye {get; internal set;} = 0 ;                   // 0 = Left | 1 = right

    // tracking settings
    public static SceneType CurrentSceneType            = SceneType.Invalid ;   // flag to mark scene type (e.g., to be used in time tracking)

    // Culling Masks
    public const int CullingMaskDominantEyeLayerBit     = 10 ;                  // bitwize value of dominant eye's layer
    public const int CullingMaskWeakerEyeLayerBit       = 11 ;                  // bitwize value of weaker   eye's layer
    public const int CullingMaskDefaultLayerBit         =  0 ;                  // bitwize value of default  eye's layer

    public const int CullingMaskDominantEye             = (1 << CullingMaskDefaultLayerBit) | (1 << CullingMaskDominantEyeLayerBit)  ; // Dominant Eye's culling mask
    public const int CullingMaskWeakerEye               = (1 << CullingMaskDefaultLayerBit) | (1 << CullingMaskWeakerEyeLayerBit)    ; // Weaker Eye's culling mask

    private static bool[] updateCullingMaskRequired     = {false, false};

    // methods --------

    // method to decide next level and load corresponding scene
    // TODO: currently, the platform moves sequencially from a level to the next one (based on build sequence)
    //       the method must evolve to correspond to the patent's evolution
    public static void NextLevel(){
        int nextLevel = ( SceneManager.GetActiveScene().buildIndex + 1 ) 
                        % SceneManager.sceneCountInBuildSettings;               // get next scene sequencially in a circular approach (from last back to firt level) 
        currentScene  = nextLevel;
        SceneManager.LoadScene(nextLevel);
    }
    public static void ReloadLevel(){
        int thisScene = SceneManager.GetActiveScene().buildIndex;               // get current scene build index
        currentScene  = thisScene;                                              // overkill...
        SceneManager.LoadScene(thisScene);                                      // reload current scene
    }

    public static void SetAmblyopicEye(int eye){
        amblyopicEye = eye;
    }

    public static int GetCullingMask(Side side, Side eye)
    {
        /* XOR  0 0 = 0  weak
                0 1 = 1  dominant
                1 0 = 1  dominant
                1 1 = 0  weak */

        if ( (int)side == 0 ^ (int)eye == 0){                                   
            return CullingMaskDominantEye;                                      // 0 0 and 1 1 return dominant eye
        } else {
            return CullingMaskWeakerEye;                                        // 0 1 and 1 0 return weak eye
        }
    }

    public static int GetCullingLayerMask( Side side, Side eye){
        /* XOR  0 0 = 0  weak
                0 1 = 1  dominant
                1 0 = 1  dominant
                1 1 = 0  weak */

        if ( (int)side == 0 ^ (int)eye == 0){                                   
            return CullingMaskDominantEyeLayerBit   ;                           // 0 0 and 1 1 return weak eye
        } else {
            return CullingMaskWeakerEyeLayerBit     ;                           // 0 1 and 1 0 return dominant eye
        }
    }

    // Cameras culling mask related methods
    public static bool GetUCullingMaskpdateFlag  (int i){ return updateCullingMaskRequired[i]  ; }
    public static void ClearCullingMaskUpdateFlag(int i){ updateCullingMaskRequired[i] = false ; }
    public static void SetCullingMaskUpdateFlag       (){ 
        updateCullingMaskRequired[0] = true ; 
        updateCullingMaskRequired[1] = true ;
    }


    // scene type definition, for time tracking
    public static void SetSceneType (SceneType sceneType){
        switch (sceneType){
            case SceneType.Invalid  :
                Error(ERROR_CODE_NO_SCENE_TYPE, "SceneType is invalid. Please identify a scene type.", ErrorType.Critical);
                break;
            default                 :
                CurrentSceneType = sceneType ;
                break;
        }
    }


    /* TODO define a error messaging layout */
    /*
        Function to handleand control errors normalization
        errorCode : a number identifier
        errorMsg  : exactly... the message you'r trying to pass on (already formtted)
        errorType : of ErrorType (identifies the severity of the error)
    */
    public static void Error (int errorCode, string errorMsg, ErrorType errorType){
        switch (errorType)
        {
            case ErrorType.Warning      : 
                Debug.Log($"Warning: {errorCode}: {errorMsg}");
                break;
            
            case ErrorType.Actionable   :
                Debug.Log($"Actionable: {errorCode}: {errorMsg}");
                break;
            case ErrorType.Critical     :
                Debug.Log($"Critical: {errorCode}: {errorMsg}");
                break;
            case ErrorType.Abend        :
                Debug.Log($"ABNORMAL END: {errorCode}: {errorMsg}");
                break;
            default:
                break;
        }
    }


    
    /* TODO review this approach and cleanup 

    // scene related data
    public struct sceneSpecs{                                                   // used to ID a scene instance and establish general data accordingly
        int gameLevel                                       ;                   // the chalenge (bars, moving obj, tetris, ...)
        int gameSubLevel                                    ;                   // the variation (bar variations, frequency, colors, ...)
        int difficulty                                      ;                   // additional level of precision
    }

    public struct ActionInstructions {
        public ActionInstructions(string text, Image img){
            ActionInstructionText = text  ;                                           // text instructions to present on instruction pannel
            ActionImageSprite     = img   ;                                           // sprite to illustrate instruction text
        }

        public string ActionInstructionText             {get;set;}
        public Image  ActionImageSprite                 {get;set;}

        public override string ToString() => $"(InstructionText)";
    }

    public static ActionInstructions  getLevelInstructions(sceneSpecs scenespecs){
        ActionInstructions data = new ActionInstructions();
        const string HORIZINTAL_INSTRUCTIONS = "Horizontal";

        //TODO a more sophisticated approach will be required for future levels
        data.ActionInstructionText = "Just a test";
        return data;
    }
    */
}

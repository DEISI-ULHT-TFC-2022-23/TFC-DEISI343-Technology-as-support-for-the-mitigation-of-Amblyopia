/*
    Script part of the base template for challenge levels.
    Main responsibilities are:
        a)  anchor to esure common behaviours of levels, independently of level
            challenge type (bars, join objects, tetris etc)
        b)  hold common level properties that induce different behaviours based
            on their values
        c)  kickstart a level (using LevelManagement class)
        d)  record challenge statistics to send externally 
        d)  manage level moments:
            1) "click to start" message
            2) pausing and presenting pause menu
            3) congratulations, you've completed message
            4) help
            5) audio messages
            6) handover to next level routing engine
            ...
        ...  
        => most of these responsabilities are not yet performed... ;)
*/

using System.Collections            ;
using System.Collections.Generic    ;
using UnityEngine                   ;

using lvl = LevelManager            ;

public class SceneEnvironmentVariables : MonoBehaviour
{
    [SerializeField] public Globals.SceneType sceneType     = Globals.SceneType.Invalid ; 
    [SerializeField] public Material DominantEyeMaterial                                ;
    [SerializeField] public Material WeakEyeMaterial                                    ;

    
    // Start is called before the first frame update
    void Start()
    {
        Globals.SetSceneType(sceneType);                                        // adjust scene tracking global setting
        lvl.LevelInit();                                                        // Initialize level

        // potencially present a "click to start" message

        lvl.LevelStart();                                                       // start level execution
        
        // inform statistics of level start
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/*
    Class to manage level execution status and related logic
    Includes level configuation, such as:
        a)  difficulty
        b)  speed
        c)  frecuency
        d)  colors
        e)  movement and rotation levels of freedom
        e)  ...
*/

using UnityEngine;
using static Globals;

public static class LevelManager 
{
    private static bool     levelCompleted      = false                     ;   // status regarding level completion
    private static bool     levelStarted        = false                     ;   // true once the level has started
    private static bool     levelPaused         = false                     ;   // true when game is paused

    public static Vector3  axisMovFreedom   {get; internal set;}    = new Vector3( 1f, 1f, 0f ) ;   // levels of axis movement freedom
    public static Vector3  axisRotFreedom   {get; internal set;}    = new Vector3( 0f, 0f, 0f ) ;   // levels of axis rotation freedom 

    public static int      difficulty       {get; internal set;}    = 1     ;   // level difficulty
    public static float    speed            {get; internal set;}    = 1f    ;   // level speed (0: stopped < slower < 1: normal < faster )
    public static float    rotationSpeed    {get; internal set;}    = 1f    ;   // level rotation speed
    public static float    precision        {get; internal set;}    = 0.05f ;

    public static void LevelSetup(){                                            // should act as a "constructor" to set level conditions
        // set different level specs (difficulty, speed, etc)
        GamepadUtils.Init();
    }

    public static void LevelInit(){
        levelStarted    = false ;
        levelCompleted  = false ;
        levelPaused     = false ;
        GamepadUtils.Init();
    }

    public static void LevelStart(){
        levelStarted    = true  ;
        levelCompleted  = false ;
        levelPaused     = false ;
        Time.timeScale  = 1     ;                                               // time progression ( 1: "normal" )
    }

    public static void Complete()       { levelCompleted    = true  ; }
    public static void Pause()          { levelPaused       = true  ; }
    public static void Unpause()        { levelPaused       = false ; }

    public static bool IsCompleted()    { return levelCompleted     ; }

    public static bool IsActive(){
        if ( ! levelStarted     )       { return false; }
        if (   levelPaused      )       { return false; }
        if (   levelCompleted   )       { return false; }
        return true ;
    }

    public static bool IsPaused(){
        if ( ! levelStarted     )       { return false; }
        if (   levelCompleted   )       { return false; }
        if ( ! levelPaused      )       { return false; }
        return true ;
    }

    public static float scaledSpeed(){
        // some of these factore may become axis specific...
        return speed * Time.deltaTime           * Time.timeScale    ;
    }

    public static float axisScaledSpeed( Axis paxis ){
        return axisMovFreedom[(int)paxis]       * scaledSpeed()     ;
    }

    public static float scaledRotation() {
        // some of these factore may become axis specific...
        return rotationSpeed * Time.deltaTime   * Time.timeScale    ;
    }

    public static float axisScaledRotationSpeed( Axis paxis ){
        Debug.Log($"pAxis: {paxis}, axisRotFreedom: {axisRotFreedom[(int)paxis]}, scaledRotation: {scaledRotation()}");
        return axisRotFreedom[(int)paxis]       * scaledRotation()  ;
    }
}
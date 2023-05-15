using UnityEngine;
using System.IO;
using System;

public class GameData : MonoBehaviour
{
    //private static  GameData    instance    ;                                   // class level reference (for singleton implementation)

    internal        uint        _level      {get; private set;}                 // current player's level
    internal        uint        _subLevel   {get; private set;}                 // within level definition
    internal        uint        _difficulty {get; private set;}                 // difficulty (not dependent of level)
    
    private void Awake() {
        // load data

        DataMangmnt dataMangmnt = new DataMangmnt();
        /*GameData gameData = dataMangmnt.LoadData();
            _level              = gameData._level           ;
            _subLevel           = gameData._subLevel        ;
            _difficulty         = gameData._difficulty      ;
            */

        /*
        _level = 0;
        _subLevel = 0;
        _difficulty = 0;

        dataMangmnt.SaveData(this);
        */
    }

    public override string ToString()
    {
        return $"Level: {_level}, subLevel: {_subLevel} and difficulty: {_difficulty}";
    }

}

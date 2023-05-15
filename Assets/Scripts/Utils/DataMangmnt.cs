using UnityEngine;
using System.IO;

public class DataMangmnt 
{
    private         string      _path           = ""  ;                         // path for development support           
    private         string      _persistentPath = ""  ;                         // the proper path for production (TODO)

    private void SetPath(){
        _path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        _persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.jason";
    }

    public GameData LoadData(){
        SetPath();

        using StreamReader reader = new StreamReader(_path);
        string json = reader.ReadToEnd();

        GameData gameData = JsonUtility.FromJson<GameData>(json);
        Debug.Log("Loaded: " + gameData.ToString());

        return gameData;
    }

    public void SaveData(GameData gameData){
        SetPath();
        string SavePath = _path;
        Debug.Log("saving in: " + SavePath);

        string json = JsonUtility.ToJson(gameData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(SavePath);
        writer.Write(json);
    }
}

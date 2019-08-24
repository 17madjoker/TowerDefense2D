using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadManager
{
    public static string pathToFile =  Application.dataPath + "/Resources/levelsData.td";
    
    public static void SaveLevel(string levelName, LevelData newLevelData)
    {
        if (!File.Exists(pathToFile))
            CreateEmptyLevelsData();
        
        Dictionary<string, LevelData> loadLevelsData = LoadLevelsData();

        LevelData currentLevel;
        loadLevelsData.TryGetValue(levelName, out currentLevel);
            
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream fStream = new FileStream(pathToFile, FileMode.Create);

        if (currentLevel == null)
            loadLevelsData.Add(levelName, newLevelData);
        else
            loadLevelsData[levelName] = newLevelData;
                
        bFormatter.Serialize(fStream, loadLevelsData);
        fStream.Close();
    }

    public static Dictionary<string, LevelData> LoadLevelsData()
    {
        if (!File.Exists(pathToFile))
            CreateEmptyLevelsData();
        
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream fStream = new FileStream(pathToFile, FileMode.Open);

        Dictionary<string, LevelData> levelsData = (Dictionary<string, LevelData>) bFormatter.Deserialize(fStream);
        fStream.Close();

        return levelsData;
    }

    private static void CreateEmptyLevelsData()
    {
        List<int> levels = GetLevelsFromFolder();
        Dictionary<string, LevelData> levelsData = new Dictionary<string, LevelData>();

        for (int i = 0; i < levels.Count; i++)
            levelsData.Add("Level_" + levels[i], new LevelData(levels[i]));
        
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream fStream = new FileStream(pathToFile, FileMode.Create);
        
        bFormatter.Serialize(fStream, levelsData);
        fStream.Close();
    }
    
    private static List<int> GetLevelsFromFolder()
    {
        string scenesFolderPath = Application.dataPath + "/Scenes/";
        
        var scenes = new DirectoryInfo(scenesFolderPath).GetFiles("*.unity").Where(s => s.Name.StartsWith("Level_"));
        List<int> levels = new List<int>();

        foreach (var scene in scenes)
        {
            string tmp = scene.Name;
            
            tmp = tmp.Split('_').Last();
            tmp = tmp.Split('.').First();
            
            levels.Add(Convert.ToInt32(tmp));
        }
        
        levels.Sort();
        
        return levels;
    }
}

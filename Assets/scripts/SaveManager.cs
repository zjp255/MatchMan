using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
   
    static  string path = "E:/uinty item/Matchman/Assets/userData";
    public static void save(object Data)
    {

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Create(path +"/userdata.DATA");

        var json = JsonUtility.ToJson(Data);

        formatter.Serialize(file, json);

        file.Close();
    }

    public static SaveData LodeGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        SaveData saveData = new SaveData();

        if (File.Exists(path + "/userdata.DATA"))
        {
            FileStream file = File.Open (path + "/userdata.DATA",FileMode.Open);

           

            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file),saveData);

            file.Close();

            return saveData;
        }

        return saveData;
    }

    public static void DeleteData()
    {
        SaveData saveData = new SaveData();
        save(saveData);

    }
    
    
}

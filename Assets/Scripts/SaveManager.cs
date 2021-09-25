using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager 
{
    public static void SaveGameState(SaveData saveData) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;
        file = File.Create(Application.persistentDataPath + "/SaveData.save");
        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }

    public static SaveData LoadGameState() {
        if(File.Exists(Application.persistentDataPath + "/SaveData.save")) {
            try {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SaveData.save", FileMode.Open);
                SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);
                file.Close();
                return saveData;
            } catch {
                
            }
        } 
            return null;
    }
}

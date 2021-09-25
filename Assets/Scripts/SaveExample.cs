using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveExample : MonoBehaviour
{


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F5))
            SaveGame();
        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();
    }

    private void SaveGame() {
        SaveData saveData = new SaveData();
        saveData.positions = new SaveData.position[1]; //dont do like that, im just showing example here
        saveData.positions[0] = new SaveData.position();
        saveData.positions[0].x = transform.position.x;
        saveData.positions[0].y = transform.position.y;
        saveData.positions[0].z = transform.position.z;
        SaveManager.SaveGameState(saveData);
        Debug.Log("Game Saved!"); 
    }

    private void LoadGame() {
        SaveData saveData = SaveManager.LoadGameState();
        if(saveData != null) {
            transform.position = new Vector3(saveData.positions[0].x, saveData.positions[0].y, saveData.positions[0].z);
            Debug.Log("Game Loaded!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    public Button saveButton;
    public Button backButton;
    public Slider volumeSlider;

    public GameObject mainMenuScreen;


    public Button[] tabButtons;
    public GameObject[] tabGameObjects;
    public Button[] graphicSettingButtons;


    private int currentTab;
    private int currentGraphicSetting;

    // Start is called before the first frame update
    void Start() {
        saveButton.onClick.AddListener(delegate { SaveSettings(); });
        backButton.onClick.AddListener(delegate { GoBack(); });
        volumeSlider.onValueChanged.AddListener(delegate { CheckSettingsChanged(); });

        for (int i = 0; i < tabButtons.Length; i++) {
            int index = i;
            tabButtons[i].onClick.AddListener(delegate { SwitchTab(index); });
        }
        for (int i = 0; i < graphicSettingButtons.Length; i++) {
            int index = i;
            graphicSettingButtons[i].onClick.AddListener(delegate { SwitchGraphicSetting(index); });
        }
    }

    private void OnEnable() {
        SwitchGraphicSetting(QualitySettings.GetQualityLevel());
        volumeSlider.value = AudioListener.volume;
        //loading this values from file/playerprefs should be on starting your game, not in entering settings
        saveButton.interactable = false;
    }

    private void SwitchTab(int newTab) {
        tabButtons[currentTab].interactable = true;
        tabGameObjects[currentTab].SetActive(false);
        tabButtons[newTab].interactable = false;
        tabGameObjects[newTab].SetActive(true);
        currentTab = newTab;
    }

    private void SwitchGraphicSetting(int newSetting) {
        graphicSettingButtons[currentGraphicSetting].interactable = true;
        graphicSettingButtons[newSetting].interactable = false;
        currentGraphicSetting = newSetting;
        CheckSettingsChanged();
    }

    private void SaveSettings() {
        QualitySettings.SetQualityLevel(currentGraphicSetting, true);
        AudioListener.volume = volumeSlider.value;
        //Generally you should store this settings in file or player prefs so they wont be lost on loading new scene or restarting your game
    }

    private void GoBack() {
        gameObject.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    private void CheckSettingsChanged() {
        saveButton.interactable = currentGraphicSetting != QualitySettings.GetQualityLevel() || volumeSlider.value != AudioListener.volume;
    }
}

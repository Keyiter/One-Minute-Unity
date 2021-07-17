using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_QuestLog : MonoBehaviour
{
    public GameObject questInListPrefab;
    public RectTransform listTransform;

    public RectTransform questDescription;
    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text questGoldRewardText;
    public TMP_Text questExpRewardText;
    public TMP_Text questObjectiveText;
    public RectTransform rewardsContent;

    private GameObject questLogObject;
    private Button[] questButtons;

    private Quest currentQuest;
    private int previousButtonIndex;

    private void Awake() {
        questLogObject = transform.GetChild(0).gameObject;
        questButtons = new Button[0];
        QuestLog.Initialize();
        QuestLog.onQuestChange += UpdateQuests;
        UpdateQuests(new List<Quest>(), new List<Quest>());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
            questLogObject.SetActive(!questLogObject.activeSelf);
        if (questLogObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            questLogObject.SetActive(false);
    }


    public void UpdateQuests(List<Quest> active, List<Quest> completed) {
        HandleSizeChange(active.Count + completed.Count);
        UpdateQuestNames(active, completed);
        UpdateSelectedQuest();
        ShowQuestDetails(currentQuest);
    }

    private void HandleSizeChange(int newCount) {
        listTransform.sizeDelta = new Vector2(0, newCount * 80);
        int oldCount = questButtons.Length;
        System.Array.Resize(ref questButtons, newCount);
        for (int i = oldCount; i < questButtons.Length; i++) {
            questButtons[i] = InitializeButton(i);
        }
    }

    private void UpdateQuestNames(List<Quest> active, List<Quest> completed) {
        for (int i = 0; i < active.Count; i++) {
            UpdateQuestText(questButtons[i], active[i]);
        }
        for (int i = 0; i < completed.Count; i++) {
            UpdateQuestText(questButtons[i + active.Count], completed[i],true);
        }
    }

    private void UpdateSelectedQuest() {
        if (currentQuest == null)
            return;
        HighlightQuestButton(questButtons[previousButtonIndex], false);
        for (int i = 0; i < questButtons.Length; i++) {
            if(questButtons[i].GetComponentInChildren<TMP_Text>().text == currentQuest.questName) {
                HighlightQuestButton(questButtons[i], true);
                previousButtonIndex = i;
                return;
            }
        }
    }

    private void ShowQuestDetails(Quest quest) {
        questDescription.gameObject.SetActive(quest != null);
        if (quest == null)
            return;
        questNameText.text = quest.questName;
        questDescriptionText.text = quest.questDescription;
        questGoldRewardText.text = quest.goldReward + "gp";
        questExpRewardText.text = quest.expReward + "Exp";
        questObjectiveText.text = quest.objective.ToString();
        questDescriptionText.rectTransform.sizeDelta = new Vector2(0, questDescriptionText.preferredHeight);
        rewardsContent.anchoredPosition = new Vector2(0, -50 - questDescriptionText.rectTransform.sizeDelta.y);
        questDescription.sizeDelta = new Vector2(0, questDescriptionText.rectTransform.sizeDelta.y + 300);
    }

    private Button InitializeButton(int index) {
        Button button = Instantiate(questInListPrefab, listTransform).GetComponent<Button>();
        button.image.rectTransform.sizeDelta = new Vector2(0, 80);
        button.image.rectTransform.anchoredPosition = new Vector2(0, -80 * index);
        button.onClick.AddListener(delegate { QuestPress(button); });
        return button;
    }

    private void UpdateQuestText(Button questButton, Quest quest, bool isCompleted = false) {
        TMP_Text text = questButton.GetComponentInChildren<TMP_Text>();
        text.text = quest.questName;
        text.color = isCompleted ? Color.gray : GetColorFromCategory(quest.questCategory);
    }

    private Color GetColorFromCategory(short category) {
        return Color.black; // you can make here different collors depending on category
    }

    private void HighlightQuestButton(Button questButton, bool active) {
        questButton.image.color = active ? Color.green : new Color(0, 0, 0, 0);
    }

    private void QuestPress(Button questButton) {
        HighlightQuestButton(questButtons[previousButtonIndex], false);
        HighlightQuestButton(questButton, true);
        previousButtonIndex = System.Array.IndexOf(questButtons, questButton);
        currentQuest = QuestLog.getQuestNo(previousButtonIndex);
        ShowQuestDetails(currentQuest);
    }
}

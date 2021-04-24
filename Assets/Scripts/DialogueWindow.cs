using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    public GameObject dialogueWindow;
    public TMP_Text dialogueText;
    public GameObject nextButton;
    public RectTransform content;
    public GameObject responsePrefab;

    private Dialogue.Part currentPart;
    private Dialogue currentDialogue;
    private TMP_Text[] responseTexts;

    public void ShowDialogue(Dialogue dialogue) {
        dialogueWindow.SetActive(true);
        currentDialogue = dialogue;
        currentPart = dialogue.getPart("START");
        ShowPart();
    }

    public void ShowPart() {
        dialogueText.text = currentPart.text;
        SpawnResponse(currentPart.responses);
        ResizeElements();
    }

    private void SpawnResponse(Dialogue.Part.Response[] responses) {
        nextButton.SetActive(responses == null);
        if (responses == null)
            return;
        responseTexts = new TMP_Text[responses.Length];
        for (int i = 0; i < responseTexts.Length; i++) {
            responseTexts[i] = Instantiate(responsePrefab, content).GetComponentInChildren<TMP_Text>();
            responseTexts[i].text = i+1+ ". " + responses[i].text;
        }
    }

    private void ResizeElements() {
        Canvas.ForceUpdateCanvases();
        float textHeight = dialogueText.textBounds.size.y + 20f;
        dialogueText.rectTransform.sizeDelta = new Vector2(0, textHeight);
        for (int i = 0; i < responseTexts.Length; i++) {
            RectTransform rectTransform = responseTexts[i].transform.parent.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(0, responseTexts[i].bounds.size.y);
            rectTransform.anchoredPosition = new Vector2(0, -textHeight);
            textHeight += responseTexts[i].bounds.size.y + 10f;

            rectTransform.name = i.ToString();
            rectTransform.GetComponent<Button>().onClick.AddListener(delegate { NextPart(int.Parse(rectTransform.name)); });
        }

        content.sizeDelta = new Vector2(0, textHeight);
        content.anchoredPosition = new Vector2(0, 0);
    }

    public void NextPart(int responseNumber) {
        if (responseNumber == -1) {
            if(currentPart.nextId == null) {
                dialogueWindow.SetActive(false);
                DeletePreviousResponse();
                return;
            }
            currentPart = currentDialogue.getPart(currentPart.nextId);
        } else
            currentPart = currentDialogue.getPart(currentPart.responses[responseNumber].ID);
        DeletePreviousResponse();
        ShowPart();
    }

    private void DeletePreviousResponse() {
        responseTexts = new TMP_Text[0];
        for (int i = 1; i < content.childCount; i++) {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}

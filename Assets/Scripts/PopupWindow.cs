using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    public TMP_Text popupText;

    private GameObject window;
    private Animator popupAnimator;

    private Queue<string> popupQueue; //make it different type for more detailed popups, you can add different types, titles, descriptions etc
    private Coroutine queueChecker;

    private void Start() {
        window = transform.GetChild(0).gameObject;
        popupAnimator = window.GetComponent<Animator>();
        window.SetActive(false);
        popupQueue = new Queue<string>();
    }

    public void AddToQueue(string text) {//parameter the same type as queue
        popupQueue.Enqueue(text);
        if (queueChecker == null)
            queueChecker = StartCoroutine(CheckQueue());
    }

    private void ShowPopup(string text) { //parameter the same type as queue
        window.SetActive(true);
        popupText.text = text;
        popupAnimator.Play("PopupAnimation");
    }

    private IEnumerator CheckQueue() {
        do {
            ShowPopup(popupQueue.Dequeue());
            do {
                yield return null;
            } while (!popupAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));

        } while (popupQueue.Count > 0);
        window.SetActive(false);
        queueChecker = null;
    }

}

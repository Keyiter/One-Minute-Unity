using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCaller : MonoBehaviour
{
    public TextAsset dialogueText;
    private Dialogue dialogue;


    // Start is called before the first frame update
    void Start()
    {
        dialogue = JsonUtility.FromJson<Dialogue>(dialogueText.text);
    }

    private void OnMouseDown() {
        FindObjectOfType<DialogueWindow>().ShowDialogue(dialogue);
    }
}

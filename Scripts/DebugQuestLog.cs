using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugQuestLog : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(AddQuest(5));
    }

    private Quest getNext(int i) {
        Quest q = new Quest();
        q.questName = "Quest test " + i;
        q.questDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        q.expReward = Random.Range(100,1000);
        q.goldReward = Random.Range(5,20);
        q.questCategory = 0;
        q.objective = new Quest.Objective();
        q.objective.type = (Quest.Objective.Type)Random.Range(0,2);
        q.objective.amount = Random.Range(2,10);
        return q;
    }

    private IEnumerator AddQuest(int iter) {
        for (int i = 0; i < iter; i++) {
            QuestLog.AddQuest(getNext(i));
            yield return new WaitForSeconds(3f);
        }
    }
}

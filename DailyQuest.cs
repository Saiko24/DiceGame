using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class DailyQuest : MonoBehaviour {

    public GameObject quest;
    public GameObjectList currentQuestsList;
    public IntArrayList currentIntQuestsList;
    public QuestLibrary questLibrary;
    public GameObject questContainer;
    public GameObjectVariable replaceQuestObj;
    public BoolVariable replaceQuestActive;

    // Use this for initialization
    void Start () {

        UpdateQuests();
    }

    public void AddNewQuest()
    {
        if (currentIntQuestsList.arrayList.Count < 3)
        {
            GameObject questObj = Instantiate(quest) as GameObject;
            questObj.transform.SetParent(questContainer.transform, false);
            int id = GenerateNewQuest();
            questObj.GetComponent<QuestDisplay>().questId = id;
            currentQuestsList.gameObjectList.Add(questObj);
            currentIntQuestsList.arrayList.Add(id);
        }
    }

    public void UpdateQuests()
    {
        currentQuestsList.gameObjectList.Clear();

        foreach (var item in currentIntQuestsList.arrayList)
        {
            GameObject questObj = Instantiate(quest) as GameObject;
            questObj.transform.SetParent(questContainer.transform, false);
            questObj.GetComponent<QuestDisplay>().questId = item;
            currentQuestsList.gameObjectList.Add(questObj);
        }
    }

    public int GenerateNewQuest()
    {
        int rand = Random.Range(0, questLibrary.questsList.Count);

        while (CheckSameQuests(rand))
        {
            rand = Random.Range(0, questLibrary.questsList.Count);
        }

        return rand;
    }

    public bool CheckSameQuests(int id)
    {
        if (currentIntQuestsList.arrayList != null)
        {
            foreach (var item in currentIntQuestsList.arrayList)
            {
                if (item == id)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ReplaceQuest()
    {
        int id = GenerateNewQuest();

        currentIntQuestsList.arrayList[currentQuestsList.gameObjectList.IndexOf(replaceQuestObj.value)] = id;

        replaceQuestObj.value.GetComponent<QuestDisplay>().questId = id;
    }
}

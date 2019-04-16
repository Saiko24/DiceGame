using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;

public class QuestDisplay : MonoBehaviour {

    public int questId;
    public QuestLibrary questLibrary;
    private Quest quest;
    public Text questProgressText;
    public Text questDescriptionText;
    public Text questRewardText;
    public Image questRewardIcon;
    public GameObjectVariable replaceQuestObj;
    public GameObject replaceQuest;
    public BoolVariable replaceQuestActive;

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {

        foreach (var item in questLibrary.questsList)
        {
            if (questId == item.questId)
            {
                quest = item;
            }
        }

        if (replaceQuestActive.value)
        {
            replaceQuest.SetActive(true);
        }
        else
        {
            replaceQuest.SetActive(false);
        }

        questProgressText.text = string.Format("{0}/{1}", quest.questProgress.value, quest.maxQuestProgress);
        questDescriptionText.text = quest.questDescription.ToString();
        questRewardText.text = quest.rewardValue.ToString();
        questRewardIcon.sprite = quest.rewardIcon;
    }

    public void ReplaceQuestGameObject()
    {
        replaceQuestObj.value = gameObject;
        replaceQuestActive.value = false;
    }
}

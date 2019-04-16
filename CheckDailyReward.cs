using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class CheckDailyReward : MonoBehaviour {


    public StringVariable LastCheckTime;
    public StringVariable LastCheckDate;
    public IntVariable CurrentRewardDay;
    public GameEvent DisplayDailyReward;
    public List<ItemDisplay> RewardGameObjectList;


    void Start()
    {
        if (PlayerPrefs.GetString("DailyRewardLastCheckDate", "") == "")
        {
            TimeManager.sharedInstance.UpdateLastCheckDate();
            PlayerPrefs.SetString("DailyRewardLastCheckDate", LastCheckDate.value);

            CurrentRewardDay.value = 0;
            DisplayDailyReward.Raise();
            DailyRewardDisplayUpdate();
            GetDailyReward();
        }
        else
        {
            CheckDate();
        }
    }


    private IEnumerator DateRequest()
    {

        yield return StartCoroutine(
            TimeManager.sharedInstance.RequestCurrentDateAndTime()
        );

        DailyRewardCheck();

    }

    public void CheckDate()
    {
        StartCoroutine("DateRequest");
    }

    private void DailyRewardCheck()
    {
        TimeManager.sharedInstance.RequestCurrentDateAndTime();

        DateTime lastDate = DateTime.Parse(LastCheckDate.value);

        string s = TimeManager.sharedInstance.getCurrentDate().ToString();

        DateTime nowDate = DateTime.Parse(s);

        if (lastDate < nowDate)
        {
            DisplayDailyReward.Raise();
            DailyRewardDisplayUpdate();
        }
        else if (lastDate > nowDate)
        {
            Debug.Log("Hack?");
        }
        else
        {
            Debug.Log("Reward time did not come.");
        }
    }

    public void DailyRewardDisplayUpdate()
    {

        if (CurrentRewardDay.value == 10)
        {
            CurrentRewardDay.value = 0;
        }

        RewardGameObjectList[CurrentRewardDay.value].itemEventVisible = true;
        RewardGameObjectList[CurrentRewardDay.value].itemEvent.SetActive(true);
    }


    public void GetDailyReward()
    {
        if (RewardGameObjectList[CurrentRewardDay.value].item.id == -1 || RewardGameObjectList[CurrentRewardDay.value].item.id == -2)
        {
            RewardGameObjectList[CurrentRewardDay.value].item.UseItem();
        }

        CurrentRewardDay.value++;

        TimeManager.sharedInstance.UpdateLastCheckDate();
        PlayerPrefs.SetString("DailyRewardLastCheckDate", LastCheckDate.value);
    }
}

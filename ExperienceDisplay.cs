using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;


public class ExperienceDisplay : MonoBehaviour {

    public Slider ExperienceSlider;
    public PlayerCurrentParameter currentExperience;
    public PlayerCurrentParameter currentMaxExperience;
    public PlayerCurrentParameter currentLevel;
    public IntArrayList levelExperienceList;
    public Text ExperienceBarText;
    public GameEvent callHelpTip;
    public IntVariable helpTipIndex;
    public float currentLevelLastCheck;

    // Use this for initialization
    void Start () {

        currentLevelLastCheck = currentLevel.ParameterValue;
        ExperienceChange();
    }
	
	// Update is called once per frame
	void Update () {
        ExperienceChange();
    }

    public void ExperienceChange()
    {
        if (currentExperience.ParameterValue >= currentMaxExperience.ParameterValue)
        {
            currentExperience.ParameterValue = (currentExperience.ParameterValue - currentMaxExperience.ParameterValue);

            NewLevel();
        }

        UpdateExperienceSlider();
    }

    public void UpdateExperienceSlider()
    {
        if (currentLevel.ParameterValue < levelExperienceList.arrayList.Count)
        {
            currentMaxExperience.ParameterValue = levelExperienceList.arrayList[(int)currentLevel.ParameterValue];

            ExperienceSlider.maxValue = currentMaxExperience.ParameterValue;
            ExperienceSlider.value = currentExperience.ParameterValue;

            ExperienceBarText.text = string.Format("{0} / {1} XP", currentExperience.ParameterValue, currentMaxExperience.ParameterValue);
        }
        else
        {
            currentExperience.ParameterValue = 0;
            currentLevel.ParameterValue = levelExperienceList.arrayList.Count;

            ExperienceSlider.maxValue = currentMaxExperience.ParameterValue;
            ExperienceSlider.value = ExperienceSlider.maxValue;

            ExperienceBarText.text = "Max level";
        }
    }

    public void NewLevel()
    {
        currentLevel.ParameterValue++;

        UpdateExperienceSlider();

        if (currentLevel.ParameterValue < levelExperienceList.arrayList.Count)
        {

            helpTipIndex.value = 1;
            callHelpTip.Raise();
        }
        else if (currentLevel.ParameterValue == levelExperienceList.arrayList.Count)
        {
            helpTipIndex.value = 2;
            callHelpTip.Raise();
        }
    }
}


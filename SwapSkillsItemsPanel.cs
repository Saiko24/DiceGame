using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using SO;


public class SwapSkillsItemsPanel : MonoBehaviour {

    public Animator animator;
    public GameObject itemsPanel;
    public GameObject skillsPanel;
    public GameEvent SwapSkillsItems;
    public bool skillsPanelActive;

    // Use this for initialization
    void Start () {
        skillsPanelActive = false;

        if (skillsPanelActive)
        {
            itemsPanel.SetActive(false);
            skillsPanel.SetActive(true);
        }
        else
        {
            itemsPanel.SetActive(true);
            skillsPanel.SetActive(false);
        }
    }

    public void SwapPanels()
    {
        if (skillsPanelActive)
        {
            skillsPanelActive = false;
            Debug.Log("skills");
            itemsPanel.SetActive(true);
            skillsPanel.SetActive(false);
        }
        else
        {
            skillsPanelActive = true;
            Debug.Log("items");
            itemsPanel.SetActive(false);
            skillsPanel.SetActive(true);
        }
    }
}

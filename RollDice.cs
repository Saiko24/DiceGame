using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using SO;

public class RollDice : MonoBehaviour {

    public PlayerCurrentParameter movesParameter;
    public Animator animator;
    public BoxCollider2D buttonCollider;
    public Image blockSprite;

    private void Start()
    {
        movesParameter.currentParameterValue = 0;
    }

    public void RandomDiceRoll()
    {
        animator.Play("RollDiceAnimation");
        movesParameter.ParameterValue = Random.Range(1, 7);
        BlockDiceRoll();
    }

    public void BlockDiceRoll()
    {
        if (buttonCollider.enabled == true)
        {
            buttonCollider.enabled = false;
            blockSprite.enabled = true;
        }
        else
        {
            buttonCollider.enabled = true;
            blockSprite.enabled = false;
        }

    }
}

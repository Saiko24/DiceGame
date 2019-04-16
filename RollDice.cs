using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using SO;

public class RollDice : MonoBehaviour {

    public PlayerCurrentParameter movesParameter;
    public Animator animator;

    public void randomDiceRoll()
    {
        animator.Play("RollDiceAnimation");
        movesParameter.ParameterValue = Random.Range(1, 7);
    }
}

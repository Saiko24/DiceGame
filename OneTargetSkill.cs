using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

[CreateAssetMenu(menuName = "Skills/OneTargetSkill")]
public class OneTargetSkill : BaseSkill
{
    public int skillValue;
    public EnemiesArray EnemiesArray;
    public GameObjectList enemyBattleCell;
    public BoolVariable SkillActivated;
    public StringVariable battlePhase;
    public GameObjectVariable selectedEnemy;
    public GameEvent nextTurn;

    public override void UseSkill()
    {
        selectedEnemy.value.GetComponent<EnemyBattle>().enemyHealth -= skillValue;

        nextTurn.Raise();
    }

    public override void ChooseTarget()
    {
        if (id == 0)
        {
            for (int i = 0; i < enemyBattleCell.gameObjectList.Count; i++)
            {
                enemyBattleCell.gameObjectList[i].GetComponent<BattleCell>().selectable = true;
                SkillActivated.value = true;
            }
        }
    }
}

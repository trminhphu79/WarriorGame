using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkill : Skill
{
    [Header("Black hole info")]
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float blackHoleDuration;
    [SerializeField] private float shrinkSpeed;
    [Space]
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneCooldownAttack;

    private BlackHoleSkillController blackHoleSkillController;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("Black hole skill used");
        GameObject newBlackHole = Instantiate(blackHolePrefab, player.transform.position, Quaternion.identity);
        blackHoleSkillController = newBlackHole.GetComponent<BlackHoleSkillController>();
        blackHoleSkillController.SetupBlackHole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldownAttack, blackHoleDuration);
    }

    public bool SkillCompleted()
    {
        if (!blackHoleSkillController)
            return false;

        if(blackHoleSkillController.playerCanExistState)
        {
            blackHoleSkillController = null;
            return true;
        }
        return false;
    }
}

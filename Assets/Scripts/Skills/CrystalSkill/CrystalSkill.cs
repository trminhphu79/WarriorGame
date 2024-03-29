using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;
    private CrystalSkillController crystalSkillController;

    [Header("Explosive crystal")]
    [SerializeField] private bool canExplode;

    [Header("Moving crystal")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canMoveToEnemy;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            crystalSkillController = currentCrystal.GetComponent<CrystalSkillController>();

            crystalSkillController.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed);
        }
        else
        {
            Vector2 playerPos = player.transform.position;
            currentCrystal.transform.position = playerPos;
            player.transform.position = currentCrystal.transform.position;
            crystalSkillController?.FnishCrystal();
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}

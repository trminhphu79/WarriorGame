using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : Skill
{
    [Header("Sword Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float swordGravity;

    public void SetUp(Vector2 launchDir, float swordGravity)
    {
        this.launchDir = launchDir;
        this.swordGravity = swordGravity;
    }
}

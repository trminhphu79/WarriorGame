using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public DashSkill dash { get; private set; }
    public CloneSkill clone { get; private set; }
    public SwordSkill sword { get; private set; }
    public BlackHoleSkill blackHole { get; private set; }

    public CrystalSkill crystalSkill { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of SkillManager found!");
            Destroy(instance);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        sword = GetComponent<SwordSkill>();
        blackHole = GetComponent<BlackHoleSkill>();
        crystalSkill = GetComponent<CrystalSkill>();
    }

    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        clone.CreateClone(_clonePosition, _offset);
    }
}

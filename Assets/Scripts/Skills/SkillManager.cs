using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public DashSkill dashSkill { get; private set; }
    public CloneSkill cloneSkill { get; private set; }
   
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
        dashSkill = GetComponent<DashSkill>();
        cloneSkill = GetComponent<CloneSkill>();
    }

    public void CreateClone(Transform _clonePosition)
    {
        cloneSkill.CreateClone(_clonePosition);
    }
}

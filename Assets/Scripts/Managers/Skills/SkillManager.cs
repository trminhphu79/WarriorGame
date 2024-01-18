using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
   public static SkillManager instance;
   
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
}

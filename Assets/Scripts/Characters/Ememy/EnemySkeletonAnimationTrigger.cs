using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTrigger : MonoBehaviour
{
    private EnemySkeleton enmey => GetComponentInParent<EnemySkeleton>();
    public void AnimationTriger()
    {
        enmey.AnimationFinishTrigger();
    }
}

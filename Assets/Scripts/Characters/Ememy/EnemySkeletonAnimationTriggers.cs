using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton enmey => GetComponentInParent<EnemySkeleton>();
    private void AnimationTriger()
    {
        enmey.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D collider2D =  Physics2D.OverlapCircle(enmey.attackCheck.position, enmey.attackCheckRadius);
        if (collider2D != null)
        {
            if (collider2D.GetComponent<Player>() != null)
            {
                collider2D.GetComponent<Player>().Damage();
            }
        }
    }

    // Logics here to trigger stun animation of skeleton
    private void OpenCouterWindow() => enmey.OpenCounterAttackWindow();
    private void CloseCouterWindow() => enmey.CloseCounterAttackWindow();
}

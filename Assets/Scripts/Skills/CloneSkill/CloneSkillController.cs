using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private float colorLoosingSpeed;
    [SerializeField] public float cloneDuration { get; private set; }
    [SerializeField] public float cloneTimer { get; private set; }

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.8f;

    private Transform closestEnemy;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime * colorLoosingSpeed));
        }
    }
    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack)
    {
        if (_canAttack)
        {
            var randomValue = Random.Range(1, 3);
            animator.SetInteger("AttackNumber", randomValue);
        }
        transform.position = _newTransform.position;
        cloneDuration = _cloneDuration;

        FaceClosestTarget();
    }

    private void AnimationTriger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().Damage();
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25f);

        float closestDistance = Mathf.Infinity;
        Debug.Log("closestDistance: " + closestDistance);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                // check enemy distance to clone and
                // if it's closer than the previous closest enemy, set it as the new closest enemy
                float distanceToTarget = Vector2.Distance(transform.position, collider.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    Debug.Log("distanceToTarget: " + distanceToTarget);
                    closestEnemy = collider.transform;
                }
            }
        }   

        if(closestEnemy != null)
        {
            // if the enemy is to the right of the clone, flip the clone to the right
            if(transform.position.x < closestEnemy.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            // if the enemy is to the left of the clone, flip the clone to the left
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

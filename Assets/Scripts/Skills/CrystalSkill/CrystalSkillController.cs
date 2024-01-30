using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D cd;

    private float crystalExistTimer;
    private bool canExplode;
    private bool canMoveToEnemy;
    private float moveSpeed;

    private bool canGrow;
    private float growSpeed = 5;

    public void SetupCrystal(float _crystalDuration, bool _canExplode, bool _canMoveToEnemy, float _moveSpeed)
    {
        crystalExistTimer = _crystalDuration;
        canExplode = _canExplode;
        canMoveToEnemy = _canMoveToEnemy;
        moveSpeed = _moveSpeed;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        crystalExistTimer -= Time.deltaTime;
        if (crystalExistTimer < 0)
        {
            FnishCrystal();
        }

        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growSpeed * Time.deltaTime);
    }

    public void FnishCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            anim.SetTrigger("Explode");
        }
        else
            SeftDestroy();
    }

    public void SeftDestroy() => Destroy(gameObject);

    private void AnimationeExplodeEvent()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, cd.radius);
        foreach (Collider2D collider in collider2Ds)
            collider.GetComponent<Enemy>()?.Damage();
    }
}

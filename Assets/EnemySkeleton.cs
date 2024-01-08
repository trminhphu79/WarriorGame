using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Entity
{

    [Header("Move info")]
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }
}

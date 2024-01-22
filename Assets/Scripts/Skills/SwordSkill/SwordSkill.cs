using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : Skill
{
    [Header("Sword Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;


    [Header("Aim dots")]
    [SerializeField] private GameObject aimDotPrefab;
    [SerializeField] private Transform dotsParent;
    [SerializeField] private int numberOfDots;
    [SerializeField] private float dotSpacing;

    Vector2 finalDir;
    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenerateDots();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
           finalDir = new Vector2(AnimDirection().normalized.x * launchForce.x, AnimDirection().normalized.y * launchForce.y);
        
       if(Input.GetKey(KeyCode.Mouse1) && !player.sword)
        {
            DotsActive(true);
            for (int i = 0; i < numberOfDots; i++)
            {
                dots[i].transform.position = DotsPosition(i * dotSpacing);
            }
        }
    }
    public void CreateSkill()
    {
        GameObject sword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController controller = sword.GetComponent<SwordSkillController>();
        Debug.Log(finalDir.y + " ---- " + finalDir.x);
        controller.SetupSword(finalDir, swordGravity, player);

        player.AssigneNewSword(sword);
        DotsActive(false);
    }

    //Caculate the direction of the sword depends on the mouse position and player position
    public Vector2 AnimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePosition - playerPosition;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    public void GenerateDots()
    {
      dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(aimDotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    // describe for me what this function does
    private Vector2 DotsPosition(float _time)
    {
        return  (Vector2)player.transform.position + new Vector2(
             AnimDirection().normalized.x * launchForce.x,
             AnimDirection().normalized.y * launchForce.y)
             * _time + .5f * (Physics2D.gravity * swordGravity) * _time * _time;
    }
}

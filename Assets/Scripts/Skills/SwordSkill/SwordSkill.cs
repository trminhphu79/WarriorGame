using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordType
{
  Regular,
  Bounce,
  Pierce,
  Spin
}
public class SwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;
    [Header("Bounce info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;


    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Sword Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    [Header("Spin info")]
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float spinGravity = 1;
    [SerializeField] private float hitCooldown = .4f;

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
        SetupGravity();
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
        SetupPropsSword(controller);
        controller.SetupSword(finalDir, swordGravity, player, freezeTimeDuration, returnSpeed);
        player.AssigneNewSword(sword);
        DotsActive(false);
    }


    private void SetupGravity()
    {
        switch (swordType)
        {
            case SwordType.Regular:
                swordGravity = pierceGravity;
                break;
            case SwordType.Bounce:
                swordGravity = bounceGravity;
                break;
            case SwordType.Pierce:
                swordGravity = pierceGravity;
                break;
            case SwordType.Spin:
                swordGravity = spinGravity;
                break;
            default:
                break;
        }
    }

    private void SetupPropsSword(SwordSkillController _controller)
    {
        switch (swordType)
        {
            case SwordType.Regular:
                break;
            case SwordType.Bounce:
                _controller.SetupBounce(true, bounceAmount, bounceSpeed);
                break;
            case SwordType.Pierce:
                _controller.SetupPierce(pierceAmount);
                break;
            case SwordType.Spin:
                _controller.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);
                break;
            default:
                break;
        }
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

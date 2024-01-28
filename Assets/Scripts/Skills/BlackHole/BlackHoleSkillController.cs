using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{

    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKeys = new List<GameObject>();

    public float maxSize;
    public float growSpeed;
    public float shrinkSpeed;

    private bool canGrow = true;
    private bool canShrink;
    private bool canCreateHotKeys = true;
    private bool cloneAttackReleased = false;
    private float blackHoleTimer;

    private int amountOfAttacks = 4;

    private float cloneCooldownAttack = 0.5f;
    private float cloneAttackTimer;

    public bool playerCanExistState {  get; private set; }

    public void SetupBlackHole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amountOfAttacks, float _cloneCooldownAttack, float _blackHoleDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneCooldownAttack = _cloneCooldownAttack;
        blackHoleTimer = _blackHoleDuration;
    }

    [System.Obsolete]
    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackHoleTimer -= Time.deltaTime;

        ReleaseCloneAttack();
        CloneAttackLogics();

        if (blackHoleTimer < 0)
        {
            blackHoleTimer = Mathf.Infinity;
            if (targets.Count > 0)
                ReleaseCloneAttack();
            else 
                FinishBlackHoleAbility();
        }

        if (canGrow && !canShrink)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ReleaseCloneAttack()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyHotKey();
            cloneAttackReleased = true;
            canCreateHotKeys = false;

            PlayerManager.instance.player.MakeTransparent(true) ;
        }
    }

    private void CloneAttackLogics()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased)
        {
            cloneAttackTimer = cloneCooldownAttack;
            int randomIndex = Random.Range(0, targets.Count);
            float xOffset = Random.RandomRange(0, 100) > 50 ? 2 : -2;
            SkillManager.instance.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));
            amountOfAttacks--;

            if (amountOfAttacks <= 0)
                Invoke("FinishBlackHoleAbility", 2);
        }
    }

    private void FinishBlackHoleAbility()
    {
        //PlayerManager.instance.player.ExisBlackHoleAbility();
        canShrink = true;
        cloneAttackReleased = false;
        playerCanExistState = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().FreezeTime(false);
    }

    private void DestroyHotKey()
    {
        Debug.Log("Destroying hotkeys: " + createdHotKeys.Count);
        if (createdHotKeys.Count < 0)
            return;

        for (int i = 0; i < createdHotKeys.Count; i++)
        {
            Destroy(createdHotKeys[i]);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        Debug.Log("Creating hotkey: " + keyCodeList.Count);
        if(keyCodeList.Count == 0)
        {
            Debug.Log("No more keys");
            return;
        }

        if (!canCreateHotKeys)
            return;
     
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKeys.Add(newHotKey);
        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        BlackHoleHotKeyController hotKeyController = newHotKey.GetComponent<BlackHoleHotKeyController>();
        hotKeyController.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform enemy)
    {
        targets.Add(enemy);
    }   

    public void CanGrow(bool _canGrow)
    {
        canGrow = _canGrow;
    }
}

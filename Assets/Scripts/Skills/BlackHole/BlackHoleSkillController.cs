using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    public float maxSize;
    public float growSpeed;
    public bool canGrow;

    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    public List<Transform> targets = new List<Transform>();
    private void Update()
    {
        if (canGrow) { 
          transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            GameObject newGameObject = Instantiate(hotKeyPrefab, collision.transform.position, Quaternion.identity);
           KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
           keyCodeList.Remove(choosenKey);
           Debug.Log("choosenKey: " + choosenKey.ToString());
           BlackHoleHotKeyController hotKeyController = newGameObject.GetComponent<BlackHoleHotKeyController>();
           hotKeyController.SetupHotKey(choosenKey);
        } 

        Debug.Log("Triggered:  " + targets.Count);
    }
}
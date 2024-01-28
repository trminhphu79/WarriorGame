using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackHoleHotKeyController : MonoBehaviour
{
    private KeyCode myHotKey;
    private SpriteRenderer mySpriteRenderer;
    private TextMeshProUGUI myText;
    private Transform enemy;
    private BlackHoleSkillController blackHoleSkillController;
    public void SetupHotKey(KeyCode _myNewHotKey, Transform enemy, BlackHoleSkillController _blackHoleSkillController)
    {

        Debug.Log("Setup hotkey" + _myNewHotKey);

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();
        myText.color =  Color.red;
        blackHoleSkillController = _blackHoleSkillController;
        this.enemy = enemy;
    }

    private void Update()
    {
        if(Input.GetKeyDown(myHotKey))
        {
            blackHoleSkillController.AddEnemyToList(enemy);
            myText.color = Color.clear;
            mySpriteRenderer.color = Color.clear;
        }
    }
}

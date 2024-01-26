using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackHoleHotKeyController : MonoBehaviour
{
    private KeyCode myHotKey;

    private TextMeshProUGUI myText;

    public void SetupHotKey(KeyCode _myNewHotKey)
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(myHotKey))
        {
            Debug.Log("Pressed " + myHotKey);
        }
    }
}

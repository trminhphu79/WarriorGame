using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventHandler : MonoBehaviour
{
    public event EventHandler<SpaceEventArgs> SpaceEvent;
    public class SpaceEventArgs : EventArgs
    {
        public int spaceCount { get; set; }
        public DateTime timeSpacing { get; set; }
    }

    private int _spaceCount { get; set;}
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spaceCount++;
            SpaceEvent?.Invoke(this, new SpaceEventArgs { spaceCount = _spaceCount, timeSpacing = DateTime.Now });
        }
    }
}

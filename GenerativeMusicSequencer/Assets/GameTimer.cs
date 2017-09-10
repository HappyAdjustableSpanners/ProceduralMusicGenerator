using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("Tick", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {

      
	}

    private void Tick()
    {
        EventManager.OnTick();
        Debug.Log("Tick");
    }
}

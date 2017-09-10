using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchwannBehaviour : MonoBehaviour {

    public bool haveGhost = false;

	// Use this for initialization
	void Start () {
        EventManager.tickMethods += Activate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeGhost(GameObject ghost)
    {
        haveGhost = true;
        ghost.transform.position = transform.position;
    }

    private void Activate()
    {
        //Move to next schwann cell
        if(haveGhost)
        {
            //Check if we are at the end of the route
            if(this.name.Equals("Shwann6"))
            {

            }
            
        }
    }
}

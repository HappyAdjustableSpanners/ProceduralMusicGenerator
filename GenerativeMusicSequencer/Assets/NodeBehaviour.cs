using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour {

    //List of connected routes
    public List<RouteBehaviour> routes = new List<RouteBehaviour>();

    private GameObject ghost;

    public bool haveGhost = false;


	// Use this for initialization
	void Start () {
        EventManager.tickMethods += Activate;

        ghost = Resources.Load<GameObject>("Prefabs/Ghost");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called every 10ms of game time
    public void Activate()
    {
            //Push ghost
            foreach(RouteBehaviour r in routes)
            {
                //Instantiate ghost at this node for every route
                GameObject ghost = Instantiate(Resources.Load<GameObject>("Prefabs/Ghost"), transform);

                //Send message to ghost to go to connected node
                r.TakeGhost(ghost);

                haveGhost = false;
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private int numNodes, numBridges = 0;

	// Use this for initialization
	void Start () {
        EventManager.createNodeMethods += IncNumNodes;
        EventManager.createBridgeMethods += IncNumBridges;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetNumNodes()
    {
        return numNodes;
    }

    private void IncNumNodes()
    {
        numNodes++;
    }

    public int GetNumBridges()
    {
        return numBridges;
    }

    public void IncNumBridges()
    {
        numBridges++;
    }

}

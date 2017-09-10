using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour {

    private LinkManager linkManager;

    public string currentNode, previousNode;


	// Use this for initialization
	void Start () {
        EventManager.tickMethods += Activate;
        linkManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LinkManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void Activate()
    {
        if(currentNode.Contains("Bridge"))
        {
            LinkManager.NodeInfo nodeInfo = linkManager.GetNextNodePos(currentNode, previousNode);

            transform.position = nodeInfo.Pos;
            previousNode = currentNode;
            currentNode = nodeInfo.Name;
        }
        else if(currentNode.Contains("Node"))
        {
            //Check if current node has any connections
            if (linkManager.HasConnections(currentNode))
            {

                //Get a random next node
                LinkManager.NodeInfo nodeInfo = linkManager.GetNextRandNodePos(currentNode);

                transform.position = nodeInfo.Pos;
                previousNode = currentNode;
                currentNode = nodeInfo.Name;
            }
        }        
    }

    public void SetCurrentNode(string value)
    {
        currentNode = value;
    }

    public void SetPreviousNode(string value)
    {
        previousNode = value;
    }

}

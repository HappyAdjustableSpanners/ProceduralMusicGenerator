using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteBehaviour : MonoBehaviour {

    //Connecting nodes
    public GameObject[] nodes = new GameObject[2];
    //Node positions
    private Vector3[] nodePositions = new Vector3[2];
    //Line
    private LineRenderer line;

	// Use this for initialization
	void Awake () {

        //Get line
        line = GetComponent<LineRenderer>();

        //Make a line between the two nodes
        MakeLine();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    void MakeLine()
    {
        nodePositions[0] = nodes[0].transform.position;
        nodePositions[1] = nodes[1].transform.position;
        line.SetPositions(nodePositions);

        //Get difference in position between the two ends of the line
        Vector3 diff = new Vector3(nodePositions[0].x - nodePositions[1].x,
                                   nodePositions[0].y - nodePositions[1].y,
                                   nodePositions[0].z - nodePositions[1].z);

        Vector3 step = diff / 7;

        for(int i = 0; i < 7; i++)
        {
            GameObject go = new GameObject();
            go.transform.position = nodePositions[1] + (step * i);
            go.transform.SetParent(this.transform);
            go.name = "Shwann" + i;
            go.AddComponent<SchwannBehaviour>();
        }

    }

    public void TakeGhost(GameObject ghost)
    {
        
        //Work out which end we are at
        if (Vector3.Distance(ghost.transform.position, transform.Find("Shwann1").position) < Vector3.Distance(ghost.transform.position, transform.Find("Shwann6").position))
        {
            //We are at schwann1
            transform.Find("Shwann0").GetComponent<SchwannBehaviour>().TakeGhost(ghost);
        }
        else
        {
            //We are at schwann6
            transform.Find("Shwann6").GetComponent<SchwannBehaviour>().TakeGhost(ghost);
        }
    }
}

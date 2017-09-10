using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndPlace : MonoBehaviour {

    public GameObject objToPlace;
    private RaycastHit hit;
    private LineRenderer lineRenderer;

    private bool dragging = false;
    private GameObject fromNode, toNode;
    private GameController gameController;
    private GameObject prevNode;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        //If left mb pressed
        if (Input.GetMouseButtonDown(0))
        {
            //Do a raycast from click point
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //See if we hit something
            if (Physics.Raycast(ray, out hit))
            {
                //If we hit the backing plane, place a node
                if (hit.collider.name == "BackingPlane")
                {
                    //Instantiate the obj
                    objToPlace = Instantiate(objToPlace, hit.point, Quaternion.identity);

                    //Bring it forward a bit so we can see it in front of the plane
                    objToPlace.transform.position = new Vector3(objToPlace.transform.position.x,
                                                                objToPlace.transform.position.y, 
                                                                objToPlace.transform.position.z - 1f);

                    //Name it
                    objToPlace.name = "Node_" + gameController.GetNumNodes().ToString();

                    //Trigger event
                    EventManager.OnCreateNode();        
                }
                else if(hit.collider.tag == "Node")
                {
                    //Start recording drag
                    dragging = true;

                    //Create a new obj and add a lineRenderer to it
                    CreateLine();

                    //Record our 'fromNode'
                    fromNode = hit.collider.gameObject;
                }
            }
        }

        if(Input.GetMouseButtonUp(0) && dragging == true)
        {
            //We have finished dragging
            dragging = false;

            //Do a raycast from  release point
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //See if we hit something
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Node" && hit.collider.name != fromNode.name)
                {
                    //Set line ending position to center of 'toNode'
                    lineRenderer.SetPosition(1, hit.collider.transform.position);

                    prevNode = fromNode;
                    toNode = hit.collider.gameObject;

                    //Make 7 points along the line
                    MakeLinePoints(lineRenderer);

                    
                    

                    ////Record our 'toNode'
                    //GameObject toNode = hit.collider.gameObject;
                    //
                    //Trigger event
                    //EventManager.OnCreateLink(fromNode,  toNode);
                }
                else
                {
                    //If we started dragging but didn't finish on another node, destroy the line
                    Destroy(lineRenderer.gameObject);
                }

                if(hit.collider.name == fromNode.name)
                {
                    //We have clicked on an already created node
                    //So we create a ghost on this node
                    CreateGhost(hit.collider.gameObject);
                }
            }           
        }
    }

    private void CreateGhost(GameObject node)
    {
        //Instantiate a ghost GO at node pos
        GameObject ghost = Instantiate(Resources.Load<GameObject>("Prefabs/Ghost"), node.transform.position, Quaternion.identity);

        //Update current position from dictionary
        ghost.GetComponent<GhostBehaviour>().SetCurrentNode(node.name);
    }
    
    private void CreateLine()
    {
        //Create a new GO, set parent (for readability)
        GameObject line = new GameObject();
        line.transform.parent = GameObject.Find("lines").transform;

        //Add a line renderer and set start position and width
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.SetPosition(0, hit.collider.transform.position);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }	

    private void MakeLinePoints(LineRenderer lineRenderer)
    {
        //Get difference in position between the two ends of the line
        Vector3 diff = lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1);

        //Get an 8th of the difference
        Vector3 step = diff / 8;


        //Create 7 positions along the line
        for (int i = 1; i < 8; i++)
        {
            GameObject go = new GameObject();
            go.transform.position = lineRenderer.GetPosition(0) - (step * i);
            go.transform.SetParent(this.transform);
            go.name = "Bridge_" + (gameController.GetNumBridges());
            EventManager.OnCreateLink(go, prevNode, gameController.GetNumBridges());
            prevNode = go;

            EventManager.OnCreateBridge();

            if(i == 7)
            {
                //We are on last bridge node, so connect it to node
                EventManager.OnCreateLink(toNode, go, gameController.GetNumBridges());
            }
            
        }
        
    }
}

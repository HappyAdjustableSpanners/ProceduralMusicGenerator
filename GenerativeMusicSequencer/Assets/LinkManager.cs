using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour {

    //A list of lists holding each node and their next nodes
    private Dictionary<string, List<NodeInfo>> links = new Dictionary<string, List<NodeInfo>>();

    private int id = 0;

    // Use this for initialization
    void Start() {
        EventManager.createLinkMethods += OnCreateLink;
    }

    // Update is called once per frame
    void Update() {

    }

    //Adjacency matrix
    private void OnCreateLink(GameObject nodeTo, GameObject nodeFrom, int index)
    {
        //If the node does not already exist, create it
        if (!links.ContainsKey(nodeTo.name))
        {
            //Then find and add value to nodeTo key
            links.Add(nodeTo.name, new List<NodeInfo>());

            //Create a new nodeInfo of itself to be the first element in it's list
            NodeInfo nodeInfo = new NodeInfo();
            nodeInfo.Name = nodeTo.name;
            nodeInfo.Pos = nodeTo.transform.position;
            nodeInfo.Index = index;

            links[nodeTo.name].Add(nodeInfo);
        }

        //If the node does not already exist, create it
        if (!links.ContainsKey(nodeFrom.name))
        {
            //Then find and add value to nodeTo key
            links.Add(nodeFrom.name, new List<NodeInfo>());

            //Create a new nodeInfo of itself to be the first element in it's list
            NodeInfo nodeInfo = new NodeInfo();
            nodeInfo.Name = nodeFrom.name;
            nodeInfo.Pos = nodeFrom.transform.position;
            nodeInfo.Index = index - 1;

            links[nodeFrom.name].Add(nodeInfo);
        }

        NodeInfo nodeToInfo = new NodeInfo();
        nodeToInfo.Name = nodeTo.name;
        nodeToInfo.Pos = nodeTo.transform.position;
        nodeToInfo.Index = index;

        NodeInfo nodeFromInfo = new NodeInfo();
        nodeFromInfo.Name = nodeFrom.name;
        nodeFromInfo.Pos = nodeFrom.transform.position;

        //look up nodeTo, and add nodeFrom as a next
        links[nodeToInfo.Name].Add( nodeFromInfo );

        //look up nodeFrom, and add nodeTo as a next
        links[nodeFromInfo.Name].Add( nodeToInfo );
    }

    public struct NodeInfo
    {
        string nameVal;
        Vector3 posVal;
        int index;
       public int Index
       {
           get
           {
               return index;
           }
           set
           {
               index = value;
           }
       }

        public string Name
        {
            get
            {
                return nameVal;
            }
            set
            {
                nameVal = value;
            }
        }

        public Vector3 Pos
        {
            get
            {
                return posVal;
            }
            set
            {
                posVal = value;
            }
        }
    }

    public bool HasConnections(string name)
    {
        if (links[name].Count == 0)
        {
            return false;
        }
        else
            return true;
    }

    public NodeInfo GetNextNodePos(string currentNode, string prevNode)
    {

        int direction = 1;
        int currIndex = links[currentNode][0].Index;
        int prevIndex = links[prevNode][0].Index;

        //Choose direction
        if (prevNode.Contains("Node"))
        {
            //If we have just left a node, and the next bridge node is greater than us
            for(int i = 1; i < links[currentNode].Count; i++)
            {
                if (links[currentNode][i].Name.Contains("Bridge"))
                {
                    if (links[currentNode][i].Index > links[currentNode][0].Index)
                    {
                        //go forward
                        direction = 1;
                    }
                    else
                        direction = 0;
                }
            }
        }
        else
        {
            //Find current node, and return the next one           
            if (currIndex > prevIndex)
            {
                direction = 1;
            }
            else
                direction = 0;

        }

        string[] split = currentNode.Split('_');
        if (direction == 1)
        {
            //Moving forward

            if ((currIndex + 1) % 7 == 0 && !prevNode.Contains("Node"))
            {
                //We have reached one end of the bridge
                //Check if the prevNode has a non-bridge node attached
                foreach (NodeInfo ni in links[currentNode])
                {
                    if (ni.Name.Contains("Node"))
                    {
                        return ni;
                    }
                }
            }

            string key = split[0] + "_" + (currIndex + 1);
            return links[key][0];
        }
        else
        {
            if (currIndex % (7) == 0 && !prevNode.Contains("Node"))
            {
                //We have reached one end of the bridge
                //Check if the prevNode has a non-bridge node attached
                foreach (NodeInfo ni in links[currentNode])
                {
                    if (ni.Name.Contains("Node"))
                    {
                        return ni;
                    }
                }
            }
            string key = split[0] + "_" + (currIndex - 1);
            return links[key][0];
        }
    }

    public NodeInfo GetNextRandNodePos(string name)
    {
        int rand = Random.Range(1, links[name].Count);
        NodeInfo ni = links[name][rand];
        return ni;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    //Delegates (parameters of message to be sent)
    public delegate void Tick();
    public delegate void CreateLink(GameObject node1, GameObject node2, int index);
    public delegate void CreateNode();
    public delegate void CreateBridge();

    //Events
    public static event Tick tickMethods;
    public static event CreateLink createLinkMethods;
    public static event CreateNode createNodeMethods;
    public static event CreateBridge createBridgeMethods;

    public static void OnTick()
    {
        if (tickMethods != null)
        {
            tickMethods();
        }
    }

    public static void OnCreateLink(GameObject node1, GameObject node2, int index)
    {
        createLinkMethods(node1, node2, index);
    }

    public static void OnCreateNode()
    {
        createNodeMethods();
    }

    public static void OnCreateBridge()
    {
        createBridgeMethods();
    }


}

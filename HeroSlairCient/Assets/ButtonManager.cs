using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
    NetManager manager;
	// Use this for initialization
	void Start () {
        manager = GameObject.Find("NetManager").GetComponent<NetManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Disconnect()
    {
        manager.OnClientDisconnect();
        
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    NetManager manager;
    public static bool newLogin;
	// Use this for initialization
	void Start () {
        manager = GameObject.Find("NetManager").GetComponent<NetManager>();
        manager.AskForLevels(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Disconnect()
    {
        manager.OnClientDisconnect();
        
    }

    public void OnClickPlay()
    {
        manager.OnClientReceiveLevel(0);
    }

    public void OnClickTitle()
    {
        Debug.Log("YUP");
    }

    public void CreateNewAccount()
    {
        newLogin = true;
        GameObject.Find("BadLogin").GetComponent<Text>().text = "You are in create a new account mode, please write a new username and password";
    }
}

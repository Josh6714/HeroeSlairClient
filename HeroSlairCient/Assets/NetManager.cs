using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetManager : NetworkManager {

	int connectionId = -1;
	const short clientMsgType = 1002;
	const short serverMsgType = 1003;
    NetworkClient myClient;

    public void Start()
    {
        SetUpClient();
        NetworkServer.RegisterHandler(serverMsgType, OnClientReadyToBeginMessage);
        NetworkServer.RegisterHandler(clientMsgType, OnClientReadyToBeginMessage);
    }

    public void SendReadyToBeginMessage(int myId)
    {
        if (connectionId != -1)
        {
            Debug.Log("Attempting to send to " + connectionId);
            var msg = new LevelMessage();
            msg.width = 10;
            msg.height = 20;
            NetworkServer.SendToClient(connectionId, serverMsgType, msg);
        }
        else
        {
            Debug.Log("ERROR: Not connected to client");
        }
    }

    void OnClientReadyToBeginMessage(NetworkMessage netMsg)
    {
        var beginMessage = netMsg.ReadMessage<LevelMessage>();
        Debug.Log("received OnClientReadyToBeginMessage " + beginMessage.message);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        connectionId = conn.connectionId;
        Debug.Log("Client Connected: " + connectionId);
    }

    //client connects to server
    public void SetUpClient()
    {
        //Network.Connect("localhost", 4444);
        StartClient();
        myClient = client;
        //myClient = new NetworkClient();
        //myClient.RegisterHandler(MsgType.Connect, OnConnected);
        //myClient.Connect("localhost", 7777);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Actually Connected blah");

    }

    public void OnConnected()
    {

    }
    //when connected to server
    void OnConnectedToServer()
    {
        Debug.Log("Actually Connected");
    }
}

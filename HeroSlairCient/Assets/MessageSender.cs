using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using LgOctEngine.CoreClasses;
public class MessageSender : NetworkBehaviour
{
	const short clientMsgType = 1002;
    const short serverMsgType = 1003;
    const short tagMsgType = 1004;

    const short locationXMsgType = 1005;
    const short locationYMsgType = 1006;
    const short locationZMsgType = 1007;

    const short scaleXMsgType = 1008;
    const short scaleYMsgType = 1009;
    const short scaleZMsgType = 1010;

    const short spawnMsgType = 1011;
    public const short doneMsgType = 1012;
    const short levelMsgType = 1013;
    public short MsgType;

	public NetworkManager myManager;
	public NetworkClient myClient;

    public List<string> Tags = new List<string>();

    public List<string> VectorsX = new List<string>();
    public List<string> VectorsY = new List<string>();
    public List<string> VectorsZ = new List<string>();

    public List<string> ScaleX = new List<string>();
    public List<string> ScaleY = new List<string>();
    public List<string> ScaleZ = new List<string>();

    public bool spawn = false;
    public static NetworkMessage levelMessage;
    LevelObject.SendLevel potato = new LevelObject.SendLevel();
	public void Start()
	{
		Init(myManager.client);
        MsgType = clientMsgType;
	}

	public void Init(NetworkClient client)
	{
        
		myClient = client;
		NetworkServer.RegisterHandler(clientMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(serverMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(tagMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(locationXMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(locationYMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(locationZMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(scaleXMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(scaleYMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(scaleZMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(spawnMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(doneMsgType, OnServerReadyToBeginMessage);
        NetworkServer.RegisterHandler(levelMsgType, OnServerReadyToBeginMessage);

        myClient.RegisterHandler(clientMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(serverMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(tagMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(locationXMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(locationYMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(locationZMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(scaleXMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(scaleYMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(scaleZMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(spawnMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(doneMsgType, OnServerReadyToBeginMessage);
        myClient.RegisterHandler(levelMsgType, OnServerReadyToBeginMessage);

        Debug.Log("Method Called");
	}

	public void SendReadyToBeginMessage(int myId)
	{
			var msg = new LevelMessage();
			msg.width = 10;
			msg.height = 20;
            msg.message = "ture";
			myClient.Send(MsgType, msg);
	}

	void OnServerReadyToBeginMessage(NetworkMessage netMsg)
	{
        if(netMsg.msgType == tagMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            Debug.Log("Received Tag: " + beginMessage.message);
            Tags.Add(beginMessage.message);
        }
        else if (netMsg.msgType == locationXMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            //Debug.Log("Received Vector: " + beginMessage.message);
            VectorsX.Add(beginMessage.message);
        }

        else if (netMsg.msgType == locationYMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            //Debug.Log("Received Vector: " + beginMessage.message);
            VectorsY.Add(beginMessage.message);
        }

        else if (netMsg.msgType == locationZMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            //Debug.Log("Received Vector: " + beginMessage.message);
            VectorsZ.Add(beginMessage.message);
        }

        else if (netMsg.msgType == scaleXMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            //Debug.Log("Received Vector: " + beginMessage.message);
            ScaleX.Add(beginMessage.message);
        }

        else if (netMsg.msgType == scaleYMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            //Debug.Log("Received Vector: " + beginMessage.message);
            ScaleY.Add(beginMessage.message);
        }

        else if (netMsg.msgType == scaleZMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            //Debug.Log("Received Vector: " + beginMessage.message);
            ScaleZ.Add(beginMessage.message);
        }
            
        else if(netMsg.msgType == levelMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            potato.SimpleArrayTest(beginMessage.message);
            Debug.Log(beginMessage.message);
        }

        else if(netMsg.msgType == spawnMsgType)
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            Debug.Log("Received Spawn: " + beginMessage.message);
            if(beginMessage.message == "true")
            {
                //spawn = true;
            }
        }
        else
        {
            var beginMessage = netMsg.ReadMessage<LevelMessage>();
            Debug.Log("received OnServerReadyToBeginMessage " + beginMessage.message);
        }
        
	}
}

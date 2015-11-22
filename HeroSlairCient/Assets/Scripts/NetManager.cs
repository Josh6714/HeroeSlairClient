using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using LgOctEngine.CoreClasses;

public class NetManager : NetworkManager {

	public NetworkClient myClient;

	public InputField username;
    public InputField password;
    public InputField ipAdress;
    public InputField portNumber;

	public void Start()
    {
        Debug.Log("NetManager:Start()");

        ipAdress.text = networkAddress;
        portNumber.text = networkPort.ToString();
        username.text = "username";
        password.text = "password";
    }

	public void OnClientSendLevel(int myId)
	{
		Debug.Log("Sending Level");
		var msg = new JsonMessage<Level>();

		// Take the level, serialize it and store in the message
		Level level = LgJsonNode.Create<Level>();
		LevelObject levelObject = level.LevelObjectArray.AddNew();
		levelObject.id = "ExamplePrefab";
		levelObject.row = 5;
		levelObject.column = 3 * 2;
		levelObject.rotation = 90;
		levelObject.status = 0;

		msg.message = level.Serialize();
		myClient.Send(MessageType.LEVEL_MSG, msg);
	}

    public void Connect()
    {
        StartClient();
        myClient = client;
    }
	public void OnClientSendLogin(int myId)
    {
		Debug.Log("Sending Login");
		var msg = new JsonMessage<Login>();
        networkAddress = ipAdress.text;
        networkPort = int.Parse(portNumber.text);
        Debug.Log("network Address: " + networkAddress + " port: " + networkPort);
		// Take the level, serialize it and store in the message
		Login login = LgJsonNode.Create<Login>();
		login.username = username.text;
        login.password = password.text;
		msg.message = login.Serialize();
        myClient.Send(MessageType.LOGIN_MSG, msg);
	}

	void OnClientReceiveMessage<T>(NetworkMessage netMsg) where T : LgJsonDictionary, IJsonable, new()
	{
		JsonMessage<T> jsonMessage = netMsg.ReadMessage<JsonMessage<T>>();
		Debug.Log("OnClientReceiveMessage: Received Message " + jsonMessage.message);
		T obj = LgJsonNode.CreateFromJsonString<T>(jsonMessage.message);
		obj.HandleNewObject();
	}

	public void OnClientDisconnect()
	{
		if (myClient.isConnected) 
		{
			Debug.Log ("Disconnecting");
			myClient.Disconnect();
		}
	}
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        myClient.RegisterHandler(MessageType.LEVEL_MSG, OnClientReceiveMessage<Level>);
        myClient.RegisterHandler(MessageType.ACKNOWLEDGE, OnClientReceiveMessage<Acknowledgement>);
        OnClientSendLogin(0);
        Debug.Log("Connected");
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }
}

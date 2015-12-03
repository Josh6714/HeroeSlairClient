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
    public Image ConnectBtn;

	public void Start()
    {
        Debug.Log("NetManager:Start()");

        //ipAdress.text = networkAddress;
        //portNumber.text = networkPort.ToString();
        //username.text = "username";
        //password.text = "password";
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

    public void OnClientReceiveLevel(int myId)
    {
        Debug.Log("Asking for level download");
        var msg = new JsonMessage<Request>();
        Request req = LgJsonNode.Create<Request>();
        req.request = "testLevel";
        msg.message = req.Serialize();
        myClient.Send(MessageType.REQUEST_LEVEL, msg);
    }

    public void Connect()
    {
        networkAddress = ipAdress.text;
        networkPort = int.Parse(portNumber.text);
        StartClient();
        myClient = client;
    }
	public void OnClientSendLogin(int myId)
    {
        
		Debug.Log("Sending Login");
		var msg = new JsonMessage<Login>();
        Debug.Log("network Address: " + networkAddress + " port: " + networkPort);
		// Take the level, serialize it and store in the message
		Login login = LgJsonNode.Create<Login>();
		login.username = username.text;
        login.password = password.text;
		msg.message = login.Serialize();

        if (ButtonManager.newLogin == false)
        {
            myClient.Send(MessageType.LOGIN_MSG, msg);
        }
        else
        {
            myClient.Send(MessageType.LOGIN_NEW, msg);
            Debug.Log("new account sent");
            ButtonManager.newLogin = false;
        }
       
	}


    public void AskForLevels(int myId)
    {
        Debug.Log("Asking for level list");
        var msg = new JsonMessage<Request>();
        Request req = LgJsonNode.Create<Request>();
        req.request = "LevelList";
        msg.message = req.Serialize();
        myClient.Send(MessageType.REQUEST_LIST, msg);
       
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
            Destroy(gameObject);
		}
	}
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        myClient.RegisterHandler(MessageType.LEVEL_MSG, OnClientReceiveMessage<Level>);
        myClient.RegisterHandler(MessageType.ACKNOWLEDGE, OnClientReceiveMessage<Acknowledgement>);
        myClient.RegisterHandler(MessageType.PLAYER, OnClientReceiveMessage<Player>);
        myClient.RegisterHandler(MessageType.LEVEL_LIST, OnClientReceiveMessage<LevelMetaDataList>);
        myClient.RegisterHandler(MessageType.LEVEL_MSG, OnClientReceiveMessage<Level>);
        //ipAdress.gameObject.SetActive(false);
        //portNumber.gameObject.SetActive(false);
          //GameObject user = Instantiate(password.gameObject);
        //GameObject pass = Instantiate(username.gameObject);ConnectBtn.gameObject.SetActive(false);
      
       
        OnClientSendLogin(0);
        Debug.Log("Connected");
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("Disconnected");
    }
    void OnDestroy()
    {
        if (Application.loadedLevel != 0)
        {
            Application.LoadLevel(0);
        }
    }

    
}

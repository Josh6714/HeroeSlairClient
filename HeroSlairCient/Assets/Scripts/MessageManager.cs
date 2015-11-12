using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using LgOctEngine.CoreClasses;

public static class MessageType
{
	public static short LOGIN_MSG = 1000;
	public static short LOGIN_ACK = 1001;
	public static short LEVEL_MSG = 1010;
}

public static class MessageValue
{
	public static short LOGIN_FAILURE = 0;
	public static short LOGIN_SUCCESS = 1;
}

// Use messages of this type to send ANY JSON formatted message
// Once you pull the string from this object, you can then
// decode it based on the message type

interface IJsonable
{
	void HandleNewObject();
}

public class JsonMessage<T> : MessageBase
{
	public string message;
}

public class LevelObject : LgJsonDictionary, IJsonable
{
	public string id { get { return GetValue<string>("id", ""); } set { SetValue<string>("id", value); } }       // The unique string identifier that corresponds to the Prefab to load
	public int row { get { return GetValue<int>("row", 0); } set { SetValue<int>("row", value); } }             // The row of the "grid" that the object occupies
	public int column { get { return GetValue<int>("column", 0); } set { SetValue<int>("column", value); } }      // The column of the "grid" that the object occupies
	public float rotation { get { return GetValue<float>("rotation", 0); } set { SetValue<float>("rotation", value); } }  // The rotation of the object, in degrees, in a clockwise manner.  A zero rotation would be "upright".
	public int status { get { return GetValue<int>("status", 0); } set { SetValue<int>("status", value); } }     // The status of the object.

	public void HandleNewObject()
	{
		Debug.Log("Handling LevelObject");
		// TODO: put code that does something with this object
	}
}

public class Level : LgJsonDictionary, IJsonable
{
	public LgJsonArray<LevelObject> LevelObjectArray
	{
		get { return GetNode<LgJsonArray<LevelObject>>("Level"); }
		set { SetNode<LgJsonArray<LevelObject>>("Level", value); }
	}

	public void HandleNewObject()
	{
		Debug.Log("Handling Level");
		// TODO: put code that does something with this object
	}
}

public class Login : LgJsonDictionary, IJsonable
{
	public string username { get { return GetValue<string>("username", ""); } set { SetValue<string>("username", value); } }
	public string password { get { return GetValue<string>("password", ""); } set { SetValue<string>("password", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling Login");
		// TODO: put code that does something with this object
	}
}

public class Acknowledgement : LgJsonDictionary, IJsonable
{
	public int ack { get { return GetValue<int>("ack", 0); } set { SetValue<int>("ack", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling Acknowledgement");
		// TODO: put code that does something with this object
	}
}

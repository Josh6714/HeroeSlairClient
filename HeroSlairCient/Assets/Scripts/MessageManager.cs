using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using LgOctEngine.CoreClasses;
using UnityEngine.UI;

// Types of messages that can be passed
public static class MessageType
{
	// Login Message and Response
	public static short LOGIN_MSG = 1000; // Login w/username, pword
	public static short LOGIN_NEW = 1001; // New user w/pword

	// Request for data
	public static short REQUEST_LIST = 1010; // Request level list
	public static short REQUEST_LEVEL = 1011; // Request level list
	public static short REQUEST_ECONOMY = 1012; // Request economy data
	public static short REQUEST_PLAYER = 1013; // Request player data

	// Acknowledge of message
	public static short ACKNOWLEDGE = 1020; // Success/Failure Ack

	// Level Management and Responses
	public static short LEVEL_LIST = 1030; // List of levels
	public static short LEVEL_MSG = 1031;  // Individual level

	// Update Player Level Data
	public static short LEVEL_COMPLETE = 1040; // Level completed
	public static short LEVEL_FAVORITE = 1041; // Favorite a level

	// Update the Economy
	public static short ECONOMY_UPDATE = 1050; // Update the economy

	// Update the Player data
	public static short PLAYER = 1060; // Player data

	// Error message
	public static short ERROR = 1111; // Any errors
}

// Constant values passed across the network
public static class MessageValue
{
	public static short FAILURE = 0;
	public static short SUCCESS = 1;
}

// Constant values for economy non-prefabs
public static class EconomyValue
{
	public static string LEVEL_COMPLETE = "COMPLETE";
	public static string IMPROVE_STANDING = "IMPROVE";
}

// Use messages of this type to send ANY JSON formatted message
// Once you pull the string from this object, you can then
// decode it based on the message type

// A class that can be converted into a Json object,
// is Jsonable and must be handled in some way
interface IJsonable
{
	void HandleNewObject();
}

// Unity-formatted Json message to send between client and server
// The use of templates means we can use this for all message types
public class JsonMessage<T> : MessageBase
{
	public string message;
}

// Client message to server that includes login information
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

// Client requests data from the server
public class Request : LgJsonDictionary, IJsonable
{
	public string request { get { return GetValue<string>("request", ""); } set { SetValue<string>("request", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling Request");
		// TODO: put code that does something with this object
	}
}

// Server or Client Acknowledges previous message
public class Acknowledgement : LgJsonDictionary, IJsonable
{
	public int ack { get { return GetValue<int>("ack", 0); } set { SetValue<int>("ack", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling Acknowledgement");
		// TODO: put code that does something with this object
        if (ack == MessageValue.SUCCESS)
        {
            Debug.Log("login is bueno");
            Application.LoadLevel(1);
        }
        else if (ack == MessageValue.FAILURE)
        {
            Debug.Log("Login is no bueno");
        }
	}
}

// An object within a level
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

// An entire level or collection of level objects
public class Level : LgJsonDictionary, IJsonable
{
	public string title { get { return GetValue<string>("title", ""); } set { SetValue<string>("title", value); } }

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

// A list of levelMetaData objects to be displayed to user
public class LevelMetaDataList : LgJsonDictionary, IJsonable
{
    public GameObject BuildLevellist(string title, string author, string time, string player, string rating)
    {
        GameObject blah = GameObject.Instantiate(Resources.Load("Content") as GameObject);
        blah.transform.parent = GameObject.Find("Viewport").transform;
        Text showName = blah.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        Text showAuthor = blah.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        Text showTime = blah.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        Text showPlayer = blah.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        Text showRating = blah.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();

        showName.text = title;
        showAuthor.text = author;
        showTime.text = time;
        showPlayer.text = player;
        showRating.text = rating;

        return blah;

    }
	public LgJsonArray<LevelMetaData> LevelMetaDataArray
	{
		get { return GetNode<LgJsonArray<LevelMetaData>>("LevelMetaDataList"); }
		set { SetNode<LgJsonArray<LevelMetaData>>("LevelMetaDataList", value); }
	}

	public void HandleNewObject()
	{
		Debug.Log("Handling Level");
		// TODO: put code that does something with this object

        for (int i = 0; i < LevelMetaDataArray.Count; i++)
        {
            BuildLevellist(LevelMetaDataArray[i].title, LevelMetaDataArray[i].author, LevelMetaDataArray[i].time.ToString(), LevelMetaDataArray[i].player, LevelMetaDataArray[i].rating.ToString());
        }
	}
}

// The metaData for a single level
public class LevelMetaData : LgJsonDictionary, IJsonable
{
	public string title { get { return GetValue<string>("title", ""); } set { SetValue<string>("title", value); } }
	public string author { get { return GetValue<string>("author", ""); } set { SetValue<string>("author", value); } }
	public int time { get { return GetValue<int>("time", 0); } set { SetValue<int>("time", value); } }
	public string player { get { return GetValue<string>("player", ""); } set { SetValue<string>("player", value); } }
	public float rating { get { return GetValue<float>("rating", 0); } set { SetValue<float>("rating", value); } }

	// Player-specific data
	public bool isFav { get { return GetValue<bool>("isFav", false); } set { SetValue<bool>("isFav", value); } }
	public int player_time { get { return GetValue<int>("player_time", 0); } set { SetValue<int>("player_time", value); } }
	public float player_rating { get { return GetValue<float>("player_rating", 0); } set { SetValue<float>("player_rating", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling LevelMetaData");
		// TODO: put code that does something with this object
	}
}

// The player's data for a single level used when a player completes
// a level.  Used to store player-specific data for that level
public class PlayerLevelData : LgJsonDictionary, IJsonable
{
	public string title { get { return GetValue<string>("title", ""); } set { SetValue<string>("title", value); } }
	public bool isFav { get { return GetValue<bool>("isFav", false); } set { SetValue<bool>("isFav", value); } }
	public int time { get { return GetValue<int>("time", 0); } set { SetValue<int>("time", value); } }
	public float rating { get { return GetValue<float>("rating", 0); } set { SetValue<float>("rating", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling LevelMetaData");
		// TODO: put code that does something with this object
	}
}

// Client requests data from the server
public class FavoriteLevel : LgJsonDictionary, IJsonable
{
	public string title { get { return GetValue<string>("title", ""); } set { SetValue<string>("title", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling FavoriteLevel");
		// TODO: put code that does something with this object
	}
}

// An object within the economy
public class EconomyObject : LgJsonDictionary, IJsonable
{
	public string id { get { return GetValue<string>("id", ""); } set { SetValue<string>("id", value); } } 
	public int credits { get { return GetValue<int>("credits", 0); } set { SetValue<int>("credits", value); } }        

	public void HandleNewObject()
	{
		Debug.Log("Handling EconomyObject");
		// TODO: put code that does something with this object
	}
}

// An entire level or collection of level objects
public class Economy : LgJsonDictionary, IJsonable
{
	public LgJsonArray<EconomyObject> EconomyObjectArray
	{
		get { return GetNode<LgJsonArray<EconomyObject>>("Economy"); }
		set { SetNode<LgJsonArray<EconomyObject>>("Economy", value); }
	}

	public void HandleNewObject()
	{
		Debug.Log("Handling Economy");
		// TODO: put code that does something with this object
	}
}

// Player data
public class Player : LgJsonDictionary, IJsonable
{
	public int credits { get { return GetValue<int>("credits", 0); } set { SetValue<int>("credits", value); } }
	public int played { get { return GetValue<int>("played", 0); } set { SetValue<int>("played", value); } }
	public int beaten { get { return GetValue<int>("beaten", 0); } set { SetValue<int>("beaten", value); } }
	public int created { get { return GetValue<int>("created", 0); } set { SetValue<int>("created", value); } }

	public void HandleNewObject()
	{
		Debug.Log("Handling Player");
		// TODO: put code that does something with this object
	}

   
}

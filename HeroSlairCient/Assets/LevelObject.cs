#pragma warning disable 0219 // unused assignment
#pragma warning disable 0168 // assigned not used
#pragma warning disable 0414 // unused variables

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// </commonheader>

using LgOctEngine.CoreClasses;

/// <summary>
/// A simple test class for the LgBaseClass functionality. You can run this script by attaching it to an object in the scene, and hitting play.
/// </summary>
/// 
public class LevelObject : MonoBehaviour
{
    //public static GameObject metalWall = Resources.Load("metalWall") as GameObject;
    //public static GameObject battery = Resources.Load("Battery (1)") as GameObject;
    //public static GameObject woodenCrate = Resources.Load("WoodenCrate") as GameObject;
    //public static GameObject plasticContainter = Resources.Load("PlasticContainer") as GameObject;
    //public static GameObject doorEnter = Resources.Load("DoorEnter") as GameObject;
    //public static GameObject doorExit = Resources.Load("DoorExit") as GameObject;
    //public static GameObject metalBarrel = Resources.Load("MetalBarrel") as GameObject;public static GameObject metalWall = Resources.Load("metalWall") as GameObject;
    public static GameObject metalWall;
    public static GameObject battery;
    public static GameObject woodenCrate;
    public static GameObject plasticContainter;
    public static GameObject doorEnter;
    public static GameObject doorExit;
    public static GameObject metalBarrel;

    SendLevel sendLevel;
    public static bool startSend = false;
    // Use this for initialization
    void Start()
    {
    metalWall = Resources.Load("metalWall") as GameObject;
    battery = Resources.Load("Battery (1)") as GameObject;
    woodenCrate = Resources.Load("WoodenCrate") as GameObject;
    plasticContainter = Resources.Load("PlasticContainer") as GameObject;
    doorEnter = Resources.Load("DoorEnter") as GameObject;
    doorExit = Resources.Load("DoorExit") as GameObject;
    metalBarrel = Resources.Load("MetalBarrel") as GameObject;
        sendLevel = new SendLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (startSend == true)
        {
            //SendLevel.SimpleArrayTest();
            startSend = false;
        }
    }

    //void OnGUI()
    //{
    //    Rect buttonRect = new Rect(30, 30, 200, 45);

    //    if (GUI.Button(buttonRect, "Level Array Test"))
    //    {
    //        SendLevel.SimpleArrayTest();
    //    }
    //}

    public class SendLevel : LgBaseClass
    {
        public enum EnumExample
        {
            None,
            ValueOne,
            ValueTwo
        }
        /// <summary>
        /// A json node of various basic types.
        /// </summary>
        public class LevelObject : LgJsonDictionary
        {
            public string id { get { return GetValue<string>("id", ""); } set { SetValue<string>("id", value); } }       // The unique string identifier that corresponds to the Prefab to load
            public float row { get { return GetValue<float>("row", 0); } set { SetValue<float>("row", value); } }        // The row of the "grid" that the object occupies
            public float column { get { return GetValue<float>("column", 0); } set { SetValue<float>("column", value); } }      // The column of the "grid" that the object occupies
            public float rotation { get { return GetValue<float>("rotation", 0); } set { SetValue<float>("rotation", value); } }  // The rotation of the object, in degrees, in a clockwise manner.  A zero rotation would be "upright".
            public int status { get { return GetValue<int>("status", 0); } set { SetValue<int>("status", value); } }     // The status of the object.  If an object does not 

        }
        /// <summary>
        /// An json node that contains two arrays - one simple, one complex.
        /// </summary>
        public class Level : LgJsonDictionary
        {
            public LgJsonArray<LevelObject> LevelObjectArray
            {
                get { return GetNode<LgJsonArray<LevelObject>>("Level"); }
                set { SetNode<LgJsonArray<LevelObject>>("Level", value); }
            }
        }

        private static LevelObject CreateLevelObject()
        {
            LevelObject levelObject = LgJsonNode.Create<LevelObject>();

            levelObject.id = "PrefabName";
            levelObject.row = 8;
            levelObject.column = 10;
            levelObject.rotation = 90;
            levelObject.status = 0;

            return levelObject;
        }

        public void SimpleArrayTest(String tomato)
        {
            Level simpleArrayClass = LgJsonNode.Create<Level>();
            string serialized = tomato;
            // Deserialize it
            Level simpleArrayClassDeserialized = LgJsonNode.CreateFromJsonString<Level>(serialized);
            for (int i = 0; i < simpleArrayClassDeserialized.LevelObjectArray.Count; i++)
            {
                switch (simpleArrayClassDeserialized.LevelObjectArray[i].id)
                {
                    case "MetalWall":
                        Instantiate(metalWall, new Vector3(simpleArrayClassDeserialized.LevelObjectArray[i].row, simpleArrayClassDeserialized.LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, simpleArrayClassDeserialized.LevelObjectArray[i].rotation));
                        break;
                    case "Battery":
                        Instantiate(battery, new Vector3(simpleArrayClassDeserialized.LevelObjectArray[i].row,simpleArrayClassDeserialized.LevelObjectArray[i].column,0), Quaternion.Euler(0,0,simpleArrayClassDeserialized.LevelObjectArray[i].rotation));
                        break;
                    case "WoodenCrate":
                        Instantiate(woodenCrate, new Vector3(simpleArrayClassDeserialized.LevelObjectArray[i].row, simpleArrayClassDeserialized.LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, simpleArrayClassDeserialized.LevelObjectArray[i].rotation));
                        break;
                    case "PlasticContainer":
                        Instantiate(plasticContainter, new Vector3(simpleArrayClassDeserialized.LevelObjectArray[i].row, simpleArrayClassDeserialized.LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, simpleArrayClassDeserialized.LevelObjectArray[i].rotation));
                        break;
                    case "DoorEnter":
                        Instantiate(doorEnter, new Vector3(simpleArrayClassDeserialized.LevelObjectArray[i].row, simpleArrayClassDeserialized.LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, simpleArrayClassDeserialized.LevelObjectArray[i].rotation));
                        break;
                    case "DoorExit":
                        Instantiate(doorExit, new Vector3(simpleArrayClassDeserialized.LevelObjectArray[i].row, simpleArrayClassDeserialized.LevelObjectArray[i].column, 0), Quaternion.Euler(0, 0, simpleArrayClassDeserialized.LevelObjectArray[i].rotation));
                        break;
                }
            }
            // Paste the output in www.jsonlint.com to easily view and debug it!
            //Debug.Log("Serialized output: " + serialized);
            Debug.Log("Deserialized output: " + simpleArrayClassDeserialized.Serialize());
        }
    }
}
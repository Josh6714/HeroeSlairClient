using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Spawning : MonoBehaviour
{
    public MessageSender msgSender;
    public GameObject metalWall;
    public GameObject battery;
    public GameObject woodenCrate;
    public GameObject plasticContainter;
    public GameObject doorEnter;
    public GameObject doorExit;
    public GameObject metalBarrel;
    public List<GameObject> levelObj = new List<GameObject>();
    public List<Vector3> vectorss = new List<Vector3>();
    bool ranOnce = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (msgSender.spawn == true)
        {
            LocationToVectors();
            TagsToObjects();
            ranOnce = true;
        }
    }

    void LocationToVectors()
    {
        foreach (string vectorX in msgSender.VectorsX)
        {
            vectorss.Add(new Vector3(float.Parse(vectorX), 0, 0));
        }
        for (int i = 0; i < vectorss.Count; i++)
        {
            vectorss[i] += new Vector3(0, float.Parse(msgSender.VectorsY[i]), 0);
        }
        for (int i = 0; i < vectorss.Count; i++)
        {
            vectorss[i] += new Vector3(0, 0, float.Parse(msgSender.VectorsZ[i]));
        }
    }
    void TagsToObjects()
    {
        Instantiate(metalWall, transform.position, transform.rotation);
        for (int i = 0; i < msgSender.Tags.Count; i++)
        {
            switch (msgSender.Tags[i])
            {
                case "MetalWall":
                    Instantiate(metalWall, vectorss[i], transform.rotation);
                    break;
                case "Battery":
                    Instantiate(battery, vectorss[i], transform.rotation);
                    break;
                case "WoodenCrate":
                    Instantiate(woodenCrate, vectorss[i], transform.rotation);
                    break;
                case "PlasticContainer":
                    Instantiate(plasticContainter, vectorss[i], transform.rotation);
                    break;
                case "DoorEnter":
                    Instantiate(doorEnter, vectorss[i], transform.rotation);
                    break;
                case "DoorExit":
                    Instantiate(doorExit, vectorss[i], transform.rotation);
                    break;
            }

            msgSender.spawn = false;
        }
    }

}

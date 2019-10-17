using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTester : MonoBehaviour
{
    private const string BOMB_SOUND = "ExpandModel";
    private const string CUBE_TAG = "cube";
    private const string HOLOGRAM_COLLECTION_NAME = "Hologram Collection";

    private GameObject collidedWith;
    private System.Random rand;
    private System.Random rSgn;

    void Start()
    {
        rand = new System.Random();
        rSgn = new System.Random();
    }
    
    void OnCollisionEnter(Collision col)
    {
        collidedWith = col.gameObject;

        if (collidedWith.GetComponent<AudioSource>() != null && collidedWith.GetComponent<AudioSource>().isPlaying == true)
        {
            Destroy(collidedWith);
            KeepScore.addScore();
            CreateNewCube();
        }
        else
        {
            KeepScore.minusKills();
        }
    }

    private void CreateNewCube()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.tag = CUBE_TAG;
        var allObjects = FindObjectsOfType<GameObject>();

        //postavljanje roditelja
        foreach (GameObject go in allObjects)
        {
            if (go.name.Equals(HOLOGRAM_COLLECTION_NAME))
            {
                cube.transform.parent = go.transform;
            }
        }

        cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().isKinematic = false;
        cube.GetComponent<Rigidbody>().useGravity = false;
        cube.GetComponent<Rigidbody>().mass = 0.01f;
        cube.GetComponent<Rigidbody>().drag = 1000f;
        cube.GetComponent<Rigidbody>().freezeRotation = true;

        cube.AddComponent<AudioSource>();
        cube.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(BOMB_SOUND);
        cube.GetComponent<AudioSource>().playOnAwake = false;
        cube.GetComponent<AudioSource>().loop = true;
        cube.GetComponent<AudioSource>().spatialBlend = 1f;
        cube.GetComponent<AudioSource>().dopplerLevel = 0f;

        cube.AddComponent<ChangeColor>();

        float x = ((float)rand.NextDouble() * 2 + 1) * findSgn();
        float y = ((float)rand.NextDouble() * 2 + 1) * findSgn();
        float z = ((float)rand.NextDouble() * 2 + 1) * findSgn();

        //creates position between 2.0 and 4.0 meters
        cube.transform.localPosition = new Vector3(x, y, z);

        //reinstanciates default position variable in ChangeColor script
        foreach (GameObject go in allObjects)
        {
            if (go.transform.parent != null && go.transform.parent.name.Equals(HOLOGRAM_COLLECTION_NAME))
            {
                go.GetComponent<ChangeColor>().reinstanciatePosition();
            }
        }
    }

    int findSgn()
    {
        int sgn;
        switch (rSgn.Next(0, 2))
        {
            case 0:
                sgn = -1;
                break;
            case 1:
                sgn = 1;
                break;
            default:
                sgn = 1;
                break;
        }
        return sgn;
    }

}

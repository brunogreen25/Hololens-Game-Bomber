using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScore : MonoBehaviour
{
    private const string LOSER_SOUND = "GSharp";
    private const string VICTORY_SOUND = "sound_Success";
    private const string GAME_OVER_TEXT = "Game Over";
    private const string CUBE_TAG = "cube";
    private const string GAME_OVER_TAG = "game_over";
    private const string SCENE_OBJECTS_TAG = "scene_object";
    private const int DEFAULT_LIFE_COUNT = 3;

    private static int Lives = DEFAULT_LIFE_COUNT;
    private static int Score = 0;

    private bool EasterEggUsed = false;

    #region SINGLETON_IMPLEMENTATION
    static KeepScore Instance;
    void Awake()
    {
        Instance = this;
    }
    public static KeepScore GetInstance()
    {
        return Instance;
    }
    #endregion

    void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        this.GetComponent<TextMesh>().text = "LIVES: \n" + Lives + "\nSCORE:\n" + Score;
    }

    void Update()
    {
        var gazeOrigin = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        this.transform.position = new Vector3(gazeOrigin.x + 4 * gazeDirection.x, gazeOrigin.y + 4 * gazeDirection.y, gazeOrigin.z + 4 * gazeDirection.z);
        this.transform.forward = Camera.main.transform.forward;
        
        if (Lives == 0)
        {
            CreateGameOverObject();
            DestroySceneObjects();
        }
    }

    private void DestroySceneObjects()
    {
        var sceneObjects = GameObject.FindGameObjectsWithTag(SCENE_OBJECTS_TAG);
        for (var i = 0; i < sceneObjects.Length; i++)
        {
            Destroy(sceneObjects[i]);
        }
        var cubeObjects = GameObject.FindGameObjectsWithTag(CUBE_TAG);
        for (var i = 0; i < sceneObjects.Length; i++)
        {
            Destroy(sceneObjects[i]);
        }
    }

    private void CreateGameOverObject()
    {
        var goObject = new GameObject();
        goObject.tag = GAME_OVER_TAG;

        goObject.AddComponent<TextMesh>();
        goObject.GetComponent<TextMesh>().text = GAME_OVER_TEXT;

        var gazeOrigin = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        goObject.transform.position = new Vector3(gazeOrigin.x + 20 * gazeDirection.x, gazeOrigin.y + 20 * gazeDirection.y, gazeOrigin.z + 20 * gazeDirection.z);
        goObject.transform.forward = Camera.main.transform.forward;
    }

    public static void addScore()
    {
        Score += 1;

        KeepScore textChanger = KeepScore.GetInstance();
        textChanger.ChangeText();

        PlaySound(VICTORY_SOUND);
    }

    public static void minusKills()
    {
        Lives -= 1;

        KeepScore textChanger = KeepScore.GetInstance();
        textChanger.ChangeText();

        PlaySound(LOSER_SOUND);
    }

    public void EasterEgg()
    {
        if (!this.EasterEggUsed)
        {
            Lives++;
            this.EasterEggUsed = true;
            this.ChangeText();
        }
    }

    private static void PlaySound(string soundName)
    {
        var audioSource = KeepScore.GetInstance().GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(soundName);
        audioSource.Play();
    }
}

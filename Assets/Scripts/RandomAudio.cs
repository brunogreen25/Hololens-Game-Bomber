using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    private AudioSource[] childrenAudios;
    private System.Random rand;

    private bool musicIsOn = false;

    void Start()
    {
        rand = new System.Random();
    }

    void LateUpdate()
    {
        musicIsOn = false;
        childrenAudios = GetComponentsInChildren<AudioSource>();
        int theOne = rand.Next(0, childrenAudios.Length);

        //check if every audio source is turned off and if it is, activate a new one
        foreach (AudioSource audioSrc in childrenAudios)
        {
            if (audioSrc.isPlaying == true)
            {
                musicIsOn = true;
            }
        }

        if (!musicIsOn)
        {
            for (int i = 0; i < childrenAudios.Length; i++)
            {
                if (i == theOne)
                {
                    childrenAudios[i].Play();
                    break;
                }
            }
        }


    }
}

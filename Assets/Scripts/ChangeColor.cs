using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private AudioSource myAudio;
    private MeshRenderer rend;

    public Vector3 position { get; set; }

    private bool theOne = false;

    void Awake()
    {
        this.myAudio = GetComponent<AudioSource>();
        this.rend = GetComponent<MeshRenderer>();
        this.rend.enabled = true;

        this.position = this.transform.position;
    }

    void LateUpdate()
    {
        if (myAudio != null && !myAudio.isPlaying && position != this.transform.position)
        {
            rend.material.color = Color.red;
            theOne = true;
        }
        if (this.transform.position == position)
        {
            rend.material.color = Color.white;
        }
    }

    public void reinstanciatePosition()
    {
        position = this.transform.position;
    }
}

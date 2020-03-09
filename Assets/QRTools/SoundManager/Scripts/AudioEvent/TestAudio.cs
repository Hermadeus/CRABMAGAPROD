using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Audio;

public class TestAudio : MonoBehaviour
{
    public MultipleAudioEvent evnt;
    public AudioSource source;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //evnt.Play(source);
        }
    }
}

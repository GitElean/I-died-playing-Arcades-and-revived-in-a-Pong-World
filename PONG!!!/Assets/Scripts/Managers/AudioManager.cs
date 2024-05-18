using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioGunshot; 
    public AudioClip audioStep;
    public AudioClip audioJump;
    public AudioClip audioHit;
    public AudioClip audioGrab;
    public AudioClip audioFlashlight;
    public AudioClip audioEngine;
    public AudioClip audioEngineStart;

    //public Slider volume;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGunshot()
    {
        audio.PlayOneShot(audioGunshot);
    }
    public void PlayStep()
    {
        audio.PlayOneShot(audioStep);
    }
    public void PlayJump()
    {
        audio.PlayOneShot(audioJump);
    }
    public void PlayHit()
    {
        audio.PlayOneShot(audioHit);
    }
    public void PlayGrab()
    {
        audio.PlayOneShot(audioGrab);
    }
    public void PlayFlashlight()
    {
        audio.PlayOneShot(audioFlashlight);
    }
    public void PlayEngine()
    {
        audio.PlayOneShot(audioEngine);
    }
    public void PlayEngineStart()
    {
        audio.PlayOneShot(audioEngineStart);
    }

    /* void OnGUI()
    {
        audio.volume = volume.value;
    } */
}

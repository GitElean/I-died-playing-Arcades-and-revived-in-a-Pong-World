using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource mainMenu;
    public Slider volume;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GetComponent<AudioSource>();
        mainMenu.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        mainMenu.volume = volume.value;
    }
}

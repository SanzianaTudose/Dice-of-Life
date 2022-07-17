using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource BGM;
    public AudioClip mainTheme;
    public AudioClip endTheme;
    public AudioClip goodEndMusic; 
    public AudioClip badEndMusic;



    void Start()
    {
        BGM.clip = mainTheme;
    }

    public void ChangeBGM(AudioClip music)
    {
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
        Debug.Log("music changed");
    }
}

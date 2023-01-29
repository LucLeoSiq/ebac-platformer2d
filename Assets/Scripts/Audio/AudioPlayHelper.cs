using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayHelper : MonoBehaviour
{
    [Header("GameOject Setup")]
    public AudioSource audioSource;
    
    public KeyCode keycode = KeyCode.P;
    

    void Update()
    {
        if(Input.GetKeyDown(keycode))
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}

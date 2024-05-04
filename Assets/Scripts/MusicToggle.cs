using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    private AudioSource musicSource;
    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        else
        {
            musicSource.Play();
        }
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }
}

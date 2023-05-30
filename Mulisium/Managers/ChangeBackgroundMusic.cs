using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backgroundMusic;

    private void Start()
    {
        audioSource.loop = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ChangeMusic());
    }

    IEnumerator ChangeMusic()
    {

        if (audioSource.clip.name != backgroundMusic.name)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
        

        yield return null;
    }
}

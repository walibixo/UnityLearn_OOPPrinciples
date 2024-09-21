using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
    }

    public static void PlaySound(AudioClip clip, bool randomPitch = false)
    {
        if (randomPitch)
        {
            instance.audioSource.pitch = (float)Math.Pow(1.059463f, UnityEngine.Random.Range(-6, 6));
        }

        instance.audioSource.PlayOneShot(clip);
    }
}

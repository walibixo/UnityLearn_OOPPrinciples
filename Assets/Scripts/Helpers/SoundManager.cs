using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip clip, bool randomPitch = false, float volumeScale = 1.0f)
    {
        if (_instance._audioSource.isPlaying)
        {
            volumeScale *= 0.2f;
        }

        if (randomPitch)
        {
            _instance._audioSource.pitch = (float)Math.Pow(1.059463f, UnityEngine.Random.Range(-6, 6));
        }


        _instance._audioSource.PlayOneShot(clip, volumeScale);
    }
}

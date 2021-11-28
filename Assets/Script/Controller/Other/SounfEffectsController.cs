using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SounfEffectsController : MonoBehaviour
{
    public static AudioClip soundToPlay;
    private static AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void playSoundEffect(string soundName)
	{
        soundToPlay = Resources.Load<AudioClip>("Audio/SoundEffects/" + soundName);
        audioSource.PlayOneShot(soundToPlay);
	}

    public static void playSoundEffect(string soundName, float volume)
    {
        soundToPlay = Resources.Load<AudioClip>("Audio/SoundEffects/" + soundName);
        audioSource.PlayOneShot(soundToPlay, volume);
    }
}

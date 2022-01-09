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
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/BackgroundMusic/hollow-knight-ost-greenpath"), 0.1f);
    }

    public static void PlaySoundEffect(string soundName)
	{
        soundToPlay = Resources.Load<AudioClip>("Audio/SoundEffects/" + soundName);
        audioSource.PlayOneShot(soundToPlay);
	}

    public static void PlaySoundEffect(string soundName, float volume)
    {
        soundToPlay = Resources.Load<AudioClip>("Audio/SoundEffects/" + soundName);
        audioSource.PlayOneShot(soundToPlay, volume);
    }


    //fonction qui verfie si le son a deja ete joué
}

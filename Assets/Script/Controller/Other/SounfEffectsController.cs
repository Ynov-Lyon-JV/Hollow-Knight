using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SounfEffectsController : MonoBehaviour
{
    public static AudioClip soundToPlay;
    private static AudioSource audioSource;
    private static AudioSource audioSourceWalking;


    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourceWalking = gameObject.AddComponent<AudioSource>(); 
        //audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/BackgroundMusic/hollow-knight-ost-greenpath"), 0.1f);
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

    public static void PlayWalkingSound(string soundName)
	{
        if(!audioSourceWalking.isPlaying)
		{
            soundToPlay = Resources.Load<AudioClip>("Audio/SoundEffects/" + soundName);
            audioSourceWalking.volume = Random.Range(0.15f, 0.3f);
            audioSourceWalking.pitch = Random.Range(0.8f, 1.1f);
            audioSourceWalking.PlayOneShot(soundToPlay);
        }
        
    }


    //fonction qui verfie si le son a deja ete joué
}

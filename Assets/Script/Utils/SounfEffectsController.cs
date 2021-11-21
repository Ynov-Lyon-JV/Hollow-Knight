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

    public static void playSound(string soundName)
	{
        soundToPlay = Resources.Load<AudioClip>(soundName);
        audioSource.PlayOneShot(soundToPlay);
	}
}

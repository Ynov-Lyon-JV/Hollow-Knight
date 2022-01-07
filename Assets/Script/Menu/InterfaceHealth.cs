using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceHealth : MonoBehaviour
{
    public static InterfaceHealth instance;

    public int maxHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    private void Awake()
    {
        if (instance != null) 
        { 
            Debug.LogWarning("Une instance de l'interface de la vie est déjà existante!"); 
            return; 
        }
        instance = this;
    }

    public void Change(int health)
    {

        for(int i =0; i < hearts.Length; i++)
        {
            if(i< health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}

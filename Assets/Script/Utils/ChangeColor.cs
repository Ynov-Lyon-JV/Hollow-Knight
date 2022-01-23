using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    public Color color;
    private Renderer[] Sprites;

    void Awake()
    {
        Sprites = GetComponentsInChildren<Renderer>();

        foreach (Renderer sprite in Sprites)
        {
            if (!sprite == false)
                sprite.material.color = color;
        }
    }
}

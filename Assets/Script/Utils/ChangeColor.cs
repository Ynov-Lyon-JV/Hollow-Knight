using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    public Color color;
    private SpriteRenderer[] Sprites;

    void Awake()
    {
        Sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in Sprites)
        {
            if (!sprite == false)
                sprite.color = color;
        }
    }
}

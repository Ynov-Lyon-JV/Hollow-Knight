using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public bool canFade = false;
    private Renderer[] Sprites;

    [SerializeField]
    private float timeToFadeStart = 1.0f;
    private float timeToFade = 1.0f;

    public void Start()
    {
        timeToFade = timeToFadeStart;
        Sprites = GetComponentsInChildren<Renderer>();
    }
    void FixedUpdate()
    {
        if (Sprites == null)
            return;
        if (canFade)
        {
            foreach(Renderer sprite in Sprites)
            {
                if (!sprite == false)
                    sprite.material.color = Color.Lerp(sprite.material.color, new Color(0, 0, 0, 0), timeToFade * Time.deltaTime);
            }
        }
        if (Sprites[0].material.color.a < 0.001)
        {
            Destroy(gameObject);
        }
        else if (Sprites[0].material.color.a < 0.2)
        {
            timeToFade = timeToFadeStart*10;
        }
        else if (Sprites[0].material.color.a < 0.6)
        {
            timeToFade = timeToFadeStart *2;
        }
    }
}

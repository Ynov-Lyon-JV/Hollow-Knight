using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite spriteToChange;
    public void NewSprite()
    {
        GetComponent<SpriteRenderer>().sprite = spriteToChange;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeLant : MonoBehaviour
{
    public List<string> liste = new List<string>();
    public static ListeLant instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Une instance de ListLanterne est déjà existante!");
            return;
        }
        instance = this;
        
    }

    // Update is called once per frame
    public bool Verif(string check)
    {
        if (liste.Contains(check))
            return true;
        return false;
    }
}

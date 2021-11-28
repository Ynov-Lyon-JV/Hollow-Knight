using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public string lastscene;
    public string lastPos;

    // Start is called before the first frame update
    void Start()
    {
        ControllerSpawn.instance.pos = lastPos;
        SceneManager.LoadScene(lastscene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public string lastscene = "Load";
    public string lastPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("UI").transform.Find("UI_Transition").gameObject.SetActive(true);
        ControllerSpawn.instance.pos = lastPos;
        SceneManager.LoadScene(lastscene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

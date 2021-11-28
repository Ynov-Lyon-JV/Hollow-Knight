using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerDoor : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(loadNextScene());
            ControllerSpawn.instance.pos = this.transform.GetChild(0).name; ;

            SceneManager.LoadScene(scene);
        }
    }


    public IEnumerator loadNextScene()
    {
        //LoadAndSaveData.instance.SaveData();
        //fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        ControllerSpawn.instance.pos = this.transform.GetChild(0).name;

        SceneManager.LoadScene(scene);
    }
}

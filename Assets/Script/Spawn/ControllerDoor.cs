using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerDoor : MonoBehaviour
{

    [SerializeField]
    private string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }


    private IEnumerator LoadNextScene()
    {
        Player.instance.Pause();
        GameObject.Find("UI_Transition").GetComponent<ControllerAnimation>().ChangeAnimationState(Dico.Get("ANIM_TRANSITION_FADEIN"));
        //LoadAndSaveData.instance.SaveData();
        yield return new WaitForSeconds(0.5f);
        ControllerSpawn.instance.pos = this.transform.GetChild(0).name;
        SceneManager.LoadScene(scene);
    }
}

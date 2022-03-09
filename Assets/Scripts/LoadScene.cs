using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading() 
    {
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadSceneAsync(LoadSceneData.sceneToLoad);
        yield return null;
    }
}

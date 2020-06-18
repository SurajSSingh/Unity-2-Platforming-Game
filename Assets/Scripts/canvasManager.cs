using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasManager : MonoBehaviour
{
    public GameObject fadingPanel;

    private void Start()
    {
        fadingPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetLevel()
    {
        StartCoroutine(FadeEffect(SceneManager.GetActiveScene().buildIndex));
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        StartCoroutine(FadeEffect(SceneManager.GetActiveScene().buildIndex+1));
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    IEnumerator FadeEffect(int sceneNumber)
    {
        fadingPanel.SetActive(true);
        for(int i = 0; i < 100; i++)
        {
            fadingPanel.GetComponent<CanvasGroup>().alpha = (float)(i*0.01f);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        SceneManager.LoadScene(sceneNumber);
    }
}

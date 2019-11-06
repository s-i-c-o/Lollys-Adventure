using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{

    private Image fadeBox;

    // Start is called before the first frame update
    void Start()
    {
        fadeBox = GameObject.Find("FadeBox").GetComponent<Image>();
        fadeBox.canvasRenderer.SetAlpha(1);
    }

    private void Awake()
    {
        StartCoroutine(WaitThenFadeIn());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelOne()
    {
        Debug.Log("load level one");
        SceneController.Instance.currentLevel = 1;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadLevelOne());
    }

    public void LoadLevelTwo()
    {
        SceneController.Instance.currentLevel = 2;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadLevelTwo());
    }

    public void LoadLevelThree()
    {
        SceneController.Instance.currentLevel = 3;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadLevelThree());
    }

    public void LoadLevelFour()
    {
        SceneController.Instance.currentLevel = 4;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadLevelFour());
    }

    public void LoadLevelFive()
    {
        SceneController.Instance.currentLevel = 5;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadLevelFive());
    }

    public void LoadMainMenu()
    {
        SceneController.Instance.currentLevel = 0;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadMainMenu());
    }
    public void LoadGameOver()
    {
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadGameOver());
    }

    public void LoadTheEnd()
    {
        SceneController.Instance.currentLevel = 0;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadTheEnd());
    }

    public void RestartCurrentLevel()
    {
        Debug.Log("Loading: " + SceneController.Instance.levelNames[SceneController.Instance.currentLevel]);
        StartCoroutine(FadeOut());
        StartCoroutine(WaitLoadRestartLevel());
    }

    IEnumerator FadeIn()
    {
        fadeBox.CrossFadeAlpha(0.0f, 0.5f, true);
        yield return null;
    }

    IEnumerator FadeOut()
    {
        Debug.Log("Fading out");
        fadeBox.CrossFadeAlpha(1.0f, 0.5f, true);
        yield return null;
    }

    IEnumerator WaitThenFadeIn()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FadeIn());
    }

    IEnumerator WaitLoadLevelOne()
    {
        Debug.Log("LOAD LEVEL ONE");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[1]);
    }

    IEnumerator WaitLoadLevelTwo()
    {
        Debug.Log("LOAD LEVEL 2");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[2]);
    }

    IEnumerator WaitLoadLevelThree()
    {
        Debug.Log("LOAD LEVEL 3");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[3]);
    }

    IEnumerator WaitLoadLevelFour()
    {
        Debug.Log("LOAD LEVEL 4");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[4]);
    }

    IEnumerator WaitLoadLevelFive()
    {
        Debug.Log("LOAD LEVEL 5");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[5]);
    }

    IEnumerator WaitLoadMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[0]);
    }

    IEnumerator WaitLoadGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[6]);
    }

    IEnumerator WaitLoadTheEnd()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("TheEnd");
    }

    IEnumerator WaitLoadRestartLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneController.Instance.levelNames[SceneController.Instance.currentLevel]);
    }
}

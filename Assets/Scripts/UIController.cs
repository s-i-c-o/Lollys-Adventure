using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Level controller
    private LevelController levelController;
    private int playerLives;

    // UI Stuff
    private Image heart1;
    private Image heart2;
    private Image heart3;
    private Image heart4;
    private Image heart5;

    private Image levelImage;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.Find("LevelController").transform.GetComponent<LevelController>();

        heart1 = GameObject.Find("Life1").transform.GetComponent<Image>();
        heart2 = GameObject.Find("Life2").transform.GetComponent<Image>();
        heart3 = GameObject.Find("Life3").transform.GetComponent<Image>();
        heart4 = GameObject.Find("Life4").transform.GetComponent<Image>();
        heart5 = GameObject.Find("Life5").transform.GetComponent<Image>();

        levelImage = GameObject.Find("LevelImg").transform.GetComponent<Image>();
        levelImage.canvasRenderer.SetAlpha(0);
    }

    private void Awake()
    {
        StartCoroutine(WaitForSomeTime());
    }

    // Update is called once per frame
    void Update()
    {
        playerLives = levelController.GetPlayerLives();
        if (playerLives < 5)
        {
            if (playerLives == 4)
            {
                heart5.gameObject.SetActive(false);
            }
            if (playerLives == 3)
            {
                heart4.gameObject.SetActive(false);
            }
            if (playerLives == 2)
            {
                heart3.gameObject.SetActive(false);
            }
            if (playerLives == 1)
            {
                heart2.gameObject.SetActive(false);
            }

        }
    }

    IEnumerator FadeLevelImage()
    {
        levelImage.CrossFadeAlpha(1.0f, 0.8f, false);
        yield return new WaitForSeconds(4.0f);
        levelImage.CrossFadeAlpha(0.0f, 0.8f, false);
    }

    IEnumerator WaitForSomeTime()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FadeLevelImage());
    }
}

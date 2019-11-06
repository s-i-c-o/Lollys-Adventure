using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private PlayerPlatformerController playerController;
    private SceneSwitcher sceneSwitcher;
    protected int playerLives;

    public GameObject rickyPrefab;
    public GameObject dogButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerPlatformerController>();
        playerLives = playerController.GetLives();
        Debug.Log("Player lives: " + playerLives);
        sceneSwitcher = GameObject.Find("SceneSwitcher").transform.GetComponent<SceneSwitcher>();

        if (PlayerStats.Instance.bHasRicky)
        {
            Instantiate(dogButtonPrefab, GameObject.Find("Canvas").transform);
            Instantiate(rickyPrefab);
        }

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playerLives = playerController.GetLives();
        if (playerLives == 0)
        {
            Time.timeScale = 0.2f;
            sceneSwitcher.LoadGameOver();
        }
    }

    public int GetPlayerLives()
    {
        return playerLives;
    }

    public void SpawnRicky()
    {
        PlayerStats.Instance.bHasRicky = true;
        Instantiate(dogButtonPrefab, GameObject.Find("Canvas").transform);
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerPos.x = playerPos.x + 30.0f;
        playerPos.y = playerPos.y - 0.7f;
        Instantiate(rickyPrefab, playerPos, Quaternion.identity);
    }
}

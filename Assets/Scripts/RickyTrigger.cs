using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickyTrigger : MonoBehaviour
{
    private LevelController lc;
    private AudioSource audioSource;

    public GameObject coltonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lc.SpawnRicky();
        audioSource.Play();
        Instantiate(coltonPrefab);
        Destroy(GetComponent<BoxCollider2D>());
    }
}

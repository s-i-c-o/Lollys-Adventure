using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public string NextLevelName;
    public SceneSwitcher ss;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (NextLevelName == "Level2")
        {
            ss.LoadLevelTwo();
        }
        else if (NextLevelName == "Level3")
        {
            ss.LoadLevelThree();
        }
        else if (NextLevelName == "Level4")
        {
            ss.LoadLevelFour();
        }
        else if (NextLevelName == "Level5")
        {
            ss.LoadLevelFive();
        }
        else if (NextLevelName == "TheEnd")
        {
            ss.LoadTheEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minPosition = 3.5f;
    public float maxPosition = 10000.0f;
    public Transform player;
    private Vector2 playerPos;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(player.position.x, player.position.y);
        Vector3 clampedPos = new Vector3(playerPos.x, playerPos.y, 0.0f);
        clampedPos += offset;
        clampedPos.y = Mathf.Clamp(clampedPos.y, 2.0f, 100.0f);
        clampedPos.x = Mathf.Clamp(clampedPos.x, minPosition, 10000.0f);
        transform.position = clampedPos;
    }
}

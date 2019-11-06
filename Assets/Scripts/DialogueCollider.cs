using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCollider : MonoBehaviour
{
    private DialogueTrigger dt;

    // Start is called before the first frame update
    void Start()
    {
        dt = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dt != null)
        {
            dt.TriggerDialogue();
            Destroy(gameObject);
        }
    }
}

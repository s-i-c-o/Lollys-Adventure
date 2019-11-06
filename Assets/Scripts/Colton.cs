using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colton : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation()
    {
        anim.SetBool("bInConversation", true);
    }

    public void EndConversation()
    {
        StartCoroutine(EndConversationRoutine());
    }

    IEnumerator EndConversationRoutine()
    {
        anim.SetBool("bInConversation", false);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueController : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public Animator animator;

    private Colton colton;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
    }       

    public void StartDialogue(Dialogue dialogue)
    {
        colton = GameObject.FindGameObjectWithTag("ConversationStarter").GetComponent<Colton>();
        Time.timeScale = 0.0f;
        animator.SetBool("isOpen", true);
        colton.StartConversation();
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        colton.EndConversation();
        Time.timeScale = 1.0f;
    }
}

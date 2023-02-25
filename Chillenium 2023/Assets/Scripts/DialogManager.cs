using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public Text nameText, dialogText;
    private Queue<string> sentences;
    public Animator animator;

    // Start is called before the first frame update
    void Start() {
        Animator animator = new Animator();
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog) {
        animator.SetBool("IsOpen", true);
        nameText.text = dialog.name;
        sentences.Clear();
        foreach (string sentence in dialog.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialog();
        }
        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    public void EndDialog() {
        animator.SetBool("IsOpen", false);
    }
}

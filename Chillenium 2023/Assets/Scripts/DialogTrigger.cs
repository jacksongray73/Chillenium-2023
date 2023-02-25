using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {
    [SerializeField] string triggerTag;
    public Dialog dialog;
    public void TriggerDialog() {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(triggerTag)) {
            TriggerDialog();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag(triggerTag)) {
            FindObjectOfType<DialogManager>().EndDialog();
        }
    }
}

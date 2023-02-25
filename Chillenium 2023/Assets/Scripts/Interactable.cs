using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField] string interactTag;
    [SerializeField] GameObject _speechBubble;
    private GameObject _currentInteraction;
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(interactTag)) {
            _currentInteraction = collision.gameObject;
            //Pop up speech bubble
            Vector3 position = transform.position;
            position.y += 1;
            _speechBubble.transform.position = position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(interactTag)) {
            if (collision.gameObject == _currentInteraction) {
                _currentInteraction = null;
                //Remove speech bubble (moves it offscreen)
                _speechBubble.transform.position = new Vector3(-10, -10, 0);
            }
        }
    }
}

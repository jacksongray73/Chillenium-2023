using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField] string interactTag;
    [SerializeField] GameObject _speechBubble;
    private float _speechBubbleHeight, _speechBubbleSpeed;
    private bool _showingBubble;
    private GameObject _currentInteraction;
    void Start() {
        _speechBubbleHeight = 1;
        _speechBubbleSpeed = 10;
    }

    void Update() {
        if (_showingBubble) {
            Vector3 bubblePos = _speechBubble.transform.position;
            Vector3 position = transform.position;
            if (bubblePos.y < transform.position.y + _speechBubbleHeight) {
                bubblePos.y += _speechBubbleSpeed * Time.deltaTime;
                if (bubblePos.y > position.y + _speechBubbleHeight) {
                    bubblePos.y = position.y + _speechBubbleHeight;
                }
                _speechBubble.transform.position = bubblePos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(interactTag)) {
            _currentInteraction = collision.gameObject;
            //Pop up speech bubble
            Vector3 position = transform.position;
            position.y += 0;
            _speechBubble.transform.position = position;
            _showingBubble = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(interactTag)) {
            if (collision.gameObject == _currentInteraction) {
                _currentInteraction = null;
                //Remove speech bubble (moves it offscreen)
                _speechBubble.transform.position = new Vector3(-10, -10, 0);
                _showingBubble = false;
            }
        }
    }
}

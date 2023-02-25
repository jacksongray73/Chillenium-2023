using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour {
    private GameObject _currentInventor;
    public bool unlocked = false;

    void Update() {
        if (_currentInventor != null) {
            if (_currentInventor.GetComponent<Inventor>().inputs["Interact"]) {
                unlocked = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            _currentInventor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Door")) {
            if (collision.gameObject == _currentInventor) {
                _currentInventor = null;
            }
        }
    }
}

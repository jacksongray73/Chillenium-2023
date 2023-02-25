using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour {
    private GameObject _currentInventor;
    public bool locked = true;

    void Update() {
        if (CompareTag("Panel")) {
            if (_currentInventor != null) {
                if (_currentInventor.GetComponent<Inventor>().inputs["Interact"]) {
                    locked = false;
                }
            }
        }
        else if (CompareTag("Button")) {
            if (_currentInventor != null) {
                locked = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            _currentInventor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            if (collision.gameObject == _currentInventor) {
                _currentInventor = null;
            }
        }
    }
}

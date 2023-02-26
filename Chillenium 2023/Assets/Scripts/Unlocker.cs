using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour {
    
    private GameObject _currentInventor, _currentRobot;
    public bool locked = true;
    [SerializeField] public Sprite unpressedButton, pressedButton; 

    void Update() {
        if (CompareTag("Panel")) {
            if (_currentInventor != null) {
                if (_currentInventor.GetComponent<Inventor>().inputs["Interact"]) {
                    locked = false;
                }
            }
        }
        else if (CompareTag("Button")) {
            if (_currentRobot != null) {
                locked = false;
                GetComponent<SpriteRenderer>().sprite = pressedButton;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            _currentInventor = collision.gameObject;
        }
        if (collision.CompareTag("Robot")) {
            _currentRobot = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            if (collision.gameObject == _currentInventor) {
                _currentInventor = null;
            }
        }
        if (collision.CompareTag("Robot")) {
            if (collision.gameObject == _currentRobot) {
                _currentRobot = null;
            }
        }
    }
}

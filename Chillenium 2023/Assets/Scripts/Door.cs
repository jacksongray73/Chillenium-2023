using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] public Door companionDoor;
    [SerializeField] public Unlocker unlocker;
    public bool locked;
    private GameObject _currentRobot;

    [SerializeField] public Sprite closedSmallDoor, openSmallDoor; 

    void Start() {
        locked = unlocker != null;
    }
    // Update is called once per frame
    void Update() {
        if (unlocker != null && locked) {
            locked = unlocker.locked;
            companionDoor.locked = locked;
        }

        if (_currentRobot != null && _currentRobot.GetComponent<Robot>().interact) {
            if (!locked) {
                _currentRobot.transform.position = companionDoor.transform.position;

                GetComponent<SpriteRenderer>().sprite = openSmallDoor;
                
            }
            else {
                GetComponent<SpriteRenderer>().sprite = closedSmallDoor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Robot")) {
            _currentRobot = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Robot")) {
            if (collision.gameObject == _currentRobot) {
                _currentRobot = null;
            }
        }
    }
}

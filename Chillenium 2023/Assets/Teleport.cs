using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] Robot robot;
    private GameObject currentDoor;

    void Update() {
        if (robot.interact && currentDoor != null) {
            if (!currentDoor.GetComponent<Door>().locked) {
                transform.position = currentDoor.GetComponent<Door>().companionDoor.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Door")) {
            currentDoor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Door")) {
            if (collision.gameObject == currentDoor) {
                currentDoor = null;
            }
        }
    }
}

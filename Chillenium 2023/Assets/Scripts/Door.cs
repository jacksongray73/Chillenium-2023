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
        _currentRobot = null;
    }
    // Update is called once per frame
    void Update() {
        
        if (locked) {
            GetComponent<Interactable>().locked = true;
            GetComponent<SpriteRenderer>().sprite = closedSmallDoor;
        }
        else {

            GetComponent<SpriteRenderer>().sprite = openSmallDoor;
            GetComponent<Interactable>().locked = false;
        }

        if (unlocker != null) {
            locked = unlocker.locked;
        }
        //Debug.Log("Interact" + _currentRobot.GetComponent<Robot>().interact);
        //Debug.Log("Unlocked" + !locked);
        bool interact = _currentRobot != null && _currentRobot.GetComponent<Robot>().interact;
        //Debug.Log(interact);
        if (interact) {
            if (!locked) {
                _currentRobot.transform.position = companionDoor.transform.position;
                
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BigDoor : MonoBehaviour {
    [SerializeField] Unlocker unlocker;
    [SerializeField] public Sprite closedLargeDoor, openLargeDoor; 
    private bool closed;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        closed = unlocker.locked;
        if (!closed) {
            //Change sprite
            GetComponent<SpriteRenderer>().sprite = openLargeDoor;

            //Ignore inventor collision
            GameObject inventor = GameObject.FindGameObjectWithTag("Inventor");
            GameObject robot = GameObject.FindGameObjectWithTag("Robot");
            Physics2D.IgnoreCollision(inventor.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(robot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else {
            GetComponent<SpriteRenderer>().sprite = closedLargeDoor;
        }
    }
}

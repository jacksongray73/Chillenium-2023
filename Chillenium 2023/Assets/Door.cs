using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] public Door companionDoor;
    [SerializeField] public Unlocker unlocker;
    [SerializeField] public bool locked;
    void Start() {
        
    }
    // Update is called once per frame
    void Update() {
        if (unlocker != null && locked) {
            locked = !unlocker.unlocked;
            companionDoor.locked = !unlocker.unlocked;
        }
        
    }

    public Transform GetDestination() {
        return companionDoor.transform;
    }
}

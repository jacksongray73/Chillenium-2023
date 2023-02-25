using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] public Door companionDoor;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public Transform GetDestination() {
        return companionDoor.transform;
    }
}

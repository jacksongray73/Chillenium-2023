using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {
    [SerializeField] string nextLevelName;
    private Inventor _inventor;
    private Robot _robot;
    private float _standTimer;
    void Start() {
        _standTimer = 0;
    }

    void Update() {
        if (_inventor!=null && _robot!=null) {

            Quaternion inventorRot, robotRot;
            
            //wait 3 seconds
            _standTimer += Time.deltaTime;
            if(_standTimer%60 >= 3){
                //Rotation
                inventorRot = _inventor.transform.rotation;
                robotRot = _robot.transform.rotation;
                inventorRot.z++;
                robotRot.z++;
                _inventor.transform.rotation = inventorRot;
                _robot.transform.rotation = robotRot;
            }
            if (_standTimer % 30 > 9) {
                //Next level

                SceneManager.LoadScene(nextLevelName);
            }
        }
        else {
            _standTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            _inventor = collision.GetComponent<Inventor>();
        }
        if (collision.CompareTag("Robot")) {
            _robot = collision.GetComponent<Robot>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            _inventor = null;
        }
        if (collision.CompareTag("Robot")) {
            _robot = null;
        }
    }
}

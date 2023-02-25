using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {
    [SerializeField] string nextLevelName;
    private bool _inventor, _robot;
    private float _standTimer;
    void Start() {
        _inventor = false;
        _robot = false;
        _standTimer = 0;
    }

    void Update() {
        if (_inventor && _robot) {
            //wait 3 seconds
            _standTimer += Time.deltaTime;
            if(_standTimer%60 > 3){
                //next level
                SceneManager.LoadScene(nextLevelName);
            }
        }
        else {
            _standTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            _inventor = true;
        }
        if (collision.CompareTag("Robot")) {
            _robot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Inventor")) {
            _inventor = false;
        }
        if (collision.CompareTag("Robot")) {
            _robot = false;
        }
    }
}

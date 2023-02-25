using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {
    KeyCode resetKey = KeyCode.R;
    private float _timer = 0;
    void Update() {
        
        if (Input.GetKey(resetKey)) {
            //wait 3 seconds
            _timer += Time.deltaTime;
            if (_timer % 60 > 3) {
                //next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else {
            _timer = 0;
        }
    }
}

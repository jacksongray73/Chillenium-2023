using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour {
    [SerializeField] GameObject tutorialMenu, background;
    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Q)) {
            tutorialMenu.SetActive(true);
            background.SetActive(true);
        }
        else {
            tutorialMenu.SetActive(false);
            background.SetActive(false);
        }
    }
}

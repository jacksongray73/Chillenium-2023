using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneTransition : MonoBehaviour
{
    [SerializeField] string nextLevelName;

    float timer = 0;
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer % 60 >= 5){
            SceneManager.LoadScene(nextLevelName);
        } 
    }
}

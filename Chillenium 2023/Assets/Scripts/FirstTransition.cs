using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstTransition : MonoBehaviour
{

    [SerializeField] string nextLevelName;

    float timer = 0;
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer % 60 >= 11.5){
            SceneManager.LoadScene(nextLevelName);
        } 
    }
}

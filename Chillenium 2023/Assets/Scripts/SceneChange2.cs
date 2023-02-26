using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange2 : MonoBehaviour
{

    [SerializeField] string nextLevelName;

    float timer = 0;
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer % 60 >= 3){
            SceneManager.LoadScene(nextLevelName);
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToPlay : MonoBehaviour
{

    [SerializeField] string nextLevelName;

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextLevelName);
        }

    }
}

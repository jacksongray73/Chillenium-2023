using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToPlay : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        //SceneManager.LoadScene("Cutscene");
        SceneManager.LoadScene("HoldingHands");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

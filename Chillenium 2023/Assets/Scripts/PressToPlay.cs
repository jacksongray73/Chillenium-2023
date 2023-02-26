using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToPlay : MonoBehaviour
{

    [SerializeField] string nextLevelName;
    float _launchPower = 100;
    // Start is called before the first frame update

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            timer = 0;
            
            GetComponent<Rigidbody2D>().AddForce( * _launchPower);

            while(timer % 60 <= 2)
            {
                timer++;
            }
            SceneManager.LoadScene(nextLevelName);
        }
        
    }
}

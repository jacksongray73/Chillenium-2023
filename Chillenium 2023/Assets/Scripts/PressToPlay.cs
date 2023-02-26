using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToPlay : MonoBehaviour
{

    [SerializeField] string nextLevelName;
    float _launchPower = 100;
    float timer = 0;
    Vector2 direction;
    // Start is called before the first frame update

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // shoot off
            // GetComponent<Rigidbody2D>().AddForce(direction, _launchPower);

            // while(timer % 60 <= 2)
            // {
            //     timer++;
            // }
            SceneManager.LoadScene(nextLevelName);
        }
    }
}

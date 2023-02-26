using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }
}
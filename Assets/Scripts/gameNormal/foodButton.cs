using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class foodButton : MonoBehaviour
{
    /*public Button a;
    public Button parent;
    Button newButton;
    bool haveButton = false;
    float speed = 400.0f;
    float time = 0.0f;*/

    public GameObject Panel;
    public GameObject MainList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }

    public void GenerateButton()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }


    public void OnClick()
    {
        Debug.Log("ABC");
    }
}

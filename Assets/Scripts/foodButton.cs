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
        /*if (!haveButton)
        {
            newButton = Instantiate(a);
            newButton.transform.SetParent(parent.transform);
            newButton.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            while (speed > 0.0f)
            {
                newButton.GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0);
                Debug.Log(speed);
            }
            Debug.Log("STOP");
            haveButton = true;
           
        }
        else if (haveButton)
        {
            while (speed > 0.0f)
            {
                newButton.GetComponent<Rigidbody>().velocity = new Vector3(0, -speed, 0);
                speed -= 20.0f;
            }
            Destroy(gameObject.transform.GetChild(0).gameObject);
            haveButton = false;
            speed = 100.0f;
        }*/
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

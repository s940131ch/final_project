using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class foodButton : MonoBehaviour
{
    public Button a;
    public Button parent;
    Button newButton;
    bool flag = false;
    Vector3 v3;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GenerateButton);
        y = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(y + "," + flag);
        
    }

    public void GenerateButton()
    {
        if (!flag)
        {
            Debug.Log(x + " , " + y + " , " + z);
            newButton = Instantiate(a);
            newButton.transform.SetParent(parent.transform);
            while (y <= 150.0f)
            {
                y += 3.0f;
                newButton.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, y, 0.0f);
            }
            flag = true;
        }
        else if (flag)
        {
            while (y >= 0.0f)
            {
                y -= 3.0f;
                newButton.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, y, 0.0f);
            }

            Destroy(gameObject.transform.GetChild(0).gameObject);
            flag = false;
        }

        
 
        
    }



    public void OnClick()
    {
        Debug.Log("ABC");
    }
}

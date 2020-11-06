using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasContorl : MonoBehaviour
{
    public Sprite food;
    public Sprite heart;
    public Sprite water;
    public Sprite poo;

    public GameObject imTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
    }

    public void ImageGenerate(int i)
    {
        GameObject im = Instantiate(imTarget) as GameObject;
        Sprite temp;
        switch (i)
        {
            case 1:
                temp = food;
                break;
            case 2:
                temp = heart;
                break;
            case 3:
                temp = water;
                break;
            case 4:
                temp = poo;
                break;
            default:
                temp = poo;
                break;

        }
        im.GetComponent<Image>().sprite = temp;
        im.transform.SetParent(transform);
        im.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        im.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
       
}

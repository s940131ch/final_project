using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour
{

    public GameObject foodSource;
    public GameObject parent;
    GameObject food = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createFood()
    {

        if (food == null)
        {
            food = Instantiate<GameObject>(foodSource);
            food.transform.parent = parent.transform;
            food.GetComponent<Transform>().localPosition = new Vector3(1.0f, 0.0f, 0.0f);
            food.GetComponent<Transform>().localScale = new Vector3(0.015f, 0.015f, 0.015f);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalFeed: MonoBehaviour
{
    public GameObject foodSource;
    public GameObject waterSource;
    GameObject food = null;
    GameObject water = null;
    public handleTask HTK;
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
            food.GetComponent<Transform>().position = new Vector3(1.0f, 0.0f, 0.0f);
            food.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            HTK.pushTask(food);
        }

    }
    public void creatWater()
    {
        if (water == null)
        {
            water = Instantiate<GameObject>(waterSource);
            water.GetComponent<Transform>().position = new Vector3(-2.0f, 0.0f, 0.0f);
            water.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            HTK.pushTask(water);
        }
    }
}

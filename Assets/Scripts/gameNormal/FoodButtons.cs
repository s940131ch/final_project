using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodButtons : MonoBehaviour
{
    public GameObject foodSource;
    public GameObject waterSource;
    GameObject food = null;
    GameObject water = null;
    public void createFood()
    {

        if (food == null)
        {
            food = Instantiate<GameObject>(foodSource);
            food.GetComponent<Transform>().position = new Vector3(1.0f, 0.0f, 0.0f);
            food.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            handleTask.pushTask(food);
        }

    }
    public void creatWater()
    {
        if (water == null)
        {
            water = Instantiate<GameObject>(waterSource);
            water.GetComponent<Transform>().position = new Vector3(-2.0f, 0.0f, 0.0f);
            water.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            handleTask.pushTask(water);
        }
    }
}

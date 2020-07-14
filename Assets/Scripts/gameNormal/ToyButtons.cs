using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyButtons : MonoBehaviour
{
    public GameObject ballSource;
    public GameObject gourdSource;
    GameObject ball = null;
    GameObject gourd = null;
    public void playBall()
    {
        if (ball == null)
        {
            ball = Instantiate<GameObject>(ballSource);
            ball.GetComponent<Transform>().position = Camera.main.transform.position;

            handleTask.pushTask(ball);
        }
    }
}

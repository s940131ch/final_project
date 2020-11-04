using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyButtons : MonoBehaviour
{
    public GameObject ballSource;
    public GameObject gourdSource;
    public GameObject jumpSource;
    public GameObject boneSource;

    GameObject ball = null;
    GameObject gourd = null;
    GameObject jump = null;
    GameObject bone = null;
    public void playBall()
    {
        if (ball == null)
        {
            ball = Instantiate<GameObject>(ballSource);
            ball.GetComponent<Transform>().position = Camera.main.transform.position;
            handleTask.pushTask(ball);
        }
    }

    public void playGourd()
    {
        if (gourd == null)
        {
            gourd = Instantiate<GameObject>(gourdSource);
            gourd.GetComponent<Transform>().position = Camera.main.transform.position;
            handleTask.pushTask(gourd);
        }
    }

    public void playBone()
    {
        if (bone == null)
        {
            bone = Instantiate<GameObject>(boneSource);
            bone.GetComponent<Transform>().position = Camera.main.transform.position;
            handleTask.pushTask(bone);
        }
    }

    public void playJump()
    {
        if (gourd == null)
        {
            jump = Instantiate<GameObject>(jumpSource);
            jump.GetComponent<Transform>().position = Camera.main.transform.position;
            handleTask.pushTask(jump);
        }
    }
}

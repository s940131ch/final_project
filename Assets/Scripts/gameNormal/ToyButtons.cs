using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyButtons : MonoBehaviour
{
    public GameObject ballSource;
    public GameObject gourdSource;
    public GameObject jumpSource;
    public GameObject boneSource;
    public GameObject scratchSource;

    GameObject ball = null;
    GameObject gourd = null;
    GameObject jump = null;
    GameObject bone = null;
    GameObject scratch = null;
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
          
            handleTask.pushTask(jump);
        }
    }

    public void playScratch()
    {
        if (scratch == null)
        {
            scratch = Instantiate<GameObject>(scratchSource);
            scratch.GetComponent<Transform>().position = new Vector3(-9.4f, -2.0f, 0.0f);
            scratch.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            scratch.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, -90.0f));
            handleTask.pushTask(scratch);
        }
    }
}

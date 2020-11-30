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
        if (jump == null)
        {
            jump = Instantiate<GameObject>(jumpSource);
            jump.GetComponent<Transform>().position = new Vector3(13.48f, -1.97f, 5.4f);
            jump.GetComponent<Transform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);

            Vector3 jump1 = new Vector3(6, 9, 1);
            Vector3 jump2 = new Vector3(-36, 10, 26);
            Vector3 jump3 = new Vector3(-12, -12, 47);
         

            GameObject jumpPos0 = new GameObject();
            jumpPos0.transform.SetParent(jump.transform);
            jumpPos0.transform.localPosition = jump1;
            jumpPos0.name = "jumpTask1";


            GameObject jumpPos1 = new GameObject();


            Vector3 temp;
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                temp = jump3;
            }
            else
            {
                temp = jump2;
            }

            jumpPos1.transform.SetParent(jump.transform);
            jumpPos1.transform.localPosition = temp;
            jumpPos1.name = "jumpTask2";

            GameObject jumpPos2 = new GameObject();
            jumpPos2.transform.SetParent(jump.transform);
            jumpPos2.transform.localPosition = jump1;
            jumpPos2.name = "jumpTask3";

            handleTask.pushTask(jumpPos0);
            handleTask.pushTask(jumpPos1);
            handleTask.pushTask(jumpPos2);

        }
    }

    public void playScratch()
    {
        if (scratch == null)
        {
            scratch = Instantiate<GameObject>(scratchSource);
            scratch.GetComponent<Transform>().position = new Vector3(-9.4f, -2.0f, 0.0f);
            scratch.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, -90.0f));
            handleTask.pushTask(scratch);
        }
    }
}

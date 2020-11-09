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
            Vector3 jump4 = new Vector3(-35, 8, 65);
            Vector3 jump5 = new Vector3(-19, 10, 87);

            GameObject jumpPos0 = new GameObject();
            jumpPos0.transform.SetParent(jump.transform);
            jumpPos0.transform.localPosition = jump1;
            jumpPos0.name = "jumpTask1";

            

            Vector3 temp;
            int a = Random.Range(2, 6);
            switch(a)
            {
                case 1:
                    temp = jump2;
                    break;
                case 2:
                    temp = jump3;
                    break;
                case 3:
                    temp = jump4;
                    break;
                case 4:
                    temp = jump5;
                    break;
                default:
                    temp = jump5;
                    break;
            }

            GameObject jumpPos1 = new GameObject();

            jumpPos1.transform.SetParent(jump.transform);
            jumpPos1.transform.localPosition = temp;
            jumpPos1.name = "jumpTask2";

            handleTask.pushTask(jumpPos0);
            handleTask.pushTask(jumpPos1);
            
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

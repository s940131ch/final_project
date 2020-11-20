using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTarget : MonoBehaviour
{
    public GameObject cat;

    public GameObject food;
    public GameObject water;
    public GameObject Ball;
    public GameObject Bone;
    public GameObject Jump;
    public GameObject Scratch;
    public GameObject Guard;

    bool hasCreateFood = false;
    bool hasCreateWater = false;
    bool hasCreateBall = false;
    bool hasCreateGuard = false;
    bool hasCreateBone = false;
    bool hasCreateJump = false;
    bool hasCreateScratch = false;
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
        if (!hasCreateFood)
        { 
            handletaskAr.pushTask(food);
        }
    }

    public void createWater()
    {
        if (!hasCreateWater)
        {
            print("推入喝水任務");
            handletaskAr.pushTask(water);
        }
    }
    public void createBone()
    {
        if (!hasCreateBone)
        {
            GameObject newBone = Instantiate<GameObject>(Bone);
            newBone.GetComponent<Transform>().position = Camera.main.transform.position;
            newBone.GetComponent<Transform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
            print("創建骨頭");
            handletaskAr.pushTask(newBone);
        }
    }
    public void createBall()
    {
        if (!hasCreateBall)
        {
            GameObject newBall = Instantiate<GameObject>(Ball);
            newBall.GetComponent<Transform>().position = Camera.main.transform.position;
            newBall.GetComponent<Transform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
            print("創建球");
            handletaskAr.pushTask(newBall);
        }
    }

    public void createGuard()
    {
        if (!hasCreateGuard)
        {
            GameObject newGuard = Instantiate<GameObject>(Guard);
            newGuard.GetComponent<Transform>().position = Camera.main.transform.position;
            newGuard.GetComponent<Transform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
            print("創建葫蘆");
            handletaskAr.pushTask(newGuard);
        }
    }

    public void createJump()
    {
        if (!hasCreateJump)
        {
            Vector3 jump1 = new Vector3(6, 9, 1);
            Vector3 jump2 = new Vector3(-36, 10, 26);
            Vector3 jump3 = new Vector3(-12, -12, 47);

            GameObject jumpPos0 = new GameObject();

            jumpPos0.transform.SetParent(Jump.transform);
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

            jumpPos1.transform.SetParent(Jump.transform);
            jumpPos1.transform.localPosition = temp;

            jumpPos1.name = "jumpTask2";

            GameObject jumpPos2 = new GameObject();
            jumpPos2.transform.SetParent(Jump.transform);

            jumpPos2.transform.localPosition = jump1;
            jumpPos2.name = "jumpTask3";

            handletaskAr.pushTask(jumpPos0);
            handletaskAr.pushTask(jumpPos1);
            handletaskAr.pushTask(jumpPos2);
        }
    }

    public void createScratch()
    {
        if (!hasCreateScratch)
        {
            Scratch.GetComponent<Transform>().localScale = new Vector3(0.16f, 0.16f, 0.16f);
            Scratch.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, -90.0f));
            print("創建貓抓板");
            handletaskAr.pushTask(Scratch);
        }
    }


    public void foundFood()
    {
        Debug.Log("找到食物");
        hasCreateFood = true;
    }
    public void notFoundFood()
    {
        Debug.Log("沒找到食物");
        hasCreateFood = false;
    }

    public void foundWater()
    {
        Debug.Log("找到水");
        hasCreateWater = true;
    }
    public void notFoundWater()
    {
        Debug.Log("沒找到水");
        hasCreateWater = false;
    }

    public void foundGuard()
    {
        hasCreateGuard = true;
    }
    public void notFoundGuard()
    {
        hasCreateGuard = false;
    }

    public void foundBone()
    {
        hasCreateBone = true;
    }
    public void notFoundBone()
    {
        hasCreateBone = false;
    }

    public void foundBall()
    {
        hasCreateBall = true;
    }
    public void notFoundBall()
    {
        hasCreateBall = false;
    }
    public void foundScratch()
    {
        hasCreateScratch = true;
    }
    public void notFoundScratch()
    {
        hasCreateScratch = false;
    }
    public void foundJump()
    {
        hasCreateJump = true;
    }
    public void notFoundJump()
    {
        hasCreateJump = false;
    }
}

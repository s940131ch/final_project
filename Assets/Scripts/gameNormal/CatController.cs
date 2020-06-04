using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{

    float direction = 0.0f;     //朝向前方
    float timeOfDirection = 0;  //要轉換方向的時間
    float timeOfWalking = 0;    //走的時間
    float timeOfHunger = 0.0f;  //飢餓度扣除時間
    float timeOfWater = 0.0f;   //口渴值扣除時間 
    float timeOfEating = 5.0f;  //吃飯時間
    float timeOfDrinking = 3.0f;  //吃飯時間
    float speed = 1.5f;         //走路速度
    bool isOk = false;          //是否決定好方向?
    bool isDoingTask = false;
    bool isWalking = false;     //有在走路嗎?

    float timeCount = 0.0f;
    GameObject temp;
    Animator am;
   
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.0f, 0.05f, 0.0f);
        am = GetComponent<Animator>();
        am.SetInteger("Status", 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*如果沒有需要Task則隨機走路*/
        if (handleTask.isEmpty())
        {
            
            /*原地隨機走路*/
            timeOfDirection += Time.deltaTime;
            //Debug.Log(timeOfDirection);
            if (!isWalking && timeOfDirection >= 3.0f)
            {
                decideDirection();
                isWalking = true;
            }
            if (isOk && timeOfWalking > 0)
            {
                walk();
                timeOfDirection = 0.0f;
                timeOfWalking -= Time.deltaTime;
            }
            else
            {
                am.SetInteger("Status", 0);
                isWalking = false;
                isOk = false;
            }
        }

        /*否則做Task*/
        else
        {
            /*拿到第一個Task的內容*/
            if (!isDoingTask)
            {
                temp = handleTask.getFirst();       
                isDoingTask = true;
            }

            /*看向Task*/
            Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);

            /*如果是吃東西的任務的話*/
            if (temp.name == "bowlHasFood(Clone)" || temp.name == "bowlHasWater(Clone)")
            {


                /*走向餐盤*/
                if (Vector3.Distance(temp.transform.position, transform.position) >= 2.0f)
                {
                    walk();
                    if (temp.name == "bowlHasFood(Clone)")
                        timeOfEating = 5.0f;
                    else if (temp.name == "bowlHasWater(Clone)")
                        timeOfDrinking = 5.0f;
                }
                /*走到之後開始吃*/
                else
                {
                    eating();
                    if (temp.name == "bowlHasFood(Clone)")
                    {
                        timeOfEating -= Time.deltaTime;
                        if (timeOfEating < 0)
                        {
                            Destroy(handleTask.getFirst());
                            StatusController.setHealth(100.0f);
                            handleTask.popTask();
                            isDoingTask = false;
                        }
                    }
                    else if (temp.name == "bowlHasWater(Clone)")
                    {
                        timeOfDrinking -= Time.deltaTime;
                        if (timeOfDrinking < 0)
                        {
                            Destroy(handleTask.getFirst());
                            StatusController.setWater(100.0f);
                            handleTask.popTask();
                            isDoingTask = false;
                        }
                    }
                }

            }

            timeCount = timeCount + Time.deltaTime * speed;
        }

        /*每隔5秒扣除飢餓度*/
        timeOfHunger += Time.deltaTime;
        timeOfWater += Time.deltaTime;
        if (timeOfHunger > 5.0f && StatusController.getHealth() > 0.0f)
        {
            StatusController.minusHealth(0.1f);
            timeOfHunger = 0.0f;
        }

        /*每隔3秒扣除口渴值*/
        if (timeOfWater > 3.0f && StatusController.getWater() > 0.0f)
        {
            
            StatusController.minusWater(0.1f);
            timeOfWater = 0.0f;
        }


    }
    private void decideDirection()
    {
        direction = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = new Vector3(0.0f, direction, 0.0f);
        timeOfWalking = Random.Range(1.0f,6.0f);
        isOk = true;
        Debug.Log("決定方向了");
    }
    private void walk()
    {
        am.SetInteger("Status", 1);
        transform.position += transform.forward * Time.deltaTime * speed;
  
        //Debug.Log("正在走路");
    }
    private void eating()
    {
        
        am.SetInteger("Status", 2);
        Debug.Log("正在吃飯");
    }

}

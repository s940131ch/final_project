using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{

    float direction = 0.0f;     //朝向前方
    float timeOfDirection = 0;  //要轉換方向的時間
    float timeOfWalking = 0;    //走的時間
    float timeOfHunger = 0;     //飢餓度扣除時間
    float timeOfEating = 3.0f;  //吃飯時間
    float speed = 1.5f;         //走路速度
    bool isOk = false;          //是否決定好方向?
    bool canEat = false;
    bool isWalking = false;     //有在走路嗎?
    float timeCount = 0.0f;


    public float hungerValue = 100.0f;
    /*public float thirstValue;
    public float cohesion;*/
    public HungerController HC;

    Animator am;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.0f, 0.05f, 0.0f);
        am = GetComponent<Animator>();
        am.SetInteger("Status", 0);

        HC.health = hungerValue - 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
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
        else
        {
            GameObject temp;
            temp = handleTask.taskQuene[handleTask.Front + 1];
            Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
            Debug.Log(Vector3.Distance(temp.transform.position, transform.position));
            if (Vector3.Distance(temp.transform.position, transform.position) >= 3.0)
            {
                timeOfEating = 3.0f;
                walk();
            }
            else
            {
                eating();
                timeOfEating -= Time.deltaTime;
                if(timeOfEating < 0)
                {
                    Destroy(handleTask.taskQuene[handleTask.Front + 1]);
                    HC.health = 0.0f;
                    handleTask.popTask();
                }
            }
            timeCount = timeCount + Time.deltaTime * speed;
        }

        /*每隔10秒扣除飢餓度*/
        timeOfHunger += Time.deltaTime;
        if(timeOfHunger > 10.0f && HC.health > -100.0f)
        {
            HC.health -= 5.0f;
            timeOfHunger = 0.0f;
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
  
        Debug.Log("正在走路");
    }
    private void eating()
    {
        am.SetInteger("Status", 2);
        

        Debug.Log("正在吃飯");
    }

}

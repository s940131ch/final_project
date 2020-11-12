using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControllerAR : MonoBehaviour
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
    bool hasFound = false;
    bool canEat = false;
    bool backOrigin = true;
    float timeCount = 0.0f;

    GameObject temp;
    public GameObject origin;
   
    Animator am;
    // Start is called before the first frame update
    void Start()
    {
        
        am = GetComponent<Animator>();
        am.SetInteger("Status", 0);

    }

    // Update is called once per frame
    void Update()
    {

        if(!handletaskAr.isEmpty())
        {
            /*拿到第一個Task的內容*/
            if (!isDoingTask)
            {
                temp = handletaskAr.getFirst();
                isDoingTask = true;
            }
            //Debug.Log(temp.name);

            /*看向Task*/

            Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);

            /*如果是吃東西的任務的話*/
            if (temp.name == "bowlHasFood" || temp.name == "bowlHasWater")
            {
                Debug.Log(Vector3.Distance(temp.transform.position, transform.position));
                Debug.Log(temp.name);
                if (!canEat && Vector3.Distance(temp.transform.parent.transform.position, transform.position) >= 0.5f)
                {
                    /*走向餐盤*/
                    walk();
                    if (temp.name == "bowlHasFood")
                        timeOfEating = 3.0f;
                    else if (temp.name == "bowlHasWater")
                        timeOfDrinking = 5.0f;
                }
                /*走到之後開始吃*/
                else
                {
                    canEat = true;
                    eating();
                    
                }

            }

            timeCount = timeCount + Time.deltaTime * speed;
        }
        else
        {
            if (Vector3.Distance(transform.position,origin.transform.position) >= 0.05f)
            {
                Debug.Log("走回原點");
                Quaternion lookOnLook = Quaternion.LookRotation(origin.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                walk();
                backOrigin = true;
            }
            else
            {
                /*走到原點轉正*/
                if (backOrigin)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(0.0f, -180.0f, 0.0f));
                    backOrigin = false;
                }
                am.SetInteger("Status", 0);
            }
            Debug.Log("現在沒在工作");
        }

        /*有貓之後每隔5秒扣除飢餓度*/
        if (hasFound)
        {        
            timeOfHunger += Time.deltaTime;
            timeOfWater += Time.deltaTime;
            if (timeOfHunger > 5.0f && StatusController.getHealth() > 0.0f)
            {
                Debug.Log("扣除飢餓值");
                StatusController.minusHealth(0.5f);
                timeOfHunger = 0.0f;
            }

            /*每隔3秒扣除口渴值*/
            if (timeOfWater > 3.0f && StatusController.getWater() > 0.0f)
            {
                Debug.Log("扣除口渴值");
                float waterTemp = StatusController.getWater();
                StatusController.minusWater(0.5f);
                timeOfWater = 0.0f;
            }
        }
        
    }
    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "bowlHasFood")
        {
            Debug.Log("碰到碗盤了");
            canEat = true;
        }
    }*/

    public void foundCat()
    {
        this.hasFound = true;
        Debug.Log("找到貓了");
    }
    public void notFountCat()
    {
        this.hasFound = false;
        transform.rotation = Quaternion.Euler(new Vector3(origin.transform.rotation.x, origin.transform.rotation.y, origin.transform.rotation.z));
        transform.position = new Vector3(origin.transform.position.x, origin.transform.position.y, origin.transform.position.z);
        Debug.Log("沒找到貓");

    }
    private void decideDirection()
    {
        direction = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = new Vector3(0.0f, direction, 0.0f);
        timeOfWalking = Random.Range(1.0f, 6.0f);
        isOk = true;
       // Debug.Log("決定方向了");
    }
    private void walk()
    {
        am.SetInteger("Status", 1);
        transform.localPosition += transform.forward * Time.deltaTime * speed;

        //Debug.Log("正在走路");
    }
    private void eating()
    {

        am.SetInteger("Status", 2);

        if (temp.name == "bowlHasFood")
        {
            timeOfEating -= Time.deltaTime;
            if (timeOfEating < 0)
            {
                Debug.Log("吃完了");
                StatusController.setHealth(100.0f);
                handletaskAr.popTask();
                isDoingTask = false;
                canEat = false;
            }
        }
        else if (temp.name == "bowlHasWater")
        {
            timeOfDrinking -= Time.deltaTime;
            if (timeOfDrinking < 0)
            {

                StatusController.setWater(100.0f);
                handletaskAr.popTask();
                isDoingTask = false;
                canEat = false;
            }
        }
        //Debug.Log("正在吃飯");
    }

}

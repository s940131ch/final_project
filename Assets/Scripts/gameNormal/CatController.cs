using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;

public class CatController : MonoBehaviour
{

    float direction = 0.0f;     //朝向前方
    float timeOfDirection = 0;  //要轉換方向的時間
    float timeOfWalking = 0;    //走的時間
    float timeOfHunger = 0.0f;  //飢餓度扣除時間
    float timeOfWater = 0.0f;   //口渴值扣除時間 
    float timeOfHeart = 0.0f;   //友好值扣除時間
    float timeOfEating = 5.0f;  //吃飯時間
    float timeOfPoo = 0.0f;    //大便任務觸發時間
    float timeOfDrinking = 3.0f;//吃飯時間
    float timeOfPlaying = 5.0f; //遊玩時間 

    float timeOfDoingPoo = 3.0f; //大便過程
    
    float timeOfSound = 0.0f;
    float speed = 2.5f;         //走路速度
    bool isOk = false;          //是否決定好方向?
    bool isDoingTask = false;
    bool isWalking = false;     //有在走路嗎?
    bool ballFlag = true;
    bool playBallFlag = true;
    bool generatePoo = false;
    float timeCount = 0.0f;
    float random = 0.0f;

    GameObject temp;
    GameObject a;
    public GameObject Poo;
    public GameObject PooSource;
    Animator am;
    AudioSource sound;          //貓叫聲

    public Canvas cv;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.0f, -2.0f, 0.0f);
        am = GetComponent<Animator>();
        am.SetInteger("Status", 0);

        /*家平的傑作*/
        sound = GetComponent<AudioSource>();
        sound.Play();
        random = Random.Range(5.0f, 10.0f);

    }

    // Update is called once per frame
    void Update()
    {
        /*如果沒有需要Task則隨機走路*/
        if (handleTask.isEmpty())
        {
            speed = 2.5f;    
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
            timeOfSound += Time.deltaTime;

            if(timeOfSound > random)
            {
                sound.Play();
                random = Random.Range(5.0f, 50.0f);
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

            

            /*如果是吃東西的任務的話*/
            if (temp.name == "bowlHasFood(Clone)" || temp.name == "bowlHasWater(Clone)")
            {
                /*看向Task*/
                Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                timeCount = timeCount + Time.deltaTime * speed;
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
                            cv.GetComponent<CanvasContorl>().ImageGenerate(1);
                            Destroy(handleTask.getFirst());
                            StatusController.setHealth(100.0f);
                            handleTask.popTask();
                            isDoingTask = false;
                            sound.Play();
                            timeOfSound = 0.0f;
                            random = Random.Range(5.0f, 50.0f);
                        }
                    }
                    else if (temp.name == "bowlHasWater(Clone)")
                    {
                        timeOfDrinking -= Time.deltaTime;
                        if (timeOfDrinking < 0)
                        {
                            cv.GetComponent<CanvasContorl>().ImageGenerate(3);

                            Destroy(handleTask.getFirst());
                            StatusController.setWater(100.0f);
                            handleTask.popTask();
                            isDoingTask = false;
                            sound.Play();
                            timeOfSound = 0.0f;
                            random = Random.Range(5.0f, 50.0f);
                        }
                    }
                }

            }
            else if(temp.name == "toy_ball(Clone)" || temp.name == "toy_gourd(Clone)" || temp.name == "toy_bone(Clone)")
            {
                am.SetInteger("Status", 3);
                
                /*看向Task*/
                if (ballFlag)
                {
                    Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                    timeCount = timeCount + Time.deltaTime * speed;

                }
                playBall(temp);
                if (Vector3.Distance(temp.transform.position, transform.position) <= 2.0f || temp.transform.position.y <= -2.0f)
                {
                    ballFlag = false;
                    temp.transform.parent = gameObject.transform;
                    temp.transform.localPosition = new Vector3(0, 0.35f, 0.6f);
                    temp.transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                    if (!ballFlag)
                    {
                        Quaternion A = Quaternion.LookRotation(new Vector3(0.0f, -2.0f, 0.0f) - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, A, timeCount);
                        timeCount = timeCount + Time.deltaTime * speed;
                    }
                    walk();
                }
                if(Vector3.Distance(new Vector3(0.0f, -2.0f, 0.0f), transform.position) <= 2.0f && !ballFlag)
                {
                    ballFlag = true;
                    playBallFlag = true;
                    Destroy(handleTask.getFirst());
                    handleTask.popTask();
                    isDoingTask = false;
                    am.speed = 1.0f;
                    speed = 2.5f;
                    print("到了");
                    cv.GetComponent<CanvasContorl>().ImageGenerate(2);
                    StatusController.setLove(StatusController.getLove() + 0.1f);
                    
                }
            }
            /*大便任務*/
            else if (temp.name == "catLittleBox")
            {
                /*看向task*/
                Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                timeCount = timeCount + Time.deltaTime * speed;

                if (Vector3.Distance(temp.transform.position, transform.position) > 0.5f)
                {
                    
                    speed = 9.0f;
                    am.speed = 1.5f;
                    walk();
                }
                else
                {
                    am.SetInteger("Status", 4);
                    if (!generatePoo)
                    {
                        a = Instantiate<GameObject>(PooSource);
                        a.transform.position = transform.position;
                        cv.GetComponent<CanvasContorl>().ImageGenerate(4);
                        generatePoo = true;
                    }
                    timeOfDoingPoo -= Time.deltaTime;

                    if (timeOfDoingPoo <= 0.0f)
                    {
                        Destroy(a, 3);
                        isDoingTask = false;
                        generatePoo = false;
                        am.speed = 1.0f;
                        speed = 2.5f;
                        timeOfDoingPoo = 3.0f;
                        handleTask.popTask();
                    }
                }
            }
            else if (temp.name == "toy_scratch(Clone)")
            {
                Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                timeCount = timeCount + Time.deltaTime * speed;

                if (Vector3.Distance(temp.transform.position, transform.position) > 2.0f)
                {

                    
                    speed = 9.0f;
                    am.speed = 1.5f;
                    walk();
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 140.0f, transform.rotation.z));
                    am.SetInteger("Status", 4);
                    timeOfPlaying -= Time.deltaTime;
                    if (timeOfPlaying <= 0.0f)
                    {
                        cv.GetComponent<CanvasContorl>().ImageGenerate(2);
                        Destroy(handleTask.getFirst());
                        isDoingTask = false;
                        am.speed = 1.0f;
                        speed = 2.5f;
                        timeOfPlaying = 5.0f;
                        handleTask.popTask();
                        StatusController.setLove(StatusController.getLove() + 0.1f);
                    }
                }
            }

        }

        /*每隔5秒扣除飢餓度*/
        timeOfHunger += Time.deltaTime;
        timeOfHeart += Time.deltaTime;
        timeOfWater += Time.deltaTime;
        if(!generatePoo)
            timeOfPoo += Time.deltaTime;
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

        /*每隔20秒扣除友好值*/
        if (timeOfHeart > 20.0f && StatusController.getLove() > 0.0f)
        {

            StatusController.minusLove(0.1f);
            timeOfHeart = 0.0f;
        }
        if (timeOfPoo > 20.0f)
        {
            Debug.Log("大便囉");
            timeOfPoo = 0.0f;
            handleTask.pushTask(Poo);
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
        //Debug.Log("正在吃飯");
    }
    private void playBall(GameObject newBall)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool flag = false;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && playBallFlag) 
        {
            Vector3 Direction = hit.point - Camera.main.transform.position;

            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 0.1f, true);
            Debug.Log(hit.transform.name);

            newBall.transform.position = Camera.main.transform.position;
            newBall.AddComponent<Rigidbody>();
            newBall.AddComponent<SphereCollider>();

            Rigidbody ballRd = newBall.GetComponent<Rigidbody>();
            ballRd.AddForce(Direction * 100.0f);
            playBallFlag = false;
        }
        if (newBall.transform.position.y <= 0.25f)
        {
            speed = 9.0f;
            am.speed = 2.0f;
            walk();
        }
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if(handleTask.isEmpty())
            timeOfWalking = 0.0f;
    }

    public void playSound()
    {
        sound.Play();
    }
  
}

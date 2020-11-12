using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAR : MonoBehaviour
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
    float timeOfChangeJump = 1.25f;
    float timeOfDoingPoo = 3.0f; //大便過程
    float timeOfSound = 0.0f;
    float speed = 2.5f;         //走路速度
    bool isOk = false;          //是否決定好方向?
    bool isDoingTask = false;
    bool isWalking = false;     //有在走路嗎?
    bool ballFlag = true;
    bool playBallFlag = true;
    bool generatePoo = false;
    bool backOrigin = true;
    float timeCount = 0.0f;
    float random = 0.0f;

    GameObject temp;
    GameObject a;
    public GameObject Poo;
    public GameObject PooSource;
    Animator am;
    AudioSource sound;          //貓叫聲

    public Canvas cv;
    public GameObject origin;
    Vector3 ori;
    // Start is called before the first frame update
    void Start()
    {
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
        if (handletaskAr.isEmpty())
        {
            if (Vector3.Distance(transform.position, origin.transform.position) >= 0.05f)
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

            timeOfSound += Time.deltaTime;

            if (timeOfSound > random)
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
                temp = handletaskAr.getFirst();
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
                            Destroy(handletaskAr.getFirst());
                            StatusController.setHealth(100.0f);
                            handletaskAr.popTask();
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

                            Destroy(handletaskAr.getFirst());
                            StatusController.setWater(100.0f);
                            handletaskAr.popTask();
                            isDoingTask = false;
                            sound.Play();
                            timeOfSound = 0.0f;
                            random = Random.Range(5.0f, 50.0f);
                        }
                    }
                }

            }
            else if (temp.name == "toy_ball(Clone)" || temp.name == "toy_gourd(Clone)" || temp.name == "toy_bone(Clone)")
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
                if ((Vector3.Distance(new Vector3(0.0f, -2.0f, 0.0f), transform.position) <= 2.0f && !ballFlag) || temp.transform.position.y < -10.0f)
                {
                    ballFlag = true;
                    playBallFlag = true;
                    Destroy(handletaskAr.getFirst());
                    handletaskAr.popTask();
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

                if (Vector3.Distance(temp.transform.position, transform.position) > 0.25f)
                {

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
                        a.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
                        handletaskAr.popTask();
                    }
                }
            }
            else if (temp.name == "toy_scratch(Clone)")
            {
                Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                timeCount = timeCount + Time.deltaTime * speed;


                if (Vector3.Distance(temp.transform.position, transform.position) > 0.05f)
                {
                    speed = 9.0f;
                    am.speed = 1.5f;
                    walk();
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 140.0f, transform.rotation.z));
                    /*待修正*/
                    am.SetInteger("Status", 5);
                    /********/
                    timeOfPlaying -= Time.deltaTime;
                    if (timeOfPlaying <= 0.0f)
                    {
                        cv.GetComponent<CanvasContorl>().ImageGenerate(2);
                        Destroy(handletaskAr.getFirst());
                        isDoingTask = false;
                        am.speed = 1.0f;
                        speed = 2.5f;
                        timeOfPlaying = 5.0f;
                        handletaskAr.popTask();
                        StatusController.setLove(StatusController.getLove() + 0.1f);
                    }
                }
            }

            else if (temp.name == "jumpTask1" || temp.name == "jumpTask2" || temp.name == "jumpTask3")
            {

                print("跳跳跳" + temp.name);
                /*看像task*/


                /*先走到該跳的位置*/
                if (temp.name == "jumpTask1")
                {
                    Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                    timeCount = timeCount + Time.deltaTime * speed;

                    print("跳1");
                    if (Vector3.Distance(temp.transform.position, transform.position) > 2.0f)
                    {
                        speed = 9.0f;
                        am.speed = 1.5f;
                        walk();
                    }
                    else
                    {

                        isDoingTask = false;
                        am.speed = 1.0f;
                        speed = 2.5f;
                        Destroy(handletaskAr.getFirst());
                        handletaskAr.popTask();
                    }
                }
                else if (temp.name == "jumpTask2")
                {

                    if (Vector3.Distance(temp.transform.position, transform.position) > 0.05f)
                    {
                        print("跳躍2");
                        Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                        timeCount = timeCount + Time.deltaTime * speed;
                        speed = 9.0f;
                        am.SetInteger("Status", 3);
                        timeOfChangeJump -= Time.deltaTime;
                        if (timeOfChangeJump <= 0)
                        {
                            transform.position += transform.forward * Time.deltaTime * speed;
                        }

                        Invoke("DelayWalk", 1.25f);
                    }
                    else
                    {
                        print("跳到了");
                        timeOfChangeJump = 1.6f;
                        transform.rotation = Quaternion.Euler(new Vector3(0.0f, transform.rotation.y, 0.0f));
                        isDoingTask = false;
                        Destroy(handletaskAr.getFirst());
                        handletaskAr.popTask();
                    }
                }
                else if (temp.name == "jumpTask3")
                {

                    if (Vector3.Distance(temp.transform.position, transform.position) > 0.05f)
                    {
                        print("跳躍3");
                        Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                        timeCount = timeCount + Time.deltaTime * speed;
                        speed = 9.0f;
                        timeOfChangeJump -= Time.deltaTime;
                        if (timeOfChangeJump <= 0)
                        {
                            transform.position += transform.forward * Time.deltaTime * speed;
                        }
                    }
                    else
                    {
                        print("跳到了");
                        timeOfChangeJump = 1.25f;
                        transform.rotation = Quaternion.Euler(new Vector3(0.0f, transform.rotation.y, 0.0f));
                        isDoingTask = false;
                        cv.GetComponent<CanvasContorl>().ImageGenerate(2);
                        Destroy(handletaskAr.getFirst());
                        handletaskAr.popTask();
                        GameObject a = GameObject.Find("toy_jump(Clone)");
                        Destroy(a);
                    }
                }

            }

        }

        /*每隔5秒扣除飢餓度*/
        timeOfHunger += Time.deltaTime;
        timeOfHeart += Time.deltaTime;
        timeOfWater += Time.deltaTime;
        if (!generatePoo)
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
            handletaskAr.pushTask(Poo);
        }

    }




    private void decideDirection()
    {
        direction = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = new Vector3(0.0f, direction, 0.0f);
        timeOfWalking = Random.Range(1.0f, 6.0f);
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
            if (newBall.name == "toy_ball(Clone)")
                newBall.AddComponent<SphereCollider>();
            else
                newBall.AddComponent<BoxCollider>();

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


    public void playSound()
    {
        sound.Play();
    }
}

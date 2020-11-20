using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MultiARCatController : MonoBehaviourPun
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

    float timeOfSound = 0.0f;
    float timeOfPoo = 0.0f;
    bool ballFlag = true;
    bool playBallFlag = true;
    bool generatePoo = false;
    float timeOfDoingPoo = 3.0f; //大便過程
    float timeOfPlaying = 5.0f; //遊玩時間 
    float timeOfChangeJump = 1.25f;

    GameObject temp;
    public Transform origin;

    handleTaskMutli tasks;

    PhotonView PV;

    Animator am;

    Text viewID;

    public Canvas cv;

    GameObject a;
    public GameObject Poo;
    public GameObject PooSource;
    AudioSource sound;          //貓叫聲
    float random = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        /*家平的傑作*/
        sound = GetComponent<AudioSource>();
        sound.Play();
        random = Random.Range(5.0f, 10.0f);

        //初始動畫
        am = GetComponent<Animator>();
        am.SetInteger("Status", 0);
        
        tasks = GameObject.Find("TaskQueue").GetComponent<handleTaskMutli>();

        PV = GetComponent<PhotonView>();
        viewID =  GameObject.Find("ViewID").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        viewID.text = PV.ViewID.ToString();
        if (PhotonNetwork.IsMasterClient)
        {
            if (!tasks.isEmpty())
            {
                /*拿到第一個Task的內容*/
                if (!isDoingTask)
                {
                    temp = tasks.getFirst();
                    isDoingTask = true;
                }
                
                if (temp.name == "bowlHasFood" || temp.name == "bowlHasWater")
                {
                    print("吃東西任務");
                    /*看向Task*/
                    Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                    timeCount = timeCount + Time.deltaTime * speed;
                    /*走向餐盤*/
                    if (Vector3.Distance(temp.transform.position, transform.position) >= 0.25f)
                    {
                        walk();
                        if (temp.name == "bowlHasFood")
                            timeOfEating = 3.0f;
                        else if (temp.name == "bowlHasWater")
                            timeOfDrinking = 3.0f;
                    }
                    /*走到之後開始吃*/
                    else
                    {
                        Debug.Log("開吃");
                        eating();
                        if (temp.name == "bowlHasFood")
                        {
                            timeOfEating -= Time.deltaTime;
                            if (timeOfEating < 0)
                            {
                                cv.GetComponent<CanvasContorl>().ImageGenerate(1);
                                PhotonView.Destroy(tasks.getFirst());
                                tasks.popTask();
                                isDoingTask = false;
                                sound.Play();
                                timeOfSound = 0.0f;
                                timeOfEating = 3.0f;
                            }
                        }
                        else if (temp.name == "bowlHasWater")
                        {
                            timeOfDrinking -= Time.deltaTime;
                            if (timeOfDrinking < 0)
                            {
                                cv.GetComponent<CanvasContorl>().ImageGenerate(3);
                                PhotonView.Destroy(tasks.getFirst());
                                tasks.popTask();
                                isDoingTask = false;
                                sound.Play();
                                timeOfSound = 0.0f;
                                timeOfDrinking = 3.0f;
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
                    if (Vector3.Distance(temp.transform.position, transform.position) <= 0.3f || temp.transform.position.y <= -2.0f)
                    {
                        ballFlag = false;
                        temp.transform.parent = gameObject.transform;
                        temp.transform.localPosition = new Vector3(0, 0.35f, 0.6f);
                        temp.transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));

                        if (!ballFlag)
                        {
                            Quaternion A = Quaternion.LookRotation(new Vector3(0.0f, -1.0f, 0.0f) - transform.position);
                            transform.rotation = Quaternion.Slerp(transform.rotation, A, timeCount);
                            timeCount = timeCount + Time.deltaTime * speed;
                        }
                        walk();
                    }

                    if ((Vector3.Distance(new Vector3(0.0f, -1.0f, 0.0f), transform.position) <= 0.3f && !ballFlag) || temp.transform.position.y < -10.0f)
                    {
                        ballFlag = true;
                        playBallFlag = true;
                        /****/
                        PhotonView.Destroy(tasks.getFirst());
                        /****/
                        tasks.popTask();
                        isDoingTask = false;
                        am.speed = 1.0f;
                        speed = 1.0f;
                        print("到了");
                        cv.GetComponent<CanvasContorl>().ImageGenerate(2);
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
                            a = PhotonNetwork.Instantiate(PooSource.name, transform.position, Quaternion.identity);
                            a.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            cv.GetComponent<CanvasContorl>().ImageGenerate(4);
                            generatePoo = true;
                        }
                        timeOfDoingPoo -= Time.deltaTime;

                        if (timeOfDoingPoo <= 0.0f)
                        {
                            PhotonView.Destroy(a, 3);
                            isDoingTask = false;
                            generatePoo = false;
                            am.speed = 1.0f;
                            speed = 2.0f;
                            timeOfDoingPoo = 3.0f;
                            tasks.popTask();
                        }
                    }
                }
                else if (temp.name == "toy_scratch")
                {
                    Quaternion lookOnLook = Quaternion.LookRotation(temp.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, timeCount);
                    timeCount = timeCount + Time.deltaTime * speed;


                    if (Vector3.Distance(temp.transform.position, transform.position) > 0.05f)
                    {
                        speed = 2.0f;
                        am.speed = 1.5f;
                        walk();
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 140.0f, transform.rotation.z));
                        /*待修正*/
                        am.SetInteger("Status", 7);
                        /********/
                        timeOfPlaying -= Time.deltaTime;
                        if (timeOfPlaying <= 0.0f)
                        {
                            cv.GetComponent<CanvasContorl>().ImageGenerate(2);
                            isDoingTask = false;
                            am.speed = 1.0f;
                            speed = 1.0f;
                            timeOfPlaying = 5.0f;
                            tasks.popTask();
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
                            speed = 1.0f;
                            PhotonView.Destroy(tasks.getFirst());
                            tasks.popTask();
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
                            speed = 2.0f;
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
                            PhotonView.Destroy(tasks.getFirst());
                            tasks.popTask();
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
                            speed = 2.0f;
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
                            PhotonView.Destroy(tasks.getFirst());
                            tasks.popTask();
                            GameObject a = GameObject.Find("toy_jump");
                            PhotonView.Destroy(a);

                        }
                    }

                }
                timeCount = timeCount + Time.deltaTime * speed;
            }
            else
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
                        Debug.Log("轉正轉正了");
                        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, -180.0f, 0.0f));
                        backOrigin = false;
                    }
                    am.SetInteger("Status", 0);
                }
                //Debug.Log("現在沒在工作");
            }
            if (!generatePoo)
                timeOfPoo += Time.deltaTime;
            if (timeOfPoo > 20.0f)
            {
                Debug.Log("大便囉");
                timeOfPoo = 0.0f;
                handletaskAr.pushTask(Poo);
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

    /*public void foundCat()
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

    }*/
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
        transform.position += transform.forward * Time.deltaTime * speed;

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
                tasks.popTask();
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
                tasks.popTask();
                isDoingTask = false;
                canEat = false;
            }
        }
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
            speed = 2.0f;
            am.speed = 1.5f;
            walk();
        }

    }


    public void playSound()
    {
        sound.Play();
    }
}

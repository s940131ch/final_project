using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Vuforia;

public class GameManager : MonoBehaviourPun
{
    [SerializeField]
    private GameObject catPrefab;
    [SerializeField]
    private GameObject catTarget;
    [SerializeField]
    private GameObject foodTarget;
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameObject waterTarget;
    [SerializeField]
    private GameObject waterPrefab;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject guardPrefab;

    [SerializeField]
    private GameObject bonePrefab;

    [SerializeField]
    private GameObject jumpTarget;
    [SerializeField]
    private GameObject jumpPrefab;

    [SerializeField]
    private GameObject scratchTarget;
    [SerializeField]
    private GameObject scratchPrefab;


    private GameObject tempCat;
    private GameObject tempFood;
    private GameObject tempWater;
    private GameObject tempBall;
    private GameObject tempBone;
    private GameObject tempGuard;
    private GameObject tempJump;
    private GameObject tempScratch;
    PhotonView PV;

    public Text roundPlayer;
    public Text RPCMessage;

    handleTaskMutli tasks;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.MasterClient.NickName);
        //PhotonNetwork.Instantiate(this.Food.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
        tasks = GameObject.Find("TaskQueue").GetComponent<handleTaskMutli>();
    }

    // Update is called once per frame
    void Update()
    {
        roundPlayer.text = "現在是" + PhotonNetwork.MasterClient.NickName + "的回合";
    }

    public void foundCat()
    {
        catPrefab.SetActive(true);
        Debug.Log("找到貓了");
    }
    public void notFountCat()
    {
        catPrefab.SetActive(false);
        Debug.Log("沒找到貓");

    }
    /*食物*/
    public void FoundFood()
    {
        /*該玩家回合才可以做事情*/
        if(PhotonNetwork.IsMasterClient)
        {
            if (tempFood == null)
            {
                tempFood = PhotonNetwork.Instantiate(foodPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                tempFood.name = "bowlHasFood";
                tempFood.transform.parent = foodTarget.transform;
                tempFood.transform.localPosition = new Vector3(0, 0, 0);
                tempFood.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                photonView.RPC("MasterFoundFood", RpcTarget.Others, foodTarget.transform.position);
                tasks.pushTask(tempFood);
            }
        }
        
    }
    [PunRPC]
    public void MasterFoundFood(Vector3 t)
    {
        Debug.Log(t);
        Debug.Log("收到訊息");
        GameObject temp = GameObject.Find("bowlHasFood(Clone)");
        if (temp != null)
            Debug.Log(temp + "不為空");
        temp.transform.position = t;
        Debug.Log("食物的位置" + temp.transform.position );
    }
    /*食物*/

    public void FoundWater()
    {
        /*該玩家回合才可以做事情*/
        if (PhotonNetwork.IsMasterClient)
        {
            if (tempWater == null)
            {
                tempWater = PhotonNetwork.Instantiate(waterPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                tempWater.name = "bowlHasWater";
                tempWater.transform.parent = waterTarget.transform;
                tempWater.transform.localPosition = new Vector3(0, 0, 0);
                tempWater.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                photonView.RPC("MasterFoundWater", RpcTarget.Others, waterTarget.transform.position);
                tasks.pushTask(tempWater);
            }
        }
    }
    [PunRPC]
    public void MasterFoundWater(Vector3 t)
    {
        Debug.Log(t);
        Debug.Log("收到訊息");
        GameObject temp = GameObject.Find("bowlHasWater(Clone)");
        if (temp != null)
            Debug.Log(temp + "不為空");
        temp.transform.position = t;
        Debug.Log("水的位置" + temp.transform.position);
    }

    public void FoundBall()
    {
        /*該玩家回合才可以做事情*/
        if (PhotonNetwork.IsMasterClient)
        {
            if (tempBall == null)
            {
                tempBall = PhotonNetwork.Instantiate(ballPrefab.name, Camera.main.transform.position, Quaternion.identity);
                tempBall.name = "toy_ball(Clone)";
                tempBall.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                print("找到球了");
                tasks.pushTask(tempBall);
            }
        }
    }
    public void FoundBone()
    {
        /*該玩家回合才可以做事情*/
        if (PhotonNetwork.IsMasterClient)
        {
            if (tempBone == null)
            {
                tempBone = PhotonNetwork.Instantiate(bonePrefab.name, Camera.main.transform.position, Quaternion.identity);
                tempBone.name = "toy_bone(Clone)";
                tempBone.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                print("找到骨頭了");
                tasks.pushTask(tempBone);
            }
        }
    }
    public void FoundGuard()
    {
        /*該玩家回合才可以做事情*/
        if (PhotonNetwork.IsMasterClient)
        {
            if (tempGuard == null)
            {
                tempGuard = PhotonNetwork.Instantiate(guardPrefab.name, Camera.main.transform.position, Quaternion.identity);
                tempGuard.name = "toy_gourd(Clone)";
                tempGuard.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                print("找到球了");
                tasks.pushTask(tempGuard);
            }
        }
    }

    public void FoundScratch()
    {
        /*該玩家回合才可以做事情*/
        if (PhotonNetwork.IsMasterClient)
        {
            if (tempScratch == null)
            {
                tempScratch = PhotonNetwork.Instantiate(scratchPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                tempScratch.name = "toy_scratch";
                tempScratch.transform.parent = scratchTarget.transform;
                tempScratch.transform.localPosition = new Vector3(0, 0, 0);
                tempScratch.transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);
                tempScratch.transform.localRotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, -90.0f));
                photonView.RPC("MasterFoundScratch", RpcTarget.Others, scratchTarget.transform.position);
                tasks.pushTask(tempScratch);
            }
        }
    }
    [PunRPC]
    public void MasterFoundScratch(Vector3 t)
    {
        Debug.Log(t);
        Debug.Log("收到訊息");
        GameObject temp = GameObject.Find("toy_scratch(Clone)");
        if (temp != null)
            Debug.Log(temp + "不為空");
        temp.transform.position = t;
        Debug.Log("抓板的位置" + temp.transform.position);
    }

    public void FoundJump()
    {
        /*該玩家回合才可以做事情*/
        if (PhotonNetwork.IsMasterClient)
        {
            if (tempJump == null)
            {
                tempJump = PhotonNetwork.Instantiate(jumpPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                tempJump.name = "toy_jump";
                tempJump.transform.parent = jumpTarget.transform;
                tempJump.transform.localPosition = new Vector3(0, 0, 0);
                tempJump.transform.localScale = new Vector3(0.024f, 0.024f, 0.024f);
                tempJump.transform.localRotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, -90.0f));
               

                Vector3 jump1 = new Vector3(6, 9, 1);
                Vector3 jump2 = new Vector3(-36, 10, 26);
                Vector3 jump3 = new Vector3(-12, -12, 47);

                GameObject jumpPos0 = new GameObject();
                GameObject jumpPos1 = new GameObject();
                jumpPos0.AddComponent<PhotonView>();
                jumpPos0.transform.SetParent(tempJump.transform);
                jumpPos0.transform.localPosition = jump1;
                jumpPos0.name = "jumpTask1";


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

                jumpPos1.transform.SetParent(tempJump.transform);
                jumpPos1.transform.localPosition = temp;
                jumpPos1.name = "jumpTask2";
                jumpPos1.AddComponent<PhotonView>();
                GameObject jumpPos2 = new GameObject();
                jumpPos2.transform.SetParent(tempJump.transform);
                jumpPos2.transform.localPosition = jump1;
                jumpPos2.name = "jumpTask3";
                jumpPos2.AddComponent<PhotonView>();
                photonView.RPC("MasterFoundJump", RpcTarget.Others, jumpTarget.transform.position);

                tasks.pushTask(jumpPos0);
                tasks.pushTask(jumpPos1);
                tasks.pushTask(jumpPos2);
            }
        }
    }
    [PunRPC]
    public void MasterFoundJump(Vector3 t)
    {
        Debug.Log(t);
        Debug.Log("收到訊息");
        GameObject temp = GameObject.Find("toy_jump(Clone)");
        if (temp != null)
            Debug.Log(temp + "不為空");
        temp.transform.position = t;
        Debug.Log("抓板的位置" + temp.transform.position);
    }

    public void changeMaster()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("changeMasterRPC", RpcTarget.Others);
        }
    }

    [PunRPC]
    public void changeMasterRPC()
    {
        Player[] playerList = PhotonNetwork.PlayerList;
        int numOfPlyaers = PhotonNetwork.PlayerList.Length;
        int i = 0;
        RPCMessage.text = "正在執行RPCFuncition";

        do
        {
            i = Random.Range(0, numOfPlyaers);
        } while (playerList[i].IsMasterClient);
        RPCMessage.text = "成功切換MasterClient給" + playerList[i].NickName;
        PhotonNetwork.SetMasterClient(playerList[i]);
    }

  


}

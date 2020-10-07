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

    private GameObject tempCat;
    private GameObject tempFood;
    PhotonView PV;

    public Text roundPlayer;
    public Text RPCMessage;

    bool isFoundTarget = false;
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
        Player[] list = PhotonNetwork.PlayerList;
 
   
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

    public void FoundFood()
    {
        /*該玩家回合才可以做事情*/
        if(PhotonNetwork.IsMasterClient)
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
        //tasks.pushTask(tempFood);
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

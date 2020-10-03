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
    private GameObject Food;

    private GameObject temp;
    PhotonView PV;

    public Text roundPlayer;
    public Text RPCMessage;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.MasterClient.NickName);   
        //PhotonNetwork.Instantiate(this.Food.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        roundPlayer.text = "現在是" + PhotonNetwork.MasterClient.NickName + "的回合";
        Player[] list = PhotonNetwork.PlayerList;
        Debug.Log(PhotonNetwork.MasterClient.NickName);
        foreach(Player x in list)
        {
            Debug.Log(x.NickName + "//");
        }
    }

    public void foundCat()
    {
        temp = Instantiate<GameObject>(catPrefab);
        temp.transform.parent = catTarget.transform;
        temp.transform.localPosition = new Vector3(0f, 0f, 0f);
        Debug.Log("找到貓了");
        
    }
    public void notFountCat()
    {
        Destroy(temp);

        
        Debug.Log("沒找到貓");

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

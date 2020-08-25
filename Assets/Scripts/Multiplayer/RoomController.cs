using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;


public class RoomController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 玩家離開遊戲室時, 把他帶回到遊戲場入口
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("RoomSearchingScene");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}

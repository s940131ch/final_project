using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.Demo.Cockpit;

public class NetworkController : MonoBehaviourPunCallbacks , ILobbyCallbacks
{

    public Text connectMessage;


    public InputField roomName;
    public InputField roomSize;
   


    TypedLobby Lobby;

    public Transform roomsPanel;
    public GameObject roomListingPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            Debug.Log("已經連線");
            connectMessage.text = "連線狀態:已連線";
        }    
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("連線至伺服器中");
            connectMessage.text = "連線狀態:連線中";
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("安安你好" + PhotonNetwork.CloudRegion + "server!");
        PhotonNetwork.AutomaticallySyncScene = true;
        connectMessage.text = "連線狀態:已連線";
        Lobby = new TypedLobby("大廳A",default);
        PhotonNetwork.JoinLobby(Lobby);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        connectMessage.text = "連線狀態:已斷線";
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        connectMessage.text = "連線狀態:已連線\n"+"目前大廳:" + Lobby.Name;
    }


    //建立房間
    public void CreateRoom()
    {
        int size = int.Parse(roomSize.text);
        string name = roomName.text;

        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)size };
        PhotonNetwork.CreateRoom(name, roomOps);
      
    }
   

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("房間建立成功");
    }



    //當加入房間
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("已加入房間");
        Debug.Log(PhotonNetwork.CurrentRoom);
    }
    //加入房間失敗
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("加入房間失敗 請重試");

    }
    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("房間狀態更新");
        RemoveOldRoom();
        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }
    
    private void RemoveOldRoom()
    {
        int childCount = roomsPanel.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }
    private void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
        
    }
   
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkConnector : Photon.MonoBehaviour {


    public bool dontDestroyOnLoad = true;


    private bool keepConnected;
    private ClientState lastConnectionState;



    // Use this for initialization
    void Start () {
        keepConnected = true;
        Connect();

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
    }


	
	// Update is called once per frame
	void Update () {

        UpdateConnectionState();

	}

 

    protected virtual void Connect()
    {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("0.1");
        lastConnectionState = PhotonNetwork.connectionStateDetailed;
    }


    private void UpdateConnectionState()
    {
        if (!keepConnected)
            return;

        if (PhotonNetwork.connectionStateDetailed != lastConnectionState)
        {
            Logger.Write(PhotonNetwork.connectionStateDetailed.ToString());
        }

        if (PhotonNetwork.connectionStateDetailed == ClientState.Joined)
        {

        }

        lastConnectionState = PhotonNetwork.connectionStateDetailed;
      }

    void OnConnectedToPhoton()
    {
        Logger.Write("ConnectedToPhoton");
    }


    public void OnJoinedLobby()
    {
        Logger.Write("JoinedLobby.");
        Logger.Write("Network:"+PhotonNetwork.countOfPlayers+"players online." );
    }

    public void OnReceivedRoomListUpdate()
    {
        if (PhotonNetwork.countOfPlayers < 2)
            TryCreateRoom();
        else
            TryJoinRoom();

    }

    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Logger.Write("FailedToConnectToPhoton");
    }

    public void OnDisconnectedFromPhoton()
    {
        Logger.Write("DisconnectedFromPhoton");
    }



    protected virtual void  TryCreateRoom()
    {

        string roomName = "Room" + (PhotonNetwork.countOfRooms + 1).ToString();

        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            if (room.Name==roomName)
            {
                PhotonNetwork.JoinRoom(room.Name);
                return;
            }
        }

        if (PhotonNetwork.CreateRoom(roomName))
        {
            Logger.Write("Create room succeed.");
        }


    }

    protected virtual bool TryJoinRoom()
    {
        return PhotonNetwork.JoinRoom("Room" + PhotonNetwork.countOfRooms.ToString());
    }

    public void OnJoinedRoom()
    {
        Logger.Write("Join room succeed.");
        photonView.RPC("RequestInitializeNetworkGame", PhotonTargets.MasterClient,PhotonNetwork.player);
    }



}

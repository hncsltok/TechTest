using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster: Photon.MonoBehaviour {

    public string currentLevel = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    [PunRPC]
    public void RequestInitializeNetworkGame(PhotonPlayer player)
    {
        Logger.Write("RequestInitializeNetworkGame");
        if (!PhotonNetwork.isMasterClient)
            return;

        ResponseLoadLevel(player);


    }

   

    private void ResponseLoadLevel(PhotonPlayer player)
    {
        photonView.RPC("ReceiveLoadLevel", player, currentLevel);

    }


    [PunRPC]
    private void RequestLoadLevelFinish(PhotonPlayer player)
    {
        ResponseSpwanActor(player);
    }



    private void ResponseSpwanActor(PhotonPlayer player)
    {
        photonView.RPC("ReceiveSpwanActor", player);
    }




}

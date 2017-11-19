using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClient : Photon.MonoBehaviour {

    [SerializeField]
    private Transform spawnPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 

    [PunRPC]
    private void ReceiveLoadLevel(string levelName,PhotonMessageInfo info)
    {
        if (info.sender != PhotonNetwork.masterClient)
            return;

        StartCoroutine( PhotonNetwork.LoadLevelAsync(levelName, LoadLevelFinish));

        
        //加载失败
        if (!PhotonNetwork.networkingPeer.loadingLevelAndPausedNetwork)
        {
            Debug.LogError("Error (" + this + ") Failed to load level: '" + levelName + "'. You may need to 1) add it to the Build Settings, or 2) verify the default level name on the master component.");
            return;
        }

       

    }

    private void LoadLevelFinish()
    {
        photonView.RPC("RequestLoadLevelFinish", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [PunRPC]
    private void ReceiveSpwanActor()
    {
        spawnPoint= GameObject.Find("SpwanPoint").transform;//找到出生点；如果没有加载完成场景，则可能找不到
        PhotonNetwork.Instantiate("Actor", spawnPoint.position, spawnPoint.rotation,0);

        Logger.Write("Spawn a actor");
    }




}

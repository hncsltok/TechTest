using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class NetworkActor : Photon.MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float healthPoint;

    private BackPack backPack;


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.root.gameObject);
        healthPoint = 100;
        backPack = new BackPack(photonView.viewID);

        if (photonView.isMine)
        {
            backPack.AddItem(new Item("sword"));
            backPack.AddItem(new Item("shield"));
            backPack.AddItem(new Item("arrow"));
            TransmitUpdateBackPack();
            //backPack.ShowItems();
        }
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

#region Fire

    public  void Fire()
    {
        Vector3 forward= transform.position + transform.forward * 2;
        GameObject instBullet = Instantiate(bullet,forward,transform.rotation);
        TransmitFire();
    }

    private void TransmitFire()
    {
        photonView.RPC("ReceiveFire", PhotonTargets.Others);
    }

    [PunRPC]
    private void ReceiveFire()
    {
        Vector3 forward = transform.position + transform.forward * 2;
        GameObject instBullet = Instantiate(bullet, forward, transform.rotation);
    }

    #endregion

#region TakeDamage

    public void TakeDamage()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        healthPoint--;
        Logger.Write("I'm taking damage! HP:"+healthPoint);
        photonView.RPC("TransmitTakeDamage", PhotonTargets.Others,healthPoint);
    }

    [PunRPC]
    private void TransmitTakeDamage(float hp)
    {
        healthPoint = hp;
        Logger.Write("I'm taking damage!HP:" + healthPoint);

    }

#endregion

#region Backpack

    private void TransmitUpdateBackPack()
    {
        string bp = JsonMapper.ToJson(backPack);
        //Logger.Write(bp);
        photonView.RPC("ReceiveUpdateBackPack", PhotonTargets.AllBuffered, bp);


    }

    [PunRPC]
    private void ReceiveUpdateBackPack(string bp)
    {
        Logger.Write(bp);
        BackPack backpack=JsonMapper.ToObject<BackPack>(bp);
        if (backpack != null)
        {
            backPack = backpack;
        }         
        else
            Logger.Write("backpack is null!");
        backPack.ShowItems();
    }

#endregion




}

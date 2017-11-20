using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

[RequireComponent(typeof( CharacterController))]
public class ActorActionManager : MonoBehaviour {

    public bool offlineMode;


    private bool controllable;
    private Vector3 lastPosition;

    private  CharacterController characterController;
    private NetworkActor networkActor;
    private AimIK aimIK;
    private Animator animator;


	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
        networkActor=GetComponent<NetworkActor>();
        aimIK = GetComponent<AimIK>();
        animator = GetComponent<Animator>();


        lastPosition = transform.position;

        if (!offlineMode)
            if (!networkActor.photonView.isMine)
            {
                controllable = false;
                return;
            }
        controllable = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(controllable)
        {
            ControlMovement();
            ControlRotation();
            ControlFire();
        }
        else
        {
            CalculateSpeed();
        }
        
            
        
    }

    private void LateUpdate()
    {
        if(controllable)    
            ControlAim();
    }

    #region OwnerController

    private void ControlMovement()
    {
        float moveSpeed = 0.5f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //修正摄像机角度
        float angle= -Camera.main.transform.eulerAngles.y;
        float finalHorizontal = Mathf.Cos(angle*Mathf.PI/180) * horizontal - Mathf.Sin(angle * Mathf.PI / 180) * vertical;
        float finalVertical = Mathf.Sin(angle * Mathf.PI / 180) * horizontal + Mathf.Cos(angle * Mathf.PI / 180) * vertical;

        //Debug.Log("sita:"+sita+"       "+ Mathf.Cos(sita) + "," + Mathf.Sin(sita));

        Vector3 moveDelta = new Vector3(finalHorizontal, 0, finalVertical);
        moveDelta = moveDelta * moveSpeed;

        characterController.Move(moveDelta);
    }

    

    private void ControlRotation()//通过世界空间计算
    {
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo))
        {
            Vector3 temp = hitInfo.point - transform.position;
            temp.y = 0;
            Quaternion targetDirection = Quaternion.LookRotation(temp);
            float horizontalAngle= Quaternion.Angle( transform.rotation, targetDirection);


            if (Vector3.Cross(transform.forward, temp.normalized).y < 0)
                horizontalAngle = -horizontalAngle;

            animator.SetFloat("HorizontalAngle", horizontalAngle);


            
            //characterController.transform.rotation = Quaternion.Lerp(characterController.transform.rotation, targetDirection,0.1f) ;
        }

    }





    private Vector3 lastAimPoint=Vector3.zero;
    private void ControlAim()
    {
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);


        aimIK.solver.IKPosition = Vector3.Lerp(lastAimPoint, hitInfo.point,0.2f);
        lastAimPoint = aimIK.solver.IKPosition;
    }


    private void ControlFire()
    {
        if (Input.GetButtonDown("Fire1"))
            if(!offlineMode)
                networkActor.Fire();
    }

    #endregion

 #region RemoteUpdate

    private void CalculateSpeed()
    {
        Vector3 velocity = transform.position - lastPosition;
        
        Debug.Log(velocity);

        lastPosition = transform.position;
    }

#endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActorAnimationManager : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMovementAnimation();
        UpdateRotationAnimation();
    }

    private Vector3 lastPostion = Vector3.zero;
    private void UpdateMovementAnimation()
    {
        Vector3 moveDelta = transform.position - lastPostion;
        lastPostion = transform.position;

        float forwardSpeed = Vector3.Dot(transform.forward, moveDelta.normalized) * moveDelta.magnitude/Time.deltaTime;
        float rightSpeed = Vector3.Dot(transform.right, moveDelta.normalized) * moveDelta.magnitude / Time.deltaTime;


        animator.SetFloat("ForwardSpeed", forwardSpeed );
        animator.SetFloat("RightSpeed", rightSpeed );
        if (forwardSpeed != 0 && rightSpeed != 0)
            animator.SetBool("Moving", true);
        else
            animator.SetBool("Moving", false);

    }

    private Quaternion lastRotation = Quaternion.identity;
    private void UpdateRotationAnimation()
    {
        float horizontalAngle = Quaternion.Angle(transform.rotation, lastRotation)/Time.deltaTime;
        lastRotation = transform.rotation;
        Vector3 targetDirect = GetComponent<ActorActionManager>().GetAimPoint()-transform.position;
        if (Vector3.Cross(transform.forward, targetDirect.normalized).y < 0)
            horizontalAngle = -horizontalAngle;

        animator.SetFloat("HorizontalAngle", horizontalAngle);

    }

    



}

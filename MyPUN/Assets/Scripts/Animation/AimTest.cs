using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTest : MonoBehaviour {

    public Vector3 offset;

    private Animator animator;
    private Transform chest;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray,out hitInfo);
        chest.LookAt(hitInfo.point);
        chest.rotation = chest.rotation * Quaternion.Euler(offset);
	}






}

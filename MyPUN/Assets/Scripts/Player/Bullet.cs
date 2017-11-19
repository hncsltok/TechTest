using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private float maxLifeTime = 10;

    private float lifeTime=0;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        Fly();

    }
	
	// Update is called once per frame
	void Update () {

        if (lifeTime <= maxLifeTime)
        {
            lifeTime += Time.deltaTime;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }

    }


    private void Fly()
    {
        rigidBody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<NetworkActor>().TakeDamage();
        }
        Destroy(this);
    }


}

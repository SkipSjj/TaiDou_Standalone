using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private int damage = 130;
    private float moveSpeed = 1.5f;
    private float force = 500f;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
    {
		transform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * force);
            Destroy(this.gameObject,0.5f);
        }
    }
}

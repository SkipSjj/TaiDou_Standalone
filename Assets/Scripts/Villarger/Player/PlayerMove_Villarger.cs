using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove_Villarger : MonoBehaviour
{
    public SimpleTouchController TouchController { get; set; }
    public float velocity = 15f;
    public float speed = 20f;

    private Animator anim;
    private new Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
	{
	    anim = this.GetComponent<Animator>();
	    rigidbody = this.GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        if (TouchController != null)
        {
            h = TouchController.GetTouchPosition.x;
            v = TouchController.GetTouchPosition.y;
        }

        Vector3 velTemp = rigidbody.velocity;
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            PlayerAutoMove.instance.agent.enabled = false;
            anim.SetBool("isRun", true);
            Quaternion temp = Quaternion.LookRotation(new Vector3(-v, 0, h));
            transform.rotation = Quaternion.Lerp(this.transform.rotation, temp, Time.deltaTime * speed);
            rigidbody.velocity = new Vector3(-v * velocity, velTemp.y, h * velocity);
        }
        else if(!PlayerAutoMove.instance.agent.enabled)
        {
            anim.SetBool("isRun", false);
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity,Vector3.zero, Time.deltaTime * speed);
        }
    }

	// Update is called once per frame
	void Update ()
	{
	    
    }
}

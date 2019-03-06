using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_GameLevel : MonoBehaviour
{
    private Animator anim;
    private new Rigidbody rigidbody;
    private bool isMove = false;
    private PlayerAttack playerAttack;

    private float velocity = 10f;
    public bool isCanControl = true;  //表示是否可以再操控主角
    public SimpleTouchController touchController;

	// Use this for initialization
	void Start ()
	{
	    anim = this.GetComponent<Animator>();
	    rigidbody = this.GetComponent<Rigidbody>();
	    playerAttack = this.GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Move();
	}

    void Move()
    {
        if (!isCanControl) return;
        if (playerAttack == null || playerAttack.hp <= 0) return;
        if (touchController == null) return;

        float h = 0f;
        float v = 0f;

        h = touchController.GetTouchPosition.x;
        v = touchController.GetTouchPosition.y;
        
        Vector3 oldVel = rigidbody.velocity;
        if ((Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f) && anim.GetCurrentAnimatorStateInfo(1).IsName("EmptyState"))
        {
            anim.SetBool("isMove", true);
            Quaternion temp = Quaternion.LookRotation(new Vector3(-v, 0, h));
            transform.rotation = Quaternion.Lerp(this.transform.rotation, temp, Time.deltaTime * 20f);
            rigidbody.velocity = new Vector3(velocity * h, oldVel.y, velocity * v);
            transform.LookAt(new Vector3(h, 0, v) + transform.position);
        }
        else
        {
            anim.SetBool("isMove", false);
            rigidbody.velocity = new Vector3(0, oldVel.y, 0);
        }
    }
}

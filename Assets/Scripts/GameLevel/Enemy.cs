using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject damgeEffect;
    public int hp = 100;
    public float speed = 0.5f;
    public float attackRate = 2f;
    public float attackDistance = 3f;
    public int damage = 20;
    public float downSpeed = 0.1f;
    public float idelDistance = 6f;
    public string guid;

    private int totalHp = 0;
    private float downDistance = 0f;
    private float attackTime = 0f;
    private float distance = 0f;
    private Transform bloodPoint;
    private Animation animation;
    private GameObject targetGo;
    private CharacterController characterController;

	// Use this for initialization
	void Start ()
	{
	    GameLevelManager.instance.AddEnemy(this.gameObject);
	    totalHp = hp;
	    bloodPoint = this.transform.Find("BloodPos");
	    animation = this.GetComponent<Animation>();
	    characterController = this.GetComponent<CharacterController>();
        InvokeRepeating("CalcDistance",0f,0.1f);
	    targetGo = GameLevelManager.instance.playerGo;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hp <= 0)
        {
            downDistance += downSpeed * Time.deltaTime;
            transform.Translate(-transform.up * downSpeed * Time.deltaTime);
            if (downDistance > 3f)
            {
                Destroy(this.gameObject);
            }
            return;
        }
        
        if (distance <= attackDistance)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= attackRate)
            {
                //进行攻击主角
                AttackPlayer();
            }
            if (!animation.IsPlaying("attack01"))
            {
                animation.CrossFade("idle");
            }
        }
        else if(distance > idelDistance)
        {
            animation.CrossFade("idle");
        }
        else
        {
            if (!animation.IsPlaying("takedamage"))
            {
                animation.CrossFade("walk");
                Move();
            }
            else
            {
                animation.CrossFade("takedamage");
            }
        }
	}

    public void TakeDamage(string args)
    {
        if (hp <= 0) return;

        Combo.instance.ShowCombo();
        string[] proArray = args.Split(',');
        int damage = int.Parse(proArray[0]);
        hp -= damage;

        this.GetComponent<HpBar>().GetHpBarGo().transform.Find("Forward").GetComponent<Image>().fillAmount = (float)hp / totalHp;

        animation.Play("takedamage");
        this.GetComponent<DamageShow>().CreateDamageText(damage + "");
        float backDistance = float.Parse(proArray[1]);

        //iTween.MoveBy(this.gameObject,(-GameLevelManager.instance.playerGo.transform.forward) * backDistance,0.3f);
        Vector3 tempVel = -transform.forward * backDistance;
        this.GetComponent<CharacterController>().SimpleMove(tempVel * 15f);

        GameObject.Instantiate(damgeEffect, bloodPoint.transform.position, Quaternion.identity);

        if (hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        GameLevelManager.instance.RemoveEnemy(guid);
        this.GetComponent<CharacterController>().enabled = false;
        if (GameLevelManager.instance.GetEnemyList().Count <= 0)
        {
            GameLevelManager.instance.CurrenTrigger.OpenNextTrigger();
        }
        
        animation.Play("die");
    }

    private void Move()
    {
        transform.LookAt(targetGo.transform.position);

        if (distance > 8f)
        {
            animation.Play("idle");
            return;
        }
        characterController.SimpleMove(transform.forward * speed);
    }

    //计算该物体和主角之间的距离
    private void CalcDistance()
    {
        targetGo = GameLevelManager.instance.playerGo;
        distance = Vector3.Distance(targetGo.transform.position, this.transform.position);
    }

    private void AttackPlayer()
    {
        if (targetGo == null)
        {
            targetGo = GameLevelManager.instance.playerGo;
        }
        
        this.transform.LookAt(targetGo.transform.position);
        animation.Play("attack01");
        attackTime = 0f;
        targetGo.SendMessage("TakeDamage",damage,SendMessageOptions.DontRequireReceiver);
    }
}

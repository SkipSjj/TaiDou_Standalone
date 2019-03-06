using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float viewAngle = 50f;
    public float rotateSpeed = 0.5f;
    public float attackDistance = 2f;
    public float moveSpeed = 2f;
    public float timeInterval = 1f;
    public int[] damageArray;
    public GameObject attack03Pre;
    public int hp;
    public string guid;
    public GameObject targetGo;

    private Animation anim;
    private float timer = 0f;
    private bool isAttacking = false;
    private int attackIndex = 0;
    private Transform attack03Pos;
    private float downDistance = 0f;
    private int totalHp;
    private new Renderer renderer;

	// Use this for initialization
	void Start ()
	{
	    totalHp = hp;
	    targetGo = GameLevelManager.instance.playerGo;
        GameLevelManager.instance.AddEnemy(this.gameObject);
	    anim = this.GetComponent<Animation>();
	    renderer = this.transform.Find("Object01").GetComponent<Renderer>();
	    attack03Pos = this.transform.Find("Attack03Pos").transform;

	    BossHpBar.instance.Show(hp);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (hp <= 0) return;

	    renderer.material.color = Color.Lerp(renderer.material.color, new Color(0.258f, 0.258f, 0.258f, 1f), Time.deltaTime);

	    if (anim.IsPlaying("hit")) return;

	    Move();
	}

    private void Move()
    {
        Vector3 tempPos = targetGo.transform.position;
        tempPos.y = this.transform.position.y;
        float angle = Vector3.Angle(tempPos - transform.position, transform.forward);
        if (angle <= viewAngle)
        {
            float distance = Vector3.Distance(tempPos, transform.position);
            if (distance <= attackDistance)
            {
                if (!isAttacking)
                {
                    anim.CrossFade("idle");
                    timer += Time.deltaTime;
                    if (timer >= timeInterval)
                    {
                        timer = 0f;
                        Attack();
                    }
                }
            }
            else
            {
                //进行追击主角
                isAttacking = false;
                anim.CrossFade("walk");
                this.GetComponent<Rigidbody>().MovePosition(this.transform.position + transform.forward * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            //主角在视野之外，需要进行转向
            anim.CrossFade("walk");
            Quaternion targetRotation = Quaternion.LookRotation(tempPos - this.transform.position);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            isAttacking = false;
        }
    }

    private void Attack()
    {
        isAttacking = true;
        attackIndex = Random.Range(1, 4);
        anim.CrossFade("attack0" + attackIndex);
    }

    public void TakeDamage(string args)
    {
        if (hp <= 0) return;

        isAttacking = false;
        Combo.instance.ShowCombo();
        string[] proArray = args.Split(',');
        hp -= int.Parse(proArray[0]);
        BossHpBar.instance.UpdateShow(hp);
        this.GetComponent<DamageShow>().CreateDamageText(proArray[0]);

        anim.CrossFade("takedamage");

        renderer.material.color = Color.red;

        if (hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        GameLevelManager.instance.RemoveEnemy(guid);

        anim.CrossFade("die");

        ResultMask.instance.Show("战斗胜利！！！");
        GameController.instance.isBossDie = true;

        Destroy(this.gameObject,2f);
    }

    void PlayerAttack03Effect()
    {
        GameObject go = Instantiate(attack03Pre, attack03Pos.position, attack03Pos.rotation);
        go.transform.position = go.transform.position + new Vector3(0, 1f, 0f);
    }

    void PlayIdleAnim()
    {
        isAttacking = false;
        //anim.CrossFade("idle");
    }
}

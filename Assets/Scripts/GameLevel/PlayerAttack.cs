using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public EffectSettings[] effectArray;
    public GameObject[] iceEffect;
    public float attackForwardDistance = 5f;
    public float attackAroundDistance = 6f;
    [NonSerialized]
    public int totalHp;
    public int hp;
    [NonSerialized]
    public int totalMagic;
    public int magic;

    public delegate void OnPlayerHpMagicChangeEvent();
    public event OnPlayerHpMagicChangeEvent OnPlayerHpMagicChange;

    private Animator animator;
    private GameObject hudTextGo;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
	{
	    totalHp = hp = PlayerInfor.instance.hp;
	    totalMagic = magic = PlayerInfor.instance.Magic;
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (hp > 0) return;

        hp = 0;
        if (OnPlayerHpMagicChange != null)
        {
            OnPlayerHpMagicChange();
        }
        animator.SetTrigger("Dead");
    }

    void Attack(string args)
    {
        string[] proArray = args.Split(',');
        string posType = proArray[0];
        //声音名字
        string soundName = proArray[1];
        SoundManager.instance.Play(soundName);

        float moveForward = float.Parse(proArray[2]);
        if (moveForward > 0.1f)
        {
            //iTween.MoveBy(this.gameObject, Vector3.forward * moveForward, 0.3f);
            this.GetComponent<Rigidbody>().velocity = transform.forward * moveForward * 5f;
        }
        
        Skill skill;
        switch (posType)
        {
            case "Normal":
                ArrayList list = GetEnemyRange("Forward");
                if (list.Count <= 0) break;
                
                foreach (GameObject go in list)
                {
                    go.SendMessage("TakeDamage",PlayerInfor.instance.damage * Random.Range(1, 2)
                    + "," + proArray[2],SendMessageOptions.DontRequireReceiver);
                }
                break;
            case "Skill1":
                ArrayList list1 = GetEnemyRange("Range");
                if (list1.Count <= 0) break;
                
                GameController.instance.skillDict.TryGetValue("One", out skill);
                foreach (GameObject go in list1)
                {
                    go.SendMessage("TakeDamage", (int)(skill.Damage * Random.Range(1f, 1.5f)) + "," + proArray[2], SendMessageOptions.DontRequireReceiver);
                }
                break;
            case "Skill2":
                ArrayList list2 = GetEnemyRange("Range");
                if (list2.Count <= 0) break;

                GameController.instance.skillDict.TryGetValue("Two", out skill);
                foreach (GameObject go in list2)
                {
                    go.SendMessage("TakeDamage", (int)(skill.Damage * Random.Range(1f, 1.5f)) + "," + proArray[2], SendMessageOptions.DontRequireReceiver);
                }
                break;
            case "Skill3":
                ArrayList list3 = GetEnemyRange("Range");
                if (list3.Count <= 0) break;

                GameController.instance.skillDict.TryGetValue("Three", out skill);
                foreach (GameObject go in list3)
                {
                    go.SendMessage("TakeDamage", (int)(skill.Damage * Random.Range(1f, 1.5f)) + "," + proArray[2], SendMessageOptions.DontRequireReceiver);
                }
                break;
        }
    }

    //显示普通攻击中最后一击的特效
    void ShowEffectDevilHand()
    {
        ArrayList arrayList = GetEnemyRange("Forward");
        foreach (GameObject go in arrayList)
        {
            RaycastHit hit;
            bool collider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground"));
            if (collider)
            {
                GameObject.Instantiate(effectArray[0], hit.point, Quaternion.identity);
            }
        }
    }

    //显示飞鸟的特效
    void ShowEffectFireBird()
    {
        ArrayList arrayList = GetEnemyRange("Around");
        foreach (GameObject go in arrayList)
        {
            GameObject goEffect = GameObject.Instantiate(effectArray[1]).gameObject;
            goEffect.transform.position = this.transform.position + Vector3.up;
            goEffect.GetComponent<Destroy>().go = go.transform.Find("BloodPos").gameObject;
            goEffect.GetComponent<EffectSettings>().Target = go.transform.Find("BloodPos").gameObject;
        }
    }

    //显示火的特效
    void ShowEffectFire()
    {
        ArrayList arrayList = GetEnemyRange("Forward");
        foreach (GameObject go in arrayList)
        {
            RaycastHit hint;
            bool colider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hint,10f, LayerMask.GetMask("Ground"));
            if (colider)
            {
                GameObject.Instantiate(effectArray[2], hint.point, Quaternion.identity);
            }
        }
    }

    //显示技能二的冰的特效
    void ShowEffectIce(int index)
    {
        iceEffect[index].SetActive(false);
        iceEffect[index].SetActive(true);

        StartCoroutine(EnableEffect(index));
    }

    IEnumerator EnableEffect(int index)
    {
        yield return new WaitForSeconds(0.3f);

        iceEffect[index].SetActive(false);
    }

    ArrayList GetEnemyRange(string range)
    {
        if (GameLevelManager.instance.GetEnemyList().Count <= 0) return new ArrayList();

        ArrayList arrayList = new ArrayList();
        if (range == "Forward")
        {
            foreach (GameObject go in GameLevelManager.instance.GetEnemyList())
            {
                Vector3 tempVec = go.transform.position - this.transform.position;
                tempVec = new Vector3(tempVec.x,0f,tempVec.z);
                float dotValue = Vector3.Dot(transform.forward.normalized, tempVec.normalized);

                if (dotValue > 0)
                {
                    float distance = Vector3.Distance(this.transform.position, go.transform.position);
                    if (distance <= attackForwardDistance)
                    {
                        arrayList.Add(go);
                    }
                }
            }
        }
        else
        {
            foreach (var go in GameLevelManager.instance.GetEnemyList())
            {
                float distance = Vector3.Distance(this.transform.position, go.transform.position);
                if (distance <= attackAroundDistance)
                {
                    arrayList.Add(go);
                }
            }
        }
        return arrayList;
    }

    void TakeDamage(int damage)
    {
        if (hp <= 0) return;

        this.hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            ResultMask.instance.Show("战斗失败！！！");
        }
        if (OnPlayerHpMagicChange != null)
        {
            OnPlayerHpMagicChange();
        }
        animator.SetTrigger("TakeDamage");
    }

    public bool  GetMagic(int magic)
    {
        if (this.magic <= 0f || this.magic < magic) return false;

        this.magic -= magic;
        if (OnPlayerHpMagicChange != null)
        {
            OnPlayerHpMagicChange();
        }
        return true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Transform hpBarParent;
    public GameObject[] enemyPre;
    public Transform[] spawnPosArray;
    public bool isTrigger = true;
    public GameObject nextTrigger;
    public float delayTime = 2f;
    
    private bool isStartTwo = false;  //是否需要启动第二次生成敌人
    private bool flag = false;  //是否是第一次启动第二次生成敌人

    // Use this for initialization
    void Start ()
    {
        this.GetComponent<Collider>().isTrigger = isTrigger;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isStartTwo && flag && GameLevelManager.instance.GetEnemyList().Count <= 0)
        {
            StartCoroutine(SpawnEnemyTwice(2));
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SpawnEnemyFirst(0));
            GameLevelManager.instance.CurrenTrigger = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<Collider>().isTrigger = false;
        }
    }

    IEnumerator SpawnEnemyFirst(int index)
    {
        yield return new WaitForSeconds(delayTime);
        SpawnEnemy(index);

        if (spawnPosArray.Length >= 4)
        {
            flag = isStartTwo = true;
        }
    }

    IEnumerator SpawnEnemyTwice(int index)
    {
        flag = false;
        isStartTwo = false;
        yield return null;

        SpawnEnemy(index);
    }

    void SpawnEnemy(int index)
    {
        GameObject tempGo;
        for (int i = 0; i < 2; i++)
        {
            if (nextTrigger == null)
            {
                tempGo = Instantiate(enemyPre[Random.Range(0, enemyPre.Length - 1)], spawnPosArray[index].position, spawnPosArray[index].rotation);
            }
            else
            {
                tempGo = Instantiate(enemyPre[Random.Range(0, enemyPre.Length)], spawnPosArray[index].position, spawnPosArray[index].rotation);
            }
            
            string GUID = System.Guid.NewGuid().ToString();

            if (tempGo.GetComponent<Enemy>() != null)
            {
                Enemy enemy = tempGo.GetComponent<Enemy>();
                enemy.guid = GUID;
                tempGo.GetComponent<HpBar>().hpBarParent = hpBarParent;
            }
            tempGo.GetComponent<DamageShow>().damageParent = hpBarParent;
            index++;
        }
    }

    void SpawnBoss()
    {
        GameObject tempGo = Instantiate(enemyPre[enemyPre.Length - 1], spawnPosArray[Random.Range(0,spawnPosArray.Length)].position, Quaternion.identity);
        string GUID = System.Guid.NewGuid().ToString();
        tempGo.GetComponent<Boss>().guid = GUID;
        tempGo.GetComponent<DamageShow>().damageParent = hpBarParent;
    }

    public void OpenNextTrigger()
    {
        if (!flag)
        {
            if (nextTrigger == null)
            {
                Invoke("SpawnBoss", 2f);
                return;
            }

            nextTrigger.GetComponent<Collider>().isTrigger = true;
        }
    }
}

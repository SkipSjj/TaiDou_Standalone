using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;
    
    public GameObject playerGo;
    public GameObject[] playerPre;
    public Transform playerPos;
    public SimpleTouchController touchController;
    public EnemyTrigger CurrenTrigger { get; set; }

    private List<GameObject> enemyList = new List<GameObject>();

    void Awake()
    {
        instance = this;
        InitPlayer();
    }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void  InitPlayer()
    {
        int playerId = PlayerPrefs.GetInt("selectPlayerId");
        string filePath = PlayerPrefs.GetString("serverPath") + "PlayerData.json";
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(filePath);

        foreach (Player player in playerList)
        {
            if (player.id == playerId)
            {
                GameObject playerGo = Instantiate(player.isMan == 0 ? playerPre[0] : playerPre[1], playerPos.position, playerPos.rotation);
                this.playerGo = playerGo;
                playerGo.GetComponent<PlayerMove_GameLevel>().touchController = touchController;
                playerGo.transform.SetParent(playerPos); 
            }
        }

        
    }

    public List<GameObject> GetEnemyList()
    {
        return enemyList;
    }

    public void AddEnemy(GameObject enemyGo)
    {
        enemyList.Add(enemyGo);
    }

    public void RemoveEnemy(string guid)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            string enemyGuid;
            if (enemyList[i].GetComponent<Enemy>() != null)
            {
                enemyGuid = enemyList[i].GetComponent<Enemy>().guid;
            }
            else
            {
                enemyGuid = enemyList[i].GetComponent<Boss>().guid;
            }

            if (enemyGuid == guid)
            {
                enemyList.RemoveAt(i);
            }
        }
    }
}

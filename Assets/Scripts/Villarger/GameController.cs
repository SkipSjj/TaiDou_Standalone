using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Player Player { get; set; }
    public SimpleTouchController touchController;

    public Dictionary<string,Skill> skillDict = new Dictionary<string,Skill>();

    public GameObject[] playerPrefabs;
    public GameObject loadingBg;
    [NonSerialized]
    public bool isBossDie = false;

    private Transform playerPos;
    private Animator playerInforAnim;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this);
        playerPos = GameObject.Find("PlayerPos").transform;
        InitPlayer();
    }

	// Use this for initialization
	void Start ()
	{
        playerInforAnim = GameObject.Find("PlayerInforMask").GetComponent<Animator>();

	    PlayerInfor.instance.OnPlayerInforChange += OnPlayerInforChange;
	}
    
    // Update is called once per frame
	void Update ()
    {
		
	}

    public void InitPlayer()
    {
        if (playerPos == null)
        {
            playerPos = GameObject.Find("PlayerPos").transform;
        }
        int playerId = PlayerPrefs.GetInt("selectPlayerId");
        string filePath = PlayerPrefs.GetString("serverPath") + "PlayerData.json";
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(filePath);

        foreach (Player player in playerList)
        {
            if (player.id == playerId)
            {
                this.Player = player;
            }
        }
    
        GameObject playerGo = Instantiate(this.Player.isMan == 0 ? playerPrefabs[0] : playerPrefabs[1], playerPos.position, Quaternion.identity);
        playerGo.transform.parent = playerPos;
        playerGo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        playerGo.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        playerGo.GetComponent<PlayerMove_Villarger>().TouchController = touchController;
    }

    public void OnPlayerInforChange(InfoType infoType)
    {
        UpdatePlayerInfor();
    }

    public void UpdatePlayerInfor()
    {
        string path = PlayerPrefs.GetString("serverPath") + "PlayerData.json";
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(path);

        foreach (Player player in playerList)
        {
            if (player.id == Player.id)
            {
                player.playerName = Player.playerName;
                player.coin = Player.coin;
                player.diamond = Player.diamond;
                player.energy = Player.energy;
                player.magic = Player.magic;
                player.exp = Player.exp;
                player.level = Player.level;
            }
        }

        JsonTools.WriteJsonFile(playerList, path);
    }

    public void ShowPlayerInforBg()
    {
        if (playerInforAnim == null)
        {
            playerInforAnim = GameObject.Find("PlayerInforMask").GetComponent<Animator>();
        }
        playerInforAnim.SetBool("isShow", true);
    }

    public void HidePlayerInforBg()
    {
        playerInforAnim.SetBool("isShow", false);
    }

    public static int GetRequireExp(int level)
    {
        //print(100 + 10 * (level - 2));
        return 100 + 10 * (level - 2);
    }

    public void OnBossDie()
    {
        int exp = 0;
        int coin = 0;
        int diamond = 0;

        int selectedLevelId = PlayerPrefs.GetInt("SelectedLevelId", -1);

        if (selectedLevelId != -1)
        {
            string path = PlayerPrefs.GetString("serverPath") + "GameLevelData.json";
            List<GameLevelInfor> levelInforList = JsonTools.AnalyticalJsonFile<List<GameLevelInfor>>(path);
             
            if (levelInforList != null)
            {
                foreach (GameLevelInfor infor in levelInforList)
                {
                    if (infor.playerId == Player.id && selectedLevelId == infor.id)
                    {
                        exp += infor.exp;
                        coin += infor.coin;
                        diamond += infor.diamond;
                        infor.isPass = 1;
                    }
                }
                JsonTools.WriteJsonFile(levelInforList, path);
            }
        }


        int selectedTaskId = PlayerPrefs.GetInt("SelectedTaskId", -1);
        if (selectedTaskId != -1)
        {
            string path = PlayerPrefs.GetString("serverPath") + "TaskData.json";
            List<Task> taskList = JsonTools.AnalyticalJsonFile<List<Task>>(path);

            if (taskList.Count > 0)
            {
                foreach (Task task in taskList)
                {
                    if (task.PlayerId == Player.id && selectedTaskId == task.Id)
                    {
                        coin += task.Coin;
                        diamond += task.Diamond;
                        task.State = "Finished";
                    }
                }
                JsonTools.WriteJsonFile(taskList, path);
            }
        }

        PlayerPrefs.SetInt("SelectedLevelId", -1);
        PlayerPrefs.SetInt("SelectedTaskId", -1);
        
        PlayerInfor.instance.PlayerReward(exp, coin, diamond);
    }

    public void OnPlayerDie()
    {
        PlayerPrefs.SetInt("SelectedLevelId", -1);
        PlayerPrefs.SetInt("SelectedTaskId", -1);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInforController : MonoBehaviour
{
    private GameObject playerInforBg_1;
    private GameObject playerInforBg_2;
    private Transform plyaer_1_pos;
    private Transform plyaer_2_pos;

    public GameObject playerBoyPre;
    public GameObject playerGirlPre;

    private Text toasInfor;
    private Text playerNameText_1;
    private Text playerLevelText_1;
    private Text playerNameText_2;
    private Text playerLevelText_2;
    private Button createPlayerBtn;

    private int[] playerId = new int[2];

    void Awake()
    {
        playerInforBg_1 = this.transform.Find("PlayerInforBG_1").gameObject;
        playerInforBg_2 = this.transform.Find("PlayerInforBG_2").gameObject;
        toasInfor = this.transform.Find("ToastInfor").GetComponent<Text>();
        plyaer_1_pos = playerInforBg_1.transform.Find("Player_1_Pos");
        plyaer_2_pos = playerInforBg_2.transform.Find("Player_2_Pos");
        playerNameText_1 = playerInforBg_1.transform.Find("PlayerNameBG/PlayerName").GetComponent<Text>();
        playerLevelText_1 = playerInforBg_1.transform.Find("PlayerNameBG/PlayerLevel").GetComponent<Text>();
        playerNameText_2 = playerInforBg_2.transform.Find("PlayerNameBG/PlayerName").GetComponent<Text>();
        playerLevelText_2 = playerInforBg_2.transform.Find("PlayerNameBG/PlayerLevel").GetComponent<Text>();
        createPlayerBtn = this.transform.Find("CreatePlayerBtn").GetComponent<Button>();
    }

	// Use this for initialization
	void Start ()
	{
	    
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public  void InitPlayer()
    {
        int haveNum = CheckPlayer(PlayerPrefs.GetInt("currentUserId"));

        switch (haveNum)
        {
            case 0:
                playerInforBg_1.SetActive(false);
                playerInforBg_2.SetActive(false);
                break;
            case 1:
                toasInfor.gameObject.SetActive(false);
                playerInforBg_1.SetActive(true);
                playerInforBg_2.SetActive(false);
                if (plyaer_1_pos.transform.childCount == 0)
                {
                    ShowPlayer(playerId[0], 0);
                }
                break;
            case 2:
                toasInfor.gameObject.SetActive(false);
                createPlayerBtn.interactable = false;
                playerInforBg_1.SetActive(true);
                playerInforBg_2.SetActive(true);
                if (plyaer_1_pos.transform.childCount == 0 || plyaer_2_pos.transform.childCount == 0)
                {
                    ShowPlayer(playerId[0], playerId[1]);
                }
                break;
        }
    }

    private int CheckPlayer(int userId)
    {
        int isHave = 0;

        string path = PlayerPrefs.GetString("serverPath") + "PlayerData.json";
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(path);

        if (playerList != null)
        {
            foreach (Player player in playerList)
            {
                if (player.userId == userId)
                {
                    isHave++;
                    int index = 0;
                    if (playerId[0] != 0)
                    {
                        index = 1;
                    }
                    playerId[index] = player.id;
                }
            }
        }

        return isHave;
    }

    private void ShowPlayer(int player_1, int player_2)
    {
        //安卓
        string path = PlayerPrefs.GetString("serverPath") + "PlayerData.json";
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(path);

        
        foreach (Player player in playerList)
        {
            if (player.id == player_1)
            {
                GameObject createGo = CreatePlayerByPos(player.isMan == 0 ? playerBoyPre : playerGirlPre, plyaer_1_pos);
                createGo.GetComponent<PlayerChangeSize>().playerId = player_1;
                UpdatePlayerText(player, 1);
            }
            if (player_2 != 0 && player.id == player_2)
            {
                GameObject createGo = CreatePlayerByPos(player.isMan == 0 ? playerBoyPre : playerGirlPre, plyaer_2_pos);
                createGo.GetComponent<PlayerChangeSize>().playerId = player_2;
                UpdatePlayerText(player, 2);
            }
        }
    }

    private GameObject CreatePlayerByPos(GameObject prefab, Transform pos)
    {
        GameObject go = Instantiate(prefab, pos.position, Quaternion.identity);
        PlayerChangeSize changeSize = go.AddComponent<PlayerChangeSize>();
        changeSize.targetScale = 1.3f;
        changeSize.originalScale = 1f;
        go.transform.parent = pos;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = new Quaternion(0f,180f,0f,0f);
        go.transform.localScale = new Vector3(1f,1f,1f);
        return go;
    }

    private void UpdatePlayerText(Player player, int index)
    {
        if (index == 1)
        {
            playerNameText_1.text = player.playerName;
            playerLevelText_1.text = "Lv." + player.level;
        }
        else
        {
            playerNameText_2.text = player.playerName;
            playerLevelText_2.text = "Lv." + player.level;
        }
    }
}

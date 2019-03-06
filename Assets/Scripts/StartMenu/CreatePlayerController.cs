using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayerController : MonoBehaviour
{
    private InputField playerNameInput;

    private string path;

    // Use this for initialization
    void Start ()
	{
	    playerNameInput = this.transform.Find("PlayerNameInput").GetComponent<InputField>();
	    path = PlayerPrefs.GetString("serverPath") + "PlayerData.json";
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CreatePlayer()
    {
        GameObject selectedPlayer = StartMenuController.instance.playerSelected;
        if (selectedPlayer == null)
        {
            HintInfor.instance.ShowInformation("请选择角色，再创建");
            return;
        }

        int type = 0;
        if (selectedPlayer.name.StartsWith("girl"))
        {
            type = 1;
        }

        int userId = PlayerPrefs.GetInt("currentUserId");
        if (CheckPlayer(userId, type))
        {
            HintInfor.instance.ShowInformation("该角色已存在，请选择另外一个角色");
            return;
        }

        string playerName = playerNameInput.text.Trim();
        if (playerName == "")
        {
            HintInfor.instance.ShowInformation("角色名字不能为空");
            return;
        }

        if(playerName.Length < 3 || playerName.Length > 6)
        {
            HintInfor.instance.ShowInformation("角色名长度为3至6位");
            return;
        }

        int playerId = 0;
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(path);

        if (playerList == null)
        {
            playerId = 1;
            playerList = new List<Player>();
        }
        else
        {
            playerId = playerList.Count + 1;
        }

        Player player = new Player
        {
            id = playerId,
            playerName = playerName,
            userId = userId,
            isMan = type
        };
        playerList.Add(player);
        JsonTools.WriteJsonFile(playerList, path);

        //显示角色信息界面
        StartMenuController.instance.HideCreatePlayerBg();
        StartMenuController.instance.ShowPlayerBg();
    }

    private bool CheckPlayer(int userId, int type)
    {
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(path);

        bool isHave = false;
        if (playerList == null) return isHave;

        foreach (Player player in playerList)
        {
            if (player.userId == userId && player.isMan == type)
            {
                isHave = true;
            }
        }

        return isHave;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelSelect : MonoBehaviour
{
    public static GameLevelSelect instance;

    private Animator gameLevelSelectAnim;
    private int playerId;
    private string path;

    public Transform combatPos;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        gameLevelSelectAnim = this.GetComponent<Animator>();
        playerId = GameController.instance.Player.id;
        path = PlayerPrefs.GetString("serverPath") + "GameLevelData.json";
        List<GameLevelInfor> levelInforList = JsonTools.AnalyticalJsonFile<List<GameLevelInfor>>(path);

        bool isHave = false;

        if (levelInforList != null)
        {
            if (levelInforList.Count > 0)
            {
                foreach (GameLevelInfor gameLevelInfor in levelInforList)
                {
                    if (gameLevelInfor.playerId == playerId)
                    {
                        isHave = true;
                        break;
                    }
                }
            }
        }
        
        if (!isHave)
        {
            SaveGameLevel();
        }

        InitGameLevel();
    }

    // Update is called once per frame
	void Update ()
    {
		
	}

    //加载关卡信息
    public void InitGameLevel()
    {
        playerId = GameController.instance.Player.id;
        path = PlayerPrefs.GetString("serverPath") + "GameLevelData.json";
        List<GameLevelInfor> levelInforList = JsonTools.AnalyticalJsonFile<List<GameLevelInfor>>(path);
        
        GameLevelBtn[] levelBtnArray = this.transform.Find("GameLevelBG").GetComponentsInChildren<GameLevelBtn>();

        int index = 0;
        for (int i = 0; i < levelInforList.Count; i++)
        {
            if (levelInforList[i].playerId == playerId)
            {
                levelBtnArray[index].id = levelInforList[i].id;
                levelBtnArray[index].needLevel = levelInforList[i].needLevel;
                levelBtnArray[index].coin = levelInforList[i].coin;
                levelBtnArray[index].energy = levelInforList[i].energy;
                levelBtnArray[index].diamond = levelInforList[i].diamond;
                levelBtnArray[index].exp = levelInforList[i].exp;
                levelBtnArray[index].isPass = levelInforList[i].isPass;
                levelBtnArray[index].des = levelInforList[i].des;
                levelBtnArray[index].playerId = levelInforList[i].playerId;
                levelBtnArray[index].UpdateShow();
                index++;
            }
        }
    }

    public void Show()
    {
        gameLevelSelectAnim.SetBool("isShow", true);
    }

    public void Hide()
    {
        gameLevelSelectAnim.SetBool("isShow",false);
    }

    private void SaveGameLevel()
    {
        List<GameLevelInfor> levelInforList = JsonTools.AnalyticalJsonFile<List<GameLevelInfor>>(path);
        int index = 0;

        if (levelInforList == null || levelInforList.Count <= 0)
        {
            index = 1;
            levelInforList = new List<GameLevelInfor>();
        }
        else
        {
            index = levelInforList.Count + 1;
        }
        
         GameLevelInfor btn1 = new GameLevelInfor
        {
            id = index,
            needLevel = 1,
            energy = 1,
            exp = 200,
            des = "这是第一关",
            coin = 500,
            diamond = 0,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn2 = new GameLevelInfor
        {
            id = index + 1,
            needLevel = 3,
            energy = 5,
            exp = 500,
            des = "这是第二关",
            coin = 1200,
            diamond = 50,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn3 = new GameLevelInfor
        {
            id = index + 2,
            needLevel = 5,
            energy = 2,
            exp = 800,
            des = "这是第三关",
            coin = 1500,
            diamond = 0,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn4 = new GameLevelInfor
        {
            id = index + 3,
            needLevel = 8,
            energy = 2,
            exp = 1000,
            des = "这是第四关",
            coin = 1700,
            diamond = 0,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn5 = new GameLevelInfor
        {
            id = index + 4,
            needLevel = 10,
            energy = 2,
            exp = 1200,
            des = "这是第五关",
            coin = 1900,
            diamond = 0,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn6 = new GameLevelInfor
        {
            id = index + 5,
            needLevel = 15,
            energy = 5,
            exp = 1800,
            des = "这是第六关",
            coin = 2300,
            diamond = 100,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn7 = new GameLevelInfor
        {
            id = index + 6,
            needLevel = 18,
            energy = 3,
            exp = 2000,
            des = "这是第七关",
            coin = 2500,
            diamond = 10,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn8 = new GameLevelInfor
        {
            id = index + 7,
            needLevel = 23,
            energy = 8,
            exp = 2500,
            des = "这是第八关",
            coin = 3000,
            diamond = 160,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn9 = new GameLevelInfor
        {
            id = index + 8,
            needLevel = 25,
            energy = 5,
            exp = 2700,
            des = "这是第九关",
            coin = 3200,
            diamond = 20,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn10 = new GameLevelInfor
        {
            id = index + 9,
            needLevel = 28,
            energy = 5,
            exp = 3000,
            des = "这是第十关",
            coin = 3500,
            diamond = 20,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn11 = new GameLevelInfor
        {
            id = index + 10,
            needLevel = 33,
            energy = 12,
            exp = 3300,
            des = "这是第十一关",
            coin = 4000,
            diamond = 200,
            isPass = 0,
            playerId = playerId
        };
        GameLevelInfor btn12 = new GameLevelInfor
        {
            id = index + 11,
            needLevel = 40,
            energy = 15,
            exp = 5000,
            des = "这是第最后一关",
            coin = 8000,
            diamond = 100,
            isPass = 0,
            playerId = playerId
        };

        levelInforList.Add(btn1);
        levelInforList.Add(btn2);
        levelInforList.Add(btn3);
        levelInforList.Add(btn4);
        levelInforList.Add(btn5);
        levelInforList.Add(btn6);
        levelInforList.Add(btn7);
        levelInforList.Add(btn8);
        levelInforList.Add(btn9);
        levelInforList.Add(btn10);
        levelInforList.Add(btn11);
        levelInforList.Add(btn12);

        JsonTools.WriteJsonFile(levelInforList, path);
    }

    public void UpdateGameLevelInfor(GameLevelBtn btn)
    {
        List<GameLevelInfor> gameLevelList = JsonTools.AnalyticalJsonFile<List<GameLevelInfor>>(path);
        if (gameLevelList.Count <= 0) gameLevelList = new List<GameLevelInfor>();

        foreach (GameLevelInfor gameLevelInfor in gameLevelList)
        {
            if (gameLevelInfor.playerId == btn.playerId && gameLevelInfor.id == btn.id)
            {
                gameLevelInfor.isPass = btn.isPass;
            }
        }

        JsonTools.WriteJsonFile(gameLevelList,path);
        InitGameLevel();
    }

    public void CombatBtnClick()
    {
        PlayerAutoMove.instance.SetDestination(combatPos.position);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    private GameObject startMenuBg;
    private GameObject serverSelectBg;
    private GameObject loginBg;
    private GameObject registerBg;
    private GameObject playerBg;
    private GameObject createPlayerBg;
    private GameObject loadingBg;

    private HintInfor hintInfor;

    private GameObject tempGo; //这个变量的作用是帮助在Start查找物体时比较方便

    private Animator startMenuBgAnim;
    private Animator serverSlectBgAnim;
    private Animator loginBgAnim;
    private Animator registerBgAnim;
    private Animator playerBgAnim;
    private Animator createPlayerBgAnim;

    private Text selectServerName;

    public static StartMenuController instance;
    [NonSerialized]
    public GameObject playerSelected;

    void Awake()
    {
        instance = this;
        Application.logMessageReceived += Handler;
    }

    // Use this for initialization
    void Start ()
    {
        InitFile();

        tempGo = GameObject.Find("Canvas/BG");

	    startMenuBg = tempGo.transform.Find("StartMenuBG").gameObject;
	    startMenuBgAnim = startMenuBg.GetComponent<Animator>();

        serverSelectBg = tempGo.transform.Find("ServerSelectMask").gameObject;
        serverSlectBgAnim = serverSelectBg.GetComponent<Animator>();

        loginBg = tempGo.transform.Find("LoginMask").gameObject;
	    loginBgAnim = loginBg.GetComponent<Animator>();

        registerBg = tempGo.transform.Find("RegisterMask").gameObject;
	    registerBgAnim = registerBg.GetComponent<Animator>();

        playerBg = tempGo.transform.Find("PlayerBG").gameObject;
	    playerBgAnim = playerBg.GetComponent<Animator>();

        createPlayerBg = tempGo.transform.Find("CreatePlayerBG").gameObject;
	    createPlayerBgAnim = createPlayerBg.GetComponent<Animator>();

	    hintInfor = tempGo.transform.Find("HintInforBG").GetComponent<HintInfor>();
        hintInfor.gameObject.SetActive(true);

        loadingBg = tempGo.transform.Find("LoadingBG").gameObject;

        startMenuBg.transform.Find("ServerselectBtn").GetComponentInChildren<Text>().text = PlayerPrefs.GetString("lastServerName", "一区  中国");

    }

    void Handler(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {
            HintInfor.instance.ShowInformation(logString + "//" + stackTrace);
        }
    }


    // Update is called once per frame
    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ConfirmExitGameMask.instance.Show();
        }
	}
    
    //显示服务器列表界面
    public void ShowServerSelectBg()
    {
        serverSelectBg.SetActive(true);
        serverSlectBgAnim.SetBool("isShow", true);
    }

    //隐藏服务器选择界面
    public void HideServerSelectBg()
    {
        serverSlectBgAnim.SetBool("isShow", false);
    }

    //显示开始界面
    public void ShowStartMenu()
    {
        startMenuBgAnim.SetBool("isShow", true);
    }

    //隐藏开始界面
    public void HideStartMenu()
    {
        startMenuBgAnim.SetBool("isShow", false);
    }

    //显示登录界面
    public void ShowLoginBg()
    {
        int currentServerId = PlayerPrefs.GetInt("lastServerId", 1);
        string serverPath = Application.dataPath + "/Servers/Server_" + currentServerId + "/";

        //安卓
        //string serverPath = Application.persistentDataPath + "/Servers/Server_" + currentServerId + "/";

        PlayerPrefs.SetString("serverPath", serverPath);
        loginBg.SetActive(true);
        loginBgAnim.SetBool("isShow", true);
    }

    //关闭登录界面
    public void CloseLoginBg()
    {
        loginBgAnim.SetBool("isShow", false);
    }

    //显示注册界面
    public void ShowRegisterBg()
    {
        registerBg.SetActive(true);
        registerBgAnim.SetBool("isShow",true);
    }

    //关闭注册界面
    public void CloseRegisterBg()
    {
        registerBgAnim.SetBool("isShow",false);
    }

    //显示角色界面
    public void ShowPlayerBg()
    {
        playerBg.SetActive(true);
        playerBgAnim.SetBool("isShow",true);
        playerBg.GetComponent<PlayerInforController>().InitPlayer();
    }
    //隐藏角色界面
    public void ClosePlayerBg()
    {
        playerBgAnim.SetBool("isShow", false);
    }

    //显示角色选择界面
    public void ShowCreatePlayerBg()
    {
        createPlayerBg.SetActive(true);
        createPlayerBgAnim.SetBool("isShow",true);
    }
    //隐藏角色选择界面
    public void HideCreatePlayerBg()
    {
        createPlayerBgAnim.SetBool("isShow", false);
    }

    //返回登录界面
    public void BackLoginBg()
    {
        playerBgAnim.SetBool("isShow",false);
    }

    //返回角色界面
    public void BackPlayerBg()
    {
        createPlayerBgAnim.SetBool("isShow",false);
    }

    //进入游戏主界面
    public void EnterMainGame()
    {
        if (playerSelected == null)
        {
            HintInfor.instance.ShowInformation("请选择角色后再进入游戏!!!");
            return;
        }
        int playerId = playerSelected.GetComponent<PlayerChangeSize>().playerId;
        if (playerId == 0)
        {
            HintInfor.instance.ShowInformation("请选择角色后再进入游戏!!!");
            return;
        }

        playerBg.SetActive(false);
        PlayerPrefs.SetInt("selectPlayerId",playerId);
        loadingBg.SetActive(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync("002-NoviceVillarger");
        LoadingController.instance.Show(ao);
    }

    //单击角色后，角色变换大小
    public void ChangePlayerSize(GameObject go, float targetScale, float originalScale)
    {
        iTween.ScaleTo(go, new Vector3(targetScale, targetScale, targetScale), 0.5f);
        if (playerSelected != null && go != playerSelected)
        {
            iTween.ScaleTo(playerSelected, new Vector3(originalScale, originalScale, originalScale), 0.5f);
        }
        playerSelected = go;
    }


    //创建各个服务器的文件夹
    private void InitFile()
    {
        for (int i = 0; i < 16; i++)
        {
            string directoryPath = Application.persistentDataPath + "/Servers/Server_" + (i + 1);
            string playerFilePath = directoryPath +"/PlayerData.json";
            string userFilePath = directoryPath + "/UserData.json";
            string gameLevelFilePath = directoryPath + "/GameLevelData.json";
            string taskFilePath = directoryPath + "/TaskData.json";
            string skillFilePath = directoryPath + "/SkillData.json";
            string inventoryFilePath = directoryPath + "/InventoryData.json";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(playerFilePath))
            {
                File.CreateText(playerFilePath).Dispose();
            }
            if (!File.Exists(userFilePath))
            {
                File.CreateText(userFilePath).Dispose();
            }
            if (!File.Exists(gameLevelFilePath))
            {
                File.CreateText(gameLevelFilePath).Dispose();
            }
            if (!File.Exists(taskFilePath))
            {
                File.CreateText(taskFilePath).Dispose();
            }
            if (!File.Exists(skillFilePath))
            {
                File.CreateText(skillFilePath).Dispose();
            }
            if (!File.Exists(inventoryFilePath))
            {
                File.CreateText(inventoryFilePath).Dispose();
            }
        }
    }
}

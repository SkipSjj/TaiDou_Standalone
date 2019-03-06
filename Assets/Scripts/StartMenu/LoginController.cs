using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    private InputField userNameText;
    private InputField passwordText;

    // Use this for initialization
    void Start()
    {
        userNameText = this.transform.Find("AccountBG/AccountInput").GetComponent<InputField>();
        passwordText = this.transform.Find("PasswordBG/PasswordInput").GetComponent<InputField>();

        userNameText.text = PlayerPrefs.GetString("currentUserName");
        passwordText.text = PlayerPrefs.GetString("currentPassword");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoginGame()
    {
        string userName = userNameText.text.Trim();
        string password = passwordText.text.Trim();

        if (userName == "")
        {
            HintInfor.instance.ShowInformation("用户名不能为空!!!!");
            return;
        }

        if (password == "")
        {
            HintInfor.instance.ShowInformation("密码不能为空!!!!");
            return;
        }
        
        string filePath = PlayerPrefs.GetString("serverPath") + "UserData.json";
        List<User> userList = JsonTools.AnalyticalJsonFile<List<User>>(filePath);


        if (userList == null)
        {
            HintInfor.instance.ShowInformation("用户不存在，请注册!!!");
            return;
        }

        bool temp = false;
        int userId = 0;
        foreach (User user in userList)
        {
            if (userName == user.userName && password == user.password)
            {
                temp = true;
                userId = user.id;
                break;
            }
        }

        if (!temp)
        {
            HintInfor.instance.ShowInformation("用户名或密码错误！！！");
            return;
        }

        PlayerPrefs.SetInt("currentUserId", userId);
        PlayerPrefs.SetString("currentUserName",userName);
        PlayerPrefs.SetString("currentPassword",password);

        HintInfor.instance.ShowInformation("登录成功！！！");
        StartMenuController.instance.HideStartMenu();
        StartMenuController.instance.CloseLoginBg();
        
        if (!CheckPlayer(userId))
        {
            //跳转到创建角色的页面
            StartMenuController.instance.ShowCreatePlayerBg();
        }
        else
        {
            //显示第一个角色,并把创建新角色按钮禁用
            StartMenuController.instance.ShowPlayerBg();
        }

    }

    private bool CheckPlayer(int userId)
    {
        bool isHave = false;
        
        string filePath = PlayerPrefs.GetString("serverPath") + "PlayerData.json";
        List<Player> playerList = JsonTools.AnalyticalJsonFile<List<Player>>(filePath);


        if (playerList != null)
        {
            foreach (Player player in playerList)
            {
                if (player.userId == userId)
                {
                    isHave = true;
                }
            }
        }

        return isHave;
    }
}

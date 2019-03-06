using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    private InputField userNameText;
    private InputField passwordText;
    private InputField confirmPasswordText;

    private string filePath;

    // Use this for initialization
    void Start ()
	{
	    userNameText = this.transform.Find("AccountBG/AccountInput").GetComponent<InputField>();
	    passwordText = this.transform.Find("PasswordBG/PasswordInput").GetComponent<InputField>();
	    confirmPasswordText = this.transform.Find("ConfirmPasswordBG/ConfirmPasswordInput").GetComponent<InputField>();
	    int currentServerId = PlayerPrefs.GetInt("lastServerId", 1);
	    filePath = PlayerPrefs.GetString("serverPath") + "UserData.json";
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void RegisterAndLogin()
    {
        string userName = userNameText.text.Trim();
        string password = passwordText.text.Trim();
        string confirmPassword = confirmPasswordText.text.Trim();

        if (userName == "")
        {
            HintInfor.instance.ShowInformation("用户名不能为空!!!!");
            return;
        }
        else if(userName.Length < 3 || userName.Length > 6)
        {
            HintInfor.instance.ShowInformation("用户名为3至6位!!!!");
            return;
        }

        if (password == "")
        {
            HintInfor.instance.ShowInformation("密码不能为空!!!!");
            return;
        }
        else if(password.Length < 3 || password.Length > 6)
        {
            HintInfor.instance.ShowInformation("密码为3至6位数字!!!!");
            return;
        }

        if (password != confirmPassword)
        {
            HintInfor.instance.ShowInformation("两次输入的密码不一致!!!!");
            return;
        }

        int currentServerId = PlayerPrefs.GetInt("lastServerId", 1);

        if (CheckUser(userName))
        {
            HintInfor.instance.ShowInformation("该用户已存在!!!!");
            return;
        }

        int userId = 0;
        List<User> userList = JsonTools.AnalyticalJsonFile<List<User>>(filePath);

        if (userList == null || userList.Count <= 0)
        {
            userId = 1;
            userList = new List<User>();
        }
        else
        {
            userId = userList.Count + 1;
        }

        User user = new User
        {
            id = userId,
            userName = userName,
            password = password,
            serverId = currentServerId
        };
        userList.Add(user);

        JsonTools.WriteJsonFile(userList, filePath);
        PlayerPrefs.SetInt("currentUserId", userId);
        PlayerPrefs.SetString("currentUserName", userName);
        PlayerPrefs.SetString("currentPassword", password);
        HintInfor.instance.ShowInformation("注册成功！！！");

        //跳转界面
        StartMenuController.instance.CloseRegisterBg();
        StartMenuController.instance.HideStartMenu();
        StartMenuController.instance.ShowCreatePlayerBg();
    }

    private bool CheckUser(string userName)
    {
        bool isExist = false;

        List<User> userList = JsonTools.AnalyticalJsonFile<List<User>>(filePath);
        
        if (userList == null || userList.Count <= 0) return isExist;

        foreach (User userItem in userList)
        {
            if (userItem.userName == userName)
            {
                isExist = true;
            }
        }

        return isExist;
    }
}
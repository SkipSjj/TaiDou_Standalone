using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SysSetupController : MonoBehaviour
{
    public static SysSetupController instance;

    private AsyncOperation ao;
    private bool isStart = false;
    private string jumpName;

    public GameObject confirmBg;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isStart) return;

        if (ao.progress == 1f)
        {
            switch (jumpName)
            {
                case "ChangePlayer":
                    StartMenuController.instance.ShowPlayerBg();
                    StartMenuController.instance.HideStartMenu();
                    break;
            }

            isStart = false;
        }
	}

    public void ChangePlayerBtnClick()
    {
        GameController.instance.loadingBg.SetActive(true);
        ao = SceneManager.LoadSceneAsync("001-StartMenu");
        LoadingController.instance.Show(ao);

        jumpName = "ChangePlayer";
        isStart = true;
    }

    public void ExitLoginBtnClick()
    {
        GameController.instance.loadingBg.SetActive(true);
        ao = SceneManager.LoadSceneAsync("001-StartMenu");
        LoadingController.instance.Show(ao);

        jumpName = "Login";
        isStart = true;
    }

    public void ExitGameBtnClick()
    {
        confirmBg.GetComponent<Animator>().SetBool("isShow",true);
    }

    public void ConfirmBtnClick()
    {
        Application.Quit();
    }

    public void CancelBtnClick()
    {
        confirmBg.GetComponent<Animator>().SetBool("isShow", false);
    }
}

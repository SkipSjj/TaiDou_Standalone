using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public float smoothing = 1f;

    public GameObject loadingGo;
    public SimpleTouchController touchController;
    public GameObject comfirmBg;

    private Vector3 offset;
    private Transform playerBip;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start ()
	{
	    if (GameObject.Find("PlayerPos").transform.childCount == 0)
	    {
	        GameController.instance.loadingBg = loadingGo;
	        GameController.instance.touchController = touchController;
	        GameObject.Find("GameController").GetComponent<SysSetupController>().confirmBg = comfirmBg;
            GameController.instance.InitPlayer();
	        PlayerInfor.instance.Energy = GameController.instance.Player.energy;
	        PlayerInfor.instance.Magic = GameController.instance.Player.magic;

            if (GameController.instance.isBossDie)
	        {
	            GameController.instance.OnBossDie();
	        }
	        else
	        {
	            GameController.instance.OnPlayerDie();
	        }

            PlayerInfor.instance.InitHpDamage();
	        //更新关卡信息
	        GameLevelSelect.instance.InitGameLevel();
	        //更新任务信息
	        TaskUI.instance.InitTaskShow();
        }

        playerBip = GameObject.Find("PlayerPos").transform.GetChild(0).Find("Bip01");
	    offset = this.transform.position - playerBip.transform.position;
	}

    void FixedUpdate()
    {
        Vector3 targetPos = offset + playerBip.position;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, smoothing * Time.deltaTime);
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ConfirmExitGameMask.instance.Show();
        }
    }
}

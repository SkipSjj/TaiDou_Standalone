using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterGameDialog : MonoBehaviour
{
    private GameObject enterGameDialogBG;

    private Animator enterGameDialogAnim;
    private GameLevelBtn levelBtn;

    private Text levelText;
    private Text expText;
    private Text coinText;
    private Text diamondText;
    private Text neddLevelText;
    private Text energyText;
    private Button enterLevelBtn;

	// Use this for initialization
	void Start ()
	{
	    enterGameDialogBG = this.transform.Find("EnterGameDialogBG").gameObject;
	    levelText = enterGameDialogBG.transform.Find("LevelNum").GetComponent<Text>();
	    expText = enterGameDialogBG.transform.Find("ExpNum").GetComponent<Text>();
	    coinText = enterGameDialogBG.transform.Find("CoinNum").GetComponent<Text>();
	    diamondText = enterGameDialogBG.transform.Find("DiamondNum").GetComponent<Text>();
	    neddLevelText = enterGameDialogBG.transform.Find("NeedLevelNum").GetComponent<Text>();
	    energyText = enterGameDialogBG.transform.Find("EnergyNum").GetComponent<Text>();
	    enterGameDialogAnim = this.GetComponent<Animator>();
	    enterLevelBtn = enterGameDialogBG.transform.Find("CombatBtn").GetComponent<Button>();
	}
	
	// Update is called once per frame  
	void Update ()
    {
		
	}

    public void InitShow(GameLevelBtn btn)
    {
        levelBtn = btn;
        int index = btn.id % 12;
        levelText.text = "第" + index + "关";
        expText.text = "X " + btn.exp;
        coinText.text = "X " + btn.coin;
        diamondText.text = "X " + btn.diamond;
        neddLevelText.text = "> " + btn.needLevel;
        energyText.text = "> " + btn.energy;

        enterLevelBtn.interactable = GameController.instance.Player.level >= btn.needLevel &&
                                     GameController.instance.Player.energy >= btn.energy;

        enterGameDialogAnim.SetBool("isShow", true);
    }

    public void EnterTrac()
    {
        if (!PlayerInfor.instance.GetEnergy(levelBtn.energy))
        {
            HintInfor.instance.ShowInformation("体力不足，请稍后再试!!!");
            return;
        }
        
        PlayerPrefs.SetInt("SelectedLevelId", levelBtn.id);
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelBtn.sceneName);
        GameController.instance.loadingBg.SetActive(true);
        LoadingController.instance.Show(ao);
    }

    public void Hide()
    {
        enterGameDialogAnim.SetBool("isShow",false);
    }
}

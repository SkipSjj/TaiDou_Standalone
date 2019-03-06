using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    private Image playerImage;
    private Text playerNameText;
    private Text playerLevelText;
    private Slider energySilder;
    private Slider magicSlider;

    void Awake()
    {
        playerImage = this.transform.Find("HeadImage").GetComponent<Image>();
        playerNameText = this.transform.Find("PlayerName").GetComponent<Text>();
        playerLevelText = this.transform.Find("PlayerLevel").GetComponent<Text>();
        energySilder = this.transform.Find("EnergySlider").GetComponent<Slider>();
        magicSlider = this.transform.Find("MagicSlider").GetComponent<Slider>();
    }
	// Use this for initialization
	void Start ()
	{
	    playerImage.GetComponent<Button>().onClick.AddListener(delegate
	    {
	        GameController.instance.ShowPlayerInforBg();
	    });
        UpdateShow();
	}

    private void OnEnable()
    {
        PlayerInfor.instance.OnPlayerInforChange += OnPlayerInforChange;
    }

    private void OnDisable()
    {
        PlayerInfor.instance.OnPlayerInforChange -= OnPlayerInforChange;
    }
    
    // Update is called once per frame
    void Update ()
    {
		
	}

    public void OnPlayerInforChange(InfoType infoType)
    {
        if(infoType == InfoType.All || infoType == InfoType.Energy || infoType == InfoType.HeadPortriait || infoType == InfoType.Magic || infoType == InfoType.Level || infoType == InfoType.Name)
        {
            UpdateShow();
        }
    }

    private void UpdateShow()
    {
        Player player = GameController.instance.Player;

        this.playerImage.sprite = Resources.Load<Sprite>(player.isMan == 0 ? "头像底板男性" : "头像底板女性");
        this.playerNameText.text = player.playerName;
        this.playerLevelText.text = player.level + "";
        this.energySilder.value = (float)player.energy / PlayerInfor.instance.totalEnergy;
    }
}

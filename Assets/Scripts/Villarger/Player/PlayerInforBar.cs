using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInforBar : MonoBehaviour
{
    private Image headImage;
    private Text nameText;
    private Text levelText;
    private Text powerText;
    private Slider expSlider;
    private Text expText;
    private Text coinText;
    private Text diamondText;
    private Text energyText;
    private Text magicText;
    private Text oneEnergyAddTime;
    private Text allEnergyAddTime;
    private Text oneMagicAddTime;
    private Text allMagicAddTime;
    private Button closeBtn;

    private GameObject rename;
    private InputField renmaeText;

    void Awake()
    {
        headImage = this.transform.Find("PlayerHeadImage").GetComponent<Image>();
        nameText = this.transform.Find("PlaerName").GetComponent<Text>();
        levelText = this.transform.Find("PlayerLevel").GetComponent<Text>();
        powerText = this.transform.Find("CombatPowerNum").GetComponent<Text>();
        expSlider = this.transform.Find("ExperienceSlider").GetComponent<Slider>();
        expText = expSlider.transform.Find("ExperienceNum").GetComponent<Text>();
        coinText = this.transform.Find("CoinBG/CoinNum").GetComponent<Text>();
        diamondText = this.transform.Find("DiamondBG/DiamondNum").GetComponent<Text>();
        energyText = this.transform.Find("EnergyBG/EnergyNum").GetComponent<Text>();
        oneEnergyAddTime = this.transform.Find("EnergyBG/RecoveryTime").GetComponent<Text>();
        allEnergyAddTime = this.transform.Find("EnergyBG/AllRecoverTime").GetComponent<Text>();
        magicText = this.transform.Find("MagicBG/MagicNum").GetComponent<Text>();
        oneMagicAddTime = this.transform.Find("MagicBG/RecoveryTime").GetComponent<Text>();
        allMagicAddTime = this.transform.Find("MagicBG/AllRecoverTime").GetComponent<Text>();
        closeBtn = this.transform.Find("CloseBtn").GetComponent<Button>();

        rename = this.transform.Find("RenameMask").gameObject;
        renmaeText = rename.transform.Find("RenameBG/RenameInput").GetComponent<InputField>();
        PlayerInfor.instance.OnPlayerInforChange += OnPlayerInforChange;

    }

    // Use this for initialization
    void Start ()
    {
        closeBtn.onClick.AddListener(delegate
        {
            GameController.instance.HidePlayerInforBg();
        });

        //UpdateShow();
    }

    private void OnDestroy()
    {
        PlayerInfor.instance.OnPlayerInforChange -= OnPlayerInforChange;
    }
    
    // Update is called once per frame
    void Update ()
    {
        UpdateEnergyAndToughenShow();
    }

    public void OnPlayerInforChange(InfoType infoType)
    {
        UpdateShow();
    }

    private void UpdateShow()
    {
        Player player = GameController.instance.Player;

        headImage.sprite = Resources.Load<Sprite>(player.isMan == 0 ? "头像底板男性" : "头像底板女性");
        nameText.text = player.playerName;
        levelText.text = player.level + "";
        powerText.text = PlayerInfor.instance.power + "";
        int requireExp = GameController.GetRequireExp(player.level + 1);
        expSlider.value = (float) player.exp / requireExp;
        expText.text = player.exp + "/" + requireExp;
        coinText.text = player.coin + "";
        diamondText.text = player.diamond + "";
        //更新体力和历练的恢复时间
        UpdateEnergyAndToughenShow();
    }

    void UpdateEnergyAndToughenShow()
    {
        Player player = GameController.instance.Player;
        float totalEnergy = PlayerInfor.instance.totalEnergy;
        float totalMagic = PlayerInfor.instance.totalMagic;
        //if (energyText == null) return;
        energyText.text = player.energy + "/" + totalEnergy;
        magicText.text = player.magic + "/" + totalMagic;
        if (player.energy >= totalEnergy)
        {
            oneEnergyAddTime.text = "00:00:00";
            allEnergyAddTime.text = "00:00:00";
        }
        else
        {
            int remainTime = 60 - (int)PlayerInfor.instance.energyTimer;
            string str = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            oneEnergyAddTime.text = "00:00:" + str;

            int minutes = 99 - player.energy;
            int hours = minutes / 60;
            minutes %= 60;
            string hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            string minutesStr = minutes <= 9 ? "0" + minutes : minutes.ToString();
            allEnergyAddTime.text = hoursStr + ":" + minutesStr + ":" + str;
        }

        magicText.text = player.magic + "/" + totalMagic;
        if (player.magic >= 50)
        {
            oneMagicAddTime.text = "00:00:00";
            allMagicAddTime.text = "00:00:00";
        }
        else
        {
            int remainTime = 60 - (int)PlayerInfor.instance.magicTimer;
            string str = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            oneMagicAddTime.text = "00:00:" + str;

            int minutes = 49 - player.magic;
            int hours = minutes / 60;
            minutes %= 60;
            string hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            string minutesStr = minutes <= 9 ? "0" + minutes : minutes.ToString();
            allMagicAddTime.text = hoursStr + ":" + minutesStr + ":" + str;
        }
    }

    public void ChangeNameBtn()
    {
        rename.SetActive(true);
        renmaeText.text = "";
        iTween.ScaleTo(rename,new Vector3(1f,1f,1f),0.5f);
        iTween.ScaleTo(rename.transform.GetChild(0).gameObject, new Vector3(1f, 1f, 1f), 0.5f);
    }

    public void CloseChangeNameBg()
    {
        iTween.ScaleTo(rename, new Vector3(0f, 0f, 0f), 0.5f);
        iTween.ScaleTo(rename.transform.GetChild(0).gameObject, new Vector3(0f, 0f, 0f), 0.5f);
    }

    public void SureRenameBtn()
    {
        string newName = renmaeText.text.Trim();
        if (newName == "")
        {
            HintInfor.instance.ShowInformation("新的名字不能为空!!!");
            return;
        }
        if (newName.Length > 6)
        {
            HintInfor.instance.ShowInformation("新的名字不能超过6个字符!!!");
            return;
        }

        bool isSuc = PlayerInfor.instance.CheckName(newName);
        HintInfor.instance.ShowInformation(isSuc ? "修改成功!!!" : "修改失败!!!");
        CloseChangeNameBg();
    }
}

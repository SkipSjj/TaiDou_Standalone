using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInfor : MonoBehaviour
{
    public PowerShow powerShow;

    private Text name;
    private Text level;
    private Image image;
    private Text hp;
    private Text damage;
    private Text power;
    private Text des;
    private Text btnText;

    private Inventory it;
    private InventoryItem item;
    private KnasackPlayerEquip playerEquip;
    private bool isLeft = false;

    void Awake()
    {
        name = this.transform.Find("EquipmentName").GetComponent<Text>();
        level = this.transform.Find("LevelText/LevelNum").GetComponent<Text>();
        image = this.transform.Find("EquipmenBG/EquipmentImage").GetComponent<Image>();
        hp = this.transform.Find("HPText/HPNum").GetComponent<Text>();
        damage = this.transform.Find("HurtText/HurtNum").GetComponent<Text>();
        power = this.transform.Find("DamageText/DamageNum").GetComponent<Text>();
        des = this.transform.Find("EquipmentDes").GetComponent<Text>();
        btnText = this.transform.Find("EquipBtn/Text").GetComponent<Text>();
    }
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Show(Inventory it,InventoryItem item,KnasackPlayerEquip playerEquip,bool isLeft = true)
    {
        this.gameObject.SetActive(true);
        this.it = it;
        this.item = item;
        this.playerEquip = playerEquip;
        this.isLeft = isLeft;
        Vector3 tempPos = this.transform.localPosition;
        transform.localPosition = isLeft
            ? new Vector3(-Mathf.Abs(tempPos.x), tempPos.y, tempPos.z)
            : new Vector3(Mathf.Abs(tempPos.x), tempPos.y, tempPos.z);
        btnText.text = this.isLeft ? "装备" : "卸下";
        image.sprite = Resources.Load<Sprite>(it.Image);
        level.text = it.Level.ToString();
        name.text = it.Name;
        hp.text = it.Hp.ToString();
        damage.text = it.Damage.ToString();
        power.text = it.Power.ToString();
        des.text = it.Des;
    }

    public void Hide()
    {
        Clear();
        this.gameObject.SetActive(false);

        transform.parent.parent.SendMessage("DisableBtn");
    }

    //穿上或者卸下装备
    public void EquipClick()
    {
        int startValue = PlayerInfor.instance.power;
        if (isLeft)
        {
            PlayerInfor.instance.DressOn(it);
            item.Clear();
            InventoryBg.instance.OnClearClick();
        }
        else
        {
            PlayerInfor.instance.DressOff(it);
            //更新角色装备信息
            playerEquip.Clear();
        }
        int endValue = PlayerInfor.instance.power;
        PowerShow.instance.ShowPowerChange(startValue,endValue);

        Hide();
    }

    //装备升级方法
    public void EquipUpgrade()
    {
        int startValue = PlayerInfor.instance.power;
        int coinNeed = (it.Level + 1) * it.Price;
        if (PlayerInfor.instance.SubMoney("", coinNeed, 0))
        {
            it.Level++;
            InventoryManager.instance.UpgradeEquip(it);
            level.text = it.Level.ToString();
            int endValue = PlayerInfor.instance.power;
            PowerShow.instance.ShowPowerChange(startValue, endValue);
        }
        else
        {
            HintInfor.instance.ShowInformation("金币不足，无法升级！");
        }
    }

    void Clear()
    {
        it = null;
        item = null;
    }
}

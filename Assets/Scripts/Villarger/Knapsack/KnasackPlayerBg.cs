using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnasackPlayerBg : MonoBehaviour
{
    private KnasackPlayerEquip helmEquip;
    private KnasackPlayerEquip clothEquip;
    private KnasackPlayerEquip weaponEquip;
    private KnasackPlayerEquip shoesEquip;
    private KnasackPlayerEquip neclnecklaceEquip;
    private KnasackPlayerEquip braceletEquip;
    private KnasackPlayerEquip ringEquip;
    private KnasackPlayerEquip wingEquip;

    private Text hpNum;
    private Text damageNum;
    private Text expNum;
    private Slider expSlider;

    void Awake()
    {
        helmEquip = this.transform.Find("EquipmentBG/Helm").GetComponent<KnasackPlayerEquip>();
        clothEquip = this.transform.Find("EquipmentBG/Cloth").GetComponent<KnasackPlayerEquip>();
        weaponEquip = this.transform.Find("EquipmentBG/Weapon").GetComponent<KnasackPlayerEquip>();
        shoesEquip = this.transform.Find("EquipmentBG/Shoes").GetComponent<KnasackPlayerEquip>();
        neclnecklaceEquip = this.transform.Find("EquipmentBG/Necklace").GetComponent<KnasackPlayerEquip>();
        braceletEquip = this.transform.Find("EquipmentBG/Bracelet").GetComponent<KnasackPlayerEquip>();
        ringEquip = this.transform.Find("EquipmentBG/Ring").GetComponent<KnasackPlayerEquip>();
        wingEquip = this.transform.Find("EquipmentBG/Wing").GetComponent<KnasackPlayerEquip>();

        hpNum = this.transform.Find("EquipmentBG/PowerBG/HP/HPNum").GetComponent<Text>();
        damageNum = this.transform.Find("EquipmentBG/PowerBG/Damage/DamageNum").GetComponent<Text>();
        expNum = this.transform.Find("EquipmentBG/PowerBG/Experience/Text").GetComponent<Text>();
        expSlider = this.transform.Find("EquipmentBG/PowerBG/Experience/Slider").GetComponent<Slider>();

        PlayerInfor.instance.OnPlayerInforChange += OnPlayerInforChange;
    }

    // Use this for initialization
    void Start ()
    {
        foreach (var item in InventoryManager.instance.itList)
        {
            if (item.IsDressed == "Yes")
            {
                int hp = item.Hp * item.Level;
                int damage = item.Damage * item.Level;
                PlayerInfor.instance.UpdateHpDamagePower(hp, damage);
            }
        }
        UpdateShow();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnDestroy()
    {
        //PlayerInfor.instance.OnPlayerInforChange -= OnPlayerInforChange;
    }

    void OnPlayerInforChange(InfoType type)
    {
        if (type == InfoType.All || type == InfoType.Damage ||type == InfoType.Exp || type == InfoType.Equip || type == InfoType.Hp)
        {
            UpdateShow();
        }
    }

    private void UpdateShow()
    {
        PlayerInfor infor = PlayerInfor.instance;

        foreach (var item in InventoryManager.instance.itList)
        {
            if (item.IsDressed == "Yes")
            {
                switch (item.EquipType)
                {
                    case "Helm":
                        helmEquip.SetEquipItem(item);
                        break;
                    case "Cloth":
                        clothEquip.SetEquipItem(item);
                        break;
                    case "Weapon":
                        weaponEquip.SetEquipItem(item);
                        break;
                    case "Shoes":
                        shoesEquip.SetEquipItem(item);
                        break;
                    case "Necklace":
                        neclnecklaceEquip.SetEquipItem(item);
                        break;
                    case "Bracelet":
                        braceletEquip.SetEquipItem(item);
                        break;
                    case "Ring":
                        ringEquip.SetEquipItem(item);
                        break;
                    case "Wing":
                        wingEquip.SetEquipItem(item);
                        break;
                }
            }
        }

        hpNum.text = infor.hp.ToString();
        damageNum.text = infor.damage.ToString();
        expSlider.value = (float) GameController.instance.Player.exp / GameController.GetRequireExp(GameController.instance.Player.level + 1);
        expNum.text = GameController.instance.Player.exp + "/" + GameController.GetRequireExp(GameController.instance.Player.level + 1);
    }
}

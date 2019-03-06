using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InfoType
{
    Name,
    HeadPortriait,
    Level,
    VIP,
    Power,
    Exp,
    Diamond,
    Coin,
    Energy,
    Magic,
    Hp,
    Damage,
    Equip,
    All
}

public class PlayerInfor : MonoBehaviour
{
    public static PlayerInfor instance;

    public delegate void OnPlayerInfoChangeEvent(InfoType inforType);
    public event OnPlayerInfoChangeEvent OnPlayerInforChange;

    [NonSerialized]
    public int totalEnergy = 100;
    [NonSerialized]
    public int totalMagic = 200;
    [NonSerialized]
    public static bool isUpdate = false;
    public float energyTimer = 0f;
    public float magicTimer = 0f;
    public int damage;
    public int hp;
    public int power;
    
    public Inventory helmInventory;
    public Inventory clothInventory;
    public Inventory weaponInventory;
    public Inventory shoesInventory;
    public Inventory necklaceInventory;
    public Inventory braceleInventory;
    public Inventory ringInventory;
    public Inventory wingInventory;

    public int Energy { get; set; }
    public int Magic { get; set; }

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        Energy = GameController.instance.Player.energy;
        Magic = GameController.instance.Player.magic;

        InitHpDamage();
    }

	// Update is called once per frame
	void Update ()
	{
	    AutoUpdate();
	}

    private void AutoUpdate()
    {
        //实现体力的自动增长
        if (Energy < totalEnergy)
        {
            energyTimer += Time.deltaTime;
            if (energyTimer >= 60)
            {
                Energy += 1;
                GameController.instance.Player.energy = Energy;
                energyTimer = 0f;
                OnPlayerInforChange(InfoType.Energy);
            }
        }
        else
        {
            energyTimer = 0f;
        }

        if (Magic < totalMagic)
        {
            magicTimer += Time.deltaTime;
            if (magicTimer >= 60)
            {
                Magic += 1;
                GameController.instance.Player.magic = Magic;
                magicTimer = 0f;
                OnPlayerInforChange(InfoType.Magic);
            }
        }
        else
        {
            magicTimer = 0f;
        }
    }

    public void InitHpDamage()
    {
        Player player = GameController.instance.Player;
        hp = player.level * 500;
        damage = player.level * 20;
        power = hp + damage;
        
        OnPlayerInforChange(InfoType.All);
    }

    //穿上装备
    public void DressOn(Inventory item)
    {
        item.IsDressed = "Yes";
        //1.首先检测有没有同类型的装备
        bool isDressed = false;
        Inventory it = null;
        switch (item.EquipType)
        {
            case "Helm":
                if (helmInventory.Name != null)
                {
                    isDressed = true;
                    it = helmInventory;
                }
                helmInventory = item;
                break;
            case "Cloth":
                if (clothInventory.Name != null)
                {
                    isDressed = true;
                    it = clothInventory;
                }
                clothInventory = item;
                break;
            case "Weapon":
                if (weaponInventory.Name != null)
                {
                    isDressed = true;
                    it = weaponInventory;
                }
                weaponInventory = item;
                break;
            case "Shoes":
                if (shoesInventory.Name != null)
                {
                    isDressed = true;
                    it = shoesInventory;
                }
                shoesInventory = item;
                break;
            case "Necklace":
                if (necklaceInventory.Name != null)
                {
                    isDressed = true;
                    it = necklaceInventory;
                }
                necklaceInventory = item;
                break;
            case "Bracelet":
                if (braceleInventory.Name != null)
                {
                    isDressed = true;
                    it = braceleInventory;
                }
                braceleInventory = item;
                break;
            case "Ring":
                if (ringInventory.Name != null)
                {
                    isDressed = true;
                    it = ringInventory;
                }
                ringInventory = item;
                break;
            case "Wing":
                if (wingInventory.Name != null)
                {
                    isDressed = true;
                    it = wingInventory;
                }
                wingInventory = item;
                break;
        }
        //2.有的话，把旧的装备脱掉，放到背包
        if (isDressed)
        {
            it.IsDressed = "No";
            InventoryBg.instance.AddInventoryItem(it);
            InventoryManager.instance.UpdateInventory(it);
            int HP = it.Hp * it.Level;
            int damage = it.Damage * it.Level;
            UpdateHpDamagePower(HP,damage,false);
        }
        InventoryManager.instance.UpdateInventory(item);
        int hp = item.Hp * item.Level;
        int Damage = item.Damage * item.Level;
        UpdateHpDamagePower(hp, Damage);
        OnPlayerInforChange(InfoType.All);
    }

    //卸下装备
    public void DressOff(Inventory item)
    {
        switch (item.EquipType)
        {
            case "Helm":
                if (helmInventory.Name != null)
                {
                    helmInventory = new Inventory();
                }
                break;
            case "Cloth":
                if (clothInventory.Name != null)
                {
                    clothInventory = new Inventory();
                }
                break;
            case "Weapon":
                if (weaponInventory.Name != null)
                {
                    weaponInventory = new Inventory();
                }
                break;
            case "Shoes":
                if (shoesInventory.Name != null)
                {
                    shoesInventory = new Inventory();
                }
                break;
            case "Necklace":
                if (necklaceInventory.Name != null)
                {
                    necklaceInventory = new Inventory();
                }
                break;
            case "Bracelet":
                if (braceleInventory.Name != null)
                {
                    braceleInventory = new Inventory();
                }
                break;
            case "Ring":
                if (ringInventory.Name != null)
                {
                    ringInventory = new Inventory();
                }
                break;
            case "Wing":
                if (wingInventory.Name != null)
                {
                    wingInventory = new Inventory();
                }
                break;
        }
        item.IsDressed = "No";
        InventoryManager.instance.UpdateInventory(item);
        InventoryBg.instance.AddInventoryItem(item);
        int hp = item.Hp * item.Level;
        int damage = item.Damage * item.Level;
        UpdateHpDamagePower(hp,damage,false);
        OnPlayerInforChange(InfoType.Equip);
    }

    //更新主角的HP和攻击力和战斗力
    public void UpdateHpDamagePower(int hp,int damage,bool isAdd = true)
    {
        if (isAdd)
        {
            this.damage += damage;
            this.hp += hp;
            this.power = (this.hp + this.damage);
        }
        else
        {
            this.damage -= damage;
            this.hp -= hp;
            this.power = (this.hp + this.damage);
        }
        OnPlayerInforChange(InfoType.All);
    }

    public bool CheckName (string newName)
    {
        if (newName == GameController.instance.Player.playerName) return false;

        string path = PlayerPrefs.GetString("serverPath") + "PlayerData.json";

        GameController.instance.Player.playerName = newName;
        //调用更新角色信息的方法
        OnPlayerInforChange(InfoType.Name);
        return true;
    }

    public bool SubMoney(string type, int subNum,int addNum)
    {
        bool isSuc = false;
        if (type == "")
        {
            if (GameController.instance.Player.coin >= subNum)
            {
                isSuc = true;
                GameController.instance.Player.coin -= subNum;
                GameController.instance.Player.coin += addNum;

                OnPlayerInforChange(InfoType.Coin);
            }
        }
        switch (type)
        {
            case "Coin":
                if (GameController.instance.Player.diamond >= subNum)
                {
                    GameController.instance.Player.coin += addNum;
                    GameController.instance.Player.diamond -= subNum;
                    OnPlayerInforChange(InfoType.All);
                    isSuc = true;
                }
                break;
            case "Diamond":
                if (GameController.instance.Player.coin >= subNum)
                {
                    GameController.instance.Player.diamond += addNum;
                    GameController.instance.Player.coin -= subNum;
                    OnPlayerInforChange(InfoType.All);
                    isSuc = true;
                }
                break;
        }
        return isSuc;
    }

    public void InventoryUse(string type,Inventory it,int count)
    {
        switch (type)
        {
            case "Energy":
                if (it.ApplyValue * count + GameController.instance.Player.energy >= 100)
                {
                    GameController.instance.Player.energy = Energy = 100;
                }
                else
                {
                    Energy = GameController.instance.Player.energy += it.ApplyValue * count;
                }

                OnPlayerInforChange(InfoType.Energy);
                break;
        }
    }

    public bool GetEnergy(int energy)
    {
        if (Energy <= 0) return false;

        if (Energy < energy) return false;

        Energy -= energy;
        GameController.instance.Player.energy = Energy;
        GameController.instance.UpdatePlayerInfor();
        OnPlayerInforChange(InfoType.Energy);

        return true;
    }

    //副本胜利后调用的方法
    public void PlayerReward(int exp,int coin,int diamond)
    {
        Player player = GameController.instance.Player;
        UpgradePlayerLevel(exp,player);

        player.coin += coin;
        player.diamond += diamond;
        
        OnPlayerInforChange(InfoType.All);
    }

    void UpgradePlayerLevel(int exp,Player player)
    {
        int needExp = GameController.GetRequireExp(player.level + 1);
        if ((exp + player.exp) >= needExp)
        {
            int tempExp = (exp + player.exp);
            tempExp -= needExp;
            //player.exp = tempExp;
            player.level += 1;

            InitHpDamage();
            if (tempExp > GameController.GetRequireExp(player.level + 1))
            {
                player.exp = 0;
                UpgradePlayerLevel(tempExp, player);
            }
            else
            {
                player.exp = tempExp;
            }
        }
        else
        {
            player.exp += exp;
        }
        OnPlayerInforChange(InfoType.Exp);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public TextAsset listInfor;

    public Dictionary<int,Inventory> itDict = new Dictionary<int, Inventory>();
    [NonSerialized]
    public List<Inventory> itList = new List<Inventory>();
    
    public delegate void OnInventoryChangeEvent();
    public event OnInventoryChangeEvent OnInventoryChange;

    private string filePath;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
	{
	    filePath = PlayerPrefs.GetString("serverPath") + "InventoryData.json";
        InitAllInventory();
	    InitPlayerInventory();
    }
	
	// Update is called once per frame
	void Update ()
    {
		AddInventory();
	}

    //增加物品
    public void AddInventory()
    {
        if (Input.touchCount == 3 && itList.Count <= 32)
        {
            int[] index;
            if (GameController.instance.Player.isMan == 0)
            {
                index = new[] { 101, 102, 103, 104, 105, 106, 107, 108, 301, 302, 401 };
            }
            else
            {
                index = new[] { 201, 202, 203, 204, 205, 206, 207, 208, 301, 302, 401 };
            }
            
            List<Inventory> itemList = JsonTools.AnalyticalJsonFile<List<Inventory>>(filePath);
            int pos = 1;
            if (itemList != null)
            {
                pos = itemList.Count + 1;
            }
            
            int itId = index[UnityEngine.Random.Range(0, index.Length)];
            Inventory it = null;
            itDict.TryGetValue(itId, out it);
            it.PlayerId = PlayerPrefs.GetInt("selectPlayerId");
            it.Id = pos;
            it.IsDressed = "No";
            it.Count = 1;
            if (it != null && it.InventoryType == "Equip")
            {
                it.Level = 1;
                UpdateInventory(it, false);
            }
            else
            {
                bool isExit = false;
                foreach (Inventory inventory in itList)
                {
                    if (inventory.InventoryId == it.InventoryId)
                    {
                        isExit = true;
                        it = inventory;
                        break;
                    }
                }
                if (isExit)
                {
                    it.Count++;
                    UpdateInventory(it);
                }
                else
                {
                    UpdateInventory(it,false);
                }
            }
            OnInventoryChange();
        }
    }

    //把所有的物品信息加载到Dictionary里
    public void InitAllInventory()
    {
        //安卓
        //string path = Application.streamingAssetsPath + "/AllInventoryData.json";
        //List<Inventory> itLists = JsonTools.AnalyticalJsonFile<List<Inventory>>(path, true);
        string path = Application.dataPath + "/AllInventoryData.json";
        List<Inventory> itLists = JsonTools.AnalyticalJsonFile<List<Inventory>>(path);

        foreach (Inventory inventory in itLists)
        {
            itDict.Add(inventory.InventoryId,inventory);
        }
    }
    
    //加载所有的物品信息到Json文件中
    public void InitAllToFile()
    {
        string str = listInfor.ToString();
        string[] itemStrArray = str.Split('-');
        foreach (string itemStr in itemStrArray)
        {
            string[] proArray = itemStr.Split('|');
            Inventory inventory = new Inventory
            {
                InventoryId = Int32.Parse(proArray[0]),
                Name = proArray[1],
                Image = proArray[2],
                InventoryType = proArray[3]
            };
            if (inventory.InventoryType == "Equip")
            {
                inventory.EquipType = proArray[4];
                inventory.Level = 1;
                inventory.Damage = Int32.Parse(proArray[8]);
                inventory.Hp = Int32.Parse(proArray[9]);
                inventory.Power = Int32.Parse(proArray[10]);
                inventory.IsDressed = "No";
            }
            inventory.Price = Int32.Parse(proArray[5]);
            inventory.InforType = proArray[11];
            if (inventory.InventoryType == "Drug")
            {
                inventory.ApplyValue = Int32.Parse(proArray[12]);
            }
            inventory.Des = proArray[13];
            itDict.Add(inventory.InventoryId, inventory);
            itList.Add(inventory);
        }

        JsonTools.WriteJsonFile(itList, Application.streamingAssetsPath + "/AllInventoryData.json");
    }

    //加载该角色下的物品
    public void InitPlayerInventory()
    {
        int playerId = PlayerPrefs.GetInt("selectPlayerId");
        List<Inventory> itLists = JsonTools.AnalyticalJsonFile<List<Inventory>>(filePath);

        if (itLists == null || itLists.Count <= 0) return;

        foreach (Inventory inventory in itLists)
        {
            if (inventory.PlayerId == playerId)
            {
                itList.Add(inventory);
            }
        }
        
        if (OnInventoryChange != null) OnInventoryChange();
    }

    //升级装备方法
    public void UpgradeEquip(Inventory item)
    {
        foreach (Inventory inventory in itList)
        {
            if (inventory.Id == item.Id)
            {
                inventory.Level = item.Level;
                //重新计算该装备的伤害和攻击力
                if (item.IsDressed == "Yes")
                {
                    PlayerInfor.instance.UpdateHpDamagePower(item.Hp, item.Damage);
                }
                UpdateInventory(item);
            }
        }
        OnInventoryChange();
    }

    public void ChangeCount(Inventory it,int count)
    {
        if (it.Count < count) return;

        it.Count -= count;
        if (it.Count <= 0)
        {
            RemoveInventroyItem(it);
        }
        PlayerInfor.instance.InventoryUse(it.InforType, it, count);

        OnInventoryChange();
    }

    //更新物品信息到Json文件
    public void UpdateInventory(Inventory it,bool isUpdate = true)
    {
        List<Inventory> itemList = JsonTools.AnalyticalJsonFile<List<Inventory>>(filePath);

        if (isUpdate)
        {
            if (itemList != null && itemList.Count > 0)
            {
                foreach (Inventory inventory in itemList)
                {
                    if (inventory.Id == it.Id)
                    {
                        inventory.Count = it.Count;
                        inventory.Level = it.Level;
                        inventory.Damage = it.Damage;
                        inventory.Hp = it.Hp;
                        inventory.IsDressed = it.IsDressed;
                        inventory.Power = it.Power;
                    }
                }
            }
        }
        else if (itemList == null || itemList.Count == 0)
        {
            itemList = new List<Inventory>();
            itemList.Add(it);
        }
        else
        {
            itemList.Add(it);
        }
        itList = itemList;
        JsonTools.WriteJsonFile(itList, filePath);
    }

    public void RemoveInventroyItem(Inventory it)
    {
        for (int i = itList.Count - 1; i >= 0; i--)
        {
            if (itList[i].Id == it.Id)
            {
                itList.RemoveAt(i);
            }
        }
        if (itList.Count > 0)
        {
            for (int i = 0; i < itList.Count; i++)
            {
                itList[i].Id = i + 1;
            }
        }
        JsonTools.WriteJsonFile(itList, filePath);
        OnInventoryChange();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBg : MonoBehaviour
{
    public static InventoryBg instance;

    public List<InventoryItem> itemList = new List<InventoryItem>();  //存放所有的物品的格子

    private Text inventoryNum;
    private int count;

    void Awake()
    {
        instance = this;
        InventoryManager.instance.OnInventoryChange += OnInventoryChange;
        inventoryNum = this.transform.Find("InventoryNum").GetComponent<Text>();
    }

    void Destroy()
    {
        InventoryManager.instance.OnInventoryChange -= OnInventoryChange;
    }

	// Use this for initialization
	void Start ()
	{
	    inventoryNum.text = 0 + "/32";
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void OnInventoryChange()
    {
        UpdateShow();
    }

    private void UpdateShow()
    {
        int temIndex = 0;
        foreach (Inventory it in InventoryManager.instance.itList)
        {
            if (it.IsDressed == "No" && it.PlayerId == PlayerPrefs.GetInt("selectPlayerId"))
            {
                itemList[temIndex].SetInventoryItem(it);
                temIndex++;
            }
        }
        count = temIndex;
        for (int i = count; i < itemList.Count; i++)
        {
            itemList[i].Clear();
        }
        inventoryNum.text = count + "/32";
    }

    public void AddInventoryItem(Inventory it)
    {
        //foreach (InventoryItem inventoryItem in itemList)
        //{
        //    if (inventoryItem != null)
        //        continue;
        //    inventoryItem.SetInventoryItem(it);
        //    count++;
        //    break;
        //}
        //inventoryNum.text = count + "/32";

        if (count >= 32) return;

        itemList[count].SetInventoryItem(it);
        count++;
        inventoryNum.text = count + "/32";
    }

    //整理背包
    public void OnClearClick()
    {
        UpdateShow();
    }
}

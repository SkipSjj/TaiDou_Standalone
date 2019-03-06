using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knapsack : MonoBehaviour
{
    public static Knapsack instance;

    private Animator anim;
    private Button saleBtn;
    private Text priceNum;

    private EquipInfor equipInfor;
    private InventoryInforBg inventoryBg;
    private InventoryItem item;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start ()
	{
	    anim = this.GetComponent<Animator>();
	    saleBtn = transform.Find("KnapsackBG/InventoryBG/SaleBtn").GetComponent<Button>();
	    priceNum = transform.Find("KnapsackBG/InventoryBG/SalePriceBG/SalePrice").GetComponent<Text>();

	    equipInfor = transform.Find("KnapsackBG/EquipmentInforBG").GetComponent<EquipInfor>();
	    inventoryBg = transform.Find("KnapsackBG/InventoryInforBG").GetComponent<InventoryInforBg>();

	    DisableBtn();
	}
	
	// Update is called once per frame
	void Update ()
   {
		
	}

    //处理物品单击事件
    public void OnInventoryItemClick(object[] objArray)
    {
        Inventory it = objArray[0] as Inventory;
        bool isLeft = (bool)objArray[1];

        if (it != null && it.InventoryType == "Equip")
        {
            item = null;
            KnasackPlayerEquip playerEquip = null;
            if (isLeft)
            {
                item = objArray[2] as InventoryItem;
            }
            else
            {
                playerEquip = objArray[2] as KnasackPlayerEquip;
            }
            inventoryBg.Hide();
            equipInfor.Show(it,item,playerEquip,isLeft);
        }
        else
        {
            item = objArray[2] as InventoryItem;
            equipInfor.Hide();
            inventoryBg.Show(it,item);
        }
        if (it != null && ((isLeft && it.InventoryType == "Equip") || (it.InventoryType != "Equip")))
        {
            priceNum.text = it.Price * it.Count + "";
            EnableBtn();
        }
    }

    //出售物品按钮事件
    public void SaleBtnClick()
    {
        PlayerInfor.instance.SubMoney("", 0, int.Parse(priceNum.text));
        InventoryManager.instance.RemoveInventroyItem(item.it);
        item.Clear();

        equipInfor.Hide();
        inventoryBg.Hide();
    }

    public void DisableBtn()
    {
        saleBtn.interactable = false;
        priceNum.text = "";
    }


    public void EnableBtn()
    {
        saleBtn.interactable = true;
    }
    public void Show()
    {
        anim.SetBool("isShow",true);
    }

    public void Hide()
    {
        anim.SetBool("isShow",false);
    }
}

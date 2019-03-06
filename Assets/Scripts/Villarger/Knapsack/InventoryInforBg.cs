using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInforBg : MonoBehaviour
{
    private Text name;
    private Text des;
    private Image image;
    private Text btnText;
    private Inventory it;
    private InventoryItem itemBg;

    void Awake()
    {
        name = this.transform.Find("InventoryName").GetComponent<Text>();
        des = this.transform.Find("InventoryDes").GetComponent<Text>();
        image = this.transform.Find("InventoryBG/InventoryImage").GetComponent<Image>();
        btnText = this.transform.Find("AllUseBtn/Text").GetComponent<Text>();
    }

	// Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Show(Inventory it,InventoryItem itemBg)
    {
        this.gameObject.SetActive(true);
        this.it = it;
        this.itemBg = itemBg;
        name.text = it.Name;
        image.sprite = Resources.Load<Sprite>(it.Image);
        des.text = it.Des;
        btnText.text = "批量使用" + "(" + it.Count + ")";

    }

    public void Hide()
    {
        Clear();
        this.gameObject.SetActive(false);
        this.transform.parent.parent.SendMessage("DisableBtn");
    }

    public void UseBtn()
    {
        if (it.InforType == "Energy")
        {
            int energy = GameController.instance.Player.energy;
            if (energy >= 100)
            {
                HintInfor.instance.ShowInformation("能量已经满了，无法使用");
            }
            else
            {
                InventoryManager.instance.ChangeCount(it, 1);
            }
        }
        else
        {
            InventoryManager.instance.ChangeCount(it, 1);
        }

        Hide();
    }

    public void UseAllBtn()
    {
        if (it.InforType == "Energy")
        {
            int energy = GameController.instance.Player.energy;
            if (energy >= 100)
            {
                HintInfor.instance.ShowInformation("能量已经满了，无法使用");
            }
            else
            {
                InventoryManager.instance.ChangeCount(it, it.Count);
            }
        }
        else
        {
            InventoryManager.instance.ChangeCount(it, it.Count);
        }

        Hide();
    }

    void Clear()
    {
        it = null;
        itemBg = null;
    }
}

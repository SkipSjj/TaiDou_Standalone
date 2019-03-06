using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Image image;
    private Text num;

    public Inventory it;

    void Awake()
    {
        image = this.transform.Find("Button").GetComponent<Image>();
        num = this.transform.Find("Text").GetComponent<Text>();
        Clear();
    }
	// Use this for initialization
	void Start ()
	{
	   
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetInventoryItem(Inventory it)
    {
        this.it = it;
        image.gameObject.SetActive(true);
        image.GetComponent<Button>().interactable = true;
        image.sprite = Resources.Load<Sprite>(this.it.Image);
        num.text = this.it.Count <= 1 ? "" : this.it.Count.ToString();
    }

    public void InventoryClick()
    {
        if (it != null)
        {
            object[] objArray = new object[3];
            objArray[0] = it;
            objArray[1] = true;
            objArray[2] = this;
            this.transform.parent.parent.parent.parent.parent.parent.SendMessage("OnInventoryItemClick", objArray);
        }
    }

    public void Clear()
    {
        it = null;
        image.gameObject.SetActive(false);
        image.GetComponent<Button>().interactable = false;
        image.sprite = Resources.Load<Sprite>("bg_道具");
        num.text = "";
    }
}

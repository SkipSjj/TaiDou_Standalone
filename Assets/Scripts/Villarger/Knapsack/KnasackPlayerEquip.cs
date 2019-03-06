using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnasackPlayerEquip : MonoBehaviour
{
    private Image image;
    private Inventory it;

    void Awake()
    {
        image = this.GetComponent<Image>();
    }
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetEquipItem(Inventory it)
    {
        if (it == null || string.IsNullOrEmpty(it.Image))
        {
            Clear();
            return;
        }
        this.it = it;
        this.GetComponent<Button>().interactable = true;
        image.sprite = Resources.Load<Sprite>(it.Image);
    }

    public void EquipClick()
    {
        if (it != null)
        {
            object[] objArray = new object[3];
            objArray[0] = it;
            objArray[1] = false;
            objArray[2] = this;

            transform.parent.parent.parent.SendMessage("OnInventoryItemClick", objArray,SendMessageOptions.DontRequireReceiver);
        }
    }

    public void Clear()
    {
        it = null;
        this.GetComponent<Button>().interactable = false;
        image.sprite = Resources.Load<Sprite>("bg_道具");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Animator shopAnim;
    private Button coinBtn;
    private Button diamondBtn;

    private GameObject coinUI;
    private GameObject diamondUI;

	// Use this for initialization
	void Start ()
	{
	    shopAnim = this.GetComponent<Animator>();
	    coinBtn = this.transform.Find("ShopBG/ExchangeCoinBtn").GetComponent<Button>();
	    diamondBtn = this.transform.Find("ShopBG/ExchangeDiamondBtn").GetComponent<Button>();

	    coinUI = this.transform.Find("ShopBG/ExchangeCoin").gameObject;
	    diamondUI = this.transform.Find("ShopBG/ExchangeDiamond").gameObject;

        InitShow();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void InitShow()
    {
        diamondBtn.GetComponent<Image>().color = new Color(0.7f,0.7f,0.7f,1f);
    }

    public void ShopBtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "ExchangeCoinBtn":
                InitShow();
                coinBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                coinUI.SetActive(true);
                diamondUI.SetActive(false);
                break;
            case "ExchangeDiamondBtn":
                coinBtn.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                diamondBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                coinUI.SetActive(false);
                diamondUI.SetActive(true);
                break;
        }
    }

    public void ExchangeBtnClick(Button btn)
    {
        int subNum = Convert.ToInt32(btn.transform.parent.GetChild(1).GetComponent<Text>().text.Replace("X ", ""));
        int addNum = Convert.ToInt32(btn.transform.parent.GetChild(4).GetComponent<Text>().text.Replace("X ", ""));

        string type = null;
        if (btn.transform.parent.parent.name.EndsWith("Coin"))
        {
            type = "Coin";
        }
        else if(btn.transform.parent.parent.name.EndsWith("Diamond"))
        {
            type = "Diamond";
        }
        HintInfor.instance.ShowInformation(PlayerInfor.instance.SubMoney(type, subNum,addNum) ? "兑换成功，请注意查收" : "兑换失败");
    }

    public void Show()
    {
        shopAnim.SetBool("isShow",true);
    }

    public void Hide()
    {
        shopAnim.SetBool("isShow",false);
    }
}

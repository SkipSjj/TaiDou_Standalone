using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBar : MonoBehaviour
{
    private Text coinText;
    private Text diamondext;
    
    void Awake()
    {
        coinText = this.transform.Find("CoinBG/CoinNum").GetComponent<Text>();
        diamondext = this.transform.Find("DiamondBG/DiamondNum").GetComponent<Text>();
        PlayerInfor.instance.OnPlayerInforChange += OnPlayerInforChange;
    }
	// Use this for initialization
	void Start ()
	{
	    
    }

    private void OnDestroy()
    {
        PlayerInfor.instance.OnPlayerInforChange -= OnPlayerInforChange;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void OnPlayerInforChange(InfoType infoType)
    {
        if (infoType == InfoType.Coin || infoType == InfoType.Diamond || infoType == InfoType.All)
        {
            UpdateShow();
        }
    }

    private void UpdateShow()
    {
        Player player = GameController.instance.Player;

        this.coinText.text = player.coin + "";
        this.diamondext.text = player.diamond + "";
    }
}

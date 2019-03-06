using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBg : MonoBehaviour
{
    private Image headImage;
    private new Text name;
    private Text level;
    private Slider hpSlider;
    private Slider magicSlider;

	// Use this for initialization
	void Start ()
	{
	    headImage = this.transform.Find("HeadImage").GetComponent<Image>();
	    name = this.transform.Find("PlayerName").GetComponent<Text>();
	    level = this.transform.Find("PlayerLevel").GetComponent<Text>();
	    hpSlider = this.transform.Find("HpSlider").GetComponent<Slider>();
	    magicSlider = this.transform.Find("MagicSlider").GetComponent<Slider>();

	    hpSlider.value = 1f;
	    magicSlider.value = 1f;
        InitShow();
	    PlayerAttack.instance.OnPlayerHpMagicChange += Show;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void InitShow()
    {
        headImage.sprite = Resources.Load<Sprite>(GameController.instance.Player.isMan == 0 ? "头像底板男性" : "头像底板女性");
        level.text = GameController.instance.Player.level + "";
        name.text = GameController.instance.Player.playerName;
    }

    public void Show()
    {
        hpSlider.value = (float)PlayerAttack.instance.hp / PlayerAttack.instance.totalHp;
        magicSlider.value = (float) PlayerAttack.instance.magic / PlayerAttack.instance.totalMagic;
    }
}

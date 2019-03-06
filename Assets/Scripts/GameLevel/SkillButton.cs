using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public int skillPos = 0;
    public int magic = 0;
    [NonSerialized]
    public int damage;
    [NonSerialized]
    public float coldTime = 2f;
    public string image;

    private float coldTimer = 0f;
    private PlayerAnimations playerAnim;
    private Image maskImage;
    private Button btn;
    private Image icon;

	// Use this for initialization
	void Start ()
	{
	    playerAnim = GameLevelManager.instance.playerGo.GetComponent<PlayerAnimations>();
	    btn = this.GetComponent<Button>();
	    icon = this.GetComponent<Image>();

        if (this.transform.Find("Mask"))
	    {
	        maskImage = this.transform.Find("Mask").GetComponent<Image>();
	        Init();
        }

	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (maskImage == null) return;

	    if (coldTimer > 0f)
	    {
	        coldTimer -= Time.deltaTime;
	        maskImage.fillAmount = (1 - coldTimer / coldTime);
	        if (coldTimer <= 0f)
	        {
	            Enable();
	        }
	    }
	    else
	    {
	        maskImage.fillAmount = 0f;
	    }
	}

    void Init()
    {
        if (image != null && icon != null)
        {
            maskImage.sprite = icon.sprite = Resources.Load<Sprite>(image);
        }
    }

    public void OnClick()
    {
        if (PlayerAttack.instance.GetMagic(magic) || skillPos == 0)
        {
            playerAnim.AttackBtnClick(skillPos);
            if (maskImage != null)
            {
                coldTimer = coldTime;
                Disable();
                maskImage.fillAmount = 1f;
            }
        }
        else
        {
            //提示魔法值不足
            HintInfor.instance.ShowInformation("魔法值不足!!!");
        }
    }

    private void Disable()
    {
        btn.interactable = false;
    }

    private void Enable()
    {
        btn.interactable = true;
    }
}

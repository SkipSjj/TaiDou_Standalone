using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public static BossHpBar instance;

    private Image hpImage;
    private Text hpText;
    private int maxHp;
    private bool isFirst = false;

    void Awake()
    {
        instance = this;
        hpImage = this.transform.Find("Image").GetComponent<Image>();
        hpText = this.transform.Find("Text").GetComponent<Text>();
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Show(int maxHp)
    {
        this.maxHp = maxHp;
        this.gameObject.SetActive(true);

        UpdateShow(maxHp);
    }

    public void UpdateShow(int currentHp)
    {
        if (currentHp <= 0)
        {
            currentHp = 0;
            Destroy(this.gameObject,0.5f);
        }

        hpImage.fillAmount = (float) currentHp / maxHp;
        hpText.text = currentHp + "/" + maxHp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    public static Combo instance;

    private float comboTime = 2f;
    private float timer = 0f;
    private int comboCount;
    private Text numberText;

    void Awake()
    {
        instance = this;
        numberText = this.GetComponent<Text>();
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timer -= Time.deltaTime;
	    if (timer <= 0)
	    {
	        this.gameObject.SetActive(false);
	        comboCount = 0;
	    }
	}

    public void ShowCombo()
    {
        this.gameObject.SetActive(true);
        timer = comboTime;
        comboCount++;
        numberText.text = comboCount + "  Combo";
        this.transform.localScale = Vector3.zero;

        iTween.ShakePosition(this.gameObject, new Vector3(0.2f, 0f, 0.2f), 0.2f);
        iTween.ScaleTo(this.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.2f);
    }
}

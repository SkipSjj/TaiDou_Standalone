using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerShow : MonoBehaviour
{
    public static PowerShow instance;

    private float startValue;
    private int endValue;
    private bool isStart = false;
    private bool isUp = true;

    private Text powerText;
    private Animator animator;

    public int speed = 500;

    void Awake()
    {
        instance = this;
        powerText = transform.Find("Text").GetComponent<Text>();
        animator = this.GetComponent<Animator>();
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (!isStart) return;
	    if (isUp)
	    {
	        powerText.color = new Color(1f,1f,23/255f,1f);
            startValue += speed * Time.deltaTime;
            if (startValue > endValue)
            {
                isStart = false;
                startValue = endValue;
                StartCoroutine(OnAnimationFinished());
            }
	    }
	    else
	    {
            powerText.color = Color.red;
	        startValue -= speed * Time.deltaTime;
	        if (startValue < endValue)
	        {
	            isStart = false;
	            startValue = endValue;
                StartCoroutine(OnAnimationFinished());
	        }
	    }
	    powerText.text = (int)startValue + "";
	}

    public void ShowPowerChange(int startValue, int endValue)
    {
        this.gameObject.SetActive(true);
        animator.SetBool("isShow",true);
        this.startValue = startValue;
        this.endValue = endValue;
        isUp = endValue > startValue;
        isStart = true;
    }

    IEnumerator OnAnimationFinished()
    {
        yield return new WaitForSeconds(0.6f);

        animator.SetBool("isShow", false);
        gameObject.SetActive(false);
    }
}

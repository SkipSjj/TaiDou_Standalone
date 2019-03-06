using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintInfor : MonoBehaviour
{

    private Text information;
    private Animator hintInforAnim;

    public static HintInfor instance;

    void Awake()
    {
        instance = this;
        information = this.transform.Find("HintInfor").GetComponent<Text>();
        hintInforAnim = this.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ShowInformation(string infor)
    {
        information.text = infor;
        hintInforAnim.SetBool("isShow",true);

        StartCoroutine(HideHintInfor());
    }

    IEnumerator HideHintInfor()
    {
        yield return new WaitForSeconds(5f);

        information.text = "";
        hintInforAnim.SetBool("isShow",false);
    }
}

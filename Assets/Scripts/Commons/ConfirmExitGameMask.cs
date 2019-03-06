using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmExitGameMask : MonoBehaviour
{
    public static ConfirmExitGameMask instance;

    private Animator anim;

    void Awake()
    {
        instance = this;
        anim = this.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start ()
	{
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnExitGameBtnClick()
    {
        Hide();
        Application.Quit();
    }

    public void Show()
    {
        anim.SetBool("isShow", true);
    }

    public void Hide()
    {
        anim.SetBool("isShow", false);
    }
}

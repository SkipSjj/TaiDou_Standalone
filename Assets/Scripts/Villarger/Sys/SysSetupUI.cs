using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SysSetupUI : MonoBehaviour
{
    private Image audioImage;
    private Text audioText;
    private Animator anim;
    private Button changePlayerBtn;
    private Button exitLoginBtn;
    private Button exitGameBtn;

    // Use this for initialization
    void Start ()
	{
	    audioImage = this.transform.Find("SystemSetupBG/AudioBtn").GetComponent<Image>();
	    audioText = audioImage.GetComponentInChildren<Text>();
	    anim = this.GetComponent<Animator>();
	    changePlayerBtn = this.transform.Find("SystemSetupBG/ChangePlayerBtn").GetComponent<Button>();
	    exitGameBtn = this.transform.Find("SystemSetupBG/ExitGameBtn").GetComponent<Button>();
	    exitLoginBtn = this.transform.Find("SystemSetupBG/ExitLoginBtn").GetComponent<Button>();

        changePlayerBtn.onClick.AddListener(delegate
        {
            SysSetupController.instance.ChangePlayerBtnClick();
        });

	    exitGameBtn.onClick.AddListener(delegate
	    {
	        SysSetupController.instance.ExitGameBtnClick();
	    });

	    exitLoginBtn.onClick.AddListener(delegate
	    {
	        SysSetupController.instance.ExitLoginBtnClick();
	    });

        InitShow();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    private void InitShow()
    {
        string spriteName = PlayerPrefs.GetString("spriteName", "pic_音效开启");
        audioImage.sprite = Resources.Load<Sprite>(spriteName);
        audioText.text = spriteName.Replace("pic_","");
    }

    public void AudioBtnClick()
    {
        string spriteName = PlayerPrefs.GetString("spriteName", "pic_音效开启");
        switch (spriteName)
        {
            case "pic_音效开启":
                PlayerPrefs.SetString("spriteName", "pic_音效关闭");
                PlayerPrefs.SetString("IsMusic", "true");
                break;
            case "pic_音效关闭":
                PlayerPrefs.SetString("spriteName", "pic_音效开启");
                PlayerPrefs.SetString("IsMusic", "false");
                break;
        }
        InitShow();
        SoundManager.instance.UpdateAudio();
    }

    public void Show()
    {
        anim.SetBool("isShow",true);
    }

    public void Hide()
    {
        anim.SetBool("isShow",false);
    }
}

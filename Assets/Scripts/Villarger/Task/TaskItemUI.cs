using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItemUI : MonoBehaviour
{
    private Task task;

    private Image typeLogo;
    private Image taskLogo;
    private Text taskName;
    private Text taskDes;
    private Text coinNum;
    private Text diamondNum;
    private Text btnText;

    void Awake()
    {
        typeLogo = this.transform.Find("TypeLogo").GetComponent<Image>();
        taskLogo = this.transform.Find("TaskLogo").GetComponent<Image>();
        taskName = this.transform.Find("TaskName").GetComponent<Text>();
        taskDes = this.transform.Find("TaskDes").GetComponent<Text>();
        coinNum = this.transform.Find("RewardCoinNum").GetComponent<Text>();
        diamondNum = this.transform.Find("RewardDiamondNum").GetComponent<Text>();
        btnText = this.transform.Find("CombatBtn/Text").GetComponent<Text>();
    }

    // Use this for initialization
    void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetTask(Task task)
    {
        this.task = task;
        UpdateShow();
    }
     
    private void UpdateShow()
    {
        Sprite sprite = null;
        switch (task.TaskType)
        {
            case "Main":
                sprite = Resources.Load<Sprite>("pic_主线");
                break;
            case "Reward":
                sprite = Resources.Load<Sprite>("pic_奖赏");
                break;
            case "Daily":
                sprite = Resources.Load<Sprite>("pic_日常");
                break;
        }
        typeLogo.sprite = sprite;

        switch (task.ICON)
        {
            case "男性手镯":
                sprite = Resources.Load<Sprite>("男性手镯");
                break;
            case "女性项链":
                sprite = Resources.Load<Sprite>("女性项链");
                break;
            case "男性头盔":
                sprite = Resources.Load<Sprite>("男性头盔");
                break;
        }
        taskLogo.sprite = sprite;

        taskName.text = task.TaskName;
        taskDes.text = task.Des;
        coinNum.text = "X " + task.Coin;
        diamondNum.text = "X " + task.Diamond;
        btnText.text = "战斗";
    }

    public void TaskBtnClick()
    {
        transform.parent.parent.parent.parent.parent.SendMessage("TaskItemClick",task,SendMessageOptions.DontRequireReceiver);
    }
}

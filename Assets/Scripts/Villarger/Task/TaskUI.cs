using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    public static TaskUI instance;

    private Animator taskAnim;
    private Transform taskPos;

    private string path;
    private int playerId;

    public GameObject taskPrefab;
    public Transform gameLevelPos;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
	{
	    taskAnim = this.GetComponent<Animator>();
	    taskPos = this.transform.Find("TaskBG/TaskList/Viewport/Content");
	    path = PlayerPrefs.GetString("serverPath") + "TaskData.json";
	    playerId = GameController.instance.Player.id;


	    List<Task> taskList = JsonTools.AnalyticalJsonFile<List<Task>>(path);
	    bool isHave = false;
	    if (taskList != null)
	    {
	        foreach (Task task in taskList)
	        {
	            if (task.PlayerId == playerId)
	            {
	                isHave = true;
	                break;
	            }
	        }
        }
	    
	    if (!isHave)
	    {
	        InitTaskToFile();
        }

        InitTaskShow();
	}
	
	// Update is called nce per frame
	void Update ()
    {
		
	}

    //把任务写入到本地文件中
    private void InitTaskToFile()
    {
        List<Task> taskList = JsonTools.AnalyticalJsonFile<List<Task>>(path) ?? new List<Task>();

        Task task1 = new Task
        {
            Id = 1,
            TaskType = "Main",
            TaskName = "试练之地",
            Coin = 500,
            Diamond = 1000,
            Des = "通过试练之地完成新手挑战",
            ICON = "男性手镯",
            NPCId = 1001,
            NPCTalkStr = "你好，英雄，准备好开始了吗？",
            PlayerId = playerId,
            State = "NoFinished"
        };
        taskList.Add(task1);
        Task task2 = new Task
        {
            Id = 2,
            TaskType = "Reward",
            TaskName = "赏金猎人",
            Coin = 100,
            Diamond = 20,
            Des = "通过赏金之地抓捕罪犯阿凡达",
            ICON = "女性项链",
            NPCId = 1002,
            NPCTalkStr = "你成长的好快，已经准备着迎接新的挑战了吗？",
            PlayerId = playerId,
            State = "NoFinished"
        };
        taskList.Add(task2);
        Task task3 = new Task
        {
            Id = 3,
            TaskType = "Daily",
            TaskName = "每日通关",
            Coin = 100,
            Diamond = 100,
            Des = "通关炼狱",
            ICON = "男性头盔",
            NPCId = 1002,
            NPCTalkStr = "准备好了吗？",
            PlayerId = playerId,
            State = "NoFinished"
        };
        taskList.Add(task3);
        
        JsonTools.WriteJsonFile(taskList, path);
    }

    public void InitTaskShow()
    {
        path = PlayerPrefs.GetString("serverPath") + "TaskData.json";
        List<Task> taskList = JsonTools.AnalyticalJsonFile<List<Task>>(path);

        foreach (Task task in taskList)
        {
            if (task.PlayerId == playerId && task.State == "NoFinished")
            {
                GameObject taskGo = Instantiate(taskPrefab, taskPos.position, Quaternion.identity);
                taskGo.transform.parent = taskPos;
                taskGo.transform.localScale = new Vector3(1f, 1f, 1f);
                TaskItemUI ti = taskGo.GetComponent<TaskItemUI>();
                ti.SetTask(task);
            }
        }
    }

    public void TaskItemClick(Task task)
    {
        PlayerPrefs.SetInt("SelectedTaskId", task.Id);
        Hide();
        PlayerAutoMove.instance.SetDestination(gameLevelPos.position,true);
    }

    public void UpdateTaskInfor(Task task)
    {
        List< Task> taskList = JsonTools.AnalyticalJsonFile<List<Task>>(path);
        if (taskList.Count <= 0) return;

        foreach (Task task1 in taskList)
        {
            if (task1.PlayerId == task.PlayerId && task1.Id == task.Id)
            {
                task1.State = task.State;
            }
        }

        JsonTools.WriteJsonFile(taskList,path);
        InitTaskShow();
    }

    public void Show()
    {
        taskAnim.SetBool("isShow",true);
    }

    public void Hide()
    {
        taskAnim.SetBool("isShow",false);
    }
}

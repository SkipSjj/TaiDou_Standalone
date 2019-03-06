using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ServerSelect : MonoBehaviour
{
    private Image lastServerBg;
    private Text lastServerName;
    private Transform content;
    private Text selectServerName;

    public GameObject serverPrefab;

	// Use this for initialization
	void Start ()
	{
	    lastServerBg = this.transform.Find("ServerBG").GetComponent<Image>();
	    lastServerName = lastServerBg.transform.Find("ServerName").GetComponent<Text>();
	    content = this.transform.Find("ServerList/Viewport/Content").transform;
	    selectServerName = GameObject.Find("StartMenuBG/ServerselectBtn").GetComponentInChildren<Text>();

	    lastServerName.text = PlayerPrefs.GetString("lastServerName", "一区  中国");

        if (PlayerPrefs.GetInt("lastOnLineNum",100) > 450)
	    {
	        lastServerName.color = Color.red;
            Sprite sprite = Resources.Load<Sprite>("btn_火爆1");
            lastServerBg.sprite = sprite;
	    }
        else
        {
            lastServerName.color = Color.green;
        }
        
        InitServerList();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void InitServerList()
    {
        if (serverPrefab == null) return;

        //安卓
        //string filePath = Application.streamingAssetsPath + "/ServerData.json";
        //List<Server> serverList = JsonTools.AnalyticalJsonFile<List<Server>>(filePath, true);
        string filePath = Application.dataPath + "/ServerData.json";
        List<Server> serverList = JsonTools.AnalyticalJsonFile<List<Server>>(filePath);

        if (serverList.Count <= 0) return;

        foreach (Server server in serverList)
        {
            GameObject go = Instantiate(serverPrefab, content.transform.position, Quaternion.identity);

            go.GetComponent<Button>().onClick.AddListener(delegate()
            {
                this.SelectServer(go);
            });
            Server serverItem = go.GetComponent<Server>();
            serverItem.serverName = server.serverName;
            serverItem.id = server.id;
            serverItem.onLineNum = server.onLineNum;

            if (server.onLineNum > 450)
            {
                Sprite sprite = Resources.Load<Sprite>("btn_火爆1");
                go.GetComponent<Image>().sprite = sprite;
                go.GetComponentInChildren<Text>().color = Color.red;


                SpriteState ss = new SpriteState
                {
                    highlightedSprite = Resources.Load<Sprite>("btn_火爆2"),
                    pressedSprite = Resources.Load<Sprite>("btn_火爆3"),
                    disabledSprite = Resources.Load<Sprite>("btn_火爆4")
                };

                go.GetComponent<Button>().spriteState = ss;
            }
            go.GetComponentInChildren<Text>().text = server.serverName;

            go.transform.parent = content.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SelectServer(GameObject go)
    {
        lastServerBg.sprite = go.GetComponent<Image>().sprite;
        Server serverItem = go.GetComponentInChildren<Server>();
        if (serverItem.onLineNum > 450)
        {
            lastServerName.color = Color.red;
        }
        else
        {
            lastServerName.color = Color.green;
        }
        lastServerName.text = serverItem.serverName;
        selectServerName.text = serverItem.serverName;
        PlayerPrefs.SetString("lastServerName", serverItem.serverName);
        PlayerPrefs.SetInt("lastOnLineNum",serverItem.onLineNum);
        PlayerPrefs.SetInt("lastServerId", serverItem.id);

        StartMenuController.instance.HideServerSelectBg();
    }
}

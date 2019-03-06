using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelBtn : MonoBehaviour
{
    public int id;
    public int needLevel;
    public string sceneName = "003-GameLevel";
    public int playerId;
    public int energy;
    public string des; 
    public int exp;
    public int coin;
    public int diamond;
    public int isPass;  //0表示未通关，1表示已通关

    private Image[] starArray;

    void Awake()
    {
        starArray = this.GetComponentsInChildren<Image>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void UpdateShow()
    {
        if (isPass == 0) return;

        Sprite sprite = Resources.Load<Sprite>("star-黄色");
        if (sprite != null)
        {
            starArray[1].sprite = starArray[2].sprite = starArray[3].sprite = sprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameLevelInfor
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
}

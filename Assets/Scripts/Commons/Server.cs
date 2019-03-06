using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Server : MonoBehaviour
{
    public int id;
    public string serverName;
    public int onLineNum;  //用来判断该区是否火爆
}

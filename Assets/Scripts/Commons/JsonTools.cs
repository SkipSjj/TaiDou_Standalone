using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using UnityEngine;

public class JsonTools
{
    public static T AnalyticalJsonFile<T>(string filePath,bool isStreaming = false)
    {
        string jsonStr;
        if (isStreaming)
        {
            WWW wRead = new WWW(filePath);
            while (!wRead.isDone)
            {

            }
            jsonStr = Encoding.UTF8.GetString(wRead.bytes);
            wRead.Dispose();
        }
        else
        {
            //安卓
            jsonStr = File.ReadAllText(filePath, Encoding.UTF8);

            //jsonStr = File.ReadAllText(filePath,Encoding.UTF8);//Application.dataPath返回的是项目下的assets文件夹
        }

        T item = JsonMapper.ToObject<T>(jsonStr);
        
        return item;
    }

    public static void WriteJsonFile<T>(T content, string filePath)
    {
        string json = JsonMapper.ToJson(content);
        json = Regex.Unescape(json);

        //写入数据
        FileStream fs = new FileStream(filePath, FileMode.Create);
        byte[] byteData = Encoding.UTF8.GetBytes(json);
        fs.Write(byteData, 0, byteData.Length);

        fs.Flush();
        fs.Close();
    }
}
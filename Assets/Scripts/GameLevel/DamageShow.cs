using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageShow : MonoBehaviour
{
    private RectTransform damageText;
    private bool isStart = false;
    private GameObject damageGo;

    public Transform damageParent;
    public Transform damagePos;
    public float YOffset = 120f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart)
            return;

        Vector2 target2DPos = Camera.main.WorldToScreenPoint(this.transform.position);
        damageText.position = target2DPos + new Vector2(0f, 150f);
    }

    public void CreateDamageText(string damage)
    {
        Vector2 target2DPos = Camera.main.WorldToScreenPoint(damagePos.position);
        damageGo = Instantiate(Resources.Load<GameObject>("DamageNum"), new Vector3(target2DPos.x,target2DPos.y,0f),Quaternion.identity);
        isStart = true;
        damageGo.GetComponent<Text>().text = damage;
        damageText = damageGo.GetComponent<RectTransform>();
        damageText.SetParent(damageParent);

        iTween.MoveTo(damageGo, damageText.position + new Vector3(0,50f,0f),2f);

        Invoke("DestroyGo",2f);
    }

    public void DestroyGo()
    {
        isStart = false;
        Destroy(damageGo);
    }
}

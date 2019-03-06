using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    private RectTransform hpBar;

    public Transform hpBarParent;
    public float YOffset = 100f;
    
	// Use this for initialization
	void Start ()
	{
	    GameObject hpGo = Instantiate(Resources.Load<GameObject>("HpBar"));
	    hpBar = hpGo.GetComponent<RectTransform>();
        hpGo.transform.SetParent(hpBarParent);
	    Vector2 target2DPos = Camera.main.WorldToScreenPoint(this.transform.position);
	    hpBar.position = target2DPos + new Vector2(0f, YOffset);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (hpBar == null)
	    {
	        return;
	    }
	    if (!this.GetComponent<CharacterController>().enabled && hpBar != null)
	    {
	        Destroy(hpBar.gameObject);
            return;
	    }

	    Vector2 target2DPos = Camera.main.WorldToScreenPoint(this.transform.position);
	    hpBar.position = target2DPos + new Vector2(0f, YOffset);

	    if (target2DPos.x > Screen.width || target2DPos.x < 0 || target2DPos.y > Screen.height || target2DPos.y < 0)
	    {
	        hpBar.gameObject.SetActive(false);
	    }
	    else
	    {
	        hpBar.gameObject.SetActive(true);
	    }
	}

    public GameObject GetHpBarGo()
    {
        return hpBar.gameObject;
    }
}

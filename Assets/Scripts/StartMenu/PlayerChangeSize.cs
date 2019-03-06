using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeSize : MonoBehaviour
{

    public float targetScale;
    public float originalScale;
    public int playerId;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseUpAsButton()
    {
        StartMenuController.instance.ChangePlayerSize(this.gameObject, targetScale, originalScale);
    }
}

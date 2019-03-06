using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float destoryTime = 2f;

    public GameObject go;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    destoryTime -= Time.deltaTime;
	    if (destoryTime <= 0f)
	    {
	        Destroy(this.gameObject);
            return;
	    }
	}
}

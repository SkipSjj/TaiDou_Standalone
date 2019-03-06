using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public static LoadingController instance;

    private Image fillImage;
    private Text loadingText;
    private AsyncOperation ao;

    private bool isAsync = false;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
	{
	    fillImage = this.transform.Find("LoadingBar/Fill").GetComponent<Image>();
	    loadingText = this.transform.Find("LoadingBar/LoadingNum").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isAsync) return;
        
        fillImage.fillAmount += ao.progress;
        loadingText.text = float.Parse(ao.progress.ToString("F2")) * 100 + "%";

        if (fillImage.fillAmount == 1f)
        {
            isAsync = false;
            Invoke("Hide",1f);
        }
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show(AsyncOperation ao)
    {
        this.ao = ao;
        isAsync = true;
    }
}

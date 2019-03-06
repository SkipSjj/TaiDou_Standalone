using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultMask : MonoBehaviour
{
    public static ResultMask instance;

    private Text resulText;

    public GameObject loadingBg;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
	{
	    resulText = this.transform.Find("ResultBg/ResultText").GetComponent<Text>();
	    resulText.text = "";
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void Show(string resultStr)
    {
        resulText.text = resultStr;
        iTween.ScaleTo(this.gameObject, new Vector3(1f, 1f, 1f), 1f);
    }

    public void Hide()
    {
        resulText.text = "";
        iTween.ScaleTo(this.gameObject, new Vector3(0f, 0f, 0f), 1f);
    }

    public void OnBackVillagerBtn()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("002-NoviceVillarger");
        loadingBg.SetActive(true);
        LoadingController.instance.Show(ao);
    }

    public void OnOnceAgain()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("003-GameLevel");
        loadingBg.SetActive(true);
        LoadingController.instance.Show(ao);
    }
}

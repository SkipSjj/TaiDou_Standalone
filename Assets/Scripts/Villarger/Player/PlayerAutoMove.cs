using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerAutoMove : MonoBehaviour
{
    public static PlayerAutoMove instance;

    public NavMeshAgent agent;
    private Animator anim;
    private bool isTask;
    
    public SimpleTouchController touchController;
    
    void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        anim = this.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (agent.enabled)
        {
            if (agent.remainingDistance <= 6f && agent.remainingDistance != 0f)
            {
                if (isTask)
                {
                    AsyncOperation ao = SceneManager.LoadSceneAsync("003-GameLevel");
                    LoadingController.instance.Show(ao);
                }
                GameLevelSelect.instance.Show();
                StopAutoMove();
            }
            if (touchController != null)
            {
                float h = touchController.GetTouchPosition.x;
                float v = touchController.GetTouchPosition.y;

                if (Mathf.Abs(h) > 0.05f ||Mathf.Abs(v) > 0.05f)
                {
                    StopAutoMove();
                }
            }
        }
    }

    public void SetDestination(Vector3 targetPos,bool isTask = false)
    {
        this.isTask = isTask;
        agent.enabled = true;
        anim.SetBool("isRun", true);
        agent.SetDestination(targetPos);
    }

    public void StopAutoMove()
    {
        agent.isStopped = true;
        anim.SetBool("isRun", false);
        agent.enabled = false;
    }
}

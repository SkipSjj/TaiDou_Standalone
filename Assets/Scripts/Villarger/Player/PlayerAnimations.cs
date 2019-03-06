using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerAttack playerAttack;

	// Use this for initialization
	void Start ()
    {
        animator = this.GetComponent<Animator>();
        playerAttack = this.GetComponent<PlayerAttack>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AttackBtnClick(int posType)
    {
        //if (playerAttack.hp <= 0) return;

        if (posType == 0)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Skill" + posType);
        }
    }
}

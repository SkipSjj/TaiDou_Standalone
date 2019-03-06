using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsBg : MonoBehaviour
{
    private SkillButton[] skillBtns;

    // Use this for initialization
    void Start()
    {
        skillBtns = this.GetComponentsInChildren<SkillButton>();

        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {
        if (skillBtns == null || GameController.instance.skillDict.Count <= 0) return;

        for (int i = 1; i < skillBtns.Length; i++)
        {
            switch (i)
            {
                case 1:
                    SetSkillBtnInfor(i, "One");
                    break;
                case 2:
                    SetSkillBtnInfor(i, "Two");
                    break;
                case 3:
                    SetSkillBtnInfor(i, "Three");
                    break;
            }
        }
    }

    void SetSkillBtnInfor(int index, string pos)
    {
        Skill skill;
        GameController.instance.skillDict.TryGetValue(pos, out skill);
        skillBtns[index].coldTime = skill.ColdTime;
        skillBtns[index].damage = skill.Damage;
        skillBtns[index].image = skill.ICON;
    }
}

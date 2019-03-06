using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    private Animator skillUIAnim;
    private SkillItem skill_1;
    private SkillItem skill_2;
    private SkillItem skill_3;

    private Text skillName;
    private Text hurt;
    private Text nextLevelHurt;
    private Text upgradeCoin;

    private string path;
    private Player player;
    private Button selectedBtn;

    // Use this for initialization
    void Start ()
	{
	    skillUIAnim = this.GetComponent<Animator>();
	    skill_1 = this.transform.Find("SkillBG/Skill_1").GetComponent<SkillItem>();
	    skill_2 = this.transform.Find("SkillBG/Skill_2").GetComponent<SkillItem>();
	    skill_3 = this.transform.Find("SkillBG/Skill_3").GetComponent<SkillItem>();

	    skillName = this.transform.Find("SkillBG/SkillName").GetComponent<Text>();
	    hurt = this.transform.Find("SkillBG/HurtNum").GetComponent<Text>();
	    nextLevelHurt = this.transform.Find("SkillBG/NextLevelHurtNum").GetComponent<Text>();
	    upgradeCoin = this.transform.Find("SkillBG/UpgradeCoinNum").GetComponent<Text>();

	    if (!IsCreateFile())
	    {
            InitSkillInforToFile();
        }

        InitSkillShow();
	}
	 
	// Update is called once per frame
	void Update ()
    {
		
	}

    private bool IsCreateFile()
    {
        path = PlayerPrefs.GetString("serverPath") + "SkillData.json";
        player = GameController.instance.Player;
        List<Skill> skillList = JsonTools.AnalyticalJsonFile<List<Skill>>(path);
        bool isHave = false;
        if (skillList != null)
        {
            foreach (Skill skill in skillList)
            {
                if (skill.PlayerId == player.id)
                {
                    isHave = true;
                    break;
                }
            }
        }

        return isHave;
    }

    public void Show()
    {
        skillUIAnim.SetBool("isShow",true);
    }

    public void Hide()
    {
        skillUIAnim.SetBool("isShow",false);
    }

    private void InitSkillInforToFile()
    {
        List< Skill> skillItem = JsonTools.AnalyticalJsonFile<List<Skill>>(path) ?? new List<Skill>();

        if (GameController.instance.Player.isMan == 0)
        {
            Skill skill1 = new Skill
            {
                Id = 101,
                PlayerId = player.id,
                ColdTime = 0,
                Damage = 30,
                ICON = "icon_zhu",
                IsMan = 0,
                Level = 1,
                Pos = "Basic",
                Type = "Basic",
                SkillName = "浮生万刃"
            };
            Skill skill2 = new Skill
            {
                Id = 102,
                PlayerId = player.id,
                ColdTime = 5,
                Damage = 100,
                ICON = "icon_up",
                IsMan = 0,
                Level = 1,
                Pos = "One",
                Type = "Skill",
                SkillName = "永恒零度"
            };
            Skill skill3 = new Skill
            {
                Id = 103,
                PlayerId = player.id,
                ColdTime = 8,
                Damage = 120,
                ICON = "iocn_ho",
                IsMan = 0,
                Level = 1,
                Pos = "Two",
                Type = "Skill",
                SkillName = "半月斩"
            };
            Skill skill4 = new Skill
            {
                Id = 104,
                PlayerId = player.id,
                ColdTime = 10,
                Damage = 180,
                ICON = "iocn_yi",
                IsMan = 0,
                Level = 1,
                Pos = "Three",
                Type = "Skill",
                SkillName = "咫尺天涯"
            };

            skillItem.Add(skill1);
            skillItem.Add(skill2);
            skillItem.Add(skill3);
            skillItem.Add(skill4);
        }
        else
        {
            Skill skill5 = new Skill
            {
                Id = 201,
                PlayerId = player.id,
                ColdTime = 0,
                Damage = 30,
                ICON = "icon_zhu",
                IsMan = 1,
                Level = 1,
                Pos = "Basic",
                Type = "Basic",
                SkillName = "雷动九天"
            };
            Skill skill6 = new Skill
            {
                Id = 202,
                PlayerId = player.id,
                ColdTime = 5,
                Damage = 100,
                ICON = "icon_li",
                IsMan = 1,
                Level = 1,
                Pos = "One",
                Type = "Skill",
                SkillName = "炽修罗"
            };
            Skill skill7 = new Skill
            {
                Id = 203,
                PlayerId = player.id,
                ColdTime = 8,
                Damage = 120,
                ICON = "iocn_fo",
                IsMan = 1,
                Level = 1,
                Pos = "Two",
                Type = "Skill",
                SkillName = "不动明王"
            };
            Skill skill8 = new Skill
            {
                Id = 204,
                PlayerId = player.id,
                ColdTime = 13,
                Damage = 180,
                ICON = "iocn_ho",
                IsMan = 1,
                Level = 1,
                Pos = "Three",
                Type = "Skill",
                SkillName = "唯我独尊"
            };
            skillItem.Add(skill5);
            skillItem.Add(skill6);
            skillItem.Add(skill7);
            skillItem.Add(skill8);
        }

        JsonTools.WriteJsonFile(skillItem, path);
    }

    private void InitSkillShow()
    {
        List<Skill> skillLsit = JsonTools.AnalyticalJsonFile<List<Skill>>(path);
        foreach (Skill skill in skillLsit)
        {
            if (skill.PlayerId == player.id)
            {
                if (!GameController.instance.skillDict.ContainsKey(skill.Pos))
                {
                    GameController.instance.skillDict.Add(skill.Pos, skill);
                }
                
                switch (skill.Pos)
                {
                    case "One":
                        skill_1.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.ICON);
                        skill_1.Skill = skill;
                        selectedBtn = skill_1.GetComponent<Button>();
                        UpdateShow(skill);
                        break;
                    case "Two":
                        skill_2.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.ICON);
                        skill_2.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                        skill_2.Skill = skill;
                        break;
                    case "Three":
                        skill_3.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.ICON);
                        skill_3.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                        skill_3.Skill = skill;
                        break;
                }
            }
        }
    }

    public void SkillBtnClick(Button btn)
    {
        selectedBtn = btn;
        switch (btn.name)
        {
            case "Skill_1":
                skill_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_2.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                skill_3.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                UpdateShow(btn.GetComponent<SkillItem>().Skill);
                break;
            case "Skill_2":
                skill_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_1.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                skill_3.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                UpdateShow(btn.GetComponent<SkillItem>().Skill);
                break;
            case "Skill_3":
                skill_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_2.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                skill_1.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                UpdateShow(btn.GetComponent<SkillItem>().Skill);
                break;
        }
    }

    private void UpdateShow(Skill skill)
    {
        skillName.text = skill.SkillName + " LV." + skill.Level;
        hurt.text = GetDamage(skill.Level, skill.Damage) + "";
        nextLevelHurt.text = GetDamage(skill.Level + 1, skill.Damage) + "";
        upgradeCoin.text = GetUpgradeCoin(skill.Level + 1) + "";
    }

    public void UpgradeBtn()
    {
        SkillItem item = selectedBtn.GetComponent<SkillItem>();
        if (GameController.instance.Player.level <= item.Skill.Level)
        {
            HintInfor.instance.ShowInformation("角色等级不够，无法升级");
        }
        else if(item.Skill.Level >= 10)
        {
            HintInfor.instance.ShowInformation("技能已满级，无法升级");
        }
        else if (!PlayerInfor.instance.SubMoney("", GetUpgradeCoin(item.Skill.Level),0))
        {
            HintInfor.instance.ShowInformation("金币不足，无法升级");
        }
        else
        {
            item.Skill.Level++;
            item.Skill.Damage = item.Skill.Damage * item.Skill.Level;
            GameController.instance.skillDict.Remove("One");
            GameController.instance.skillDict.Add("One", item.Skill);
            UpdateShow(item.Skill);
            UpdateSkillInfor(item.Skill);
        }
    }

    private void UpdateSkillInfor(Skill skill)
    {
        List<Skill> skillList = JsonTools.AnalyticalJsonFile<List<Skill>>(path);
        if (skillList == null || skillList.Count == 0) return;

        foreach (Skill skill1 in skillList)
        {
            if (skill1.PlayerId == skill.PlayerId && skill1.Id == skill.Id)
            {
                skill1.Level = skill.Level;
                skill1.Damage = skill.Damage;
            }
        }

        JsonTools.WriteJsonFile(skillList,path);
    }

    private int GetDamage(int level,int damage)
    {
        return level * damage;
    }

    private int GetUpgradeCoin(int level)
    {
        return level * 500;
    }
}

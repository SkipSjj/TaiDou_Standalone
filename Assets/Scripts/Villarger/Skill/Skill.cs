using System;

[Serializable]
public class Skill
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public string SkillName { get; set; }
    public string ICON { get; set; }
    public int IsMan { get; set; }
    public string Type { get; set; }
    public string Pos { get; set; }
    public int ColdTime { get; set; }
    public int Damage { get; set; }
    public int Level { get; set; }
}
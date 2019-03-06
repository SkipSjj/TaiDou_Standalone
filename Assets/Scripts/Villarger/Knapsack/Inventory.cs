using System;

[Serializable]
public class Inventory
{
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string InventoryType { get; set; }
    public string EquipType { get; set; }
    public int Count { get; set; }
    public int Level { get; set; }
    public int PlayerId { get; set; }
    public int Price { get; set; }
    public int Damage { get; set; }
    public int Hp { get; set; }
    public int Power { get; set; }
    public string InforType { get; set; }
    public int ApplyValue { get; set; }
    public string Des { get; set; }
    public string IsDressed { get; set; }
}

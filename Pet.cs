public class Pet
{
    public string Name { get; set; }
    public PetType Type { get; set; }
    public int Hunger { get; set; } = 50;
    public int Fun { get; set; } = 50;
    public int Sleep { get; set; } = 50;

    public void ApplyItemEffect(Item item)
    {
        switch (item.AffectedStat)
        {
            case PetStat.Hunger:
                Hunger = Math.Min(100, Hunger + item.EffectAmount);
                break;
            case PetStat.Fun:
                Fun = Math.Min(100, Fun + item.EffectAmount);
                break;
            case PetStat.Sleep:
                Sleep = Math.Min(100, Sleep + item.EffectAmount);
                break;
        }
    }

    public void DisplayStats()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Type: {Type}");
        Console.WriteLine($"Hunger: {Hunger}/100");
        Console.WriteLine($"Fun: {Fun}/100");
        Console.WriteLine($"Sleep: {Sleep}/100");
    }
}
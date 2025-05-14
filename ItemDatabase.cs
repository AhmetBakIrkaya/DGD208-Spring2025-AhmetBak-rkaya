using System.Collections.Generic;

public static class ItemDatabase
{
    public static List<Item> AllItems = new List<Item>
    {
        new Item {
            Name = "Kibble", Type = ItemType.Food,
            CompatibleWith = new List<PetType> { PetType.Dog, PetType.Cat },
            AffectedStat = PetStat.Hunger, EffectAmount = 15, Duration = 2.5f
        },
        new Item {
            Name = "Ball", Type = ItemType.Toy,
            CompatibleWith = new List<PetType> { PetType.Dog, PetType.Cat },
            AffectedStat = PetStat.Fun, EffectAmount = 10, Duration = 2.0f
        }
    };
}
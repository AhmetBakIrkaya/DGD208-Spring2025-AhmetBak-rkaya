using System.Collections.Generic;

public static class ItemDatabase
{
    public static List<Item> AllItems = new List<Item>
    {
        
        new Item
        {
            Name = "Dog Bone",
            AffectedStat = PetStat.Fun,
            EffectAmount = 20,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Dog }
        },
        new Item
        {
            Name = "Dog Food",
            AffectedStat = PetStat.Hunger,
            EffectAmount = 20,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Dog }
        },
        new Item
        {
            Name = "Dog Bed",
            AffectedStat = PetStat.Sleep,
            EffectAmount = 15,
            Duration = 3,
            CompatibleWith = new List<PetType> { PetType.Dog }
        },

        
        new Item
        {
            Name = "Cat Toy",
            AffectedStat = PetStat.Fun,
            EffectAmount = 15,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Cat }
        },
        new Item
        {
            Name = "Cat Food",
            AffectedStat = PetStat.Hunger,
            EffectAmount = 15,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Cat }
        },
        new Item
        {
            Name = "Cat Pillow",
            AffectedStat = PetStat.Sleep,
            EffectAmount = 20,
            Duration = 3,
            CompatibleWith = new List<PetType> { PetType.Cat }
        },

        
        new Item
        {
            Name = "Turtle Heater Stone",
            AffectedStat = PetStat.Sleep,
            EffectAmount = 25,
            Duration = 3,
            CompatibleWith = new List<PetType> { PetType.Turtle }
        },
        new Item
        {
            Name = "Turtle Treat",
            AffectedStat = PetStat.Hunger,
            EffectAmount = 20,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Turtle }
        },
        new Item
        {
            Name = "Floating Log Toy",
            AffectedStat = PetStat.Fun,
            EffectAmount = 10,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Turtle }
        },

        
        new Item
        {
            Name = "Bird Treat",
            AffectedStat = PetStat.Hunger,
            EffectAmount = 10,
            Duration = 1,
            CompatibleWith = new List<PetType> { PetType.Bird }
        },
        new Item
        {
            Name = "Bird Swing",
            AffectedStat = PetStat.Fun,
            EffectAmount = 15,
            Duration = 3,
            CompatibleWith = new List<PetType> { PetType.Bird }
        },
        new Item
        {
            Name = "Bird Nest",
            AffectedStat = PetStat.Sleep,
            EffectAmount = 20,
            Duration = 3,
            CompatibleWith = new List<PetType> { PetType.Bird }
        },

        
        new Item
        {
            Name = "Fish Flakes",
            AffectedStat = PetStat.Hunger,
            EffectAmount = 15,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Fish }
        },
        new Item
        {
            Name = "Bubble Cave",
            AffectedStat = PetStat.Sleep,
            EffectAmount = 15,
            Duration = 3,
            CompatibleWith = new List<PetType> { PetType.Fish }
        },
        new Item
        {
            Name = "Spinning Water Toy",
            AffectedStat = PetStat.Fun,
            EffectAmount = 10,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Fish }
        },

        
        new Item
        {
            Name = "Rabbit Carrot Snack",
            AffectedStat = PetStat.Fun,
            EffectAmount = 15,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Rabbit }
        },
        new Item
        {
            Name = "Rabbit Hay",
            AffectedStat = PetStat.Hunger,
            EffectAmount = 20,
            Duration = 2,
            CompatibleWith = new List<PetType> { PetType.Rabbit }
        },
        new Item
        {
            Name = "Rabbit Burrow Nest",
            AffectedStat = PetStat.Sleep,
            EffectAmount = 15,
            Duration = 3,
            CompatibleWith = new List<PetType> { PetType.Rabbit }
        },
        
        
        
        new Item
        {
            Name = "Talk to your Pet",
            AffectedStat = PetStat.Fun,
            EffectAmount = 15,
            Duration = 2,
            CompatibleWith = new List<PetType> 
            { 
                PetType.Dog, PetType.Cat, PetType.Turtle,
                PetType.Bird, PetType.Fish, PetType.Rabbit
                
            }
        },
        new Item
        {
            Name = "Mystery Pet Snack",
            AffectedStat = PetStat.Hunger,
            EffectAmount = 15,
            Duration = 2,
            CompatibleWith = new List<PetType>
            {
                PetType.Dog, PetType.Cat, PetType.Turtle,
                PetType.Bird, PetType.Fish, PetType.Rabbit
            }
        }
    };
}


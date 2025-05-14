public class Game
{
    private bool _isRunning;
    private Pet _currentPet;
    
    
    
    public async Task Start()
    {
        _isRunning = true;

        while (_isRunning)
        {
            ShowMainMenu(); 
            string choice = Console.ReadLine(); 
            await ProcessUserChoice(choice); 
        }

        Console.WriteLine("Thanks for playing Pet Care Game!!! Press any key to exit...");
        Console.ReadKey();
    }

    
    

    public async Task GameLoop()
    {
        Initialize();

        _isRunning = true;
        while (_isRunning)
        {
            string userChoice = GetUserInput();
            await ProcessUserChoice(userChoice);
        }

        Console.WriteLine("Thanks for playing!");
    }

    
    
    private void Initialize()
    {
        // Future initialization logic here if needed
    }

    
    
    private string GetUserInput()
    {
        Console.Clear();
        
        Console.WriteLine("Pet Care Game - Developed by: Ahmet Bakırkaya (2305041026) and ChatGPT's Help");
        Console.WriteLine("--------------------------------------------------");

        Console.WriteLine("1. Adopt a Pet");
        Console.WriteLine("2. View Pet Stats");
        Console.WriteLine("3. Use an Item on Your Pet");
        Console.WriteLine("4. Exit Game");
        Console.Write("Enter choice: ");

        return Console.ReadLine();
    }

    
    
    private async Task ProcessUserChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                AdoptPet();
                break;
            case "2":
                ViewPetStats();
                break;
            case "3":
                await UseItemOnPet(); 
                break;
            case "4":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
    
    
    
    
    
    private void ViewPetStats()
    {
        if (_currentPet == null)
        {
            Console.WriteLine("You haven't adopted a pet yet!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("=== Pet Stats ===");
            _currentPet.DisplayStats();
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
    
    
    
    
    
    
    private void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Pet Care Game ===");
        Console.WriteLine("Student: Ahmet Bakırkaya - 2305041026 and ChatGPT's Help");
        Console.WriteLine();
        Console.WriteLine("1. Adopt a Pet");
        Console.WriteLine("2. View Pet Stats");
        Console.WriteLine("3. Use an Item on Your Pet");
        Console.WriteLine("4. Exit Game");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
    }


    
    
    private async Task AdoptPet()
    {
        var petTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
        var petMenu = new Menu<PetType>("Select a Pet to Adopt", petTypes, pt => pt.ToString());

        PetType? selectedType = petMenu.ShowAndGetSelectionNullable(); 
        if (selectedType == null) return;

        Console.Write("Enter a name for your pet: ");
        string name = Console.ReadLine();

        _currentPet = new Pet
        {
            Name = name,
            Type = selectedType.Value,
            Hunger = 50,
            Sleep = 50,
            Fun = 50
        };

        Console.WriteLine($"{name} the {selectedType} has been adopted!");
        await Task.Delay(1500);
    }

    
    
    
    
    
    private async Task UseItemOnPet()
    {
        if (_currentPet == null)
        {
            Console.WriteLine("You haven't adopted a pet yet!");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

        var compatibleItems = ItemDatabase.AllItems
            .Where(item => item.CompatibleWith.Contains(_currentPet.Type))
            .ToList();

        var itemMenu = new Menu<Item>("Select an Item to Use", compatibleItems, item => 
            $"{item.Name} (Affects: {item.AffectedStat}, +{item.EffectAmount})");

        var selectedItem = itemMenu.ShowAndGetSelection();
        if (selectedItem == null)
            return;

        Console.WriteLine($"{_currentPet.Name} is using {selectedItem.Name}...");
        await Task.Delay(TimeSpan.FromSeconds(selectedItem.Duration));
        _currentPet.ApplyItemEffect(selectedItem);

        Console.WriteLine($"{selectedItem.Name} used! Stat updated.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    
    

    private void ShowPetStats()
    {
        Console.Clear();

        if (_currentPet == null)
        {
            Console.WriteLine("You haven't adopted a pet yet!");
        }
        else
        {
            Console.WriteLine($"Name: {_currentPet.Name}");
            Console.WriteLine($"Type: {_currentPet.Type}");
            Console.WriteLine($"Hunger: {_currentPet.Hunger}");
            Console.WriteLine($"Sleep: {_currentPet.Sleep}");
            Console.WriteLine($"Fun: {_currentPet.Fun}");
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}

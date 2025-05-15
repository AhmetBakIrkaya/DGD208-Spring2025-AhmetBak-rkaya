public class Game
{
    private bool _isRunning;
    private bool _petIsDead;
    private Pet _currentPet;

    private CancellationTokenSource _statDecreaseCts;

    public async Task Start()
    {
        _isRunning = true;
        _petIsDead = false;

        // Stat azaltma işlemini başlat
        StartStatDecreaseLoop();

        while (_isRunning)
        {
            if (_petIsDead && _currentPet != null)
            {
                Console.Clear();
                Console.WriteLine($"Sorry! {_currentPet.Name} has died because of neglect.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            ShowMainMenu();
            string choice = Console.ReadLine();
            await ProcessUserChoice(choice);
        }

        Console.WriteLine("Thanks for playing!");
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

    
    
    private async Task ProcessUserChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                await AdoptPet();
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


    
    
    
    
    private async Task AdoptPet()
    {
        var petTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();

        Console.WriteLine("Select a Pet to Adopt:");
        Console.WriteLine("0. Go Back");

        for (int i = 0; i < petTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {petTypes[i]}");
        }

        Console.Write("Enter choice: ");
        string input = Console.ReadLine();

        if (input == "0")
        {
            // Geri dön
            return;
        }

        if (int.TryParse(input, out int selectedIndex) &&
            selectedIndex >= 1 &&
            selectedIndex <= petTypes.Count)
        {
            PetType selectedType = petTypes[selectedIndex - 1];

            Console.Write("Enter a name for your pet: ");
            string name = Console.ReadLine();

            _currentPet = new Pet
            {
                Name = name,
                Type = selectedType,
                Hunger = 50,
                Sleep = 50,
                Fun = 50
            };

            Console.WriteLine($"{name} the {selectedType} has been adopted!");
            await Task.Delay(1500);
        }
        else
        {
            Console.WriteLine("Invalid choice. Returning to main menu.");
            await Task.Delay(1000);
        }
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

    
    
    
    private void StartStatDecreaseLoop()
    {
        _statDecreaseCts?.Cancel();
        _statDecreaseCts = new CancellationTokenSource();
        var token = _statDecreaseCts.Token;

        _ = Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(1000);

                if (_currentPet != null && !_petIsDead)
                {
                    _currentPet.Hunger = Math.Max(0, _currentPet.Hunger - 1);
                    _currentPet.Sleep = Math.Max(0, _currentPet.Sleep - 1);
                    _currentPet.Fun = Math.Max(0, _currentPet.Fun - 1);

                    if (_currentPet.Hunger == 0 || _currentPet.Sleep == 0 || _currentPet.Fun == 0)
                    {
                        _petIsDead = true;
                        
                        Console.Clear();
                        Console.WriteLine($"Sorry! {_currentPet.Name} has died because of neglect.");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }
        });
    }
}

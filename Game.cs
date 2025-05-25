public class Game
{
    private bool _isRunning;
    private List<Pet> _ownedPets = new List<Pet>();
    private const int MaxPets = 3;
    private CancellationTokenSource _statDecreaseCts;
    private bool _hasAdoptedPetBefore = false; // Daha önce evcil hayvan sahiplenildi mi takibi

    public async Task Start()
    {
        _isRunning = true;
        StartStatDecreaseLoop();

        while (_isRunning)
        {
            _ownedPets.RemoveAll(pet => pet.IsDead);

            if (_ownedPets.Count == 0)
            {
                if (_hasAdoptedPetBefore)
                {
                    Console.Clear();
                    Console.WriteLine("All your pets have died due to neglect. Game over.");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You have no pets currently.");
                    Console.WriteLine("Please adopt a pet to continue playing.");
                    Console.WriteLine("Press any key to go to the main menu...");
                    Console.ReadKey();
                }
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
        Console.WriteLine("3. Use an Item on a Pet");
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
        if (_ownedPets.Count == 0)
        {
            Console.WriteLine("You haven't adopted any pets yet!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("=== Pet Stats ===");
            foreach (var pet in _ownedPets)
            {
                pet.DisplayStats();
                Console.WriteLine();
            }
        }
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private async Task AdoptPet()
    {
        if (_ownedPets.Count >= MaxPets)
        {
            Console.WriteLine("You already own the maximum number of pets (3).");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

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
            return;

        if (int.TryParse(input, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= petTypes.Count)
        {
            PetType selectedType = petTypes[selectedIndex - 1];

            Console.Write("Enter a name for your pet: ");
            string name = Console.ReadLine();

            var pet = new Pet(name, selectedType);
            _ownedPets.Add(pet);
            _hasAdoptedPetBefore = true;  // Sahiplenme bilgisi güncellendi

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
        if (_ownedPets.Count == 0)
        {
            Console.WriteLine("You haven't adopted any pets yet!");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

        var petMenu = new Menu<Pet>("Select a pet to use an item on", _ownedPets, pet => $"{pet.Name} the {pet.Type}");
        var selectedPet = petMenu.ShowAndGetSelection();
        if (selectedPet == null)
            return;

        var compatibleItems = ItemDatabase.AllItems
            .Where(item => item.CompatibleWith.Contains(selectedPet.Type))
            .ToList();

        var itemMenu = new Menu<Item>("Select an Item to Use", compatibleItems, item =>
            $"{item.Name} (Affects: {item.AffectedStat}, +{item.EffectAmount})");

        var selectedItem = itemMenu.ShowAndGetSelection();
        if (selectedItem == null)
            return;

        Console.WriteLine($"{selectedPet.Name} is using {selectedItem.Name}...");
        await Task.Delay(TimeSpan.FromSeconds(selectedItem.Duration));
        selectedPet.ApplyItemEffect(selectedItem);

        Console.WriteLine("Item used! Stat updated.");
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
                foreach (var pet in _ownedPets)
                {
                    pet.DecreaseStats();
                }
            }
        });
    }
}

using System;

Console.WriteLine("Prosty RPG (wersja konsolowa)");
Console.WriteLine("Stwórz bohatera:");
Console.Write("Podaj imię: ");
var name = Console.ReadLine()?.Trim();
if (string.IsNullOrEmpty(name)) name = "Bohater";

var rnd = new Random();
var player = new Player(name);

Console.WriteLine();
Console.WriteLine($"Witaj, {player.Name}! Twoje HP: {player.HP}/{player.MaxHP}, Atak: {player.Attack}");
Console.WriteLine("Naciśnij Enter, aby rozpocząć przygodę...");
Console.ReadLine();

while (true)
{
    Console.Clear();
    Console.WriteLine($"-- {player.Name} -- HP: {player.HP}/{player.MaxHP}  Poziom: {player.Level}  XP: {player.XP}/{player.XPToLevel}  Mikstury: {player.Potions}  Złoto: {player.Gold}");
    Console.WriteLine();
    Console.WriteLine("Co chcesz zrobić?");
    Console.WriteLine("1) Wyrusz na potyczkę");
    Console.WriteLine("2) Sklep (kup miksturę za 10 złota)");
    Console.WriteLine("3) Odpoczynek (+50% HP, koszt: 5 złota)");
    Console.WriteLine("0) Zakończ grę");
    Console.Write("Wybór: ");
    var choice = Console.ReadLine()?.Trim();
    if (choice == "0") break;

    if (choice == "2")
    {
        if (player.Gold >= 10)
        {
            player.Gold -= 10;
            player.Potions++;
            Console.WriteLine("Kupiłeś miksturę.");
        }
        else
        {
            Console.WriteLine("Brak wystarczająco złota.");
        }
        Pause();
        continue;
    }

    if (choice == "3")
    {
        if (player.Gold >= 5)
        {
            player.Gold -= 5;
            var heal = Math.Max(1, player.MaxHP / 2);
            player.HP = Math.Min(player.MaxHP, player.HP + heal);
            Console.WriteLine($"Odpoczynek przywrócił {heal} HP.");
        }
        else
        {
            Console.WriteLine("Za mało złota na odpoczynek.");
        }
        Pause();
        continue;
    }

    // walka
    var enemy = Enemy.Generate(rnd, player.Level);
    Console.WriteLine($"Napotykasz {enemy.Name} (HP: {enemy.HP}, Atak: {enemy.Attack})");
    while (enemy.HP > 0 && player.HP > 0)
    {
        Console.WriteLine();
        Console.WriteLine($"Twoje HP: {player.HP}/{player.MaxHP} | {enemy.Name} HP: {enemy.HP}");
        Console.WriteLine("1) Atakuj  2) Użyj mikstury  3) Spróbuj uciec");
        Console.Write("Akcja: ");
        var a = Console.ReadLine()?.Trim();
        if (a == "1")
        {
            var dmg = rnd.Next(player.Attack - 1, player.Attack + 2);
            dmg = Math.Max(1, dmg);
            enemy.HP -= dmg;
            Console.WriteLine($"Zadajesz {dmg} obrażeń.");
        }
        else if (a == "2")
        {
            if (player.Potions > 0)
            {
                player.Potions--;
                var heal = Math.Min(player.MaxHP - player.HP, player.MaxHP / 3);
                if (heal <= 0) heal = 1;
                player.HP += heal;
                Console.WriteLine($"Używasz mikstury i odzyskujesz {heal} HP.");
            }
            else
            {
                Console.WriteLine("Nie masz mikstur!");
            }
        }
        else if (a == "3")
        {
            if (rnd.NextDouble() < 0.5)
            {
                Console.WriteLine("Udało się uciec!");
                break;
            }
            else
            {
                Console.WriteLine("Ucieczka nie powiodła się!");
            }
        }
        else
        {
            Console.WriteLine("Nieznana akcja.");
        }

        if (enemy.HP > 0)
        {
            var edmg = rnd.Next(enemy.Attack - 1, enemy.Attack + 2);
            edmg = Math.Max(1, edmg);
            player.HP -= edmg;
            Console.WriteLine($"{enemy.Name} zadaje Ci {edmg} obrażeń.");
        }
    }

    if (player.HP <= 0)
    {
        Console.WriteLine("Zginąłeś. Koniec gry.");
        break;
    }

    if (enemy.HP <= 0)
    {
        var rewardXp = enemy.Level * 10;
        var rewardGold = rnd.Next(5, 15) + enemy.Level * 2;
        player.XP += rewardXp;
        player.Gold += rewardGold;
        Console.WriteLine($"Pokonałeś {enemy.Name}! Zdobywasz {rewardXp} XP i {rewardGold} złota.");
        while (player.XP >= player.XPToLevel)
        {
            player.LevelUp();
            Console.WriteLine($"Awans! Jesteś teraz na poziomie {player.Level}. Max HP i Atak wzrosły.");
        }
    }

    Pause();
}

Console.WriteLine("Dzięki za grę!");

static void Pause()
{
    Console.WriteLine("Naciśnij Enter, aby kontynuować...");
    Console.ReadLine();
}

class Player
{
    public string Name { get; }
    public int Level { get; private set; } = 1;
    public int XP { get; set; } = 0;
    public int XPToLevel => Level * 50;
    public int MaxHP { get; private set; } = 30;
    public int HP { get; set; }
    public int Attack { get; private set; } = 5;
    public int Potions { get; set; } = 1;
    public int Gold { get; set; } = 10;

    public Player(string name)
    {
        Name = name;
        HP = MaxHP;
    }

    public void LevelUp()
    {
        XP -= XPToLevel;
        Level++;
        MaxHP += 8;
        Attack += 2;
        HP = MaxHP;
        Gold += 5;
    }
}

class Enemy
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }

    public static Enemy Generate(Random rnd, int playerLevel)
    {
        var level = Math.Max(1, playerLevel + rnd.Next(-1, 2));
        var names = new[] { "Szkielet", "Wilk", "Bandyta", "Goblin", "Pajęczyca" };
        var name = names[rnd.Next(names.Length)];
        return new Enemy
        {
            Name = name,
            Level = level,
            HP = 10 + level * 6 + rnd.Next(0, 6),
            Attack = 2 + level * 2 + rnd.Next(0, 3)
        };
    }
}1
# Aufgabenblatt 3

## Aufgabe 1: Character Creator

```csharp
enum PlayerClass {
    Mage = 1,
    Paladin = 2,
    Thief = 3,
    Barbarian = 4
}
enum WeaponType {
    Sword = 1,
    Polearm = 2,
    Staff = 3
}
class CombatStats {
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public int Luck { get; set; }
    public int MaxHP {  get; set; }
    public int CurrentHP { get; set; }

    public CombatStats(int attack, int defense, int speed, int luck, int maxHP) {
        this.Attack = attack;
        this.Defense = defense;
        this.Speed = speed;
        this.Luck = luck;
        this.MaxHP = maxHP;
        this.CurrentHP = maxHP;
    }
}
class PlayerCharacter {
    public PlayerClass PlayerClass { get; set; }
    public WeaponType WeaponType { get; set; }
    public float Height { get; set; }
    public string Name { get; set; }
    public CombatStats Stats { get; set; }

    public PlayerCharacter(PlayerClass playerClass, WeaponType weaponType, float height, string name, CombatStats stats) {
        this.PlayerClass = playerClass;
        this.WeaponType = weaponType;
        this.Height = height;
        this.Name = name;
        this.Stats = stats;
    }
}

class PlayerCharacterBuilder {
    public PlayerClass PlayerClass { get; set; }
    public WeaponType WeaponType { get; set; }
    public float Height { get; set; }
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public int Luck { get; set; }
    public int MaxHP { get; set; }

    public PlayerCharacterBuilder SetPlayerClass(PlayerClass playerClass) {
        this.PlayerClass = playerClass;
        return this;
    }
    public PlayerCharacterBuilder SetWeaponType(WeaponType weaponType) {
        this.WeaponType = weaponType;
        return this;
    }
    public PlayerCharacterBuilder SetHeight(float height) {
        this.Height = height;
        return this;
    }
    public PlayerCharacterBuilder SetName(string name) { 
        this.Name = name; 
        return this; 
    }
    public PlayerCharacterBuilder SetAttack(int attack) {
        this.Attack = attack;
        return this;
    }
    public PlayerCharacterBuilder SetDefense(int defense) {
        this.Defense = defense;
        return this;
    }
    public PlayerCharacterBuilder SetSpeed(int speed) {
        this.Speed = speed;
        return this;
    }
    public PlayerCharacterBuilder SetLuck(int luck) {
        this.Luck = luck;
        return this;
    }
    public PlayerCharacterBuilder SetMaxHP(int maxHP) {
        this.MaxHP = maxHP;
        return this;
    }

    public PlayerCharacter Builder() {
        return new PlayerCharacter(this.PlayerClass, this.WeaponType, this.Height, this.Name, new CombatStats(this.Attack, this.Defense, this.Speed, this.Luck, this.MaxHP));
    }
}

class Program {
    private const int START_STATS = 20;

    static void AskName(PlayerCharacterBuilder builder) {
        Console.Write("How is your character called? ");
        builder.SetName(Console.ReadLine());
    }
    static void AskClass(PlayerCharacterBuilder builder) {
        Console.Write("Which class is your character (1 = Mage, 2 = Paladin, 3 = Thief, 4 = Barbarian)? ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int classInt)) {
            if (classInt > 0 && classInt < 5) {
                builder.SetPlayerClass((PlayerClass)classInt);
            }
            else {
                AskClass(builder);
            }
        }
        else {
            AskClass(builder);
        }
    }
    static void AskWeaponType(PlayerCharacterBuilder builder) {
        Console.Write("Which weapon type is your character (1 = Sword, 2 = Polearm, 3 = Staff)? ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int weaponInt)) {
            if (weaponInt > 0 && weaponInt < 4) {
                builder.SetWeaponType((WeaponType)weaponInt);
            }
            else {
                AskWeaponType(builder);
            }
        }
        else {
            AskWeaponType(builder);
        }
    }
    static void AskHeight(PlayerCharacterBuilder builder) {
        Console.Write("What height is your character? ");
        string input = Console.ReadLine();

        if (float.TryParse(input, out float height)) {
            builder.SetHeight(height);
        }
        else {
            AskHeight(builder);
        }
    }
    static void AskStats(PlayerCharacterBuilder builder) {
        int statsLeft = START_STATS;
        Console.WriteLine($"Now let's choose the initial stats for your character! You can distribute up to {statsLeft} points into attack, defense, speed, luck and HP.");


        bool hpDone = false;
        while (!hpDone) {
            Console.Write("HP: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number)) {
                if (number > statsLeft) {
                    Console.WriteLine($"That is too much, you only have {statsLeft} points left to distribute.");
                    continue;
                }
                else if (number <= 0) {
                    Console.WriteLine("HP can not be zero.");
                }

                builder.SetMaxHP(number);
                statsLeft -= number;
                hpDone = true;
            }
        }

        bool attackDone = false;
        while (!attackDone) {
            Console.Write("Attack: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number)) {
                if (number > statsLeft) {
                    Console.WriteLine($"That is too much, you only have {statsLeft} points left to distribute.");
                    continue;
                }

                builder.SetAttack(number);
                statsLeft -= number;
                attackDone = true;
            }
        }

        bool defenseDone = false;
        while (!defenseDone) {
            Console.Write("Defense: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number)) {
                if (number > statsLeft) {
                    Console.WriteLine($"That is too much, you only have {statsLeft} points left to distribute.");
                    continue;
                }

                builder.SetDefense(number);
                statsLeft -= number;
                defenseDone = true;
            }
        }

        bool speedDone = false;
        while (!speedDone) {
            Console.Write("Speed: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number)) {
                if (number > statsLeft) {
                    Console.WriteLine($"That is too much, you only have {statsLeft} points left to distribute.");
                    continue;
                }

                builder.SetSpeed(number);
                statsLeft -= number;
                speedDone = true;
            }
        }

        bool luckDone = false;
        while (!luckDone) {
            Console.Write("Luck: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number)) {
                if (number > statsLeft) {
                    Console.WriteLine($"That is too much, you only have {statsLeft} points left to distribute.");
                    continue;
                }

                builder.SetLuck(number);
                statsLeft -= number;
                luckDone = true;
            }
        }
    }

    static void Main(string[] args) {
        Console.WriteLine("Hi! Let's create your character!");
        var builder = new PlayerCharacterBuilder();

        AskName(builder);
        AskClass(builder);
        AskWeaponType(builder);
        AskHeight(builder);
        AskStats(builder);

        Console.WriteLine("Done!");
    }
}
```

## Aufgabe 2: Observer Pattern

```csharp
class ChangeHPEventArgs : EventArgs {
    public int HPChange { get; set; }
    public int NewHP { get; set; }

    public ChangeHPEventArgs(int hpChange, int newHP) {
        this.HPChange = hpChange;
        this.NewHP = newHP;
    }
}

class Player {
    public event EventHandler<ChangeHPEventArgs> ChangeHPEvent;

    public int HP { get; private set; }

    public Player(int initialHP) {
        this.HP = initialHP;
    }

    public void TakeAttackDamage(int damage) {
        this.HP = Math.Max(this.HP - damage, 0);
        this.ChangeHPEvent?.Invoke(this, new ChangeHPEventArgs(-damage, this.HP));
    }
    public void TakeFallDamage(int damage) {
        this.HP = Math.Max(this.HP - damage, 0);
        this.ChangeHPEvent?.Invoke(this, new ChangeHPEventArgs(-damage, this.HP));
    }
    public void Heal(int healing) {
        this.HP = this.HP + healing;
        this.ChangeHPEvent?.Invoke(this, new ChangeHPEventArgs(healing, this.HP));
    }
}

class UserInterfaceManager {
    public void RegisterPlayer(Player player) {
        player.ChangeHPEvent += Player_ChangeHPEvent;
    }

    private void Player_ChangeHPEvent(object? sender, ChangeHPEventArgs e) {
        Console.WriteLine($"Current player HP: {e.NewHP}");
    }
}

class SoundManager {
    public void RegisterPlayer(Player player) {
        player.ChangeHPEvent += Player_ChangeHPEvent;
    }

    private void Player_ChangeHPEvent(object? sender, ChangeHPEventArgs e) {
        if (e.HPChange < 0) {
            Console.WriteLine("Warning! Player HP reduced!");
        }
    }
}

class LevelManager {
    public void RegisterPlayer(Player player) {
        player.ChangeHPEvent += Player_ChangeHPEvent;
    }

    private void Player_ChangeHPEvent(object? sender, ChangeHPEventArgs e) {
        if (e.NewHP == 0) {
            System.Environment.Exit(0);
        }
    }
}

class Program {
    static void Main(string[] args) {
        var player = new Player(100);
        var uiManager = new UserInterfaceManager();
        var soundManager = new SoundManager();
        var levelManager = new LevelManager();

        uiManager.RegisterPlayer(player);
        soundManager.RegisterPlayer(player);
        levelManager.RegisterPlayer(player);

        while (true) {
            int change = Random.Shared.Next(1, 21);
            int action = Random.Shared.Next(1, 4);

            switch (action) {
                case 1:
                    Console.WriteLine($"Taking {change} attack damage ...");
                    player.TakeAttackDamage(change);
                    break;
                case 2:
                    Console.WriteLine($"Taking {change} fall damage ...");
                    player.TakeFallDamage(change);
                    break;
                case 3:
                    Console.WriteLine($"Taking {change} healing ...");
                    player.Heal(change);
                    break;
            }
        }
    }
}
```

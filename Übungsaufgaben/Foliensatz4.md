# Foliensatz 4: Sortier-Algorithmen (Teil 2)

## Übung 2: IComparable

```csharp
class Player : IComparable<Player> {
    public string Name { get; set; }
    public int Score { get; set; }

    public Player(string name, int score) {
        Name = name;
        Score = score;
    } 

    public int CompareTo(Player? other) {
        if (other == null) {
            return 1;
        }

        if (this.Score < other.Score) {
            return -1;
        }
        else if (this.Score > other.Score) {
            return 1;
        }

        return 0;
    }

    public override string ToString() {
        return Name;
    }
}
    
internal class Program {
    public static void InsertionSort<T>(List<T> list) where T : IComparable<T> {
        for (int i = 1; i < list.Count; i++) {
            for (int j = i - 1; j >= 0; j--) {
                if (list[j].CompareTo(list[j + 1]) <= 0) {
                    break;
                }

                Swap(list, j, j + 1);
            }
        }
    }

    private static void Swap<T>(List<T> list, int firstIndex, int secondIndex) {
        var temp = list[firstIndex];
        list[firstIndex] = list[secondIndex];
        list[secondIndex] = temp;
    }

    public static void Main(string[] args) {
        var l = new List<int>() { 5, 4, 3, 2, 1 };
        InsertionSort(l);
        Console.WriteLine(string.Join(", ", l));

        var players = new List<Player>() {
            new Player("Max", 15),
            new Player("Nora", 10),
        };
        InsertionSort(players);
        Console.WriteLine(string.Join(", ", players));
    }
}
```

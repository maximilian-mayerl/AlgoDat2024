# Aufgabenblatt 1

## Aufgabe 2: Merge Sort

```csharp
class Program {
    public static List<int> MergeSort(List<int> list) {
        // Base case: We are done of the list has at most one element remaining.
        if (list.Count <= 1) {
            return list;
        }

        // Split list in half.
        int middleIndex = list.Count / 2;
        var leftList = list[..middleIndex];
        var rightList = list[middleIndex..];

        // Now, recursively merge sort both lists.
        leftList = MergeSort(leftList);
        rightList = MergeSort(rightList);

        // Finally, merge both lists.
        return Merge(leftList, rightList);
    }

    public static List<int> Merge(List<int> leftList, List<int> rightList) {
        var result = new List<int>(leftList.Count + rightList.Count);
        int i = 0;
        int j = 0;

        // Iterate in lockstep and get the smallest element from either side.
        while (i < leftList.Count && j < rightList.Count) {
            if (leftList[i] <= rightList[j]) {
                result.Add(leftList[i]);
                i++;
            }
            else {
                result.Add(rightList[j]);
                j++;
            }
        }

        // Add the remaining elements.
        while (i < leftList.Count) {
            result.Add(leftList[i]);
            i++;
        }

        while (j < rightList.Count) {
            result.Add(rightList[j]);
            j++;
        }

        // Done.
        return result;
    }

    private static void TestList(List<int> list) {
        Console.Write("{" + string.Join(", ", list) + "} => ");
        var sorted = MergeSort(list);
        Console.WriteLine("{" + string.Join(", ", sorted) + "}");
    }

    static void Main(string[] args) {
        TestList(new List<int>() { 1, 2, 3, 4, 5, 6 });
        TestList(new List<int>() { 6, 5, 4, 3, 2, 1 });
        TestList(new List<int>() { 1, 1, 3, 7, 8, 9 });
        TestList(new List<int>() { 1, 7, 2, 1, 4, 3 });
        TestList(new List<int>() { 1, 1, 1, 1, 1, 1 });
        TestList(new List<int>() { });
    }
}
```

## Aufgabe 3: Generic Merge Sort

```csharp
class City : IComparable<City> {
    public string Country { get; set; }
    public string State { get; set; }
    public string Name { get; set; }
    public int Population { get; set; }

    public City(string country, string state, string name, int population) {
        this.Country = country;
        this.State = state;
        this.Name = name;
        this.Population = population;
    }

    public int CompareTo(City? other) {
        if (other == null) {
            return 1;
        }

        return this.Population.CompareTo(other.Population);
    }

    public override string ToString() {
        return this.Name;
    }
}

class Program {
    public static List<T> MergeSort<T>(List<T> list) where T : IComparable<T> {
        // Base case: We are done of the list has at most one element remaining.
        if (list.Count <= 1) {
            return list;
        }

        // Split list in half.
        int middleIndex = list.Count / 2;
        var leftList = list[..middleIndex];
        var rightList = list[middleIndex..];

        // Now, recursively merge sort both lists.
        leftList = MergeSort(leftList);
        rightList = MergeSort(rightList);

        // Finally, merge both lists.
        return Merge(leftList, rightList);
    }

    public static List<T> Merge<T>(List<T> leftList, List<T> rightList) where T : IComparable<T> {
        var result = new List<T>(leftList.Count + rightList.Count);
        int i = 0;
        int j = 0;

        // Iterate in lockstep and get the smallest element from either side.
        while (i < leftList.Count && j < rightList.Count) {
            if (leftList[i].CompareTo(rightList[j]) <= 0) {
                result.Add(leftList[i]);
                i++;
            }
            else {
                result.Add(rightList[j]);
                j++;
            }
        }

        // Add the remaining elements.
        while (i < leftList.Count) {
            result.Add(leftList[i]);
            i++;
        }

        while (j < rightList.Count) {
            result.Add(rightList[j]);
            j++;
        }

        // Done.
        return result;
    }

    private static void TestList<T>(List<T> list) where T : IComparable<T> {
        Console.Write("{" + string.Join(", ", list) + "} => ");
        var sorted = MergeSort(list);
        Console.WriteLine("{" + string.Join(", ", sorted) + "}");
    }

    static void Main(string[] args) {
        TestList(new List<int>() { 1, 2, 3, 4, 5, 6 });
        TestList(new List<int>() { 6, 5, 4, 3, 2, 1 });
        TestList(new List<int>() { 1, 1, 3, 7, 8, 9 });
        TestList(new List<int>() { 1, 7, 2, 1, 4, 3 });
        TestList(new List<int>() { 1, 1, 1, 1, 1, 1 });
        TestList(new List<int>() { });

        var cities = new List<City>() {
            new City("Austria", "Vienna", "Vienna", 1_897_000),
            new City("Austria", "Tyrol", "Innsbruck", 132_493),
            new City("Austria", "Salzburg", "Salzburg", 157_245),
            new City("Austria", "Carinthia", "Klagenfurt", 100_316),
        };
        TestList(cities);
    }
}
```

# Aufgabenblatt 1

## Aufgabe 1: Duplikatsentfernung

```csharp
class Program {
    public static void RemoveDuplicates(List<string> list) {
        // There are basically two ways to identify (and subsequently remove)
        // duplicates from a list:
        //  (a) Use a HashSet to keep track of which elements we have already seen.
        //  (b) Sort the list. In the resulting sorted list, all duplicates will be
        //     right next to each other.
        // In this solution, we are going with (a).

        // Remember the unique strings in the list.
        var seen = new HashSet<string>();

        // Add strings to `seen`. We simply add all of them.
        // Here, we _could_ selectively remove elements from `list` if they are
        // in the set, but there is an easier way to do this ...
        foreach (var item in list) {
            seen.Add(item);
        }

        // `seen` now contains all the unique strings in the list.
        // We can simply replace the contents of `list` with that of `seen`.
        list.Clear();
        list.AddRange(seen);
    }

    private static void TestRemoveDuplicates(List<string> list) {
        Console.Write("{ " + string.Join(", ", list) + " } => ");
        RemoveDuplicates(list);
        Console.WriteLine("{ " + string.Join(", ", list) + " }");
    }

    static void Main(string[] args) {
        TestRemoveDuplicates(new List<string>());
        TestRemoveDuplicates(new List<string>() { "a", "b", "c", "d" });
        TestRemoveDuplicates(new List<string>() { "a", "b", "c", "a", "d" });
        TestRemoveDuplicates(new List<string>() { "a", "a", "a", "a" });
    }
}
```

## Stack

```csharp
class MyStack<T> {
    private List<T> data;

    public MyStack() {
        this.data = new List<T>();
    }

    public int Count {
        get => this.data.Count;
    }

    public void Push(T element) {
        this.data.Add(element);
    }

    public T Peek() {
        return this.data[this.data.Count - 1];
    }
    public T Pop() {
        var result = this.Peek();
        this.data.RemoveAt(this.data.Count - 1);
        return result;
    }
}

class Program {
    private static void TestStack<T>(List<T> items) {
        // Create stack.
        var stack = new MyStack<T>();

        // Add items to the stack.
        Console.WriteLine("Pushing elements to the stack ...");
        foreach (var item in items) {
            stack.Push(item);
        }

        // Test pop and peek.
        Console.WriteLine($"Stack has {stack.Count} elements, should have {items.Count}: {stack.Count == items.Count}");

        var poppedElement = stack.Pop();
        Console.WriteLine($"Popping one element: {poppedElement}. Should have been {items[items.Count - 1]}");
        Console.WriteLine($"Stack has {stack.Count} elements, should have {items.Count - 1}: {stack.Count == items.Count - 1}");

        var peekedElement = stack.Peek();
        Console.WriteLine($"Peeking one element: {peekedElement}. Should have been {items[items.Count - 2]}");
        Console.WriteLine($"Stack has {stack.Count} elements, should have {items.Count - 1}: {stack.Count == items.Count - 1}");

        // Test clearing the stack.
        Console.WriteLine("Clearing the stack ...");
        while (stack.Count > 0) {
            Console.WriteLine($"Popping element {stack.Pop()}. The stack has {stack.Count} elements remaining.");
        }

        Console.WriteLine();
    }

    static void Main(string[] args) {
        TestStack(new List<int>() { 1, 2, 3, 4, 5 });
        TestStack(new List<string>() { "a", "b", "c", "d", "e" });
    }
}
```

## Binäre Suche

```csharp
class Program {
    public static bool BinarySearch(List<int> list, int searchValue) {
        // This is a very "direct" implementation of binary search, using
        // simple C#.

        // Make variables that we will use to keep track of which part of
        // the list we are still looking at.
        int minIndex = 0;
        int maxIndex = list.Count - 1;

        // We continue shrinking the list and checking the middle element
        // until we have reached a single element.
        while (minIndex <= maxIndex) {
            int middleIndex = minIndex + (maxIndex - minIndex) / 2;

            // Check if the middle is what we want.
            // If yes, we are done - return true.
            if (list[middleIndex] == searchValue) {
                return true;
            }

            // Otherwise, shrink the list.
            if (list[middleIndex] > searchValue) {
                // If the search value is smaller than the middle element,
                // we continue in the left half.
                maxIndex = middleIndex - 1;
            }
            else {
                // Otherwise, we continue in the right half.
                minIndex = middleIndex + 1;
            }
        }

        // If we never found the search value, return false.
        return false;
    }
    public static bool BinarySearchRecursive(List<int> list, int searchValue) {
        // This is a "nicer" solution using recursion.
        // Decide for yourself which version you prefer.

        // Calculate the index of the middle element.
        int middleIndex = list.Count / 2;

        // Recursion: Base cases.
        if (list.Count == 0) {
            // If the list is emply, search value can not exist.
            return false;
        }
        else if (list[middleIndex] == searchValue) {
            // We have found the search value - return true.
            return true;
        }
        else if (list.Count == 1) {
            // We have reached the end (only one element left), and that element
            // is not the search value - return false.
            return false;
        }

        // Recursion: Recursive case.
        // Recursively call on the rest of the list.
        var remainingList = (list[middleIndex] > searchValue) ? list[..middleIndex] : list[(middleIndex + 1)..];
        return BinarySearchRecursive(remainingList, searchValue);
    }

    private static void TestBinarySearch(List<int> list, int searchValue) {
        Console.WriteLine($"Looking for {searchValue} in {{ {string.Join(", ", list)} }}: {BinarySearch(list, searchValue)}");
    }

    static void Main(string[] args) {
        // Test empty list.
        TestBinarySearch(new List<int> { }, 10);

        // Test existing value in arbitrary position.
        for (int i = 1; i <= 9; i++) {
            TestBinarySearch(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, i);
        }

        // Test non-existing value.
        TestBinarySearch(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 45);
    }
}
```

# Foliensatz 2: Collections-Framework

## Übung 1: Wörter zählen

```csharp
static Dictionary<string, int> CountWords(string text) {
    var result = new Dictionary<string, int>();

    // Split text into words.
    string[] words = text.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);

    // Count how often each word occurs.
    foreach (string word in words) {
        if (!result.ContainsKey(word)) {
            result[word] = 0;
        }

        result[word]++;
    }

    // Done.
    return result;
}
```

## Übung 2: New Words

```csharp
static void Main(string[] args) {
    // Remember which word were already seen before.
    var seenWords = new HashSet<string>();

    // Let the user input words.
    while (true) {
        Console.Write("> ");
        string input = Console.ReadLine();

        // Check if we saw that word before.
        if (!seenWords.Contains(input)) {
            Console.WriteLine("That was a new word!");
        }
        else {
            Console.WriteLine("We already had that one!");
        }

        // Add the word to the set.
        seenWords.Add(input);
    }
}
```

## Übung 3: Stack

```csharp
static void Main(string[] args) {
    // Store user input in a stack.
    var inputs = new Stack<string>();

    // Read inputs.
    while (true) {
        Console.Write("> ");
        string input = Console.ReadLine();

        // End if the user inputs a blank line.
        if (string.IsNullOrEmpty(input)) {
            break;
        }

        inputs.Push(input);
    }

    // Print the user input from the stack.
    Console.WriteLine("Your input in reverse order:");

    while (inputs.Count > 0) {
        Console.WriteLine(inputs.Pop());
    }
}
```

## Übung 4: Generics

```csharp
class MyQueue<T> {
    private List<T> data;

    public MyQueue() {
        data = new List<T>();
    }

    public int Count {
        get {
            return data.Count;
        }
    }

    public void Enqueue(T item) {
        this.data.Add(item);
    }

    public T Dequeue() { 
        if (this.data.Count == 0) {
            throw new InvalidOperationException("The queue is empty.");
        }

        T result = this.data[0];
        this.data.RemoveAt(0);
        return result;
    }
}
```

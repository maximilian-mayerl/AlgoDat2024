# Foliensatz 3: Sortier-Algorithmen

## Übung 1: Bubble Sort

```csharp
public static void BubbleSort(List<int> list) {
    bool didSwap = true;

    while (didSwap) {
        didSwap = false;

        for (int i = 1; i < list.Count; i++) {
            if (list[i - 1] > list[i]) {
                Swap(list, i, i - 1);
                didSwap = true;
            }
        }
    }
}

private static void Swap(List<int> list, int firstIndex, int secondIndex) {
    int temp = list[firstIndex];
    list[firstIndex] = list[secondIndex];
    list[secondIndex] = temp;
}
```

## Übung 2: Rekursion

```csharp
private static int Fib(int n) {
    if (n <= 1) {
        return 1;
    }

    return Fib(n - 1) + Fib(n - 2);
}
```

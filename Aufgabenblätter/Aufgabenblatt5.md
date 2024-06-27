# Aufgabenblatt 5

## Aufgabe 1: Tree Dictionary

```csharp
using System.Collections.Frozen;

interface ITreeDictionary<TKey, TValue> {
    // Indexer for reading and writing keys.
    TValue this[TKey key] { get; set; }

    // Property for determining how many entries are in the dictionary right now.
    int Count { get; }

    // Add the given `key` with the given `value` to the dictionary.
    void Add(TKey key, TValue value);

    // Remove the given `key` from the dictionary.
    void Remove(TKey key);

    // Clear the dictionary, i.e. remove all keys from it.
    void Clear();

    // Determine whether the given `key` exists in the dictionary.
    bool ContainsKey(TKey key);
    // Determine whether the given `value` exists in the dictionary.
    bool ContainsValue(TValue value);

    // The set of all keys in the dictionary.
    ISet<TKey> Keys { get; }
}

class TreeDictionary<TKey, TValue> : ITreeDictionary<TKey, TValue> where TKey : IComparable<TKey> {
    class TreeDictionaryNode {
        public TKey Key { get; }
        public TValue Value { get; set; }


        public TreeDictionaryNode? Parent { get; set; }
        public TreeDictionaryNode? Left { get; set; }
        public TreeDictionaryNode? Right { get; set; }

        public TreeDictionaryNode(TKey key, TValue value) {
            this.Key = key;
            this.Value = value;
        }

        public bool IsLeaf => Left == null && Right == null;
        public int NumberOfChildren =>
            (this.Left != null ? 1 : 0) +
            (this.Right != null ? 1 : 0);

        public bool Add(TKey key, TValue value) {
            // If the key matches the key of the current node, replace the value.
            if (this.Key.CompareTo(key) == 0) {
                this.Value = value;
                return false;
            }

            // Otherwise, we have to continue left or right.
            // If the subtree exists, continue recursively.
            // Otherwise, insert there.
            if (key.CompareTo(this.Key) < 0) {
                if (this.Left != null) {
                    return this.Left.Add(key, value);
                }
                else {
                    this.Left = new TreeDictionaryNode(key, value);
                    this.Left.Parent = this;
                    return true;
                }
            }
            else {
                if (this.Right != null) {
                    return this.Right.Add(key, value);
                }
                else {
                    this.Right = new TreeDictionaryNode(key, value);
                    this.Right.Parent = this;
                    return true;
                }
            }
        }

        public TreeDictionaryNode? FindNode(TKey key) {
            // Check if the current node has the key.
            if (this.Key.CompareTo(key) == 0) {
                return this;
            }

            // Otherwise, we have to continue left or right, if that subtree exists.
            // If it does not, we are done and the searched key does not exist in the dictionary.
            if (key.CompareTo(this.Key) < 0) {
                if (this.Left != null) {
                    return this.Left.FindNode(key);
                }
                else {
                    return null;
                }
            }
            else {
                if (this.Right != null) {
                    return this.Right.FindNode(key);
                }
                else {
                    return null;
                }
            }

        }

        public bool ContainsValue(TValue value) {
            // Check current node.
            if (this.Value.Equals(value)) {
                return true;
            }

            // Otherwise, recurse. We are search for value here, which is not what the tree
            // is ordered by, so we have to search in both children.
            bool result = false;
            if (this.Left != null) {
                result |= this.Left.ContainsValue(value);
            }
            if (this.Right != null) {
                result |= this.Right.ContainsValue(value);
            }

            return result;
        }
    }

    private TreeDictionaryNode? root;
    private readonly HashSet<TKey> keys = new HashSet<TKey>();

    public int Count { get; private set; }
    public ISet<TKey> Keys => this.keys.ToFrozenSet();


    public TValue this[TKey key] {
        get {
            var node = this.root?.FindNode(key);

            if (node != null) {
                return node.Value;
            }
            else {
                throw new KeyNotFoundException($"They key {key} does not exist in the dictionary.");
            }
        }
        set => this.Add(key, value);
    }

    public void Add(TKey key, TValue value) {
        // Insert root if we don't have one yet.
        if (this.root == null) {
            this.root = new TreeDictionaryNode(key, value);
            this.Count++;
            this.keys.Add(key);
            return;
        }

        // Otherwise, insert recursively.
        bool wasInserted = this.root.Add(key, value);
        if (wasInserted) {
            this.Count++;
            this.keys.Add(key);
        }
    }

    public void Clear() {
        // For clearing, we can just "cut off" the root.
        this.root = null;
        this.Count = 0;
    }

    public bool ContainsKey(TKey key) {
        // Search recursively.
        return this.root?.FindNode(key) != null;
    }

    public bool ContainsValue(TValue value) {
        // Check root.
        if (this.root == null) {
            return false;
        }

        // Recurse.
        return this.root.ContainsValue(value);
    }

    public void Remove(TKey key) {
        // Note: This is a very direct implementation of what we
        // talked about in the lecture. There are more succinct ways of
        // implementing this.

        // First, search for the node we have to delete.
        var nodeToDelete = this.root?.FindNode(key);

        // If there is no such node, we are done.
        if (nodeToDelete == null) {
            return;
        }

        // Otherwise, we have to remove it from the tree.
        // Here, we have to handle multiple cases.
        var successorNode = this.GetSuccessor(nodeToDelete);

        // (1) The node is a leaf.
        if (nodeToDelete.IsLeaf) {
            // If the node is a leaf, we simply remove it from the tree.
            if (nodeToDelete.Parent?.Left == nodeToDelete) {
                nodeToDelete.Parent.Left = null;
            }
            else if (nodeToDelete.Parent?.Right == nodeToDelete) {
                nodeToDelete.Parent.Right = null;
            }
            else {
                this.root = null;
            }
        }
        // (2) The node we want to delete has only one child.
        else if (nodeToDelete.NumberOfChildren == 1) {
            // If the node has only one child, we can simply replace it with that child.
            var child = nodeToDelete.Left ?? nodeToDelete.Right;

            // Update parent.
            child.Parent = nodeToDelete.Parent;

            // Replace the node with its child.
            if (nodeToDelete.Parent?.Left == nodeToDelete) {
                nodeToDelete.Parent.Left = child;
            }
            else if (nodeToDelete.Parent?.Right == nodeToDelete) {
                nodeToDelete.Parent.Right = child;
            }
            else {
                this.root = child;
            }
        }
        // (3) The node has two children and the successor is the right child.
        else if (successorNode == nodeToDelete.Right) {
            // In thise case, we can just replace the node with it's right child.
            
            // Update parent.
            successorNode.Parent = nodeToDelete.Parent;

            // Update left child of successor.
            successorNode.Left = nodeToDelete.Left;

            // Replace.
            if (nodeToDelete.Parent?.Left == nodeToDelete) {
                nodeToDelete.Parent.Left = successorNode;
            }
            else if (nodeToDelete.Parent?.Right == nodeToDelete) {
                nodeToDelete.Parent.Right = successorNode;
            }
            else {
                this.root = successorNode;
            }
        }
        // (4) The node has two children and the successor is deeper in the tree.
        else {
            // Here, we have to do two things.
            // (a) Replace the successor with its right child.
            successorNode.Right.Parent = successorNode.Parent;
            successorNode.Parent.Left = successorNode.Right;

            // (b) Replace the node we want to delete with the successor.
            successorNode.Parent = nodeToDelete.Parent;
            successorNode.Left = nodeToDelete.Left;
            if (nodeToDelete.Parent?.Left == nodeToDelete) {
                nodeToDelete.Parent.Left = successorNode;
            }
            else if (nodeToDelete.Parent?.Right == nodeToDelete) {
                nodeToDelete.Parent.Right = successorNode;
            }
            else {
                this.root = successorNode;
            }
        }

        // Update count and set.
        this.Count--;
        this.keys.Remove(key);
    }

    private TreeDictionaryNode? GetSuccessor(TreeDictionaryNode node) {
        var currentNode = node.Right;

        if (currentNode == null) {
            return null;
        }

        while (currentNode.Left != null) {
            currentNode = currentNode.Left;
        }

        return currentNode;
    }
}

internal class Program {
    static void Main(string[] args) {
        var grades = new TreeDictionary<string, int>();

        // Should print "Current number of entries: 0".
        Console.WriteLine($"Current number of entries: {grades.Count}");

        // Add some entries.
        grades.Add("Nora", 1);
        grades["Koro"] = 2;

        // Test if keys exist.
        Console.WriteLine($"Contains Nora: {grades.ContainsKey("Nora")}");
        Console.WriteLine($"Contains Max: {grades.ContainsKey("Max")}");

        // Print values.
        Console.WriteLine($"Nora: {grades["Nora"]}");
        Console.WriteLine($"Koro: {grades["Koro"]}");

        // Change value.
        grades["Nora"] = 3;

        // Print values.
        Console.WriteLine($"Nora: {grades["Nora"]}");
        Console.WriteLine($"Koro: {grades["Koro"]}");

        // Test if values exist.
        Console.WriteLine($"Contains 1: {grades.ContainsValue(1)}");
        Console.WriteLine($"Contains 2: {grades.ContainsValue(2)}");

        // Should print "Current number of entries: 2".
        Console.WriteLine($"Current number of entries: {grades.Count}");

        // Iterate all keys.
        foreach (var key in grades.Keys) {
            Console.WriteLine($"{key}: {grades[key]}");
        }

        // Remove.
        grades.Remove("Koro");

        // Test if key was removed.
        Console.WriteLine($"Contains Koro: {grades.ContainsKey("Koro")}");

        // Should print "Current number of entries: 1".
        Console.WriteLine($"Current number of entries: {grades.Count}");

        // Clear.
        grades.Clear();

        // Should print "Current number of entries: 2".
        Console.WriteLine($"Current number of entries: {grades.Count}");

        // Add some new entries.
        grades.Add("Max", 1);
        grades.Add("Nora", 1);
        grades.Add("Koro", 1);
        grades.Add("Kai", 1);
        grades.Add("Christopher", 1);

        // Iterate all keys.
        foreach (var key in grades.Keys) {
            Console.WriteLine($"{key}: {grades[key]}");
        }

        // Remove root.
        grades.Remove("Max");

        // Iterate all keys.
        foreach (var key in grades.Keys) {
            Console.WriteLine($"{key}: {grades[key]}");
        }
    }
}
```

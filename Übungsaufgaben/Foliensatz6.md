# Foliensatz 6: Clean Code

## Übung 1: Breadth-first Traversal

```csharp
class BinaryTreeNode {
    public int Value { get; set; }

    public BinaryTreeNode LeftChild { get; set; }
    public BinaryTreeNode RightChild { get; set; }
}
class BinaryTree {
    public BinaryTreeNode Root { get; set; }

    public void PrintValuesBreadthFirst() {
        // Do nothing if the tree is empty.
        if (this.Root == null) {
            return;
        }

        // We create a queue to hold the next nodes we need
        // to visit. A queue's FIFO ordering gives us the 
        // correct order to get breadth-first behavior.
        var nodesToVisit = new Queue<BinaryTreeNode>();
        nodesToVisit.Enqueue(this.Root);

        // Now, we handle nodes as long as the queue is not empty.
        while (nodesToVisit.Count > 0) {
            // Get next node.
            var node = nodesToVisit.Dequeue();

            // Handle this node - in this case, print it.
            Console.Write($"{node.Value}, ");

            // Add the node's children to the queue, if they exist.
            if (node.LeftChild != null) {
                nodesToVisit.Enqueue(node.LeftChild);
            }
            if (node.RightChild != null) {
                nodesToVisit.Enqueue(node.RightChild);
            }
        }
    }
}
```

## Übung 2: Binary Search Tree Insertion

Hinweis: Die Lösung beinhaltet auch die Such-Operation.

```csharp
class BinarySearchTree {
    class BinarySearchTreeNode {
        public int Value { get; init; }

        public BinarySearchTreeNode? Parent { get; private set; }

        public BinarySearchTreeNode? Left { get; private set; }
        public BinarySearchTreeNode? Right { get; private set; }

        public bool Search(int searchValue) {
            // See if this node has the value.
            if (this.Value == searchValue) { 
                return true; 
            }

            // Otherwise, search left or right, depending on the value.
            if (searchValue < this.Value && this.Left != null) {
                return this.Left.Search(searchValue);
            }
            else if (searchValue > this.Value && this.Right != null) {
                return this.Right.Search(searchValue);
            }

            // If we get here, the value can't be in the tree.
            return false;
        }

        public void Insert(int value) {
            // Value is already in the tree? => stop.
            if (value == this.Value) {
                return;
            }

            // We descend through the tree until we would have to continue
            // descending into a subtree that doesn't exist. At that point, we insert.
            if (value < this.Value) {
                if (this.Left != null) {
                    this.Left.Insert(value);
                }
                else {
                    this.Left = new BinarySearchTreeNode() { Value = value, Parent = this };
                }
            }
            else {
                if (this.Right != null) {
                    this.Right.Insert(value);
                }
                else {
                    this.Right = new BinarySearchTreeNode() { Value = value, Parent = this };
                }
            }
        }
    }

    private BinarySearchTreeNode? root;

    public bool Search(int searchValue) {
        // If the tree is empty, it can't contain the value.
        if (root == null) {
            return false;
        }

        // Otherwise, search recursively.
        return this.root.Search(searchValue);
    }

    public void Insert(int value) {
        // If the tree is empty, this value will be the new root.
        if (this.root == null) {
            this.root = new BinarySearchTreeNode() { Value = value };
            return;
        }

        // Otherwise, recursively insert.
        this.root.Insert(value);
    }
}

class Program {
    public static void Main(String[] args) {
        var bst = new BinarySearchTree();
        bst.Insert(5);
        bst.Insert(6);
        bst.Insert(3);
        bst.Insert(4);
        bst.Insert(2);
        bst.Insert(9);
        bst.Insert(9);

        Console.WriteLine($"Contains 5? {bst.Search(5)}");
        Console.WriteLine($"Contains 6? {bst.Search(6)}");
        Console.WriteLine($"Contains 3? {bst.Search(3)}");
        Console.WriteLine($"Contains 4? {bst.Search(4)}");
        Console.WriteLine($"Contains 2? {bst.Search(2)}");
        Console.WriteLine($"Contains 9? {bst.Search(9)}");
        Console.WriteLine($"Contains 11? {bst.Search(11)}");
        Console.WriteLine($"Contains 1? {bst.Search(1)}");
        Console.WriteLine($"Contains 7? {bst.Search(7)}");
    }
}
```
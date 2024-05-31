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

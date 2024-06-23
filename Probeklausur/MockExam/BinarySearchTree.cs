using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockExam {
    class BinarySearchTree {
        public class BinarySearchTreeNode {
            public int Value { get; init; }

            public BinarySearchTreeNode? Parent { get; private set; }

            public BinarySearchTreeNode? Left { get; private set; }
            public BinarySearchTreeNode? Right { get; private set; }

            public bool Contains(int searchValue) {
                // See if this node has the value.
                if (this.Value == searchValue) {
                    return true;
                }

                // Otherwise, search left or right, depending on the value.
                if (searchValue < this.Value && this.Left != null) {
                    return this.Left.Contains(searchValue);
                }
                else if (searchValue > this.Value && this.Right != null) {
                    return this.Right.Contains(searchValue);
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

        public BinarySearchTreeNode? Root { get; private set; }

        public bool Contains(int searchValue) {
            // If the tree is empty, it can't contain the value.
            if (this.Root == null) {
                return false;
            }

            // Otherwise, search recursively.
            return this.Root.Contains(searchValue);
        }

        public void Insert(int value) {
            // If the tree is empty, this value will be the new root.
            if (this.Root == null) {
                this.Root = new BinarySearchTreeNode() { Value = value };
                return;
            }

            // Otherwise, recursively insert.
            this.Root.Insert(value);
        }
    }
}

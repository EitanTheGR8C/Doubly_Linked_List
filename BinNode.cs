using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doubly_Linked_List
{
    // Represents one node in a doubly linked list.
    // T is the type of value stored in the node, such as int or string.
    // Each node stores a value and two references:
    // left points to the previous node, and right points to the next node.
    // In a non-circular list, the first node's left reference is null,
    // and the last node's right reference is null.
    // Example: null <- [4] <-> [8] <-> [15] -> null
    // Constructors and setters only update this node.
    // The code using the class must connect neighbouring nodes in both directions.
    public class BinNode<T>
    {
        #region Part A
        private T value;
        private BinNode<T> left;
        private BinNode<T> right;

        public BinNode(T x)//buildes BinNode with 1 value and no one next to it.
        {
            this.value = x;
            this.left = null;
            this.right = null;
        }
        public BinNode(BinNode<T> left, T x, BinNode<T> right)//buildes BinNode with 1 value and two neighbors.
        {
            this.value = x;
            this.left = left;
            this.right =right;
        }
        public T GetValue()//returns value.
        {
            return this.value;
        }
        public void SetValue(T x)//sets new value for node.
        {
            this.value = x;
        }
        public BinNode<T> GetLeft()//returns the left node.
        {
            return this.left;
        }
        public BinNode<T> GetRight()//returns the right node.
        {
            return this.right;
        }
        public void SetLeft(BinNode<T> left)//sets the left node.
        {
            this.left = left;
        }
        public void SetRight(BinNode<T> right)//sets the right node.
        {
            this.right = right;
        }
        public override string ToString()
        {
            return "" + this.value;
        }
        #endregion Part A
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doubly_Linked_List
{
    internal class BinNode<T>
    {
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
    }
}

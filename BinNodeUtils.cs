using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doubly_Linked_List
{
    // Contains static methods for doubly linked lists of BinNode<int>.
    // GetLeft() points to the previous node; GetRight() points to the next.
    // Most methods receive the first node. Some receive another node or an array.
    // A null list reference represents an empty list.
    // Parts B-F cover basic operations, adding, removing, circular lists,
    // and advanced questions.
    //
    // Complexity describes how the work or memory grows with the input size.
    // n usually means the number of nodes, or the array length for array input.
    // For two lists, n and m can represent their separate lengths.
    // For ReadReversed, n can mean the number of values entered before 999.
    // Always state what n means for the method being analysed.
    //
    // Time complexity describes how the number of operations grows.
    // O(1): bounded work independent of input size.
    // O(n): work grows at most linearly, such as scanning all n nodes once.
    //
    // Auxiliary space describes extra working memory, excluding the input
    // and any returned output. A fixed number of variables uses O(1) space.
    // State output space separately when creating a new list.
    // Example: BuildList uses O(1) auxiliary space and O(n) output space.
    internal class BinNodeUtils
    {
        // === Part B ===
        // Part B: basic operations.
        // Build a list from an array, print in either direction, count nodes,
        // sum values, search, and find the first or last node.
        // Also check that the links are consistent and the values are sorted.


        // Time complexity: O(n).
        // Auxiliary space: O(1). Output space: O(n) for the new nodes.
        public static BinNode<int> BuildList(int[] arr)//QB1
        {
            if (arr == null || arr.Length == 0)
                return null;

            BinNode<int> first = new BinNode<int>(arr[0]);
            BinNode<int> last = first;

            for (int i = 1; i < arr.Length; i++)
            {
                BinNode<int> newNode = new BinNode<int>(arr[i]);
                last.SetRight(newNode);
                newNode.SetLeft(last);
                last = newNode;
            }

            return first;
        }

        // Time complexity: O(n^2), due to repeated string copying.
        // Auxiliary space: O(n) for the accumulated string.
        public static void PrintForward(BinNode<int> first)//QB2
        {//assuming first on left
            if (first == null)
            {
                Console.WriteLine();
                return;
            }
            string str = "";
            while (first != null)
            {
                str += first.GetValue() + " ";
                first = first.GetRight();
            }
            Console.WriteLine(str);
        }

        // Time complexity: O(n).
        // Auxiliary space: O(1).
        public static BinNode<int> GetLast(BinNode<int> first)//QB3
        {//assuming first on left
            if ( first == null)
                return null;
            while (first.GetRight() != null)
            {
                first = first.GetRight();
            }
            return first;
        }

        // Time complexity: O(n).
        // Auxiliary space: O(1).
        public static void PrintBackward(BinNode<int> last)//QB4
        {//assuming last on right
            while (last != null)
            {
                Console.Write(last.GetValue() + " ");
                last = last.GetLeft();
            }
            Console.WriteLine();
        }

        // Time complexity: O(n).
        // Auxiliary space: O(1).
        public static int Length(BinNode<int> first)//QB5
        {//assuming first on left
            if (first == null)
                return 0;
            int cnt = 0;
            while (first != null)
            {
                cnt++;
                first = first.GetRight();
            }
            return cnt;
        }

        // Time complexity: O(n).
        // Auxiliary space: O(1).
        public static int Sum(BinNode<int> first)//QB5
        {//assuming first on left
            if (first == null)
                return 0;
            int sum = 0;
            while (first != null)
            {
                sum += first.GetValue();
                first = first.GetRight();
            }    
            return sum;
        }

        // Time complexity: O(n) worst case; O(1) if x is found immediately.
        // Auxiliary space: O(1).
        public static bool IsExist(BinNode<int> first, int x)//QB6
        {//assuming first on left
            while (first != null)
            {
                if(x == first.GetValue()) 
                    return true;
                first = first.GetRight();
            }
            return false;
        }

        // Time complexity: O(n) worst case; O(1) if x is found immediately.
        // Auxiliary space: O(1).
        public static BinNode<int> Find(BinNode<int> first, int x)//QB6
        {//assuming first on left
            while (first != null)
            {
                if(first.GetValue() == x)
                    return first;
                first = first.GetRight();
            }
            return null;
        }

        // Time complexity: O(n) worst case, walking left from the last node.
        // Auxiliary space: O(1).
        public static BinNode<int> GetFirst(BinNode<int> p)//QB7
        {
            if(p == null)
                return null;
            while (p.GetLeft() != null)
            {
                p = p.GetLeft();
            }
            return p;
        }

        // Time complexity: O(n), finding the start and then printing the list.
        // Auxiliary space: O(1).
        public static void PrintAll(BinNode<int> p)//QB7
        {
            p = GetFirst(p);
            PrintForward(p);
        }

        // Time complexity: O(n) worst case.
        // Auxiliary space: O(1).
        public static bool IsConsistent(BinNode<int> first)//QB8
        {
            if (first == null)
                return true;

            if (first.GetLeft() != null)
                return false;

            while (first.GetRight() != null)
            {
                if (first.GetRight().GetLeft() != first)
                    return false;

                first = first.GetRight();
            }
            return true;
        }

        // Time complexity: O(n) worst case.
        // Auxiliary space: O(1).
        public static bool IsSorted(BinNode<int> first)//QB9
        {
            if (first == null)
                return true;

            while (first.GetRight() != null)
            {
                if (first.GetValue() > first.GetRight().GetValue())
                    return false;

                first = first.GetRight();
            }
            return true;
        }


        // === Part C ===
        // Part C: adding nodes.
        // Insert at the start, end, before or after a node, or into a sorted list.
        // Update the links in both directions.
        // Methods that may replace the first node return the updated first node.
        // Handle empty lists and insertions at either end where required.

        public static BinNode<int> AddFirst(BinNode<int> first, int x)
        {
            // TODO
            return null;
        }

        public static BinNode<int> AddLast(BinNode<int> first, int x)
        {
            // TODO
            return null;
        }

        public static void AddAfter(BinNode<int> p, int x)
        {
            // TODO
        }

        public static void AddBefore(BinNode<int> p, int x)
        {
            // TODO
        }

        public static BinNode<int> AddBeforeAny(BinNode<int> first, BinNode<int> p, int x)
        {
            // TODO
            return null;
        }

        public static BinNode<int> InsertSorted(BinNode<int> first, int x)
        {
            // TODO
            return null;
        }

        public static BinNode<int> BuildSorted(int[] arr)
        {
            // TODO
            return null;
        }


        // === Part D ===
        // Part D: removing nodes.
        // Remove a given node, the first occurrence of a value, or all occurrences.
        // Reconnect the remaining neighbours in both directions.
        // Handle removal of the first, last or only node.
        // Return the updated first node, which may be null.

        public static BinNode<int> RemoveNode(BinNode<int> first, BinNode<int> p)
        {
            // TODO
            return null;
        }

        public static BinNode<int> RemoveValue(BinNode<int> first, int x)
        {
            // TODO
            return null;
        }

        public static BinNode<int> RemoveAll(BinNode<int> first, int x)
        {
            // TODO
            return null;
        }


        // === Part E ===
        // Part E: circular doubly linked lists.
        // The last node's right link points to the first node.
        // The first node's left link points to the last node.
        // In a valid non-empty circular list, neither link is null.
        // Traverse one full circle, stopping when the starting node is reached again.
        // Handle empty lists and single-node lists separately where needed.

        public static BinNode<int> BuildCircular(int[] arr)
        {
            // TODO
            return null;
        }

        public static void PrintCircular(BinNode<int> p)
        {
            // TODO
        }

        public static BinNode<int> AddLastCircular(BinNode<int> first, int x)
        {
            // TODO
            return null;
        }

        public static BinNode<int> RemoveCircular(BinNode<int> first, BinNode<int> p)
        {
            // TODO
            return null;
        }

        // === Part F ===
        // Part F: advanced questions.
        // Check for a palindrome, find the middle node, build a reversed input list,
        // insert one list into another, and add numbers represented by digit lists.
        // Use the previous methods where helpful.
        // Follow each question's restrictions on arrays, extra lists and traversal.

        public static bool IsPalindrome(BinNode<int> first)
        {
            // TODO
            return false;
        }

        public static BinNode<int> GetMiddle(BinNode<int> first)
        {
            // TODO
            return null;
        }

        public static BinNode<int> ReadReversed()
        {
            // TODO
            return null;
        }

        public static BinNode<int> InsertToCenter(BinNode<int> list1, BinNode<int> list2)
        {
            // TODO
            return null;
        }

        public static BinNode<int> AddBig(BinNode<int> list1, BinNode<int> list2)
        {
            // TODO
            return null;
        }
    }
}

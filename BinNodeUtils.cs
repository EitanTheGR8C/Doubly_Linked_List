using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
    public static class BinNodeUtils
    {
        #region Part B
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

        // Time complexity: O(n). Since only the first node is given, we must follow the right links to reach the last node.
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

        //QB7: From any node, we can follow the left links to reach the first node,
        //then print the whole list.In a singly linked list, we cannot move backward,
        //so a middle node does not give us access to the nodes before it.

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
        #endregion Part B
        #region Part C
        // === Part C ===
        // Part C: adding nodes.
        // Insert at the start, end, before or after a node, or into a sorted list.
        // Update the links in both directions.
        // Methods that may replace the first node return the updated first node.
        // Handle empty lists and insertions at either end where required.

        // Time complexity: O(1).
        // Auxiliary space: O(1). Output space: one new node.
        public static BinNode<int> AddFirst(BinNode<int> first, int x)//QC1
        {//assuming first on very left
            BinNode<int> to_add = new BinNode<int>(null, x, first);

            if (first != null)
                first.SetLeft(to_add);

            return to_add;
        }

        // Time complexity: O(n), to reach the last node.
        // Auxiliary space: O(1). Output space: one new node.
        //To make it O(1), store a reference to the last node as well as the first.
        //Keep that reference updated whenever the last node changes.
        //Adding at the end then requires only linking the new node and updating last,
        //without traversing the list.
        public static BinNode<int> AddLast(BinNode<int> first, int x)//QC2
        {//assuming first on very left
            BinNode<int> toAdd = new BinNode<int>(x);
            if (first == null)
            {
                return toAdd;
            }
            BinNode<int> last = first;
            while (last.GetRight() != null)
            {
                last = last.GetRight();
            }
            last.SetRight(toAdd);
            toAdd.SetLeft(last);
            return first;
        }

        // Time complexity: O(1), since p is given.
        // Auxiliary space: O(1). Output space: one new node.
        public static void AddAfter(BinNode<int> p, int x)//QC3
        {
            if (p == null)
                return;

            BinNode<int> newNode = new BinNode<int>(x);
            BinNode<int> next = p.GetRight();

            newNode.SetLeft(p);
            newNode.SetRight(next);
            p.SetRight(newNode);

            if (next != null)
                next.SetLeft(newNode);
        }

        // Time complexity: O(1), since p and its left link are given.
        // Auxiliary space: O(1). Output space: one new node.
        public static void AddBefore(BinNode<int> p, int x)//QC4
        {
            if (p == null || p.GetLeft() == null)
                return;

            BinNode<int> prev = p.GetLeft();
            BinNode<int> newNode = new BinNode<int>(prev, x, p);

            prev.SetRight(newNode);
            p.SetLeft(newNode);
        }

        // Time complexity: O(1), using AddFirst or AddBefore.
        // Auxiliary space: O(1). Output space: one new node.
        public static BinNode<int> AddBeforeAny(BinNode<int> first, BinNode<int> p, int x)//QC5
        {
            if (p == null)
                return first;

            if (p == first)
                return AddFirst(first, x);

            AddBefore(p, x);
            return first;
        }

        // Time complexity: O(n), to find the insertion position.
        // Auxiliary space: O(1). Output space: one new node.
        public static BinNode<int> InsertSorted(BinNode<int> first, int x)//QC6
        {//assuming first is very left
            BinNode<int> toAdd = new BinNode<int>(x);

            if (first == null)
                return toAdd;

            if (first.GetValue() >= x)
            {
                first.SetLeft(toAdd);
                toAdd.SetRight(first);
                return toAdd;
            }

            BinNode<int> p = first;
            while (p.GetRight() != null && p.GetRight().GetValue() < x)
            {
                p = p.GetRight();
            }

            BinNode<int> next = p.GetRight();
            toAdd.SetLeft(p);
            toAdd.SetRight(next);
            p.SetRight(toAdd);

            if (next != null)
                next.SetLeft(toAdd);

            return first;
        }

        // Time complexity: O(n^2), using repeated InsertSorted calls.
        // Auxiliary space: O(1). Output space: O(n) for the new nodes.
        public static BinNode<int> BuildSorted(int[] arr)//QC7
        {
            BinNode<int> first = null;

            if (arr == null)
                return first;

            for (int i = 0; i < arr.Length; i++)
            {
                first = InsertSorted(first, arr[i]);
            }

            return first;
        }
        #endregion Part C
        #region Part D
        // === Part D ===
        // Part D: removing nodes.
        // Remove a given node, the first occurrence of a value, or all occurrences.
        // Reconnect the remaining neighbours in both directions.
        // Handle removal of the first, last or only node.
        // Return the updated first node, which may be null.

        // Time complexity: O(1), since p is given.
        // Auxiliary space: O(1).
        public static BinNode<int> RemoveNode(BinNode<int> first, BinNode<int> p)//QD1
        {//assuming first on very left
            if (first == null || p == null)
                return first;

            BinNode<int> prev = p.GetLeft();
            BinNode<int> next = p.GetRight();

            if (prev != null)
                prev.SetRight(next);

            if (next != null)
                next.SetLeft(prev);

            if (p == first)
                return next;

            return first;
        }

        // Time complexity: O(n), to find the value.
        // Auxiliary space: O(1).
        public static BinNode<int> RemoveValue(BinNode<int> first, int x)//QD2
        {//assuming first on very left
            BinNode<int> p = Find(first, x);
            first = RemoveNode(first, p);
            return first;
        }

        // Time complexity: O(n), using one traversal.
        // Auxiliary space: O(1).
        public static BinNode<int> RemoveAll(BinNode<int> first, int x)//QD3
        {//assuming first on very left
            BinNode<int> current = first;
            while (current != null)
            {
                BinNode<int> next = current.GetRight();
                if (current.GetValue() == x)
                    first = RemoveNode(first, current);
                current = next;
            }
            return first;
        }
        #endregion Part D
        #region Part E
        // === Part E ===
        // Part E: circular doubly linked lists.
        // The last node's right link points to the first node.
        // The first node's left link points to the last node.
        // In a valid non-empty circular list, neither link is null.
        // Traverse one full circle, stopping when the starting node is reached again.
        // Handle empty lists and single-node lists separately where needed.

        // Time complexity: O(n), n = array length.
        // Auxiliary space: O(1). Output space: O(n) for the new nodes.
        public static BinNode<int> BuildCircular(int[] arr)//QE1
        {
            if(arr == null || arr.Length == 0) 
                return null;
            
            BinNode<int> first = BuildList(arr);
            BinNode<int> last  = GetLast(first);

            last.SetRight(first);
            first.SetLeft(last);
            return first;
        }

        //If q starts at p, the condition q != p is false before the first iteration,
        //so a regular while loop never runs and nothing is printed.
        //A do-while loop fits because it prints the current node and moves to the next node before checking whether q has returned to p.
        //This also works for a list containing only one node.
        //
        // Time complexity: O(n), using direct printing for one full circle.
        // Auxiliary space: O(1).
        public static void PrintCircular(BinNode<int> p)//QE2
        {
            if (p == null)
            {
                Console.WriteLine();
                return;
            }
            BinNode<int> q = p;
            do
            {
                Console.Write(q + " ");
                q = q.GetRight();
            } while (p != q);
            Console.WriteLine();

        }

        // Time complexity: O(1), using first.GetLeft() to reach the last node.
        // Auxiliary space: O(1). Output space: one new node.
        public static BinNode<int> AddLastCircular(BinNode<int> first, int x)//QE3
        {//assuming circular
            BinNode<int> toAdd = new BinNode<int>(x);
            if (first == null)
            {
                toAdd.SetRight(toAdd);
                toAdd.SetLeft(toAdd);
                return toAdd;
            }
 
            toAdd.SetLeft(first.GetLeft());
            first.GetLeft().SetRight(toAdd);
            first.SetLeft(toAdd);
            toAdd.SetRight(first);
            return first;
        }

        // Time complexity: O(1), since p is given.
        // Auxiliary space: O(1).
        public static BinNode<int> RemoveCircular(BinNode<int> first, BinNode<int> p)//QE4
        {
            if (first == null || p == null)
                return first;

            if (p.GetRight() == p)
                return null;

            p.GetLeft().SetRight(p.GetRight());
            p.GetRight().SetLeft(p.GetLeft());

            if (p == first)
                return p.GetRight();

            return first;
        }
        #endregion Part E
        #region Part F
        // === Part F ===
        // Part F: advanced questions.
        // Check for a palindrome, find the middle node, build a reversed input list,
        // insert one list into another, and add numbers represented by digit lists.
        // Use the previous methods where helpful.
        // Follow each question's restrictions on arrays, extra lists and traversal.

        // Time complexity: O(n), finding the last node and comparing from both ends.
        // Auxiliary space: O(1).
        public static bool IsPalindrome(BinNode<int> first)//QF1
        {
            // TODO
            return false;
        }

        // Time complexity: O(n), moving references inward from both ends.
        // Auxiliary space: O(1).
        public static BinNode<int> GetMiddle(BinNode<int> first)//QF2
        {
            // TODO
            return null;
        }

        // Time complexity: O(n), adding each input number at the start.
        // n = number of inputs before 999.
        // Auxiliary space: O(1). Output space: O(n) for the new nodes.
        public static BinNode<int> ReadReversed()//QF3
        {
            // TODO
            return null;
        }

        // Time complexity: O(n + m), n and m = lengths of the two lists.
        // Auxiliary space: O(1), reusing the existing nodes.
        public static BinNode<int> InsertToCenter(BinNode<int> list1, BinNode<int> list2)//QF4
        {
            // TODO
            return null;
        }

        // Time complexity: O(n + m), n and m = digit counts of the two numbers.
        // Auxiliary space: O(1).
        // Output space: O(max(n, m)) for the result's new nodes.
        public static BinNode<int> AddBig(BinNode<int> list1, BinNode<int> list2)//QF5
        {
            // TODO
            return null;
        }
        #endregion PartF
    }
}

// ============================================================================
//  Tests.cs  -  automatic tests for the "Doubly Linked List (BinNode)" exercise
// ----------------------------------------------------------------------------
//  HOW TO USE
//  1. Add this file to your project (next to BinNodeUtils.cs). Do not edit the code
//     below, only the #define lines.
//  2. Your functions must be:  public static  inside the class named BinNodeUtils (file BinNodeUtils.cs),
//     and must have exactly the names and signatures given in the exercise.
//  3. In Main, call:   Tests.RunAll();
//  4. Un-comment a #define line ONLY after you finished that part. If you
//     enable a part whose functions do not exist yet, the project will not compile.
//  5. FAIL lines tell you which case failed, what you returned and what was expected.
//
//  Note: the tests build their own lists, so a bug in your BuildList does not
//  affect the tests of the other functions.
// ============================================================================

#define PART_A
#define PART_B
#define PART_C
#define PART_D
#define PART_E
#define PART_F

using Doubly_Linked_List;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class Tests
{
    // ------------------------------------------------------------------ runner
    static int passed = 0, failed = 0;
    static List<string> problems;

    public static void RunAll()
    {
        Console.WriteLine("=== Doubly linked list tests ===");
#if PART_A
        Console.WriteLine("--- Part A: BinNode ---");
        Run("BinNode: constructor with value", TestBinNodeCtor1);
        Run("BinNode: constructor with neighbours", TestBinNodeCtor2);
        Run("BinNode: SetValue / SetLeft / SetRight", TestBinNodeSetters);
        Run("BinNode: ToString", TestBinNodeToString);
#endif
#if PART_B
        Console.WriteLine("--- Part B: basic operations ---");
        Run("B1 BuildList", TestBuildList);
        Run("B2 PrintForward", TestPrintForward);
        Run("B3 GetLast", TestGetLast);
        Run("B4 PrintBackward", TestPrintBackward);
        Run("B5 Length", TestLength);
        Run("B5 Sum", TestSum);
        Run("B6 IsExist", TestIsExist);
        Run("B6 Find", TestFind);
        Run("B7 GetFirst", TestGetFirst);
        Run("B7 PrintAll", TestPrintAll);
        Run("B8 IsConsistent", TestIsConsistent);
        Run("B9 IsSorted", TestIsSorted);
#endif
#if PART_C
        Console.WriteLine("--- Part C: insertion ---");
        Run("C1 AddFirst", TestAddFirst);
        Run("C2 AddLast", TestAddLast);
        Run("C3 AddAfter", TestAddAfter);
        Run("C4 AddBefore", TestAddBefore);
        Run("C5 AddBeforeAny", TestAddBeforeAny);
        Run("C6 InsertSorted", TestInsertSorted);
        Run("C7 BuildSorted", TestBuildSorted);
#endif
#if PART_D
        Console.WriteLine("--- Part D: removal ---");
        Run("D1 RemoveNode", TestRemoveNode);
        Run("D2 RemoveValue", TestRemoveValue);
        Run("D3 RemoveAll", TestRemoveAll);
#endif
#if PART_E
        Console.WriteLine("--- Part E: circular list ---");
        Run("E1 BuildCircular", TestBuildCircular);
        Run("E2 PrintCircular", TestPrintCircular);
        Run("E3 AddLastCircular", TestAddLastCircular);
        Run("E4 RemoveCircular", TestRemoveCircular);
#endif
#if PART_F
        Console.WriteLine("--- Part F: advanced ---");
        Run("F1 IsPalindrome", TestIsPalindrome);
        Run("F2 GetMiddle", TestGetMiddle);
        Run("F3 ReadReversed", TestReadReversed);
        Run("F4 InsertToCenter", TestInsertToCenter);
        Run("F5 AddBig", TestAddBig);
#endif
        Console.WriteLine();
        Console.WriteLine("Passed: " + passed + "   Failed: " + failed);
        if (failed == 0)
            Console.WriteLine("All enabled tests passed!");
    }

    static void Run(string name, Action test)
    {
        problems = new List<string>();
        try
        {
            test();
        }
        catch (Exception e)
        {
            problems.Add("crashed: " + e.GetType().Name + " (probably a null reference or an endless loop in your code)");
        }
        if (problems.Count == 0)
        {
            passed++;
            Console.WriteLine("  PASS  " + name);
        }
        else
        {
            failed++;
            Console.WriteLine("  FAIL  " + name);
            foreach (string s in problems)
                Console.WriteLine("          " + s);
        }
    }

    // ----------------------------------------------------------- test helpers
    static void Eq(string what, string actual, string expected)
    {
        if (actual != expected)
            problems.Add(what + ": got [" + actual + "], expected [" + expected + "]");
    }
    static void EqInt(string what, int actual, int expected)
    {
        if (actual != expected)
            problems.Add(what + ": got " + actual + ", expected " + expected);
    }
    static void EqBool(string what, bool actual, bool expected)
    {
        if (actual != expected)
            problems.Add(what + ": got " + actual + ", expected " + expected);
    }
    static void IsNull(string what, BinNode<int> node)
    {
        if (node != null)
            problems.Add(what + ": got a node with value " + node.GetValue() + ", expected null");
    }
    static void Same(string what, BinNode<int> actual, BinNode<int> expected)
    {
        if (actual != expected)
            problems.Add(what + ": returned a different node than expected");
    }

    // builds a list with BinNode only (independent of the student's BuildList)
    static BinNode<int> Make(params int[] a)
    {
        BinNode<int> first = null, last = null;
        for (int i = 0; i < a.Length; i++)
        {
            BinNode<int> n = new BinNode<int>(a[i]);
            if (first == null) first = n;
            else { last.SetRight(n); n.SetLeft(last); }
            last = n;
        }
        return first;
    }
    static BinNode<int> MakeCircular(params int[] a)
    {
        BinNode<int> first = Make(a);
        if (first == null) return null;
        BinNode<int> last = first;
        while (last.GetRight() != null) last = last.GetRight();
        last.SetRight(first);
        first.SetLeft(last);
        return first;
    }
    static BinNode<int> At(BinNode<int> first, int index)
    {
        BinNode<int> p = first;
        for (int i = 0; i < index; i++) p = p.GetRight();
        return p;
    }
    static BinNode<int> Last(BinNode<int> first)
    {
        BinNode<int> p = first;
        while (p.GetRight() != null) p = p.GetRight();
        return p;
    }
    // forward string, e.g. "1 2 3"; "LOOP" if the list never ends
    static string Fwd(BinNode<int> first)
    {
        StringBuilder sb = new StringBuilder();
        int steps = 0;
        for (BinNode<int> p = first; p != null; p = p.GetRight())
        {
            if (++steps > 1000) return "LOOP";
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(p.GetValue());
        }
        return sb.ToString();
    }
    // backward string, starting from the last node (found by going right)
    static string Bwd(BinNode<int> first)
    {
        if (first == null) return "";
        BinNode<int> p = first;
        int steps = 0;
        while (p.GetRight() != null)
        {
            if (++steps > 1000) return "LOOP";
            p = p.GetRight();
        }
        StringBuilder sb = new StringBuilder();
        steps = 0;
        for (; p != null; p = p.GetLeft())
        {
            if (++steps > 1000) return "LOOP";
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(p.GetValue());
        }
        return sb.ToString();
    }
    static string Rev(string s)
    {
        if (s == "") return "";
        string[] parts = s.Split(' ');
        Array.Reverse(parts);
        return string.Join(" ", parts);
    }
    // checks forward order, backward order (so both links are right) and the two ends
    static void List(string what, BinNode<int> first, string expected)
    {
        Eq(what + " (forward)", Fwd(first), expected);
        Eq(what + " (backward, checks the Left links)", Bwd(first), Rev(expected));
        if (first != null && first.GetLeft() != null)
            problems.Add(what + ": the first node must have Left == null");
    }
    // circular list: n steps right and n steps left must return to the start
    static void Circular(string what, BinNode<int> first, string expected)
    {
        if (expected == "")
        {
            IsNull(what, first);
            return;
        }
        if (first == null) { problems.Add(what + ": got null, expected a list"); return; }
        string[] exp = expected.Split(' ');
        int n = exp.Length;
        BinNode<int> p = first;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            if (p == null) { problems.Add(what + ": a Right link is null"); return; }
            if (i > 0) sb.Append(' ');
            sb.Append(p.GetValue());
            p = p.GetRight();
        }
        Eq(what + " (going right)", sb.ToString(), expected);
        if (p != first) problems.Add(what + ": after " + n + " steps right we should be back at the first node");
        p = first;
        sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            if (p == null) { problems.Add(what + ": a Left link is null"); return; }
            if (i > 0) sb.Append(' ');
            sb.Append(p.GetValue());
            p = p.GetLeft();
        }
        Eq(what + " (going left)", sb.ToString(), Rev(expected).Length == 0 ? "" : RotateRev(exp));
        if (p != first) problems.Add(what + ": after " + n + " steps left we should be back at the first node");
    }
    // going left from first gives: first, last, ..., second
    static string RotateRev(string[] exp)
    {
        List<string> r = new List<string>();
        r.Add(exp[0]);
        for (int i = exp.Length - 1; i >= 1; i--) r.Add(exp[i]);
        return string.Join(" ", r);
    }
    // runs an action and returns everything it printed, normalised to single spaces
    static string Capture(Action a)
    {
        TextWriter old = Console.Out;
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        try { a(); }
        finally { Console.SetOut(old); }
        string[] parts = sw.ToString().Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Join(" ", parts);
    }

    // =========================================================== PART A
#if PART_A
    static void TestBinNodeCtor1()
    {
        BinNode<int> n = new BinNode<int>(7);
        EqInt("GetValue", n.GetValue(), 7);
        IsNull("GetLeft", n.GetLeft());
        IsNull("GetRight", n.GetRight());
    }
    static void TestBinNodeCtor2()
    {
        BinNode<int> l = new BinNode<int>(1);
        BinNode<int> r = new BinNode<int>(3);
        BinNode<int> n = new BinNode<int>(l, 2, r);
        EqInt("GetValue", n.GetValue(), 2);
        Same("GetLeft", n.GetLeft(), l);
        Same("GetRight", n.GetRight(), r);
        IsNull("the constructor must not change l.GetRight()", l.GetRight());
        IsNull("the constructor must not change r.GetLeft()", r.GetLeft());
        BinNode<int> m = new BinNode<int>(null, 5, null);
        IsNull("null left", m.GetLeft());
        IsNull("null right", m.GetRight());
    }
    static void TestBinNodeSetters()
    {
        BinNode<int> a = new BinNode<int>(1), b = new BinNode<int>(2), c = new BinNode<int>(3);
        a.SetValue(10);
        EqInt("SetValue", a.GetValue(), 10);
        a.SetRight(b);
        b.SetLeft(a);
        a.SetLeft(c);
        Same("SetRight", a.GetRight(), b);
        Same("SetLeft", b.GetLeft(), a);
        Same("SetLeft", a.GetLeft(), c);
        a.SetRight(null);
        IsNull("SetRight(null)", a.GetRight());
    }
    static void TestBinNodeToString()
    {
        Eq("int", new BinNode<int>(42).ToString(), "42");
        Eq("string node", new BinNode<string>("abc").ToString(), "abc");
        BinNode<char> c = new BinNode<char>('x');
        Eq("char node", c.ToString(), "x");
    }
#endif

    // =========================================================== PART B
#if PART_B
    static void TestBuildList()
    {
        IsNull("empty array", BinNodeUtils.BuildList(new int[0]));
        List("one element", BinNodeUtils.BuildList(new int[] { 5 }), "5");
        List("three elements", BinNodeUtils.BuildList(new int[] { 1, 2, 3 }), "1 2 3");
        List("with repeated values", BinNodeUtils.BuildList(new int[] { 4, 4, 9, 4 }), "4 4 9 4");
        int[] arr = { 7, 8 };
        BinNodeUtils.BuildList(arr);
        Eq("the array must not change", arr[0] + " " + arr[1], "7 8");
    }
    static void TestPrintForward()
    {
        Eq("empty list", Capture(delegate { BinNodeUtils.PrintForward(null); }), "");
        Eq("one node", Capture(delegate { BinNodeUtils.PrintForward(Make(5)); }), "5");
        Eq("four nodes", Capture(delegate { BinNodeUtils.PrintForward(Make(4, 8, 15, 16)); }), "4 8 15 16");
    }
    static void TestGetLast()
    {
        IsNull("empty list", BinNodeUtils.GetLast(null));
        BinNode<int> one = Make(5);
        Same("one node", BinNodeUtils.GetLast(one), one);
        BinNode<int> l = Make(1, 2, 3, 4);
        Same("four nodes", BinNodeUtils.GetLast(l), At(l, 3));
    }
    static void TestPrintBackward()
    {
        Eq("null", Capture(delegate { BinNodeUtils.PrintBackward(null); }), "");
        Eq("one node", Capture(delegate { BinNodeUtils.PrintBackward(Make(5)); }), "5");
        BinNode<int> l = Make(4, 8, 15, 16);
        Eq("four nodes", Capture(delegate { BinNodeUtils.PrintBackward(At(l, 3)); }), "16 15 8 4");
    }
    static void TestLength()
    {
        EqInt("empty", BinNodeUtils.Length(null), 0);
        EqInt("one node", BinNodeUtils.Length(Make(9)), 1);
        EqInt("five nodes", BinNodeUtils.Length(Make(1, 2, 3, 4, 5)), 5);
    }
    static void TestSum()
    {
        EqInt("empty", BinNodeUtils.Sum(null), 0);
        EqInt("one node", BinNodeUtils.Sum(Make(9)), 9);
        EqInt("several", BinNodeUtils.Sum(Make(1, 2, 3, 4, 5)), 15);
        EqInt("negative", BinNodeUtils.Sum(Make(-5, 3, -1)), -3);
    }
    static void TestIsExist()
    {
        EqBool("empty list", BinNodeUtils.IsExist(null, 3), false);
        BinNode<int> l = Make(4, 8, 15);
        EqBool("first", BinNodeUtils.IsExist(l, 4), true);
        EqBool("middle", BinNodeUtils.IsExist(l, 8), true);
        EqBool("last", BinNodeUtils.IsExist(l, 15), true);
        EqBool("missing", BinNodeUtils.IsExist(l, 16), false);
    }
    static void TestFind()
    {
        IsNull("empty list", BinNodeUtils.Find(null, 3));
        BinNode<int> l = Make(4, 8, 15, 8);
        Same("first", BinNodeUtils.Find(l, 4), At(l, 0));
        Same("first occurrence of 8", BinNodeUtils.Find(l, 8), At(l, 1));
        Same("last", BinNodeUtils.Find(l, 15), At(l, 2));
        IsNull("missing", BinNodeUtils.Find(l, 99));
    }
    static void TestGetFirst()
    {
        IsNull("null", BinNodeUtils.GetFirst(null));
        BinNode<int> l = Make(1, 2, 3, 4);
        Same("from the first", BinNodeUtils.GetFirst(At(l, 0)), At(l, 0));
        Same("from the middle", BinNodeUtils.GetFirst(At(l, 2)), At(l, 0));
        Same("from the last", BinNodeUtils.GetFirst(At(l, 3)), At(l, 0));
    }
    static void TestPrintAll()
    {
        BinNode<int> l = Make(1, 2, 3, 4);
        Eq("from the middle", Capture(delegate { BinNodeUtils.PrintAll(At(l, 2)); }), "1 2 3 4");
        Eq("from the last", Capture(delegate { BinNodeUtils.PrintAll(At(l, 3)); }), "1 2 3 4");
        Eq("from the first", Capture(delegate { BinNodeUtils.PrintAll(At(l, 0)); }), "1 2 3 4");
    }
    static void TestIsConsistent()
    {
        EqBool("empty list", BinNodeUtils.IsConsistent(null), true);
        EqBool("one node", BinNodeUtils.IsConsistent(Make(1)), true);
        EqBool("good list", BinNodeUtils.IsConsistent(Make(1, 2, 3)), true);
        BinNode<int> a = Make(1, 2, 3);
        At(a, 1).SetLeft(null);
        EqBool("a Left link is missing", BinNodeUtils.IsConsistent(a), false);
        BinNode<int> b = Make(1, 2, 3);
        At(b, 2).SetLeft(At(b, 0));
        EqBool("a Left link points to the wrong node", BinNodeUtils.IsConsistent(b), false);
        BinNode<int> c = Make(1, 2, 3);
        c.SetLeft(At(c, 2));
        EqBool("the first node has a Left", BinNodeUtils.IsConsistent(c), false);
    }
    static void TestIsSorted()
    {
        EqBool("empty", BinNodeUtils.IsSorted(null), true);
        EqBool("one node", BinNodeUtils.IsSorted(Make(5)), true);
        EqBool("sorted", BinNodeUtils.IsSorted(Make(1, 2, 2, 7)), true);
        EqBool("not sorted at the start", BinNodeUtils.IsSorted(Make(3, 1, 2)), false);
        EqBool("not sorted at the end", BinNodeUtils.IsSorted(Make(1, 2, 9, 4)), false);
        EqBool("descending", BinNodeUtils.IsSorted(Make(3, 2, 1)), false);
    }
#endif

    // =========================================================== PART C
#if PART_C
    static void TestAddFirst()
    {
        List("empty list", BinNodeUtils.AddFirst(null, 5), "5");
        List("one node", BinNodeUtils.AddFirst(Make(2), 1), "1 2");
        BinNode<int> l = Make(2, 3, 4);
        BinNode<int> r = BinNodeUtils.AddFirst(l, 1);
        List("three nodes", r, "1 2 3 4");
        EqInt("returned node value", r.GetValue(), 1);
    }
    static void TestAddLast()
    {
        List("empty list", BinNodeUtils.AddLast(null, 5), "5");
        BinNode<int> one = Make(1);
        Same("the first is not replaced", BinNodeUtils.AddLast(one, 2), one);
        List("one node", one, "1 2");
        BinNode<int> l = Make(1, 2, 3);
        List("three nodes", BinNodeUtils.AddLast(l, 4), "1 2 3 4");
    }
    static void TestAddAfter()
    {
        BinNode<int> a = Make(1);
        BinNodeUtils.AddAfter(a, 2);
        List("after the only node", a, "1 2");
        BinNode<int> b = Make(1, 2, 4);
        BinNodeUtils.AddAfter(At(b, 1), 3);
        List("in the middle", b, "1 2 3 4");
        BinNode<int> c = Make(1, 2, 3);
        BinNodeUtils.AddAfter(At(c, 2), 4);
        List("after the last", c, "1 2 3 4");
        BinNode<int> d = Make(2, 3);
        BinNodeUtils.AddAfter(d, 9);
        List("after the first", d, "2 9 3");
    }
    static void TestAddBefore()
    {
        BinNode<int> a = Make(1, 3);
        BinNodeUtils.AddBefore(At(a, 1), 2);
        List("before the last", a, "1 2 3");
        BinNode<int> b = Make(1, 2, 4, 5);
        BinNodeUtils.AddBefore(At(b, 2), 3);
        List("in the middle", b, "1 2 3 4 5");
    }
    static void TestAddBeforeAny()
    {
        BinNode<int> a = Make(2, 3);
        BinNode<int> ra = BinNodeUtils.AddBeforeAny(a, At(a, 0), 1);
        List("before the first", ra, "1 2 3");
        EqInt("returned first", ra.GetValue(), 1);
        BinNode<int> b = Make(1, 3);
        Same("first stays the first", BinNodeUtils.AddBeforeAny(b, At(b, 1), 2), b);
        List("before the second", b, "1 2 3");
        BinNode<int> c = Make(7);
        List("before the only node", BinNodeUtils.AddBeforeAny(c, c, 6), "6 7");
    }
    static void TestInsertSorted()
    {
        List("empty list", BinNodeUtils.InsertSorted(null, 5), "5");
        List("smaller than all", BinNodeUtils.InsertSorted(Make(2, 4, 6), 1), "1 2 4 6");
        List("bigger than all", BinNodeUtils.InsertSorted(Make(2, 4, 6), 9), "2 4 6 9");
        List("in the middle", BinNodeUtils.InsertSorted(Make(2, 4, 6), 5), "2 4 5 6");
        List("between two nodes", BinNodeUtils.InsertSorted(Make(2, 4), 3), "2 3 4");
        List("one node, smaller", BinNodeUtils.InsertSorted(Make(5), 3), "3 5");
        List("one node, bigger", BinNodeUtils.InsertSorted(Make(5), 8), "5 8");
        List("equal values", BinNodeUtils.InsertSorted(Make(2, 4, 4, 6), 4), "2 4 4 4 6");
    }
    static void TestBuildSorted()
    {
        IsNull("empty array", BinNodeUtils.BuildSorted(new int[0]));
        List("one element", BinNodeUtils.BuildSorted(new int[] { 5 }), "5");
        List("unsorted array", BinNodeUtils.BuildSorted(new int[] { 5, 3, 8, 1, 9, 2 }), "1 2 3 5 8 9");
        List("with repeated values", BinNodeUtils.BuildSorted(new int[] { 3, 1, 3, 1 }), "1 1 3 3");
    }
#endif

    // =========================================================== PART D
#if PART_D
    static void TestRemoveNode()
    {
        BinNode<int> a = Make(1, 2, 3, 4);
        BinNode<int> ra = BinNodeUtils.RemoveNode(a, At(a, 0));
        List("remove the first", ra, "2 3 4");
        EqInt("new first", ra.GetValue(), 2);
        BinNode<int> b = Make(1, 2, 3, 4);
        Same("first stays the first", BinNodeUtils.RemoveNode(b, At(b, 3)), b);
        List("remove the last", b, "1 2 3");
        BinNode<int> c = Make(1, 2, 3, 4);
        BinNodeUtils.RemoveNode(c, At(c, 1));
        List("remove from the middle", c, "1 3 4");
        BinNode<int> d = Make(7);
        IsNull("remove the only node", BinNodeUtils.RemoveNode(d, d));
        BinNode<int> e = Make(1, 2);
        List("two nodes, remove the first", BinNodeUtils.RemoveNode(e, At(e, 0)), "2");
        BinNode<int> f = Make(1, 2);
        List("two nodes, remove the last", BinNodeUtils.RemoveNode(f, At(f, 1)), "1");
    }
    static void TestRemoveValue()
    {
        IsNull("empty list", BinNodeUtils.RemoveValue(null, 1));
        List("missing value", BinNodeUtils.RemoveValue(Make(1, 2, 3), 9), "1 2 3");
        List("the first", BinNodeUtils.RemoveValue(Make(1, 2, 3), 1), "2 3");
        List("the middle", BinNodeUtils.RemoveValue(Make(1, 2, 3), 2), "1 3");
        List("the last", BinNodeUtils.RemoveValue(Make(1, 2, 3), 3), "1 2");
        IsNull("the only node", BinNodeUtils.RemoveValue(Make(5), 5));
        List("only the first occurrence", BinNodeUtils.RemoveValue(Make(4, 2, 4, 2), 2), "4 4 2");
    }
    static void TestRemoveAll()
    {
        IsNull("empty list", BinNodeUtils.RemoveAll(null, 1));
        List("no occurrence", BinNodeUtils.RemoveAll(Make(1, 2, 3), 9), "1 2 3");
        List("one occurrence", BinNodeUtils.RemoveAll(Make(1, 2, 3), 2), "1 3");
        List("several, not adjacent", BinNodeUtils.RemoveAll(Make(2, 1, 2, 3, 2), 2), "1 3");
        List("adjacent", BinNodeUtils.RemoveAll(Make(1, 2, 2, 2, 3), 2), "1 3");
        List("at the start", BinNodeUtils.RemoveAll(Make(2, 2, 1), 2), "1");
        List("at the end", BinNodeUtils.RemoveAll(Make(1, 2, 2), 2), "1");
        IsNull("everything", BinNodeUtils.RemoveAll(Make(5, 5, 5), 5));
    }
#endif

    // =========================================================== PART E
#if PART_E
    static void TestBuildCircular()
    {
        IsNull("empty array", BinNodeUtils.BuildCircular(new int[0]));
        Circular("one element", BinNodeUtils.BuildCircular(new int[] { 5 }), "5");
        Circular("two elements", BinNodeUtils.BuildCircular(new int[] { 1, 2 }), "1 2");
        Circular("four elements", BinNodeUtils.BuildCircular(new int[] { 1, 2, 3, 4 }), "1 2 3 4");
    }
    static void TestPrintCircular()
    {
        Eq("null", Capture(delegate { BinNodeUtils.PrintCircular(null); }), "");
        BinNode<int> one = MakeCircular(5);
        Eq("one node", Capture(delegate { BinNodeUtils.PrintCircular(one); }), "5");
        BinNode<int> l = MakeCircular(1, 2, 3, 4);
        Eq("from the first", Capture(delegate { BinNodeUtils.PrintCircular(l); }), "1 2 3 4");
        Eq("from the third node (one lap only)", Capture(delegate { BinNodeUtils.PrintCircular(At(l, 2)); }), "3 4 1 2");
    }
    static void TestAddLastCircular()
    {
        Circular("empty list", BinNodeUtils.AddLastCircular(null, 5), "5");
        BinNode<int> one = MakeCircular(1);
        Same("first stays the first", BinNodeUtils.AddLastCircular(one, 2), one);
        Circular("one node", one, "1 2");
        BinNode<int> l = MakeCircular(1, 2, 3);
        Circular("three nodes", BinNodeUtils.AddLastCircular(l, 4), "1 2 3 4");
    }
    static void TestRemoveCircular()
    {
        BinNode<int> one = MakeCircular(7);
        IsNull("the only node", BinNodeUtils.RemoveCircular(one, one));
        BinNode<int> a = MakeCircular(1, 2, 3, 4);
        BinNode<int> ra = BinNodeUtils.RemoveCircular(a, At(a, 0));
        Circular("remove the first", ra, "2 3 4");
        EqInt("new first", ra.GetValue(), 2);
        BinNode<int> b = MakeCircular(1, 2, 3, 4);
        Same("first stays the first", BinNodeUtils.RemoveCircular(b, At(b, 3)), b);
        Circular("remove the last", b, "1 2 3");
        BinNode<int> c = MakeCircular(1, 2, 3, 4);
        BinNodeUtils.RemoveCircular(c, At(c, 1));
        Circular("remove from the middle", c, "1 3 4");
        BinNode<int> d = MakeCircular(1, 2);
        BinNodeUtils.RemoveCircular(d, At(d, 1));
        Circular("two nodes, remove the last", d, "1");
        BinNode<int> e = MakeCircular(1, 2);
        Circular("two nodes, remove the first", BinNodeUtils.RemoveCircular(e, At(e, 0)), "2");
    }
#endif

    // =========================================================== PART F
#if PART_F
    static void TestIsPalindrome()
    {
        EqBool("empty", BinNodeUtils.IsPalindrome(null), true);
        EqBool("one node", BinNodeUtils.IsPalindrome(Make(1)), true);
        EqBool("two equal", BinNodeUtils.IsPalindrome(Make(3, 3)), true);
        EqBool("two different", BinNodeUtils.IsPalindrome(Make(3, 4)), false);
        EqBool("odd palindrome", BinNodeUtils.IsPalindrome(Make(1, 2, 3, 2, 1)), true);
        EqBool("even palindrome", BinNodeUtils.IsPalindrome(Make(1, 2, 2, 1)), true);
        EqBool("odd, not palindrome", BinNodeUtils.IsPalindrome(Make(1, 2, 3, 4, 1)), false);
        EqBool("only the middle differs from a palindrome", BinNodeUtils.IsPalindrome(Make(1, 2, 5, 3, 2, 1)), false);
        BinNode<int> l = Make(1, 2, 1);
        BinNodeUtils.IsPalindrome(l);
        List("the list must not change", l, "1 2 1");
    }
    static void TestGetMiddle()
    {
        BinNode<int> one = Make(5);
        Same("one node", BinNodeUtils.GetMiddle(one), one);
        BinNode<int> three = Make(1, 2, 3);
        Same("three nodes", BinNodeUtils.GetMiddle(three), At(three, 1));
        BinNode<int> seven = Make(1, 2, 3, 4, 5, 6, 7);
        Same("seven nodes", BinNodeUtils.GetMiddle(seven), At(seven, 3));
        List("the list must not change", seven, "1 2 3 4 5 6 7");
    }
    static void TestReadReversed()
    {
        TextReader old = Console.In;
        try
        {
            Console.SetIn(new StringReader("999\n"));
            IsNull("only 999", BinNodeUtils.ReadReversed());
            Console.SetIn(new StringReader("5\n999\n"));
            List("one number", BinNodeUtils.ReadReversed(), "5");
            Console.SetIn(new StringReader("5\n3\n8\n999\n"));
            List("three numbers (999 is not part of the list)", BinNodeUtils.ReadReversed(), "8 3 5");
            Console.SetIn(new StringReader("1\n2\n3\n4\n5\n6\n999\n"));
            List("six numbers", BinNodeUtils.ReadReversed(), "6 5 4 3 2 1");
        }
        finally
        {
            Console.SetIn(old);
        }
    }
    static void TestInsertToCenter()
    {
        BinNode<int> a = Make(3, 6, 4, 88);
        Same("returns list1", BinNodeUtils.InsertToCenter(a, Make(10, 40, 30)), a);
        List("the example from the exercise", a, "3 6 10 40 30 4 88");
        BinNode<int> b = Make(1, 2);
        BinNodeUtils.InsertToCenter(b, Make(9));
        List("two nodes, one inserted", b, "1 9 2");
        BinNode<int> c = Make(1, 2, 3, 4, 5, 6);
        BinNodeUtils.InsertToCenter(c, Make(7, 8));
        List("six nodes", c, "1 2 3 7 8 4 5 6");
    }

    // digits as a list: "4793568" -> 4,7,9,3,5,6,8
    static BinNode<int> Digits(string s)
    {
        int[] d = new int[s.Length];
        for (int i = 0; i < s.Length; i++) d[i] = s[i] - '0';
        return Make(d);
    }
    static string DigitsStr(BinNode<int> first)
    {
        StringBuilder sb = new StringBuilder();
        int steps = 0;
        for (BinNode<int> p = first; p != null; p = p.GetRight())
        {
            if (++steps > 1000) return "LOOP";
            sb.Append(p.GetValue());
        }
        return sb.ToString();
    }
    static void CheckAdd(string what, string a, string b, string expected)
    {
        BinNode<int> l1 = Digits(a), l2 = Digits(b);
        BinNode<int> sum = BinNodeUtils.AddBig(l1, l2);
        if (sum == null) { problems.Add(what + ": got null"); return; }
        Eq(what, DigitsStr(sum), expected);
        Eq(what + " (backward, checks the Left links)", Bwd(sum), Rev(Fwd(sum)));
        if (sum.GetLeft() != null) problems.Add(what + ": the first node must have Left == null");
        Eq(what + " - list1 must not change", DigitsStr(l1), a);
        Eq(what + " - list2 must not change", DigitsStr(l2), b);
    }
    static void TestAddBig()
    {
        CheckAdd("the example from the exercise", "4793568", "21399", "4814967");
        CheckAdd("same length, no carry", "123", "456", "579");
        CheckAdd("carry creates a new first digit", "999", "1", "1000");
        CheckAdd("second list is longer", "5", "9995", "10000");
        CheckAdd("one digit each", "7", "8", "15");
        CheckAdd("zeros", "0", "0", "0");
        string nines = new string('9', 30);
        CheckAdd("very long numbers (do not use int or long!)", nines, "1", "1" + new string('0', 30));
    }
#endif
}
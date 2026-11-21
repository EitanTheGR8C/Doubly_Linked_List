# Doubly Linked List (BinNode<T>)

School project about two-way nodes in C#. A doubly linked list is a chain of
nodes where each node holds a value and two references: left (previous node)
and right (next node).

The project has 3 files:

- **BinNode.cs** - the node class.
- **BinNodeUtils.cs** - all the static methods that work on lists.
- **Tests.cs** - automatic tests. Enable a part with `#define PART_X` at the
  top, then run to see PASS/FAIL for each check.

## Part A - The node class (BinNode.cs)

`BinNode<T>` is a single node. `T` is the type of the stored value.

- `BinNode(T x)` - builds a node with a value and no neighbors.
- `BinNode(BinNode<T> left, T x, BinNode<T> right)` - builds a node with two neighbors.
- `GetValue()` / `SetValue(T x)` - read or change the value.
- `GetLeft()` / `GetRight()` - return the neighbor nodes.
- `SetLeft(...)` / `SetRight(...)` - connect a neighbor node.
- `ToString()` - returns the value as a string.

## Part B - Basic operations

- `BuildList(arr)` - builds a list from an array, returns the first node.
- `PrintForward(first)` - prints the list from first to last.
- `GetLast(first)` - returns the last node.
- `PrintBackward(last)` - prints the list from last to first.
- `Length(first)` - counts the nodes.
- `Sum(first)` - sums all the values.
- `IsExist(first, x)` - checks if x is in the list.
- `Find(first, x)` - returns the node holding x.
- `GetFirst(p)` - from any node, walks left to the first node.
- `PrintAll(p)` - prints the whole list starting from any node.
- `IsConsistent(first)` - checks that left and right links match each other.
- `IsSorted(first)` - checks if the values are in ascending order.

## Part C - Adding nodes

- `AddFirst(first, x)` - adds x at the start, returns the new first node.
- `AddLast(first, x)` - adds x at the end.
- `AddAfter(p, x)` - adds x right after node p.
- `AddBefore(p, x)` - adds x right before node p.
- `AddBeforeAny(first, p, x)` - adds x before p, even when p is the first node.
- `InsertSorted(first, x)` - inserts x into a sorted list in the right place.
- `BuildSorted(arr)` - builds a sorted list from an array.

## Part D - Removing nodes

- `RemoveNode(first, p)` - removes the node p.
- `RemoveValue(first, x)` - removes the first node holding x.
- `RemoveAll(first, x)` - removes every node holding x.

## Part E - Circular lists

In a circular list the last node points right to the first, and the first
points left to the last. There is no null at the ends.

- `BuildCircular(arr)` - builds a circular list from an array.
- `PrintCircular(p)` - prints the circular list once around.
- `AddLastCircular(first, x)` - adds x at the end (just before the first).
- `RemoveCircular(first, p)` - removes p and keeps the circle connected.

## Part F - Advanced questions

- `IsPalindrome(first)` - checks if the list reads the same both ways.
- `GetMiddle(first)` - returns the middle node (the question assumes an odd
  number of nodes).
- `ReadReversed()` - reads numbers from the user until 999 (999 not included)
  and returns them as a new list in reverse input order.
- `InsertToCenter(list1, list2)` - inserts list2 into the center of list1
  (list1 has an even length) without creating new nodes.
- `AddBig(list1, list2)` - adds two big numbers stored as digit lists and
  returns a new list with the result.

namespace Kata.Graphs.Tests
{
    [TestClass]
    public class LinkedListExtensionsTests
    {
        private List<int>[] _arrayOfTestLists = new List<int>[]
        {
            new List<int> { }, //0
            new List<int> { 1 }, //1
            new List<int> { 1, 2 }, //2
            new List<int> { 3, 4 }, //3
            new List<int> { 1, 3 }, //4
            new List<int> { 2, 4 }, //5
            new List<int> { 1, 3, 5}, //6
            new List<int> { 0, 2, 4, 6 }, //7
            new List<int> { 1, 4, 7 }, //8

            new List<int> { 1, 2, 3, 4 }, //9
            new List<int> { 1, 2, 3, 4, 5 }, //10
            new List<int> { 0, 1, 2, 3, 4, 5, 6 }, //11
            new List<int> { 1, 3, 4, 5, 7 }, //12
        };

        [TestMethod]
        [DataRow(0, 0, 0, DisplayName = "Two empty lists.")]
        [DataRow(0, 1, 1, DisplayName = "In place list is empty, added list is not.")]
        [DataRow(1, 0, 1, DisplayName = "In place list has entry, added list is empty.")]

        //Duplicates
        [DataRow(1, 2, 2, DisplayName = "One duplicate in smaller inner list.")]
        [DataRow(2, 1, 2, DisplayName = "One duplicate in smaller outer list.")]
        [DataRow(2, 2, 2, DisplayName = "Two duplicates.")]

        //No gap with overlaps
        [DataRow(2, 3, 9, DisplayName = "No gap left overlap of inner list")]
        [DataRow(3, 2, 9, DisplayName = "No gap right overlap of inner list")]

        //Single gap with overlaps
        [DataRow(4, 5, 9, DisplayName = "Single gap left overlap of inner list")]
        [DataRow(5, 4, 9, DisplayName = "Single gap right overlap of inner list")]

        //Multiple gaps with multiple overlaps
        [DataRow(5, 6, 10, DisplayName = "Two gaps in outer list")]
        [DataRow(6, 5, 10, DisplayName = "Two gaps in inner list")]

        //Two sided overlap with gaps.
        [DataRow(6, 7, 11, DisplayName = "Two sided overlap of inner list with gaps")]
        [DataRow(7, 6, 11, DisplayName = "Two sided overlap of outer list with gaps")]

        //Multiple inter overlaps
        [DataRow(6, 8, 12, DisplayName = "Inner list multiple value gap.")]
        [DataRow(8, 6, 12, DisplayName = "Outer list multiple value gap.")]
        public void MergedSortedLinkedListTests(int testList1Number, int testList2Number, int expectedListAfterMerge)
        {
            SortedSinglyLinkedList<int> linkedList1 = GetTestLinkedList(testList1Number);
            SortedSinglyLinkedList<int> linkedList2 = GetTestLinkedList(testList2Number);
            List<int> expectedMergedList = _arrayOfTestLists[expectedListAfterMerge];

            linkedList1.MergeSortedLinkedList(linkedList2);

            int entry = 0;
            SinglyLinkedListNode<int> listNode = linkedList1.Head;
            while (listNode != null)
            {
                Assert.AreEqual(expectedMergedList[entry], listNode.Value, "The lists aren't value equal");
                listNode = listNode.Next;
                entry++;
            }
            Assert.AreEqual(_arrayOfTestLists[expectedListAfterMerge].Count, entry, "The merged list is not of the expected length.");
        }

        private SortedSinglyLinkedList<int> GetTestLinkedList(int testNumber)
        {
            return GetLinkedListFromlist(_arrayOfTestLists[testNumber]);
        }

        private SortedSinglyLinkedList<int> GetLinkedListFromlist(List<int> list)
        {
            var linkedList = new SortedSinglyLinkedList<int>();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var node = new SinglyLinkedListNode<int>()
                {
                    Value = list[i],
                    Next = linkedList.Head,
                };
                linkedList.Head = node;
            }
            return linkedList;
        }
    }
}

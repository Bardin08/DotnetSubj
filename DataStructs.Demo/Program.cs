using DataStructs;

Console.WriteLine("Data structs library usage example!");

var dllst = new DoublyLinkedList<int>
{
    1,
    2,
    3,
    4,
    5
};

Console.WriteLine("Initial doubly linked list: " + string.Join(", ", dllst));
dllst.Remove(4);
Console.WriteLine("After remove {4} element: " + string.Join(", ", dllst));

Console.WriteLine("Add an element {4}. Collection before add: " + string.Join(", ", dllst));
dllst.Add(4);
Console.WriteLine("Collection after add: " + string.Join(", ", dllst));

Console.WriteLine("Clear the list. Lenght before: " + dllst.Count);
dllst.Clear();
Console.WriteLine("Collection size after clean: " + dllst.Count);

Console.WriteLine("Fill the collection with values: 1, 2, 3");
dllst.Add(1);
dllst.Add(2);
dllst.Add(3);

Console.WriteLine("Current collection: " + string.Join(", ", dllst));

Console.WriteLine("Collection contains {1}: " + dllst.Contains(1));
Console.WriteLine("Collection contains {2}: " + dllst.Contains(2));
Console.WriteLine("Collection contains {3}: " + dllst.Contains(3));
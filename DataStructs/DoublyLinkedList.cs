using System.Collections;

namespace DataStructs;

public class DoublyLinkedList<T> : ICollection<T> where T: IComparable<T>
{
    public event EventHandler<CollectionUpdateEventHandler<T>>? ItemAdd;
    public event EventHandler<CollectionUpdateEventHandler<T>>? ItemRemoved;
    public event EventHandler<CollectionUpdateEventHandler<T>>? CollectionCleared;

    private Node<T>? Head { get; set; }
    private Node<T>? Tail { get; set; }
    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public void Add(T item)
    {
        var temp = new Node<T>(item);

        // This is a first element, so head & tail is a same element
        if (Head == null)
        {
            Head = temp;
            Tail = temp;
        }

        Tail!.Next = temp;
        temp.Prev = Tail;
        Tail = temp;

        ++Count;

        ItemAdd?.Invoke(this, new CollectionUpdateEventHandler<T>()
        {
            Value = item,
            ActionType = CollectionActionType.ItemAdd
        });
    }
    
    public bool Contains(T? item)
    {
        var temp = Head;

        if (temp is null)
        {
            return false;
        }

        if (item is null && temp.Data is null)
        {
            return true;
        }

        while (temp?.Next != null)
        {
            if (temp.Data?.CompareTo(item) == 0)
            {
                return true;
            }

            temp = temp.Next;
        }

        return temp!.Data?.CompareTo(item) == 0;
    }

    /// <summary>
    /// Remove specified item from the collection if it exists.
    /// </summary>
    /// <param name="item">Item that should be removed from the collection</param>
    /// <returns>True when item was removed and False if such item is not exists at the collection.</returns>
    public bool Remove(T item)
    {
        if (!Contains(item))
        {
            return false;
        }

        var nodeToRemove = GetNode(item);
        if (nodeToRemove is null)
        {
            return false;
        }

        var prevNode = nodeToRemove.Prev;
        var nextNode = nodeToRemove.Next;

        if (prevNode != null)
        {
            prevNode.Next = nextNode;
        }

        if (nextNode != null)
        {
            nextNode.Prev = prevNode;
        }

        ItemRemoved?.Invoke(this, new CollectionUpdateEventHandler<T>()
        {
            Value = item,
            ActionType = CollectionActionType.ItemRemove
        });
        return true;
    }

    public void CopyTo(T?[] array, int arrayIndex)
    {
        if ((uint) arrayIndex <= array.Length)
        {
            throw new ArgumentException("Index outbound of a range");
        }

        var index = 0;
        while (array.Length > arrayIndex)
        {
            var node = GetNodeByIndex(index);
            if (node is null)
            {
                return;
            }

            array[arrayIndex] = node.Data ?? default;

            ++arrayIndex;
        }
    }

    private Node<T>? GetNodeByIndex(int index)
    {
        var nodesPassed = 0;

        if (Head is null)
        {
            return null;
        }

        var currNode = Head;
        while (currNode!.Next != null || index != nodesPassed)
        {
            currNode = currNode.Next;
            ++nodesPassed;
        }

        return currNode;
    }

    public void Clear()
    {
        Head = null;
        Tail = null;
        Count = 0;
        
        CollectionCleared?.Invoke(this, new CollectionUpdateEventHandler<T>()
        {
            ActionType = CollectionActionType.CollectionCleared
        });
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new DoublyLinkedListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private Node<T>? GetNode(T? item)
    {
        if (Head is null)
        {
            return null;
        }

        var temp = Head;

        while (temp!.Next != null)
        {
            if (temp.Data?.CompareTo(item) == 0)
            {
                return temp;
            }

            temp = temp.Next;
        }

        if (temp.Data == null && item == null)
        {
            return temp;
        }
        
        return temp.Data!.CompareTo(item) == 0 ? temp : null;
    }
    
    private struct DoublyLinkedListEnumerator : IEnumerator<T>
    {
        public T? Current { get; private set; }
        object IEnumerator.Current => Current!;

        private Node<T>? _currentNode;
        private readonly DoublyLinkedList<T> _list;


        public DoublyLinkedListEnumerator(DoublyLinkedList<T> list)
        {
            _list = list;

            _currentNode = list.Head;

            Current = list.Head != null
                ? list.Head.Data
                : default;
        }

        public bool MoveNext()
        {
            if (_currentNode == null) {
                return false;
            }

            Current = _currentNode.Data;   
            _currentNode = _currentNode.Next;  
            if (_currentNode == _list.Head) {
                _currentNode = null;
            }

            return true;
        }

        public void Reset()
        {
            _currentNode = _list.Head;
            Current = default;
        }

        public void Dispose()
        {
        }
    }
}

namespace DataStructs;

public class CollectionUpdateEventHandler<T> : EventArgs where T: IComparable<T>
{
    public CollectionActionType ActionType { get; set; }
    public T? Value { get; set; }
}

public enum CollectionActionType
{
    ItemAdd,
    ItemRemove,
    CollectionCleared
}
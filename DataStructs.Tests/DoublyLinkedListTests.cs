using System.Collections.Generic;
using Xunit;

namespace DataStructs.Tests;

public class DoublyLinkedListTests
{
    [Fact]
    public void AddElementsToDlsTest()
    {
        var dls = new DoublyLinkedList<int>();

        dls.Add(1);
        dls.Add(2);
        dls.Add(3);

        var dslAsString = string.Join("", dls);

        Assert.Equal("123", dslAsString);
    }

    [Fact]
    public void DlsEnumerateElementsTest()
    {
        var dls = new DoublyLinkedList<int>
        {
            1, 2, 3
        };

        var dslAsString = string.Join("", dls);

        Assert.Equal("123", dslAsString);
    }

    [Fact]
    public void DlsClearTest()
    {
        var dls = new DoublyLinkedList<int>
        {
            1, 2, 3
        };

        dls.Clear();

        Assert.Empty(dls);
    }

    [Fact]
    public void EnumeratorResetTest()
    {
        var dls = new DoublyLinkedList<int>()
        {
            1, 2, 3
        };

        var res = "";
        using var enumerator = dls.GetEnumerator();

        while (enumerator.MoveNext())
        {
            res += enumerator.Current;
        }

        enumerator.Reset();

        while (enumerator.MoveNext())
        {
            res += enumerator.Current;
        }

        Assert.Equal("123123", res);
    }

    [Fact]
    public void CollectionsReadonlyTest()
    {
        var dls = new DoublyLinkedList<int>
        {
            1, 2, 3
        };

        Assert.False(dls.IsReadOnly);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(100)]
    public void RemoveTest(int valToRemove)
    {
        var dls = new DoublyLinkedList<int>()
        {
            1, 2, 3, 4
        };

        dls.Remove(valToRemove);

        Assert.DoesNotContain(valToRemove, dls);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void DlsContainsElementTest(string[] values)
    {
        var dls = new DoublyLinkedList<string>();

        foreach (var value in values)
        {
            dls.Add(value);
        }

        Assert.Contains(values[0], dls);
    }

    public static IEnumerable<object[]> Values =>
        new List<object[]>
        {
            new object[] {new[] {"1", "2", "3"}},
            new object[] {new string[] {null!}}
        };
}
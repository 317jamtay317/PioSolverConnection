using System.Collections;
using System.Collections.ObjectModel;

namespace PioConnection.Dtos;

public class Flop : ICollection<Card>
{
    
    public void CopyTo(Array array, int index)
    {
        ((ICollection)_cards).CopyTo(array, index);
    }

    public bool IsSynchronized => ((ICollection)_cards).IsSynchronized;

    public object SyncRoot => ((ICollection)_cards).SyncRoot;

    public int Add(object? value) =>
        ((IList)_cards).Add(value);

    public bool Contains(object? value) =>
        ((IList)_cards).Contains(value);

    public int IndexOf(object? value) =>
        ((IList)_cards).IndexOf(value);

    public void Insert(int index, object? value)
    {
        ((IList)_cards).Insert(index, value);
    }

    public void Remove(object? value)
    {
        ((IList)_cards).Remove(value);
    }

    public bool IsFixedSize => ((IList)_cards).IsFixedSize;

    public void Add(Card item)
    {
        if (Count == 3)
        {
            throw new NotSupportedException("A flop can only have 3 cards");
        }
        _cards.Add(item);
    }

    public void AddRange(IEnumerable<Card> collection)
    {
        if (Count >= 3 || collection.Count() > 3)
        {
            throw new NotSupportedException("A flop can only have 3 cards");
        }
        _cards.AddRange(collection);
    }

    public ReadOnlyCollection<Card> AsReadOnly() =>
        _cards.AsReadOnly();

    public int BinarySearch(int index, int count, Card item, IComparer<Card>? comparer) =>
        _cards.BinarySearch(index, count, item, comparer);

    public int BinarySearch(Card item) =>
        _cards.BinarySearch(item);

    public int BinarySearch(Card item, IComparer<Card>? comparer) =>
        _cards.BinarySearch(item, comparer);

    public void Clear()
    {
        _cards.Clear();
    }

    public bool Contains(Card item) =>
        _cards.Contains(item);

    public List<TOutput> ConvertAll<TOutput>(Converter<Card, TOutput> converter) =>
        _cards.ConvertAll(converter);

    public void CopyTo(int index, Card[] array, int arrayIndex, int count)
    {
        _cards.CopyTo(index, array, arrayIndex, count);
    }

    public void CopyTo(Card[] array)
    {
        _cards.CopyTo(array);
    }

    public void CopyTo(Card[] array, int arrayIndex)
    {
        _cards.CopyTo(array, arrayIndex);
    }

    public int EnsureCapacity(int capacity) =>
        _cards.EnsureCapacity(capacity);

    public bool Exists(Predicate<Card> match) =>
        _cards.Exists(match);

    public Card Find(Predicate<Card> match) =>
        _cards.Find(match);

    public List<Card> FindAll(Predicate<Card> match) =>
        _cards.FindAll(match);

    public int FindIndex(int startIndex, int count, Predicate<Card> match) =>
        _cards.FindIndex(startIndex, count, match);

    public int FindIndex(int startIndex, Predicate<Card> match) =>
        _cards.FindIndex(startIndex, match);

    public int FindIndex(Predicate<Card> match) =>
        _cards.FindIndex(match);

    public Card FindLast(Predicate<Card> match) =>
        _cards.FindLast(match);

    public int FindLastIndex(int startIndex, int count, Predicate<Card> match) =>
        _cards.FindLastIndex(startIndex, count, match);

    public int FindLastIndex(int startIndex, Predicate<Card> match) =>
        _cards.FindLastIndex(startIndex, match);

    public int FindLastIndex(Predicate<Card> match) =>
        _cards.FindLastIndex(match);

    public void ForEach(Action<Card> action)
    {
        _cards.ForEach(action);
    }

    public List<Card> GetRange(int index, int count) =>
        _cards.GetRange(index, count);

    public int IndexOf(Card item) =>
        _cards.IndexOf(item);

    public int IndexOf(Card item, int index) =>
        _cards.IndexOf(item, index);

    public int IndexOf(Card item, int index, int count) =>
        _cards.IndexOf(item, index, count);

    public void Insert(int index, Card item)
    {
        _cards.Insert(index, item);
    }

    public void InsertRange(int index, IEnumerable<Card> collection)
    {
        _cards.InsertRange(index, collection);
    }

    public int LastIndexOf(Card item) =>
        _cards.LastIndexOf(item);

    public int LastIndexOf(Card item, int index) =>
        _cards.LastIndexOf(item, index);

    public int LastIndexOf(Card item, int index, int count) =>
        _cards.LastIndexOf(item, index, count);

    public bool Remove(Card item) =>
        _cards.Remove(item);

    public int RemoveAll(Predicate<Card> match) =>
        _cards.RemoveAll(match);

    public void RemoveAt(int index)
    {
        _cards.RemoveAt(index);
    }

    public void RemoveRange(int index, int count)
    {
        _cards.RemoveRange(index, count);
    }

    public void Reverse()
    {
        _cards.Reverse();
    }

    public void Reverse(int index, int count)
    {
        _cards.Reverse(index, count);
    }

    public List<Card> Slice(int start, int length) =>
        _cards.Slice(start, length);

    public void Sort()
    {
        _cards.Sort();
    }

    public void Sort(IComparer<Card>? comparer)
    {
        _cards.Sort(comparer);
    }

    public void Sort(Comparison<Card> comparison)
    {
        _cards.Sort(comparison);
    }

    public void Sort(int index, int count, IComparer<Card>? comparer)
    {
        _cards.Sort(index, count, comparer);
    }

    public Card[] ToArray() =>
        _cards.ToArray();

    public void TrimExcess()
    {
        _cards.TrimExcess();
    }

    public bool TrueForAll(Predicate<Card> match) =>
        _cards.TrueForAll(match);

    public int Capacity
    {
        get => _cards.Capacity;
        set => _cards.Capacity = value;
    }

    public int Count => _cards.Count;

    public Card this[int index]
    {
        get => _cards[index];
        set => _cards[index] = value;
    }

    public bool IsReadOnly => ((ICollection<Card>)_cards).IsReadOnly;

    public IEnumerator<Card> GetEnumerator() =>
        _cards.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public Card FistCard
    {
        get
        {
            if (_cards.Count != 3)
            {
                throw new NotSupportedException("A flop must have 3 cards");
            }

            return this[0];
        }
    }

    public Card SecondCard
    {
        get
        {
            if (_cards.Count != 3)
            {
                throw new NotSupportedException("A flop must have 3 cards");
            }

            return this[1];
        }
    }

    public Card ThirdCard
    {
        get
        {
            if (_cards.Count != 3)
            {
                throw new NotSupportedException("A flop must have 3 cards");
            }

            return this[2];
        }
    }
    
    private List<Card> _cards = new();
}
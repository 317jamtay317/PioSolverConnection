using System.Collections;
using System.Collections.ObjectModel;

namespace PioConnection.Dtos;

/// <summary>
/// Represents a collection of exactly 3 cards, known as a "flop" in poker.
/// This class ensures that a flop can only contain exactly 3 cards.
/// </summary>
public class Flop : ICollection<Card>
{
    /// <summary>
    /// Validates the flop is valid and has 3 unique cards
    /// </summary>
    /// <returns></returns>
    public bool IsValidFlop()
    {
        bool isValid = Count == 3;
        foreach (Card card in _cards)
        {
            var currentIndex = this.IndexOf(card);
            for (int i = 0; i < Count; i++)
            {
                if(i == currentIndex)continue;
                isValid = isValid && !card.Equals(this[i]);
            }
        }
        return isValid;
    }
    
    // Implements ICollection<Card> and provides card management methods specific to a flop in poker.
    /// <summary>
    /// Copies the elements of the Flop to an Array, starting at the specified index.
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination of the elements copied from Flop.</param>
    /// <param name="index">The zero-based index in array at which copying begins.</param>
    public void CopyTo(Array array, int index)
    {
        ((ICollection)_cards).CopyTo(array, index);
    }

    /// <summary>
    /// Gets a value indicating whether access to the Flop is synchronized (thread safe).
    /// </summary>
    public bool IsSynchronized => ((ICollection)_cards).IsSynchronized;

    /// <summary>
    /// Gets an object that can be used to synchronize access to the Flop.
    /// </summary>
    public object SyncRoot => ((ICollection)_cards).SyncRoot;

    /// <summary>
    /// Adds an object to the Flop.
    /// This is an explicit interface implementation for non-generic collections.
    /// </summary>
    public int Add(object? value) => ((IList)_cards).Add(value);

    /// <summary>
    /// Determines whether the Flop contains a specific value.
    /// This is an explicit interface implementation for non-generic collections.
    /// </summary>
    public bool Contains(object? value) => ((IList)_cards).Contains(value);

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence.
    /// </summary>
    public int IndexOf(object? value) => ((IList)_cards).IndexOf(value);

    /// <summary>
    /// Inserts an object into the Flop at the specified index.
    /// </summary>
    public void Insert(int index, object? value) => ((IList)_cards).Insert(index, value);

    /// <summary>
    /// Removes the first occurrence of a specific object from the Flop.
    /// </summary>
    public void Remove(object? value) => ((IList)_cards).Remove(value);

    /// <summary>
    /// Gets a value indicating whether the Flop has a fixed size.
    /// </summary>
    public bool IsFixedSize => ((IList)_cards).IsFixedSize;

    /// <summary>
    /// Adds a card to the flop. A flop can only contain 3 cards; exceeding this limit throws an exception.
    /// </summary>
    /// <param name="item">The card to add to the flop.</param>
    /// <exception cref="NotSupportedException">Thrown when attempting to add more than 3 cards.</exception>
    public void Add(Card item)
    {
        if (Count == 3)
        {
            throw new NotSupportedException("A flop can only have 3 cards");
        }
        _cards.Add(item);
    }

    /// <summary>
    /// Adds a range of cards to the flop. If the collection contains more than 3 cards, an exception is thrown.
    /// </summary>
    /// <param name="collection">The collection of cards to add.</param>
    /// <exception cref="NotSupportedException">Thrown when attempting to add more than 3 cards in total.</exception>
    public void AddRange(IEnumerable<Card> collection)
    {
        if (Count >= 3 || collection.Count() > 3)
        {
            throw new NotSupportedException("A flop can only have 3 cards");
        }
        _cards.AddRange(collection);
    }

    /// <summary>
    /// Returns a read-only collection of cards in the flop.
    /// </summary>
    public ReadOnlyCollection<Card> AsReadOnly() => _cards.AsReadOnly();

    // Additional utility methods related to searching, sorting, and manipulating cards in the flop.
    // These are standard list operations delegated to the internal _cards list.

    /// <summary>
    /// Clears all cards from the flop.
    /// </summary>
    public void Clear() => _cards.Clear();

    /// <summary>
    /// Determines whether the flop contains a specific card.
    /// </summary>
    public bool Contains(Card item) => _cards.Contains(item);

    /// <summary>
    /// Copies and array into a specific location of the flop
    /// </summary>
    /// <param name="array">the items you'd like to copy</param>
    /// <param name="arrayIndex">the index that you'd like to copy to.</param>
    public void CopyTo(Card[] array, int arrayIndex)
    {
        _cards.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes a card from the Flop
    /// </summary>
    /// <param name="item">The card that you'd like to remove</param>
    /// <returns>true if successful, otherwise, false</returns>
    public bool Remove(Card item) =>
        _cards.Remove(item);

    /// <summary>
    /// Copies the elements of the flop to a specified array.
    /// </summary>
    public void CopyTo(Card[] array) => _cards.CopyTo(array);

    /// <summary>
    /// Gets the number of cards currently in the flop.
    /// </summary>
    public int Count => _cards.Count;

    /// <summary>
    /// Gets a value indicating whether the Flop is read-only.
    /// </summary>
    public bool IsReadOnly => ((ICollection<Card>)_cards).IsReadOnly;

    /// <summary>
    /// Returns an enumerator that iterates through the flop.
    /// </summary>
    public IEnumerator<Card> GetEnumerator() => _cards.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Gets the first card in the flop. Throws an exception if there are not exactly 3 cards.
    /// </summary>
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

    /// <summary>
    /// Gets the second card in the flop. Throws an exception if there are not exactly 3 cards.
    /// </summary>
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

    /// <summary>
    /// Gets the third card in the flop. Throws an exception if there are not exactly 3 cards.
    /// </summary>
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

    /// <summary>
    /// Gets the card from that index
    /// </summary>
    /// <param name="index">the 0 based index location of the card</param>
    public Card this[int index] => _cards[index];

    /// <summary>
    /// Internal storage for cards in the flop.
    /// </summary>
    private List<Card> _cards = new();
}

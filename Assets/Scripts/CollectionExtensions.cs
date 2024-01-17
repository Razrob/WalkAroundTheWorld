using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    public static void Do<TElement>(this IEnumerable<TElement> collection, Action<TElement> action)
    {
        if (collection is null)
            throw new ArgumentNullException("Input collection cannot be null");

        if (action is null)
            throw new NullReferenceException("Action cannot be null");

        foreach (TElement element in collection)
            action(element);
    }

    public static void DoWithIndex<TElement>(this IEnumerable<TElement> collection, Action<TElement, int> action)
    {
        if (collection is null)
            throw new ArgumentNullException("Input collection cannot be null");

        if (action is null)
            throw new NullReferenceException("Action cannot be null");

        int index = 0;

        foreach (TElement e in collection)
        {
            action(e, index);
            index++;
        }
    }

    public static TElement Random<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> filter = null)
    {
        if (collection is null)
            throw new ArgumentNullException("Input collection cannot be null");

        if (filter != null && collection.All(p => !filter(p)))
            return default;

        int length = collection.Count();
        int value = UnityEngine.Random.Range(0, length);
        int index = 0;

        int iterationsCount = 0;

        while (true)
        {
            if (iterationsCount > length * 100)
                throw new StackOverflowException();

            iterationsCount++;

            if (filter != null && !filter(collection.ElementAt(index)))
                goto End;

            if (value == 0)
                return collection.ElementAt(index);
            else
                value--;

        End:

            if (index < length - 1)
                index++;
            else
                index = 0;
        }
    }

    public static TElement Find<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> comparator)
    {
        if (collection is null)
            throw new ArgumentNullException("Input collection cannot be null");

        if (comparator is null)
            throw new NullReferenceException("Comparator cannot be null");

        foreach (TElement element in collection)
            if (comparator(element))
                return element;

        return default;
    }

    public static bool Contains<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> comparator)
    {
        if (collection is null)
            throw new ArgumentNullException("Input collection cannot be null");

        if (comparator is null)
            throw new NullReferenceException("Comparator cannot be null");

        foreach (TElement element in collection)
            if (comparator(element))
                return true;

        return false;
    }

    public static int IndexOf<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> comparison)
    {
        int index = 0;

        foreach (TElement e in collection)
        {
            if (comparison(e))
                return index;

            index++;
        }

        return -1;
    }

    public static TElement FindMin<TElement>(this IEnumerable<TElement> collection,  
        Func<TElement, TElement, bool> nextElementLess)
    {
        TElement last = collection.FirstOrDefault();

        foreach (TElement e in collection)
        {
            if (nextElementLess(last, e))
                last = e;
        }

        return last;
    }

    public static TElement FindMin<TElement>(this IEnumerable<TElement> collection,
        Func<TElement, float> func)
    {
        TElement last = collection.First();
        float lastValue = func(last);

        foreach (TElement e in collection)
        {
            float value = func(e);
            if (lastValue > value)
            {
                last = e;
                lastValue = value;
            }
        }

        return last;
    }

    public static TElement FindMax<TElement>(this IEnumerable<TElement> collection,
        Func<TElement, float> func)
    {
        TElement last = collection.First();
        float lastValue = func(last);

        foreach (TElement e in collection)
        {
            float value = func(e);
            if (lastValue < value)
            {
                last = e;
                lastValue = value;
            }
        }

        return last;
    }
}

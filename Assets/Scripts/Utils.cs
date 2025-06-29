using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        T[] elements = source.ToArray();
        for (int i = elements.Length - 1; i >= 0; i--)
        {
            // Swap element "i" with a random earlier element it (or itself)
            // ... except we don't really need to swap it fully, as we can
            // return it immediately, and afterwards it's irrelevant.
            int swapIndex = Random.Range(0, i + 1);
            yield return elements[swapIndex];
            elements[swapIndex] = elements[i];
        }
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.ElementAt(Random.Range(0, source.Count()));
    }
}

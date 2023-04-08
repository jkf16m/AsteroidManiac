

using System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static Godot.Collections.Array<T> ToGodotArray<T>(this IEnumerable<T> enumerable)
    {
        var godotArray = new Godot.Collections.Array<T>();
        foreach (var item in enumerable)
        {
            godotArray.Add(item);
        }
        return godotArray;
    }
}
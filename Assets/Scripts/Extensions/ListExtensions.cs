using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ListExtensions
{
    public static class ListExtensionClass
    {
        public static T RandomChoose<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static bool TryRemove<T>(this List<T> list, T element)
        {
            if (list.Contains(element))
            {
                list.Remove(element);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
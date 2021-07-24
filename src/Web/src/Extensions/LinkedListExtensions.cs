using System.Collections.Generic;

namespace Application.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="LinkedList{T}" />.
    /// </summary>
    public static class LinkedListExtensions
    {
        /// <summary>
        /// Makes classic linked list circular (forward direction).
        /// </summary>
        /// <typeparam name="T">type of items in linked list.</typeparam>
        /// <param name="node">node from which we want find the next one.</param>
        /// <returns>next node if there is one; otherwise null.</returns>
        public static LinkedListNode<T>? Next<T>(this LinkedListNode<T> node)
        {
            if (node != null && node.List != null)
            {
                return node.Next ?? node.List.First;
            }

            return null;
        }

        /// <summary>
        /// Iteratively returns sequence of linked list nodes from the given list.
        /// </summary>
        /// <typeparam name="T">type of item stored in the linked list node.</typeparam>
        /// <param name="list">collection of linked list nodes we want to return.</param>
        public static IEnumerable<LinkedListNode<T>> GetNodes<T>(this LinkedList<T> list)
        {
            for (var node = list.First; node != null; node = node.Next)
            {
                yield return node;
            }
        }
    }
}
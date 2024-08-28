using System.Collections;
using System.Collections.Generic;
// Ubicación: Scripts/LinkedList/LinkedList.cs

using UnityEngine;

public class LinkedList
{
    public Node Head { get; private set; }
    public Node Tail { get; private set; }
    public int Length { get; private set; }

    public LinkedList(Vector2Int initialPosition)
    {
        Node initialNode = new Node(initialPosition);
        Head = initialNode;
        Tail = initialNode;
        Length = 1;
    }

    public void AddNode(Vector2Int newPosition)
    {
        Node newNode = new Node(newPosition);
        Tail.Next = newNode;
        Tail = newNode;
        Length++;
    }

    public void RemoveTail()
    {
        if (Head == Tail)
        {
            // Solo hay un nodo, no se puede eliminar
            return;
        }

        Node current = Head;
        while (current.Next != Tail)
        {
            current = current.Next;
        }

        current.Next = null;
        Tail = current;
        Length--;
    }

    public Vector2Int[] GetPositions()
    {
        Vector2Int[] positions = new Vector2Int[Length];
        Node current = Head;
        int index = 0;

        while (current != null)
        {
            positions[index] = current.Position;
            current = current.Next;
            index++;
        }

        return positions;
    }
}

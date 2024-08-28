using System.Collections;
using System.Collections.Generic;
// Ubicaci�n: Scripts/LinkedList/Node.cs

using UnityEngine;

public class Node
{
    public Vector2Int Position { get; set; }
    public Node Next { get; set; }

    public Node(Vector2Int position)
    {
        Position = position;
        Next = null;
    }
}

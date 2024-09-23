using UnityEngine;

public class Nodo
{
    public Vector2Int position; // Coordenadas del nodo en el grid
    public Nodo norte, sur, este, oeste; // Referencias a nodos vecinos

    public Nodo(Vector2Int position)
    {
        this.position = position;
    }
}

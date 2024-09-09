using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    public int width;  // Ancho del grid (número de columnas)
    public int height; // Altura del grid (número de filas)
    private Node[,] grid;  // Matriz que representa el grid

    // Clase interna que representa cada nodo (celda) del grid
    public class Node
    {
        public int x, y;  // Posición del nodo en el grid
        public Node up, down, left, right;  // Referencias a nodos adyacentes

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    void Start()
    {
        GenerateGrid();
    }

    // Método para generar el grid
    void GenerateGrid()
    {
        grid = new Node[width, height];

        // Crear nodos
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Node(x, y);
            }
        }

        // Conectar nodos adyacentes
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x > 0) grid[x, y].left = grid[x - 1, y];
                if (x < width - 1) grid[x, y].right = grid[x + 1, y];
                if (y > 0) grid[x, y].down = grid[x, y - 1];
                if (y < height - 1) grid[x, y].up = grid[x, y + 1];
            }
        }

        Debug.Log("Grid creado con éxito.");
    }

    // Método para obtener un nodo específico del grid
    public Node GetNode(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return grid[x, y];
        }
        return null;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class LinkedList
{
    public Nodo[,] grid;
    private int width;
    private int height;

    public LinkedList(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new Nodo[width, height];
        CreateGrid();
    }

    // Crear la lista enlazada que representa el grid
    private void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Nodo(new Vector2Int(x, y));

                if (x > 0) grid[x, y].oeste = grid[x - 1, y];
                if (x < width - 1) grid[x, y].este = grid[x + 1, y];
                if (y > 0) grid[x, y].sur = grid[x, y - 1];
                if (y < height - 1) grid[x, y].norte = grid[x, y + 1];
            }
        }
    }

    // Obtener nodo en una posición específica
    public Nodo GetNodo(Vector2Int position)
    {
        if (position.x >= 0 && position.x < width && position.y >= 0 && position.y < height)
        {
            return grid[position.x, position.y];
        }
        return null;
    }
}

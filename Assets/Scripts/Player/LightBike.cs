using System.Collections;
using System.Collections.Generic;
// Ubicación: Scripts/Player/LightBike.cs

using UnityEngine;

public class LightBike : MonoBehaviour
{
    public LinkedList trail;   // Lista enlazada para la estela
    public int maxTrailLength = 3;  // Longitud máxima de la estela

    private Vector2Int direction;  // Dirección actual de la moto
    private float moveTimer = 0f;
    private float moveInterval = 0.1f;  // Intervalo de tiempo entre movimientos

    void Start()
    {
        // Inicializar la estela con la posición inicial de la moto
        Vector2Int initialPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        trail = new LinkedList(initialPosition);

        // Dirección inicial (por ejemplo, hacia la derecha)
        direction = Vector2Int.right;
    }

    void Update()
    {
        HandleInput();  // Controlar la entrada del jugador

        // Controlar el movimiento de la moto en intervalos
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            Move();
            moveTimer = 0f;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2Int.right;
        }
    }

    void Move()
    {
        // Nueva posición en la dirección actual
        Vector2Int newPosition = trail.Tail.Position + direction;

        // Mover la moto a la nueva posición
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        // Añadir la nueva posición a la estela
        trail.AddNode(newPosition);

        // Mantener la estela en la longitud máxima
        if (trail.Length > maxTrailLength)
        {
            trail.RemoveTail();
        }

        // Aquí puedes actualizar la representación visual de la estela si es necesario
    }
}

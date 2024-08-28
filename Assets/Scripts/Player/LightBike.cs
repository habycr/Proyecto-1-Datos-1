using System.Collections;
using System.Collections.Generic;
// Ubicaci�n: Scripts/Player/LightBike.cs

using UnityEngine;

public class LightBike : MonoBehaviour
{
    public LinkedList trail;   // Lista enlazada para la estela
    public int maxTrailLength = 3;  // Longitud m�xima de la estela

    private Vector2Int direction;  // Direcci�n actual de la moto
    private float moveTimer = 0f;
    private float moveInterval = 0.1f;  // Intervalo de tiempo entre movimientos

    void Start()
    {
        // Inicializar la estela con la posici�n inicial de la moto
        Vector2Int initialPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        trail = new LinkedList(initialPosition);

        // Direcci�n inicial (por ejemplo, hacia la derecha)
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
        // Nueva posici�n en la direcci�n actual
        Vector2Int newPosition = trail.Tail.Position + direction;

        // Mover la moto a la nueva posici�n
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        // A�adir la nueva posici�n a la estela
        trail.AddNode(newPosition);

        // Mantener la estela en la longitud m�xima
        if (trail.Length > maxTrailLength)
        {
            trail.RemoveTail();
        }

        // Aqu� puedes actualizar la representaci�n visual de la estela si es necesario
    }
}

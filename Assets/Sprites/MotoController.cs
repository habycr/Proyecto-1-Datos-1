using UnityEngine;

public class MotoController : MonoBehaviour
{
    public float velocidad = 5f;  // Velocidad de movimiento de la moto
    private Vector2 direccion = Vector2.right;  // Dirección inicial de la moto

    void Update()
    {
        // Capturar el input del jugador para cambiar la dirección
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direccion = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direccion = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direccion = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direccion = Vector2.right;
        }

        // Mover la moto en la dirección actual
        Mover();
    }

    void Mover()
    {
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        // Aquí podrías agregar lógica para asegurarte de que la moto se mantenga dentro del grid
    }
}

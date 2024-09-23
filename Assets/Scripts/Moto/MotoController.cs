using UnityEngine;
using System.Collections.Generic;

public class MotoController : MonoBehaviour
{
    public GridManager gridManager;
    public Nodo currentNode;
    public EstelaController estelaController; // Referencia a EstelaController
    public float moveInterval = 0.5f; // Intervalo de movimiento (en segundos)
    private float nextMoveTime;
    private Vector2Int direction;
    public float speed = 1f; // Velocidad de la moto

    public float combustible = 100f; // Cantidad de combustible (0 a 100)
    private int distanciaRecorrida = 0; // Contador de celdas recorridas
    private const int consumoPorDistancia = 5; // Cada cuántas celdas se consume 1 de combustible

    private Queue<string> itemQueue = new Queue<string>(); // Cola de ítems recogidos
    private float nextItemApplyTime; // Tiempo en que se puede aplicar el siguiente ítem
    public float itemApplyDelay = 1f; // Delay entre la aplicación de ítems

    private Stack<Power> powerStack = new Stack<Power>(); // Pila de poderes
    private bool isHyperSpeedActive = false; // Estado del poder de hipervelocidad
    private float originalSpeed; // Velocidad original de la moto
    private float powerEndTime; // Tiempo en que el poder termina


    private void Start()
    {
        currentNode = gridManager.GetStartNode();
        direction = Vector2Int.up;
        transform.position = new Vector3(currentNode.position.x, currentNode.position.y, 0);
        nextMoveTime = Time.time;
    }

    private void Update()
    {
        HandleInput();
        MoveMoto();
        ApplyItem();
        ApplyPower();
    }
    private void ApplyPower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && powerStack.Count > 0)
        {
            Power nextPower = powerStack.Pop(); // Aplicar el poder del tope
            if (nextPower.name == "Hipervelocidad")
            {
                originalSpeed = speed; // Guardar la velocidad original
                speed += nextPower.speedIncrease; // Aumentar la velocidad
                isHyperSpeedActive = true; // Activar hipervelocidad
                powerEndTime = Time.time + nextPower.duration; // Establecer el tiempo de finalización
                Debug.Log($"¡Hipervelocidad activa! Velocidad aumentada a {speed} por {nextPower.duration} segundos.");
            }
        }

        // Resetear velocidad si el poder ha terminado
        if (isHyperSpeedActive && Time.time >= powerEndTime)
        {
            speed = originalSpeed;
            isHyperSpeedActive = false;
            Debug.Log("Hipervelocidad ha terminado.");
        }
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) direction = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) direction = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) direction = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) direction = Vector2Int.right;
        if (Input.GetKeyDown(KeyCode.R) && powerStack.Count > 1)
        {
            Power topPower = powerStack.Pop(); // Mover el tope de la pila
            powerStack.Push(topPower);
            Debug.Log($"Poder cambiado de lugar: {topPower.name}");
        }
    }

    private void MoveMoto()
    {
        if (Time.time >= nextMoveTime)
        {
            Vector2Int newPosition = currentNode.position + direction;
            Nodo newNode = gridManager.linkedList.GetNodo(newPosition);

            if (newNode != null)
            {
                Vector2Int posicionAnterior = currentNode.position;

                if (gridManager.IsPowerItem(newPosition))
                {
                    powerStack.Push(new Power("Hipervelocidad", Random.Range(5, 11), Random.Range(5, 16)));
                    Debug.Log("¡Poder de Hipervelocidad recogido!");

                    // Eliminar ítem de poder de la lista
                    gridManager.powerItemPositions.Remove(newPosition);
                    GameObject powerItemToDestroy = GameObject.Find("PowerHipervelocidad(Clone)");
                    if (powerItemToDestroy != null)
                    {
                        Destroy(powerItemToDestroy);
                    }
                }


                // Verificar si la nueva posición es una bomba
                if (gridManager.IsBomb(newPosition))
                {
                    GameOver(); // Terminar el juego si toca una bomba
                    return; // Detener el movimiento
                }

                // Verificar si la nueva posición es un ítem de crecimiento
                if (gridManager.IsGrowthItem(newPosition))
                {
                    // Aquí solo recogemos el ítem y lo añadimos a la cola
                    itemQueue.Enqueue("Growth");

                    // Eliminar ítem de la lista
                    gridManager.growthItemPositions.Remove(newPosition);
                    GameObject itemToDestroy = GameObject.Find($"ItemCrecimiento(Clone)");
                    if (itemToDestroy != null)
                    {
                        Destroy(itemToDestroy); // Eliminar el objeto visualmente
                    }
                }

                // Verificar si la nueva posición es un ítem de combustible
                if (gridManager.IsFuelItem(newPosition))
                {
                    Debug.Log("¡Recogiste un ítem de combustible!");
                    itemQueue.Enqueue("Fuel");

                    // Eliminar ítem de la lista de posiciones
                    gridManager.fuelItemPositions.Remove(newPosition);
                    GameObject fuelItemToDestroy = GameObject.Find("ItemCombustible(Clone)");
                    if (fuelItemToDestroy != null)
                    {
                        Destroy(fuelItemToDestroy);
                    }
                }

                // Mover la moto
                currentNode = newNode;
                transform.position = new Vector3(currentNode.position.x, currentNode.position.y, 0);

                // Actualizar la estela con la posición anterior de la moto
                estelaController.UpdateEstela(posicionAnterior); // Pasar la posición anterior

                // Aumentar el contador de distancia
                distanciaRecorrida++;

                // Consumir combustible
                if (distanciaRecorrida >= consumoPorDistancia)
                {
                    combustible--;
                    distanciaRecorrida = 0; // Reiniciar el contador
                }

                // Comprobar si el combustible ha llegado a 0
                if (combustible == 0)
                {
                    GameOver(); // Llamar a la función de fin de juego
                }
            }

            // Actualizar el tiempo para el próximo movimiento
            nextMoveTime = Time.time + moveInterval / speed;
        }
    }


    private void ApplyItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= nextItemApplyTime && itemQueue.Count > 0)
        {
            string nextItem = itemQueue.Dequeue();

            if (nextItem == "Fuel")
            {
                if (combustible < 100)
                {
                    int fuelAmount = Random.Range(1, 101); // Aumentar entre 1 y 100
                    combustible = Mathf.Min(combustible + fuelAmount, 100); // Aumentar sin exceder 100
                    Debug.Log($"Combustible aumentado a {combustible}.");
                }
                else
                {
                    // Si el combustible está lleno, reinsertar el ítem en la cola
                    itemQueue.Enqueue("Fuel");
                    Debug.Log("Combustible lleno, ítem de combustible no aplicado.");
                }
            }
            else if (nextItem == "Growth")
            {
                int growthAmount = Random.Range(1, 11); // Aumentar de 1 a 10
                estelaController.longitudEstela += growthAmount; // Aumentar longitud de la estela
                Debug.Log($"Estela crecida en {growthAmount}.");
            }

            // Establecer el tiempo para aplicar el siguiente ítem
            nextItemApplyTime = Time.time + itemApplyDelay;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Implementar lógica de fin de juego
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public Vector2Int currentPosition;  // Posici�n actual del bot en el grid
    public float moveDelay = 1f;        // Intervalo de tiempo entre cada movimiento
    private float moveTimer;
    private GridManager gridManager;
    private MotoController motoController;

    public List<Item> collectedItems = new List<Item>();  // Lista de �tems que recoge el bot
    public Stack<Power> collectedPowers = new Stack<Power>();  // Pila de poderes recogidos
    public int maxItems = 20;
    public int maxPowers = 20;

    void Start()
    {
        moveTimer = moveDelay;
        gridManager = FindObjectOfType<GridManager>();  // Referencia al GridManager
        motoController = FindObjectOfType<MotoController>();  // Referencia al MotoController (para manejar GameOver)
        // Inicializar posici�n del bot si es necesario
        currentPosition = gridManager.GetInitialBotPosition();
    }

    void Update()
    {
        // Movimiento controlado por temporizador
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            MoveRandomly();
            moveTimer = moveDelay;
        }
    }

    void MoveRandomly()
    {
        // Generar un movimiento aleatorio
        int randomDirection = Random.Range(0, 4);
        Vector2Int newDirection = Vector2Int.zero;

        switch (randomDirection)
        {
            case 0: newDirection = Vector2Int.up; break;     // Arriba
            case 1: newDirection = Vector2Int.down; break;   // Abajo
            case 2: newDirection = Vector2Int.left; break;   // Izquierda
            case 3: newDirection = Vector2Int.right; break;  // Derecha
        }

        Vector2Int newPosition = currentPosition + newDirection;

        // Gestionar el teletransporte a trav�s de los bordes del grid
        newPosition = gridManager.WrapAround(newPosition);

        // Verificar colisiones con estelas, bombas, jugadores y otros bots
        CheckCollision(newPosition);

        // Actualizar posici�n si no est� ocupada
        if (!gridManager.IsPositionOccupied(newPosition))
        {
            currentPosition = newPosition;
            gridManager.UpdateBotPosition(this, currentPosition);
        }
    }

    void CheckCollision(Vector2Int newPosition)
    {
        // Colisi�n con una estela (propia o de otro bot o jugador)
        if (gridManager.IsPositionOccupiedByTail(newPosition))
        {
            DestroyBot();
        }

        // Colisi�n con una bomba
        if (gridManager.IsPositionOccupiedByBomb(newPosition))
        {
            DestroyBot();
        }

        // Colisi�n con un jugador
        if (gridManager.IsPositionOccupiedByPlayer(newPosition))
        {
            DestroyBot();
            motoController.GameOver();  // El jugador pierde (Game Over)
        }

        // Colisi�n con otro bot
        if (gridManager.IsPositionOccupiedByBot(newPosition))
        {
            BotController otherBot = gridManager.GetBotAtPosition(newPosition);
            gridManager.ScatterItemsAndPowers(otherBot);
            Destroy(otherBot.gameObject);  // Destruye el otro bot
            DestroyBot();
        }
    }

    void DestroyBot()
    {
        // Esparcir �tems y poderes del bot antes de destruirlo
        gridManager.ScatterItemsAndPowers(this);
        Destroy(gameObject);  // Elimina el bot del juego
    }

    public void AddItem(Item item)
    {
        if (collectedItems.Count < maxItems)
        {
            collectedItems.Add(item);
        }
    }

    public void AddPower(Power power)
    {
        if (collectedPowers.Count < maxPowers)
        {
            collectedPowers.Push(power);
        }
    }

    void UseRandomItemOrPower()
    {
        // Probabilidad aleatoria de usar un �tem o poder en cada turno
        if (collectedItems.Count > 0 && Random.Range(0, 100) < 10) // 10% probabilidad de usar �tem
        {
            UseItem();
        }
        else if (collectedPowers.Count > 0 && Random.Range(0, 100) < 10) // 10% probabilidad de usar poder
        {
            UsePower();
        }
    }

    void UseItem()
    {
        if (collectedItems.Count > 0)
        {
            Item itemToUse = collectedItems[0];
            collectedItems.RemoveAt(0);  // Elimina el �tem usado
            itemToUse.Activate();  // Activar el efecto del �tem
        }
    }

    void UsePower()
    {
        if (collectedPowers.Count > 0)
        {
            Power powerToUse = collectedPowers.Pop();  // Usa el poder de la pila
            powerToUse.Activate();  // Activar el efecto del poder
        }
    }
}

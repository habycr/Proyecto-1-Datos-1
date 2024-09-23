using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 100;
    public int gridHeight = 100;
    public LinkedList linkedList; // El grid representado como una lista enlazada
    public List<Vector2Int> bombPositions; // Lista para las posiciones de las bombas
    public List<Vector2Int> growthItemPositions; // Lista para las posiciones de los �tems de crecimiento
    public int numberOfBombs = 5; // N�mero de bombas a generar
    public GameObject bombPrefab; // Asigna el prefab de bomba en el inspector
    public GameObject growthItemPrefab; // Asigna el prefab del �tem de crecimiento en el inspector
    private float nextGrowthItemSpawnTime; // Temporizador para �tems de crecimiento
    private int maxGrowthItems = 1;

    public List<Vector2Int> fuelItemPositions; // Lista para las posiciones de los �tems de combustible
    public GameObject fuelItemPrefab; // Prefab del �tem de combustible
    private float nextFuelItemSpawnTime; // Temporizador para �tems de combustible
    private int maxFuelItems = 1; // N�mero m�ximo de �tems de combustible en el grid
    public List<Vector2Int> powerItemPositions; // Lista para las posiciones de los �tems de poderes
    public GameObject hyperSpeedPowerPrefab; // Prefab del �tem de hipervelocidad
    private float nextPowerItemSpawnTime; // Temporizador para �tems de poder
    private int maxPowerItems = 1; // N�mero m�ximo de �tems de poder en el grid
    private void Awake()
    {
        linkedList = new LinkedList(gridWidth, gridHeight);
        bombPositions = new List<Vector2Int>();
        growthItemPositions = new List<Vector2Int>();
        fuelItemPositions = new List<Vector2Int>();
        GenerateBombs(); // Generar bombas aleatoriamente
        powerItemPositions = new List<Vector2Int>();
        nextGrowthItemSpawnTime = Time.time + 20f; // Iniciar temporizador para �tems de crecimiento
        nextFuelItemSpawnTime = Time.time + 15f; // Iniciar temporizador para �tems de combustible
        nextPowerItemSpawnTime = Time.time + 15f; // Iniciar temporizador para �tems de poder
    }

    private void Update()
    {
        SpawnGrowthItem();
        SpawnFuelItem(); // Generar �tems de combustible
        SpawnPowerItem(); // Generar �tems de poder
    }
    private void SpawnPowerItem()
    {
        if (Time.time >= nextPowerItemSpawnTime && powerItemPositions.Count < maxPowerItems)
        {
            Vector2Int randomPosition;
            do
            {
                randomPosition = new Vector2Int(
                    Random.Range(0, gridWidth),
                    Random.Range(0, gridHeight)
                );
            } while (bombPositions.Contains(randomPosition) || growthItemPositions.Contains(randomPosition) || fuelItemPositions.Contains(randomPosition) || powerItemPositions.Contains(randomPosition));

            powerItemPositions.Add(randomPosition); // A�adir �tem de poder a la lista
            Instantiate(hyperSpeedPowerPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
            nextPowerItemSpawnTime = Time.time + 15f; // Reiniciar temporizador
        }
    }

    // M�todo para verificar si una posici�n tiene un �tem de poder
    public bool IsPowerItem(Vector2Int position)
    {
        return powerItemPositions.Contains(position);
    }
    // M�todo para generar bombas en posiciones aleatorias
    private void GenerateBombs()
    {
        for (int i = 0; i < numberOfBombs; i++)
        {
            Vector2Int randomPosition = new Vector2Int(
                Random.Range(0, gridWidth),
                Random.Range(0, gridHeight)
            );
            bombPositions.Add(randomPosition); // A�adir bomba a la lista
            Instantiate(bombPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
        }
    }

    private void SpawnFuelItem()
    {
        if (Time.time >= nextFuelItemSpawnTime && fuelItemPositions.Count < maxFuelItems)
        {
            Vector2Int randomPosition;
            do
            {
                randomPosition = new Vector2Int(
                    Random.Range(0, gridWidth),
                    Random.Range(0, gridHeight)
                );
            } while (bombPositions.Contains(randomPosition) || growthItemPositions.Contains(randomPosition) || fuelItemPositions.Contains(randomPosition));

            fuelItemPositions.Add(randomPosition); // A�adir �tem de combustible a la lista
            Instantiate(fuelItemPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
            nextFuelItemSpawnTime = Time.time + 15f; // Reiniciar temporizador
        }
    }

    // M�todo para verificar si una posici�n tiene un �tem de combustible
    public bool IsFuelItem(Vector2Int position)
    {
        return fuelItemPositions.Contains(position);
    }

    // M�todo para verificar si una posici�n tiene una bomba
    public bool IsBomb(Vector2Int position)
    {
        return bombPositions.Contains(position);
    }

    // M�todo para verificar si una posici�n tiene un �tem de crecimiento
    public bool IsGrowthItem(Vector2Int position)
    {
        return growthItemPositions.Contains(position);
    }

    // M�todo para generar �tems de crecimiento cada 20 segundos
    private void SpawnGrowthItem()
    {
        if (Time.time >= nextGrowthItemSpawnTime && growthItemPositions.Count < maxGrowthItems)
        {
            Vector2Int randomPosition;
            do
            {
                randomPosition = new Vector2Int(
                    Random.Range(0, gridWidth),
                    Random.Range(0, gridHeight)
                );
            } while (bombPositions.Contains(randomPosition) || growthItemPositions.Contains(randomPosition));

            growthItemPositions.Add(randomPosition); // A�adir �tem de crecimiento a la lista
            Instantiate(growthItemPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
            nextGrowthItemSpawnTime = Time.time + 20f; // Reiniciar temporizador
        }
    }

    // M�todo para obtener la posici�n de inicio (centro del grid)
    public Nodo GetStartNode()
    {
        return linkedList.GetNodo(new Vector2Int(gridWidth / 2, gridHeight / 2));
    }
}

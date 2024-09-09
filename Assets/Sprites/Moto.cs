using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Moto : MonoBehaviour
{
    //Atributos
    public int velocidad;
    public int combustible;
    public Estela estela;
    public Queue<Item> items;
    public Stack<Poder> poderes;
    

    void Start()
    {
        velocidad = Random.Range(1, 11);
        combustible = 100;
        estela = new Estela();
        items = new Queue<Item>();
        poderes = new Stack<Poder>();

        // Inicializar estela con 3 posiciones
        for (int i = 0; i < 3; i++)
        {
            estela.AgregarPosicion(0, 0); // Usar valores iniciales
        }
    }
    public void Mover(int x, int y)
    {
        // Lógica para mover la moto
        estela.AgregarPosicion(x, y);
        estela.RemoverPosicion();
        combustible -= 1; // Ejemplo simplificado
    }
    public void RecogerItem(Item item)
    {
        items.Enqueue(item);
    }
    public void Destruir()
    {
        // Colocar ítems y poderes en el mapa

    }
    void Update()
    {
        
    }
}

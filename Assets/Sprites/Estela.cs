using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estela : MonoBehaviour
{
    private NodoEstela cabeza;

    public void AgregarPosicion(int x, int y)
    {
        NodoEstela nuevoNodo = new NodoEstela(x, y);
        nuevoNodo.siguiente = cabeza;
        cabeza = nuevoNodo;
    }

    public void RemoverPosicion()
    {
        if (cabeza != null)
        {
            cabeza = cabeza.siguiente;
        }
    }

    public bool VerificarColision(int x, int y)
    {
        NodoEstela actual = cabeza;
        while (actual != null)
        {
            if (actual.x == x && actual.y == y)
            {
                return true;
            }
            actual = actual.siguiente;
        }
        return false;
    }

    void Start()
    {
    }

    void Update()
    {
    }
}

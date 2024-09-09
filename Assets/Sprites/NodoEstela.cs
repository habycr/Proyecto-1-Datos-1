using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoEstela
{
    public int x;
    public int y;
    public NodoEstela siguiente;

    public NodoEstela(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.siguiente = null;
    }

    void Start()
    {
    }

    void Update()
    {
    }
}

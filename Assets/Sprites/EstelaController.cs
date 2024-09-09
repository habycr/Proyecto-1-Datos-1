using System.Collections.Generic;
using UnityEngine;

public class EstelaController : MonoBehaviour
{
    public GameObject estelaPrefab;  // Prefab para la estela
    private LinkedList<Transform> estelas = new LinkedList<Transform>();  // Lista enlazada de estelas
    public int tamanoInicialEstela = 3;  // Tama�o inicial de la estela

    void Start()
    {
        // Crear la estela inicial
        for (int i = 0; i < tamanoInicialEstela; i++)
        {
            CrearEstela(transform.position);
        }
    }

    void Update()
    {
        // Agregar nueva estela en la posici�n actual
        CrearEstela(transform.position);
    }

    void CrearEstela(Vector3 posicion)
    {
        // Instanciar el prefab de estela en una posici�n ligeramente detr�s del jugador
        Vector3 posicionAjustada = transform.position - transform.forward * 1f; // Ajusta 1f seg�n sea necesario
        GameObject nuevaEstela = Instantiate(estelaPrefab, posicionAjustada, Quaternion.identity);
        estelas.AddLast(nuevaEstela.transform);

        // Limitar el tama�o de la estela
        if (estelas.Count > tamanoInicialEstela)
        {
            Destroy(estelas.First.Value.gameObject);
            estelas.RemoveFirst();
        }
    }

}

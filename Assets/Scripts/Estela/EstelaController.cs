using UnityEngine;

public class EstelaController : MonoBehaviour
{
    public MotoController motoController; // Referencia a la moto
    public GameObject estelaPrefab; // Prefab para la part�cula de la estela
    public int longitudEstela = 3; // Longitud inicial de la estela
    private GameObject[] estelaParticulas; // Array para almacenar las part�culas de la estela

    private void Start()
    {
        // Inicializar el array de part�culas de estela
        estelaParticulas = new GameObject[longitudEstela];

        // Posicionar la estela inicialmente
        for (int i = 0; i < longitudEstela; i++)
        {
            estelaParticulas[i] = Instantiate(estelaPrefab, transform.position, Quaternion.identity);
            estelaParticulas[i].transform.position = new Vector3(motoController.transform.position.x, motoController.transform.position.y - (i + 1), 0);
        }
    }

    // M�todo p�blico para actualizar la estela
    public void UpdateEstela(Vector2Int posicionAnteriorMoto)
    {
        // Desplazar cada part�cula de la estela a la posici�n anterior
        for (int i = longitudEstela - 1; i > 0; i--)
        {
            if (i < estelaParticulas.Length)
            {
                estelaParticulas[i].transform.position = estelaParticulas[i - 1].transform.position;
            }
        }

        // La primera part�cula de la estela toma la posici�n anterior de la moto
        if (longitudEstela > 0) // Aseg�rate de que la longitud es mayor que 0
        {
            estelaParticulas[0].transform.position = new Vector3(posicionAnteriorMoto.x, posicionAnteriorMoto.y, 0);
        }

        // Si la longitud de la estela ha cambiado, actualizar el tama�o del array
        if (estelaParticulas.Length != longitudEstela)
        {
            GameObject[] newEstelaParticulas = new GameObject[longitudEstela];

            // Copiar las part�culas existentes o crear nuevas si es necesario
            for (int i = 0; i < longitudEstela; i++)
            {
                if (i < estelaParticulas.Length)
                {
                    newEstelaParticulas[i] = estelaParticulas[i]; // Mantener la part�cula existente
                }
                else
                {
                    newEstelaParticulas[i] = Instantiate(estelaPrefab, transform.position, Quaternion.identity); // Crear nueva
                }
            }

            estelaParticulas = newEstelaParticulas; // Actualizar el array de part�culas
        }
    }
}

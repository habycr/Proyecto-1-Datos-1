using UnityEngine;

public class EstelaController : MonoBehaviour
{
    public MotoController motoController; // Referencia a la moto
    public GameObject estelaPrefab; // Prefab para la partícula de la estela
    public int longitudEstela = 3; // Longitud inicial de la estela
    private GameObject[] estelaParticulas; // Array para almacenar las partículas de la estela

    private void Start()
    {
        // Inicializar el array de partículas de estela
        estelaParticulas = new GameObject[longitudEstela];

        // Posicionar la estela inicialmente
        for (int i = 0; i < longitudEstela; i++)
        {
            estelaParticulas[i] = Instantiate(estelaPrefab, transform.position, Quaternion.identity);
            estelaParticulas[i].transform.position = new Vector3(motoController.transform.position.x, motoController.transform.position.y - (i + 1), 0);
        }
    }

    // Método público para actualizar la estela
    public void UpdateEstela(Vector2Int posicionAnteriorMoto)
    {
        // Desplazar cada partícula de la estela a la posición anterior
        for (int i = longitudEstela - 1; i > 0; i--)
        {
            if (i < estelaParticulas.Length)
            {
                estelaParticulas[i].transform.position = estelaParticulas[i - 1].transform.position;
            }
        }

        // La primera partícula de la estela toma la posición anterior de la moto
        if (longitudEstela > 0) // Asegúrate de que la longitud es mayor que 0
        {
            estelaParticulas[0].transform.position = new Vector3(posicionAnteriorMoto.x, posicionAnteriorMoto.y, 0);
        }

        // Si la longitud de la estela ha cambiado, actualizar el tamaño del array
        if (estelaParticulas.Length != longitudEstela)
        {
            GameObject[] newEstelaParticulas = new GameObject[longitudEstela];

            // Copiar las partículas existentes o crear nuevas si es necesario
            for (int i = 0; i < longitudEstela; i++)
            {
                if (i < estelaParticulas.Length)
                {
                    newEstelaParticulas[i] = estelaParticulas[i]; // Mantener la partícula existente
                }
                else
                {
                    newEstelaParticulas[i] = Instantiate(estelaPrefab, transform.position, Quaternion.identity); // Crear nueva
                }
            }

            estelaParticulas = newEstelaParticulas; // Actualizar el array de partículas
        }
    }
}

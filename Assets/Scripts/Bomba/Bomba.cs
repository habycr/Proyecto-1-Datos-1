using UnityEngine;

public class Bomba : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Moto")) // Asegúrate de que la moto tenga el tag "Moto"
        {
            // Llama al método GameOver en el MotoController
            MotoController motoController = collision.GetComponent<MotoController>();
            if (motoController != null)
            {
                motoController.GameOver(); // Llama a GameOver en MotoController
            }

            // Destruye la bomba después de que explota
            Destroy(gameObject);
        }
    }
}

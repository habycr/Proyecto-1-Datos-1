using UnityEngine;

public class GrowthItem :MonoBehaviour , IItem
{
    public string ItemName => "Growth"; // Nombre del ítem

    public void Apply(MotoController moto)
    {
        int growthAmount = Random.Range(1, 11);
        moto.estelaController.longitudEstela += growthAmount;
        Debug.Log($"Estela crecida en {growthAmount}.");
    }
}

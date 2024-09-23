using UnityEngine;

public class FuelItem :MonoBehaviour, IItem
{
    public string ItemName => "Fuel"; // Nombre del ítem

    public void Apply(MotoController moto)
    {
        if (moto.combustible < 100)
        {
            int fuelAmount = Random.Range(1, 101);
            moto.combustible = Mathf.Min(moto.combustible + fuelAmount, 100);
            Debug.Log($"Combustible aumentado a {moto.combustible}.");
        }
        else
        {
            Debug.Log("Combustible lleno, ítem de combustible no aplicado.");
        }
    }
}

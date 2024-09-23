using UnityEngine;

public interface IItem
{
    string ItemName { get; } // Propiedad para el nombre del �tem
    void Apply(MotoController moto); // M�todo para aplicar el efecto del �tem a la moto
}

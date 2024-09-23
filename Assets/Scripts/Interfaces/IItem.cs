using UnityEngine;

public interface IItem
{
    string ItemName { get; } // Propiedad para el nombre del ítem
    void Apply(MotoController moto); // Método para aplicar el efecto del ítem a la moto
}

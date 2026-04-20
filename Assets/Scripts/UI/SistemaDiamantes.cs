using TMPro;
using UnityEngine;

public class SistemaDiamantes : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI textoCantidadDiamantes; 
   [SerializeField] private int cantidadDiamantes;

    private void Start()
    {
        ActualizarTexto();
    }
    private void SumarDiamantes(int cantidadEntrada)
    {
        cantidadDiamantes += cantidadEntrada;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textoCantidadDiamantes.text = cantidadDiamantes.ToString();
    }
}

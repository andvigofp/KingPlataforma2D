using Unity.Cinemachine;
using UnityEngine;

public class SeguirJugadorCamara : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    

    private void Start()
    {
        SeguirJugador();
    }

    private void SeguirJugador()
    {
        MovimientoJugador jugador = FindAnyObjectByType<MovimientoJugador>();

        if (jugador == null)
        {
            Debug.LogWarning("No se econtro el jugador");

            return;
        }

        Transform jugadorTransform = jugador.transform;
        cinemachineCamera.Follow = jugadorTransform;
       
    }
}

using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControladorVibracion : MonoBehaviour
{   
    [Header("Referencias")]
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

    [Header("Vibracion Camara")]
    [SerializeField] private float vibracionX;
    [SerializeField] private float vibracionY;
     
     private float velocidadAleatoriaX, velocidadAleatoriaY;
     private Vector2 velocidad;

    void OnEnable()
    {
        CombateJugador.JugadorGolpeoUnObjectivo += GenerarMovimientoCamara;        
    }

    void OnDisable()
    {
       CombateJugador.JugadorGolpeoUnObjectivo -= GenerarMovimientoCamara;          
    }

    private void GenerarMovimientoCamara()
    {   
        velocidadAleatoriaX = Random.Range(-vibracionX, vibracionX);
        velocidadAleatoriaY = Random.Range(-vibracionY, vibracionY);

        velocidad = new(velocidadAleatoriaX, velocidadAleatoriaY);

        cinemachineImpulseSource.GenerateImpulse(velocidad);
    }
}
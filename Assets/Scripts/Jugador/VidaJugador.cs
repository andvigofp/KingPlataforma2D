using UnityEngine;

public class VidaJugador : MonoBehaviour
{
   [SerializeField] private int VidaMaxima;
   [SerializeField] private int vidaActual;

    private void Awake()
    {
        vidaActual = VidaMaxima;
    }

    public void TomarDaño(int daño)
    {
        int vidaTemporal = vidaActual - daño;

        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, VidaMaxima);

        vidaActual = vidaTemporal;

        if(vidaActual <= 0)
        {
            DestruirJugador();
        }
    }

    private void DestruirJugador()
    {
        Destroy(gameObject);
    }
}

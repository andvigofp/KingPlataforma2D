using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    [SerializeField] private int daño = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // daño
        VidaJugador vida = other.GetComponent<VidaJugador>();
        if (vida != null)
        {
            vida.TomarDaño(daño);
        }

        // respawn (llamas al del jugador)
        RespawnJugador respawn = other.GetComponent<RespawnJugador>();
        if (respawn != null)
        {
            respawn.Respawn();
        }
    }
}
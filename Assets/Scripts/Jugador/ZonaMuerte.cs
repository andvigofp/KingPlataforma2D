using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    [SerializeField] private Transform puntoRespawn;
    [SerializeField] private int daño = 1;
void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Algo entró: " + other.name);

    if (other.CompareTag("Player"))
    {
        Debug.Log("Es el player");
        
        VidaJugador vida = other.GetComponent<VidaJugador>();
        if (vida != null)
        {
            vida.TomarDaño(daño);
        }

        other.transform.position = puntoRespawn.position;
    }
}
}
using UnityEngine;

public class RespawnJugador : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;
    }

    public void Respawn()
    {
        rb2D.linearVelocity = Vector2.zero;
        rb2D.angularVelocity = 0f;
        transform.position = posicionInicial;
    }
}

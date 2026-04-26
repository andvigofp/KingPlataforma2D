using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    private const string STRING_VELOCIDAD_HORIZONTAL = "VelocidadHorizontal";
    private const string STRING_VELOCIDAD_VERTICAL = "VelocidadVertical";
    private const string STRING_EN_SUELO = "EnSuelo";
    private const string STRING_ATERRIZAR = "Aterrizar";

    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D colisionadorJugador;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadMovimiento = 5f;
    private float entradaHorizontal;
    private float entraVertical;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimensionesCaja = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask capasSalto;
    [SerializeField] private bool sePuedeMoverEnElAire = true;

    private Vector2 input;

    private bool enSuelo;
    private bool entradaSalto;

    private void Update()
    {
        ComprobarSuelo();
        ControladorAnimaciones();
    }

    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();
        ControladorSalto();
        entradaSalto = false;
    }

    // ================= INPUT =================

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        entradaHorizontal = input.x;
        entraVertical = input.y;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            entradaSalto = true;
        }
    }

    // ================= SUELO =================

    private void ComprobarSuelo()
    {
        bool estabaEnElSuelo = enSuelo;

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, capasSalto);

        // Detectar aterrizaje
        if (enSuelo && !estabaEnElSuelo && rb2D.linearVelocity.y <= 0)
        {
            animator.ResetTrigger(STRING_ATERRIZAR);
            animator.SetTrigger(STRING_ATERRIZAR);
        }
    }

    // ================= SALTO =================

    private void ControladorSalto()
    {
        if (!entradaSalto) return;
        if (!enSuelo) return;

        // ↓ + salto = bajar plataforma
        if (entraVertical < -0.5f)
        {
            DesactivarPlataformas();
        }
        else
        {
            Saltar();
        }

        entradaSalto = false;
    }

    private void Saltar()
    {
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0);
        rb2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }

    // ================= PLATAFORMAS =================

    private void DesactivarPlataformas()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(controladorSuelo.position, dimensionesCaja, 0f, capasSalto);

        foreach (Collider2D obj in objetos)
        {
            if (obj.GetComponent<PlatformEffector2D>() != null)
            {
                Physics2D.IgnoreCollision(colisionadorJugador, obj, true);
                StartCoroutine(ReactivarColision(obj));
            }
        }
    }

    private IEnumerator ReactivarColision(Collider2D plataforma)
    {
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(colisionadorJugador, plataforma, false);
    }

    // ================= MOVIMIENTO =================

    private void ControlarMovimientoHorizontal()
    {
        if (!enSuelo && !sePuedeMoverEnElAire) return;

        float velocidadObjetivo = entradaHorizontal * velocidadMovimiento;

        rb2D.linearVelocity = new Vector2(
            Mathf.Lerp(rb2D.linearVelocity.x, velocidadObjetivo, 0.2f),
            rb2D.linearVelocity.y
        );

        // Giro sprite
        if (entradaHorizontal > 0)
            spriteRenderer.flipX = false;
        else if (entradaHorizontal < 0)
            spriteRenderer.flipX = true;
    }

    // ================= ANIMACIONES =================

    private void ControladorAnimaciones()
    {
       animator.SetFloat(STRING_VELOCIDAD_HORIZONTAL, Mathf.Abs(rb2D.linearVelocity.x));
        animator.SetFloat(STRING_VELOCIDAD_VERTICAL, rb2D.linearVelocity.y);
        animator.SetBool(STRING_EN_SUELO, enSuelo);
    }

    // ================= DEBUG =================

    private void OnDrawGizmos()
    {
        if (controladorSuelo == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
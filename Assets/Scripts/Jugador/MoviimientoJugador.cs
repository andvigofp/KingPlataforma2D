using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    private const string STRING_VELOCIDAD_HORIZONTAL = "VelocidadHorizontal";
    private const string STRING_VELOCIDAD_VERTICAL = "VelocidadVertical";
    private const string STRING_EN_SUELO = "EnSuelo";

    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadMovimiento = 5f;
    private float entradaHorizontal;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimensionesCaja = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask capasSalto;
    [SerializeField] private bool sePuedeMoverEnElAire = true;

    private bool enSuelo;
    private bool entradaSalto;

    private void Update()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, capasSalto);
        ControladorAnimaciones();
    }

    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();
        ControladorSalto();
        entradaSalto = false;
    }

    //Mover el Personaje
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        entradaHorizontal = input.x;
    }

    //Saltar el personaje
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            entradaSalto = true;
        }
    }

    private void ControladorSalto()
    {
        if (!entradaSalto || !enSuelo) return;

        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
        rb2d.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }

    private void ControlarMovimientoHorizontal()
    {
        if (!enSuelo && !sePuedeMoverEnElAire) return;

        float velocidadObjetivo = entradaHorizontal * velocidadMovimiento;

        rb2d.linearVelocity = new Vector2(
            Mathf.Lerp(rb2d.linearVelocity.x, velocidadObjetivo, 0.2f),
            rb2d.linearVelocity.y
        );

        //GIRO CORRECTO
        if (entradaHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (entradaHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void ControladorAnimaciones()
    {
        animator.SetFloat(STRING_VELOCIDAD_HORIZONTAL, Mathf.Abs(rb2d.linearVelocity.x));
        animator.SetFloat(STRING_VELOCIDAD_VERTICAL, rb2d.linearVelocity.y);
        animator.SetBool(STRING_EN_SUELO, enSuelo);
    }

    private void OnDrawGizmos()
    {
        if (controladorSuelo == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
using System;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private const string STRING_VELOCIDAD_HORIZONTAL = "VelocidadHorizontal";
    private const string STRING_VELOCIDAD_VERTICAL = "VelocidadVertical";
    private const string STRING_EN_SUELO = "EnSuelo";

    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;
    
    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadMovimiento;
    private float entradaHorizontal;

    
    [Header("Salto")]
    
    [SerializeField] private float fuerzaSalto;

    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private LayerMask capasSalto;
    [SerializeField] private bool sePuedeMoverEnElAire;
    private bool enSuelo;
    private bool entradaSalto;

    private void Update()
    {
        entradaHorizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            entradaSalto = true;
        }

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, capasSalto);

        ControladorAnimaciones();
    }

    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();
        ControladorSalto();
        entradaSalto = false;
    }

    private void ControladorSalto()
    {
        if (!entradaSalto) {return;}
        if (!enSuelo) {return;}
        Saltar();

    }

    private void Saltar()
    {
        entradaSalto = false;
        rb2d.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
    }

    private void ControlarMovimientoHorizontal()
    {

        if(!enSuelo && !sePuedeMoverEnElAire){return;}
        
             rb2d.linearVelocity = new Vector2(entradaHorizontal * velocidadMovimiento, rb2d.linearVelocity.y);

            if ((entradaHorizontal > 0 && !MirandoALaDerecha()) || (entradaHorizontal < 0 && MirandoALaDerecha()))
            {
                Girar();
            }
        
       
    }

    private void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private bool MirandoALaDerecha()
    {
        return transform.localScale.x == 1;
    }

    private void ControladorAnimaciones()
    {
        animator.SetFloat(STRING_VELOCIDAD_HORIZONTAL, Mathf.Abs(rb2d.linearVelocity.x));
        animator.SetFloat(STRING_VELOCIDAD_VERTICAL, Mathf.Sign(rb2d.linearVelocity.y));
        animator.SetBool(STRING_EN_SUELO, enSuelo);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}

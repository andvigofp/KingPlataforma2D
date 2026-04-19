using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombateJugador : MonoBehaviour
{   
    private const string STRING_ANIMACION_ATAQUE = "Atacar";

    [Header("Refrerencias")]
    [SerializeField] private Animator animator;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private int dañoAtaque;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoUltimoAtaque;

   
    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            IntentarAtacar();
        }
    }

    private void IntentarAtacar()
    {
        if(Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) { return; }
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        animator.SetTrigger(STRING_ANIMACION_ATAQUE);

        tiempoUltimoAtaque = Time.time;
        
        Collider2D[] objetosTocados = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);


        foreach (Collider2D objecto in objetosTocados)
        {
            if(objecto.TryGetComponent(out VidaEnemigo vidaEnemigo))
            {
                vidaEnemigo.TomarDaño(dañoAtaque);            
            }
        }
    }

   private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}

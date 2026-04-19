using UnityEngine;

public class CorazonUI : MonoBehaviour
{
   [SerializeField] private Animator animator; 
   [SerializeField] private bool estadoActivo;

   public void ActivarCorazon()
    {
        animator.SetTrigger("Restaurar");
        estadoActivo = true;
    }

    public void DesactivarCorazon()
    {
        animator.SetTrigger("Golpe");
        estadoActivo = false;
    }

    public bool EstadoActivo() => estadoActivo;
}

using UnityEngine;
using UnityEngine.InputSystem;

public class InteractuarObjetoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorInteractuar;
    [SerializeField] private Vector2 dimensionCaja;
    [SerializeField] private LayerMask capasInteractuables;

   public void OnInteractuar()
    {
        Interactuar();
    }

    private void Interactuar()
    {
        Collider2D[] objectoTocados = Physics2D.OverlapBoxAll(controladorInteractuar.position, dimensionCaja, 0f, capasInteractuables);

        foreach (Collider2D objeto in objectoTocados)
        {
            Debug.Log(objeto.name);

            if(objeto.TryGetComponent(out Interactuable interactuable))
            {
                interactuable.Interactuar();
                break;
            } 
        }  
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(controladorInteractuar.position, dimensionCaja);
    }
}

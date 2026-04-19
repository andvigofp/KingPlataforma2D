using System;
using UnityEngine;

public class ContenedorCorazonesUI : MonoBehaviour
{
    [SerializeField] private CorazonUI[] corazones;
    [SerializeField] private VidaJugador vidaJugador;

    private void Start()
    {
        vidaJugador = FindAnyObjectByType<VidaJugador>();

         vidaJugador.JugadorTomoDaño += ActivarCorazones;
         vidaJugador.JugadorSeCuro += ActivarCorazones;

        ActivarCorazones(vidaJugador.GetVidaActual());
    }

    void OnDisable()
    {
         vidaJugador.JugadorTomoDaño -= ActivarCorazones;
         vidaJugador.JugadorSeCuro -= ActivarCorazones;

    }

    private void ActivarCorazones(int vidaActual)
    {
        for(int i = 0; i < corazones.Length; i++)
        {
            if (i < vidaActual)
            {
                if(corazones[i].EstadoActivo()) { continue; }
                
                corazones[i].ActivarCorazon();
            }
            else
            {
                if(!corazones[i].EstadoActivo()) { continue; }
                
                corazones[i].DesactivarCorazon();
            }
        }
    }
}

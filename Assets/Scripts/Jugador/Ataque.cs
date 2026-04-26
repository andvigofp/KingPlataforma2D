using System;
using UnityEngine;

[Serializable]
public class Ataque 
{
   public TipoDeAtaque tipoAtaque;
   public string nombreAtaque;
   public int cantidadDeDaño;
   public Transform controladorAtaque;
   public string stringAnimacion;
   public float radioAtaque;
   public Vector2 dimensionesCaja;
}
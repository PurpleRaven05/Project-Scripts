using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject isla;
    public GameObject barco;
    public GameObject enemigo;
    public Transform zonaSpawn;
    public AudioSource audioSource;
    public int nivel = 1;

    private void Start()
    {
        //GenerarBarco(4);
    }
    public void GenerarBarco(int cantidadUnidades = 0)
    {
        audioSource.Play(0);
        if (cantidadUnidades <= 0)
            cantidadUnidades = Random.Range(1, 5);
        transform.rotation = Quaternion.Euler(0,  Random.Range(-180, 180), 0);
        GameObject barquito = Instantiate(barco, zonaSpawn.position, zonaSpawn.rotation);
        Barco ship = barquito.GetComponent<Barco>();
        ship.objetivo = isla.transform;
        ship.enemigos = new GameObject[cantidadUnidades];
        for (int i = 0; i < cantidadUnidades; i++)
        {
            ship.enemigos[i] = enemigo;
            ship.enemigos[i].GetComponent<Unidad>().actualizarNivel(nivel);
        }
            
    }
}

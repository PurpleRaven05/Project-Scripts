using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barco : MonoBehaviour
{
    public Transform objetivo;
    public GameObject[] enemigos;

    private NavMeshAgent agente;

    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        goTo(objetivo);
    }

    private void goTo(Transform destino)
    {
        agente.SetDestination(destino.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject enemigo in enemigos)
        {
            GameObject barco = Instantiate(enemigo,transform.position, transform.rotation, transform.parent);
            barco.GetComponent<Unidad>().escenario = objetivo.gameObject;
        }
        Destroy(gameObject);
    }
}

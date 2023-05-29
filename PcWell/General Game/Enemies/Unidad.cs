using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unidad : MonoBehaviour
{
    public GameObject escenario;
    public Transform objetivo;
    public float HP;
    public float damage = 0;
    //Porcentaje
    public float defense = 0;
    public Slider vida;
    private bool aliado;
    private Vector3 ubicacion;
    private NavMeshPath camino;

    private NavMeshAgent agente;

    void Awake()
    {
        if (gameObject.layer == 7)
            aliado = true;
        else
            aliado = false;
        if (!aliado && (defense == 0 || damage == 0))
        {
            damage = Random.Range(1, 10);
            defense = Random.Range(5, 22);
        }
        HP = 100;
        agente = GetComponent<NavMeshAgent>();
        ubicacion = transform.localPosition;
        camino = new NavMeshPath();
    }

    public void actualizarNivel(int nivel)
    {
        damage = Random.Range(1, 10) * nivel;
        defense = Random.Range(5, 22) * nivel;
    }

    // Update is called once per frame
    void Update()
    {
        if (aliado)
        {
            if (objetivo == null)
            {
                objetivo = SearchClosestEnemy();
                if(objetivo == null)
                {
                    if ( agente.remainingDistance < 1 || !agente.CalculatePath(ubicacion, camino))
                    {
                        ubicacion = new Vector3(transform.localPosition.x + (Random.Range(0, 2) == 1 ? 3 : -3),
                                                transform.localPosition.y,
                                                transform.localPosition.z + (Random.Range(0, 2) == 1 ? 3 : -3));
                        
                    }
                    goTo(ubicacion);
                }
            }
        }
        else
        {
            if (objetivo == null)
            {
                objetivo = SearchObjective();
            }
        }
        if (objetivo != null)
            goTo(objetivo.position);
        vida.value = HP;
    }

    GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] goArray = FindObjectsOfType<GameObject>();
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++) 
        {
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
            return null;
        return goList.ToArray();
    }

    /// <summary>
    /// Funci�n para ir a una ubicaci�n
    /// </summary>
    /// <param name="destino"></param>
    private void goTo(Vector3 destino)
    {
        agente.SetDestination(destino);
    }

    /// <summary>
    /// Funci�n que devuelve el enemigo mas cercano
    /// </summary>
    /// <returns>El transform del enemigo mas cercano</returns>
    private Transform SearchClosestEnemy()
    {
        GameObject[] enemigos = FindGameObjectsWithLayer(8);
        if (enemigos != null) {
            GameObject closest = enemigos[0];
            foreach (GameObject objeto in enemigos)
            {
                if((objeto.transform.position - transform.position).magnitude < (closest.transform.position - transform.position).magnitude)
                {
                closest = objeto;
                }
            }
            return closest.transform;
        }
        return null;
    }

    /// <summary>
    /// Funcion para buscar objetivos en el escenario
    /// </summary>
    private Transform SearchObjective()
    {
        //Buscar los objetivos en el escenario. Tienen layer 6
        GameObject[] objetivos= FindGameObjectsWithLayer(6);
        Debug.Log(objetivos);
        if(objetivos == null){
            Destroy(this);
        }
        else{
            int elegido = Random.Range(0, objetivos.Length);
        return objetivos[elegido].transform;
        }
        return null;
    }

    private void combate(Unidad enemigo)
    {
        float dañoRecibido = (enemigo.damage * (1 - (defense / 100)));
        float dañoRealizado = (damage * (1 - (enemigo.defense / 100)));
        HP -= dañoRecibido;
        enemigo.HP -= dañoRealizado;
        if (enemigo.HP <= 0) Destroy(enemigo.gameObject);
        if(HP <= 0) Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 7 && gameObject.layer == 8)
            combate(collision.gameObject.GetComponent<Unidad>());
        else if(collision.gameObject.layer == 6 && gameObject.layer == 8)
            atacarObjetivo(collision.gameObject.GetComponent<Objetivo>());
    }

    private void atacarObjetivo(Objetivo objetivo)
    {
        float dañoRealizado = (damage * (1 - (objetivo.defense / 100)));
        objetivo.Golpeado(dañoRealizado);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour
{
    public Jugador jugador;
    public float HP;
    private float maxHP;
    public float damage;
    //Porcentaje
    public float defense;
    public GameObject particulas;
    public GameObject hoja1;
    public GameObject hoja2;
    public GameObject hoja3;

    //Sound
    public AudioSource damageS;
    public AudioSource destroyS;

    void Awake()
    {
        damage = 0;
        defense = 85;
        HP = 100;
        maxHP = 100;
    }

    public void Golpeado(float dano)
    {
        damageS.Play(0);
        float danoReal = Mathf.Clamp(dano, 0, HP);
        HP -= danoReal;
        if (hoja1.activeSelf && 3 * (maxHP / 4) > HP)
            hoja1.SetActive(false);
        if (hoja2.activeSelf && 2 * (maxHP / 4) > HP)
            hoja2.SetActive(false);
        if (hoja3.activeSelf && maxHP / 4 > HP)
            hoja3.SetActive(false);
        jugador.UpdateHP(-danoReal);
        if (HP <= 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        destroyS.Play(0);
        Instantiate(particulas, hoja3.transform.position, transform.rotation);
    }
}

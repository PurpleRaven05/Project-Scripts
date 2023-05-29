using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class minijuegoRuleta : MonoBehaviour
{

    public GameObject ruleta;
    public GameObject confeti;
    public GameObject texto;
    public GameObject botonMal;

    private TextMeshProUGUI mensaje;
    private Image fondoBoton;

    private float tiempoColor;
    private float tiempoRotacion;
    private int angulo;
    private bool rotar = false;
    private bool mostrarTexto = false;

    private Color actual;
    private Color siguiente;

    private CambiarCanvas controlador;
    private bool correcto = true;
    void Start()
    {
        tiempoColor = 1f;
        controlador = FindObjectOfType<CambiarCanvas>();
        mensaje = texto.GetComponent<TextMeshProUGUI>();
        fondoBoton = botonMal.GetComponent<Image>();
        tiempoRotacion = Random.Range(3f, 5f);
        angulo = Random.Range(300, 400);

        actual = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        siguiente = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    void Update()
    {
        if (rotar)
        {
            texto.SetActive(false);
            confeti.SetActive(false);
            botonMal.SetActive(false);


            mostrarTexto = false;
            tiempoRotacion -= Time.deltaTime;

            if(tiempoRotacion >= 0f)
            {
                ruleta.transform.Rotate(Vector3.forward * angulo * Time.deltaTime);

            }
            else
            {
                confeti.SetActive(true);
                texto.SetActive(true);
                botonMal.SetActive(true);

                rotar = false;
                mostrarTexto = true;

                angulo = Random.Range(250, 400);
                tiempoRotacion = Random.Range(5f, 8f);
            }
        }


        if (mostrarTexto)
        {
            Color letras = Color.Lerp(actual,siguiente, Mathf.PingPong(Time.time, 1));
            mensaje.color = letras;
            fondoBoton.color = letras;
            tiempoColor -= Time.deltaTime;

            if (tiempoColor <= 0f)
            {
                actual = siguiente;
                siguiente = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                tiempoColor = 1f;
            }
        }
    }

    public void spin()
    {
        rotar = true;
    }
    public void perder()
    {
        correcto = false;
        GameObject root = this.gameObject.transform.parent.parent.gameObject;
        controlador.generador.GenerarBarco(Random.Range(1, 4));
        Destroy(root);
        //Llamar a la funcion del juego principal de perder
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        if (correcto)
            controlador.jugador.ganarDinero(10f);
    }
}

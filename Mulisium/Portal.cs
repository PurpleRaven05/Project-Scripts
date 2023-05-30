using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Portal : MonoBehaviour
{
    private Transform camOBJ;
    private Renderer renderizado;
    private Transform player;
    private Transform playerCamera;
    private CharacterController playerCC;
    public GameObject linkedPortal;
    private Camera cam;
    public Material cameraMat;
    private bool playerInPortal;

    //TODO Crear material propio en vez de asignarlo
    //TODO Cambiar posición del trigger según Tamaño

    private void Awake ()
    {
		renderizado = GetComponent<Renderer>();
        playerInPortal = false;
	}

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerCamera = player.GetChild(0);
        playerCC = player.GetComponent<CharacterController>();
		camOBJ = linkedPortal.transform.GetChild(0);
        cam = camOBJ.GetComponent<Camera>();
        cam.fieldOfView = playerCamera.GetComponent<Camera>().fieldOfView;
        if(cam.targetTexture != null) 
        {
            cam.targetTexture.Release();
        }
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat.mainTexture = cam.targetTexture;
        renderizado.material = cameraMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (renderizado.isVisible) 
        {
            //Teleport
            if (playerInPortal)
            {
                teleportPlayer();
            }
            //Visual
            //Debug.Log("Portal is visible.");
			camOBJ.gameObject.SetActive(true);

			Vector3 portalToPlayer = playerCamera.position - transform.position;
            float angDiff = Vector3.SignedAngle(transform.up, linkedPortal.transform.up, Vector3.up);
            angDiff += 180;
            //Debug.Log(angDiff);
            Vector3 newCamDir = Quaternion.AngleAxis(angDiff, Vector3.up) * playerCamera.forward;
			camOBJ.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);

			Vector3 positionOffset = Quaternion.AngleAxis(angDiff, Vector3.up) * portalToPlayer;
			camOBJ.position = linkedPortal.transform.position + positionOffset;
			
        }
        else 
        {
            //Debug.Log("Portal is no longer visible.");
			camOBJ.gameObject.SetActive(false);
		}
    }

    private void teleportPlayer ()
    {
        //Debug.Log("Teleporting Player");
		Vector3 portalToPlayer = player.position - transform.position;
		float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
        float secondDot = Vector3.Dot(transform.up, playerCC.velocity.normalized);

		if (dotProduct > 0 && (Mathf.RoundToInt(secondDot * 100) < 0))
		{
            playerCC.enabled = false;

			float rotationDiff = Vector3.SignedAngle(transform.up, linkedPortal.transform.up, Vector3.up); ;
			rotationDiff += 180;
            Vector3 newPlayDir = Quaternion.AngleAxis(rotationDiff, Vector3.up) * player.forward;
            player.rotation = Quaternion.LookRotation(newPlayDir, Vector3.up);

			Vector3 positionOffset = Quaternion.AngleAxis(rotationDiff, Vector3.up) * portalToPlayer;
            player.position = linkedPortal.transform.position + positionOffset;

            playerCC.enabled = true;
			playerInPortal = false;
		}
	}

    private void OnTriggerEnter (Collider other)
    {
        //Debug.Log("Something entered");
        if(other.tag == "Player")
        {
           // Debug.Log("Player entered");
            playerInPortal = true;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if(other.tag == "Player")
        {
            playerInPortal = false;
        }
    }
}

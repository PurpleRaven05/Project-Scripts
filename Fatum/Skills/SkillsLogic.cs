using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsLogic : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject ghostPrefab;
    public GameObject ghostField;
    public GameObject ghost;
    public GameObject effectShpere;
    public Cinemachine.CinemachineFreeLook _camera;
    public bool ghostActive = false;
    private float InnerCooldown = 0f;
    private bool timerOn = false;
    // Start is called before the first frame update
    public void HermitSkill(){
        if(!ghostActive && !timerOn){
            ghostActive = true;
            playerTransform.gameObject.GetComponent<PlayerController>().enabled=false;
            playerTransform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Transform spawnZone = playerTransform.GetChild(20);
            ghost = Instantiate(ghostPrefab, spawnZone.position, playerTransform.rotation);
            Vector3 playerTransformCorrector = new Vector3(playerTransform.position.x, playerTransform.position.y+1.5f, playerTransform.position.z);
            effectShpere = Instantiate(ghostField, playerTransformCorrector, playerTransform.rotation);
            _camera.LookAt = ghost.transform;
            _camera.Follow =ghost.transform;
            timerOn = true;
        }
        else{
            if(!timerOn){
                ghostActive = false;
                _camera.LookAt = playerTransform;
                _camera.Follow =playerTransform;
                playerTransform.gameObject.GetComponent<PlayerController>().enabled=true;
                playerTransform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                Destroy(effectShpere);
                Destroy(ghost);
                timerOn = true;
            }
            
        }
    }
    public void UseLoversSkill(){
        UnityEngine.Debug.Log("Lovers");
        playerTransform.gameObject.GetComponent<PlayerResources>().TakeDamage(-10);
    }
    public void Update(){
        if(timerOn){
            InnerCooldown+= Time.deltaTime;
            if(InnerCooldown >= 0.5){
                timerOn = false;
                InnerCooldown = 0f;
            }
        }
    }
}

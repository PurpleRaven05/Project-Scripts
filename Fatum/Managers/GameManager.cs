using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InventoryManager _manager;
    public PlayerResources _resources;
    public SaveDataManager _dataManager;
    public List<InventorySlate> slates;
    public InventorySlate actualSlate;
    public string currentMap, currentSavedMap;
    private float gravity = -9.8f;
    public string checkpoint;
    void Start()
    {
    
        if(currentMap != "Main Menu"){
            Cursor.lockState = CursorLockMode.Locked;
            actualSlate = slates[0];
        }
        else{
            Cursor.lockState = CursorLockMode.None;
        }
        
        bool fileExist = SaveDataManager.GameDataFileExists();
        if(fileExist){
            _dataManager.LoadActualLevel();
            currentSavedMap = _dataManager.currentMap;
            if(currentMap!= "Main Menu"){
                _dataManager.LoadInventory();
                checkpoint = _dataManager.LoadPlayer();
                _manager.SetAllImages();
                LoadPlayerOnCheckpoint();
            }
            
        }
        
        
    }
    void Awake(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMap != "Main Menu"){
            if(_resources.health <= 0){
            StartCoroutine(ResurrectPlayer(2.7f));
            }
        }
        
    }
    public void SaveGameData(){
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        SaveDataManager.SaveData(currentMap, _resources.health.ToString(), _resources.mana.ToString(), actualSlate.id, _manager.allSlots, 
        _manager.mainSkill1.skillName, _manager.mainSkill2.skillName, _manager.mainSkill3.skillName, _manager.mainSkill4.skillName, _manager.mainSkill5.skillName, 
        _manager.pasiveSkill1.skillName, _manager.pasiveSkill2.skillName, _manager.maskSkill.skillName, _manager.movSkill.skillName);
    }
    public static Vector3 CalculateVelocityForParabola(Transform bola, float height){
        float gravity = -9.8f;
        //Transform playerTransform = GameObject.Find("Player").transform.GetChild(0).GetComponent<Transform>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        float movementY = playerTransform.position.y - bola.position.y;
        if(movementY<0){
            movementY = 1-movementY;
        }
        //UnityEngine.Debug.Log("VelocityH: "+movementY);
        Vector3 movementXZ = new Vector3(playerTransform.position.x - bola.position.x, 0, playerTransform.position.z - bola.position.z);
        //UnityEngine.Debug.Log("VelocityMov: "+movementXZ);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2*gravity*height);
        //UnityEngine.Debug.Log("VelocityY: "+velocityY);
        Vector3 velocityXZ = movementXZ / (Mathf.Sqrt(-2*height/gravity) + Mathf.Sqrt(2*(movementY - height)/gravity));
        //UnityEngine.Debug.Log("VelocityBoom: "+movementXZ / (Mathf.Sqrt(-2*height/gravity)));
        //UnityEngine.Debug.Log("VelocityBoom2: "+Mathf.Sqrt(2*movementY - height)/gravity);
        //UnityEngine.Debug.Log("VelocityXZ: "+velocityXZ);
        return (velocityXZ + velocityY);
    }
    public void LoadPlayerOnCheckpoint(){
        for(int i = 0; i<slates.Count; i++){
            //UnityEngine.Debug.Log("Checkpoints same"+slates[i].id.ToString());
            //UnityEngine.Debug.Log("Checkpoints same"+checkpoint);
            //UnityEngine.Debug.Log("Checkpoints same"+slates[i].id.ToString() == checkpoint);
            UnityEngine.Debug.Log("X "+slates[i].coordX+" Y "+slates[i].coordY+1f+" Z "+slates[i].coordZ);
            if(slates[i].id.ToString() == checkpoint){
                //UnityEngine.Debug.Log("Checkpoints same"+slates[i].id.ToString());
                //UnityEngine.Debug.Log("Checkpoints same"+checkpoint);
                Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
                Vector3 position = new Vector3(slates[i].coordX,slates[i].coordY+0.3f,slates[i].coordZ);
                if(position != Vector3.zero){
                    playerTransform.position = position;
                    playerTransform.rotation = slates[i].transform.GetComponent<Transform>().rotation;
                    Physics.SyncTransforms();
                return;
                }
                //UnityEngine.Debug.Log("X "+slates[i].coordX+" Y "+slates[i].coordY+1f+" Z "+slates[i].coordZ);
                
            }
            else
                UnityEngine.Debug.Log("No entro");
        }
    }
    public void LoadPlayerOnActualCheckpoint(){
        for(int i = 0; i<slates.Count; i++){

            if(slates[i].id == actualSlate.id){

                Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
                Vector3 position = new Vector3(actualSlate.transform.position.x, actualSlate.transform.position.y+1f, actualSlate.transform.position.z);
                playerTransform.position =position;
                Physics.SyncTransforms();
            }
        }
    }
    public void LoadSpecificLevel(){
        MenuManager.LoadSpecificScene(currentSavedMap);
    }
    public void LoadFirstLevel(){
        MenuManager.LoadFirstLevel();
    }
    public void ExitGame(){
        MenuManager.ExitGame();
    }
    public void ReturnToMenu(){
        MenuManager.ReturnToMenu();
    }
    public void NewGame(){
        SaveDataManager.DeleteDataFile();
        MenuManager.LoadFirstLevel();
    }
    
    public IEnumerator ResurrectPlayer(float length)
	{  
        PlayerController playerCont = GameObject.Find("Player").GetComponent<PlayerController>();
        playerCont.playerAnimator.SetTrigger("Die");
        if(!playerCont.death.isPlaying)
            playerCont.death.Play();
        playerCont.enabled = false;
		yield return new WaitForSeconds(length); 
        _resources.health = _resources.maxHealth;
        _resources.mana = _resources.maxMana;
        SaveGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ContinueChecker : MonoBehaviour
{
    // Start is called before the first frame update
    private string path = "";
    public string fileName = "";
    private string fullPath = "";
    public bool continueExists;
    public GameObject ContinueContainer;
    
    void Awake(){
        path = Application.persistentDataPath;
        fileName = "/GameDataSave.txt";
        fullPath = path + fileName;
    }
    void Start(){
        GameDataCheck();
        ContinueContainer.SetActive(continueExists);
    }
    void Update(){
        GameDataCheck();
        ContinueContainer.SetActive(continueExists);
    }
    public void GameDataCheck()
    {
        continueExists = File.Exists(fullPath);
    }
}

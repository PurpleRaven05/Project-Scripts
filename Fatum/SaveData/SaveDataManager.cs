using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;


public class SaveDataManager : MonoBehaviour
{
    static  string path = "";
    static string fileName = "";
    static string fullPath = "";
    public string confirmPath = "";
    public InventoryManager _manager;
    public PlayerResources _resources;
    public string currentMap;
    public Vector3 spawnPosition;
    void Awake(){
        path = Application.persistentDataPath;
        fileName = "/GameDataSave.txt";
        fullPath = path + fileName;
        confirmPath = fullPath;
    }
    public static bool GameDataFileExists()
    {
        return File.Exists(fullPath);
    }
    public static void DeleteDataFile()
    {
        File.Delete(fullPath);
    }
    public static void SaveData(string currentMap, string health, 
    string mana, float checkPointID, List<InventorySlot> allSlots, 
    string mainSkill1, string mainSkill2, string mainSkill3, 
    string mainSkill4, string mainSkill5, string pasiveSkill1, 
    string pasiveSkill2, string mask, string movement){
        UnityEngine.Debug.Log("Saving Game");
        File.WriteAllText(fullPath, string.Empty);
        StreamWriter writer = new StreamWriter(fullPath, false);

        string currentLevel = "CurrentLevel:" + currentMap;
        string playerHealth = "PlayerHealth:"+ health;
        string playerMana = "PlayerMana:"+ mana;
        string Checkpoint = "Checkpoint:"+checkPointID;
        string itemsState = "Items:";
        string mainSkills = "MainSkillEquiped:"+mainSkill1+","+mainSkill2+","+mainSkill3+","+mainSkill4
        +","+mainSkill5;
        string pasiveSkillsEquiped = "PasiveSkillsEquiped:"+pasiveSkill1+","+pasiveSkill2;
        string maskEquiped = "MaskEquiped:"+mask;
        string movEquiped = "MovementEquiped:"+movement;
        
        List<InventorySlot> tempSlot = allSlots;
        for(int i = 0; i< tempSlot.Count; i++){
            itemsState += tempSlot[i].skillName + "," + tempSlot[i].unlocked +",";
        }
        if(itemsState[itemsState.Length-1].ToString().Equals(","))
            itemsState = itemsState.Substring(0, itemsState.Length - 1);
        

        writer.WriteLine(currentLevel);
        writer.WriteLine(playerHealth);
        writer.WriteLine(playerMana);
        writer.WriteLine(Checkpoint);
        writer.WriteLine(itemsState);
        writer.WriteLine(mainSkills);
        writer.WriteLine(pasiveSkillsEquiped);
        writer.WriteLine(maskEquiped);
        writer.WriteLine(movEquiped);

        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadInventory(){
        //unlocked skills and skills in positions
        string line;
        List<string> itemName = new List<string>();
        List<string> itemUnlocked = new List<string>();
        string _mainSkill1 = "";
        string _mainSkill2 = "";
        string _mainSkill3= "";
        string _mainSkill4= "";
        string _mainSkill5= "";
        string _passiveSkill1= "";
        string _passiveSkill2= "";
        string _mask= "";
        string _movement= "";
        StreamReader file = new StreamReader(fullPath);
        while((line = file.ReadLine())!= null){
            string[] word = line.Split(':');
            if(word[0]== "Items"){
                string [] auxiliar = word[1].Split(',');
                for(int i = 0; i< auxiliar.Length; i++){
                    //Cero o par deberian ser las posiciones de los nombres de los objetos
                    if(i == 0 || i%2 == 0){
                        itemName.Add(auxiliar[i]);
                    }
                    else{
                        itemUnlocked.Add(auxiliar[i]);
                    }
                }
            }
            if(word[0] =="MainSkillEquiped"){
                string[] auxiliar = word[1].Split(',');
                _mainSkill1 = auxiliar[0];
                _mainSkill2 = auxiliar[1];
                _mainSkill3 = auxiliar[2];
                _mainSkill4 = auxiliar[3];
                _mainSkill5 = auxiliar[4];
            }
            if(word[0] == "PasiveSkillsEquiped"){
                string[] auxiliar = word[1].Split(',');
                _passiveSkill1 = auxiliar[0];
                _passiveSkill2 = auxiliar[1];
            }
            if(word[0] == "MaskEquiped"){
                _mask = word[1];
            }
            if(word[0] == "MovementEquiped"){
                _movement = word[1];
            }
        }
        file.Close();
        _manager.maskSkill.skillName = _mask;
        _manager.movSkill.skillName = _movement;
        _manager.pasiveSkill1.skillName = _passiveSkill1;
        _manager.pasiveSkill2.skillName = _passiveSkill2;

        _manager.mainSkill1.skillName =_mainSkill1;
        _manager.mainSkill2.skillName=_mainSkill2;
        _manager.mainSkill3.skillName=_mainSkill3;
        _manager.mainSkill4.skillName=_mainSkill4;
        _manager.mainSkill5.skillName=_mainSkill5;
        for(int i = 0; i< _manager.allSlots.Count; i++){
            int index = itemName.IndexOf(_manager.allSlots[i].skillName);
            _manager.allSlots[i].skillName = itemName[index];
            _manager.allSlots[i].unlocked = bool.Parse(itemUnlocked[index]);
        }
        
    }
    public string LoadPlayer(){
        string line;
        string pHealth = "";
        string pMana = "";
        string checkpointID = "";
        StreamReader file = new StreamReader(fullPath);
        while((line = file.ReadLine())!=null){
            string[] word = line.Split(':');
            if(word[0]=="PlayerHealth"){
                pHealth = word[1];
            }
            if(word[0] == "PlayerMana"){
                pMana = word[1];
            }
            if(word[0] == "Checkpoint"){
                checkpointID = word[1];
            }
        }
        file.Close();
        string[] auxiliar = pHealth.Split(',');
        pHealth = auxiliar[0]+".0";
        auxiliar = pMana.Split(',');
        pMana = auxiliar[0]+".0";
        //para asegurar que los floats están en el formato que queremos usamos el System.Globalization.NumberStyles
        //float.TryParse(pHealth, System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("es-ES"), out _resources.health);
        _resources.health = float.Parse(pHealth, System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
        _resources.mana = float.Parse(pMana, System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
//        UnityEngine.Debug.Log("checkpoint3"+checkpointID);
        return checkpointID;
        
    }
    public void LoadActualLevel(){
        string line;
        string stage = "";
        StreamReader file = new StreamReader(fullPath);
        while((line = file.ReadLine())!= null){
            string[] word = line.Split(':');
            if(word[0]=="CurrentLevel"){
                stage = word[1];
                break;
            }
        }
        file.Close();
        currentMap = stage;
    }
}

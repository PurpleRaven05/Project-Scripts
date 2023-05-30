using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public PlayerController controller;
    public Transform inventory;
    private bool inventoryOpen, canMove, movingPasive = false, movingMain = false,movingMask = false,movingMove = false, description = false;
    public Transform itemSelectedTransform, pasiveSelected, mainMask, mainMov, lastItemSelected, inventorySelector;
    public InventorySlot itemSelected;
    public Skill skillSelected;
    public SkillManager skillManager;
    private float delayOnOpen = 0.3f, delayOnMoving = 0.2f;
    private bool delayPassed = true, delayMovePassed = true;
    private float timeDelay1 = 0f, timeDelay2 = 0f;
    public List<InventorySlot> allSlots = new List<InventorySlot>();
    public List<string> allNames = new List<string>();
    public Image moveImage, maskImage, pasive1Image, pasive2Image;
    public InventorySlot mainSkill1, mainSkill2, mainSkill3, mainSkill4, mainSkill5, pasiveSkill1, pasiveSkill2, movSkill, maskSkill;
    public TextMeshProUGUI descriptionText;

    void Awake()
    {
        //controller = transform.Find("Player").GetComponent<PlayerController>();
        //UnityEngine.Debug.Log(controller);
        inventory.gameObject.SetActive(false);
        inventoryOpen = false;
        delayPassed = true;
        GetAllInventorySlots();
        SetAllImages();
        UpdatePlayerSkills();
        //
    }
    void Start(){
        itemSelectedTransform = inventory.transform.GetChild(2).transform.GetChild(0);
        itemSelected = itemSelectedTransform.GetComponent<InventorySlot>();
        itemSelected.active = true;
        skillSelected = skillManager.GetSkill(itemSelected.skillName);
        
        //UnityEngine.Debug.Log(inventory.transform.GetChild(2).transform.GetChild(0));
    }

    void Update()
    {
        UpdateImagesLockedUnlocked();
        UpdatePlayerSkills();
        if(Input.GetButton("OpenInventory")){
            //UnityEngine.Debug.Log("Check1");
            if(controller.onInventorySlate){
                //UnityEngine.Debug.Log("Check2");
                if(delayPassed){
                    if(inventoryOpen){
                        //UnityEngine.Debug.Log("Check3");
                        inventory.gameObject.SetActive(false);
                        inventoryOpen = false;
                        delayPassed = false;
                        controller.canMove = true;
                        canMove = false;
                        controller.dataManager.SaveGameData();
                        //Time.timeScale = 1f;
                    }
                    else{
                        //UnityEngine.Debug.Log("Check4");
                        inventory.gameObject.SetActive(true);
                        inventoryOpen = true;
                        delayPassed = false;
                        controller.canMove = false;
                        canMove = true;
                        //Time.timeScale = 0f;
                    }
                }
                controller.DisableCamera();
            }
        }
        if(!delayPassed){
            if(delayOnOpen <= timeDelay1){
                timeDelay1 = 0f;
                delayPassed = true;
            }
            else{
                timeDelay1+= Time.deltaTime;
            }
        }
        if(!delayMovePassed){
            if(delayOnMoving <= timeDelay2){
                timeDelay2 = 0f;
                delayMovePassed = true;
            }
            else{
                timeDelay2+= Time.deltaTime;
            }
        }
    }
    void FixedUpdate(){
        if(canMove && delayMovePassed){
            if(Input.GetKey(KeyCode.W)){
                MoveUp();
                delayMovePassed = false;
            }
            if(Input.GetKey(KeyCode.A)){
                MoveLeft();
                delayMovePassed = false;
            }
            if(Input.GetKey(KeyCode.S)){
                MoveDown();
                delayMovePassed = false;
            }
            if(Input.GetKey(KeyCode.D)){
                MoveRight();
                delayMovePassed = false;
            }
            
        }
        if(inventoryOpen && delayMovePassed){
            if(Input.GetButton("Jump")){
                if(!itemSelected.equiped){
                    //UnityEngine.Debug.Log("Type: "+skillSelected.Type.ToString());
                    if(movingPasive||movingMain||movingMask||movingMove){
                        ChangeEquipedSkill();
                    }
                    else{
                        lastItemSelected =itemSelectedTransform;
                        CheckAction(skillSelected.Type.ToString());
                    }
                    delayMovePassed = false;
                } 
            }
            if(Input.GetButton("ChangeDescription")){
                ChangeDescription(true);
            }
        }
        if(movingPasive && delayMovePassed){
            if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)){
                MovePasiveLeft();
                delayMovePassed = false;
            }
            if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D)){
                MovePasiveRight();
                delayMovePassed = false;
            }
        }
    }
    #region InventoryActions
    #region BaseMovement
    public void MoveLeft(){
        itemSelected.active = false;
        itemSelectedTransform = itemSelected.itemLeft;
        itemSelected = itemSelectedTransform.GetComponent<InventorySlot>();
        if(itemSelected.unlocked){
            skillSelected = skillManager.GetSkill(itemSelected.skillName);
        }
        else{
            skillSelected = skillManager.GetSkill("None");
        }
        
       ChangeDescription(false);
        
        //UnityEngine.Debug.Log("Description: "+skillSelected.Descritption);
        itemSelected.active = true;
    }
    public void MoveRight(){
        itemSelected.active = false;
        itemSelectedTransform = itemSelected.itemRight;
        itemSelected = itemSelectedTransform.GetComponent<InventorySlot>();
        if(itemSelected.unlocked){
            skillSelected = skillManager.GetSkill(itemSelected.skillName);
        }
        else{
            skillSelected = skillManager.GetSkill("None");
        }

        
        ChangeDescription(false);
        //UnityEngine.Debug.Log("Description: "+skillSelected.Descritption);
        itemSelected.active = true;
    }
    public void MoveUp(){
        itemSelected.active = false;
        itemSelectedTransform = itemSelected.itemUp;
        itemSelected = itemSelectedTransform.GetComponent<InventorySlot>();
        if(itemSelected.unlocked){
            skillSelected = skillManager.GetSkill(itemSelected.skillName);
        }
        else{
            skillSelected = skillManager.GetSkill("None");
        }
        ChangeDescription(false);
        //UnityEngine.Debug.Log("Description: "+skillSelected.Descritption);
        itemSelected.active = true;
    }
    public void MoveDown(){
        itemSelected.active = false;
        itemSelectedTransform = itemSelected.itemDown;
        itemSelected = itemSelectedTransform.GetComponent<InventorySlot>();
        if(itemSelected.unlocked){
            skillSelected = skillManager.GetSkill(itemSelected.skillName);
        }
        else{
            skillSelected = skillManager.GetSkill("None");
        }
        ChangeDescription(false);
        //UnityEngine.Debug.Log("Description: "+skillSelected.Descritption);
        itemSelected.active = true;
    }
    #endregion
    public void MovePasiveLeft(){
        pasiveSelected.GetComponent<InventorySlot>().active = false;
        itemSelectedTransform = pasiveSelected.GetComponent<InventorySlot>().itemLeft;
        pasiveSelected = pasiveSelected.GetComponent<InventorySlot>().itemLeft;
        pasiveSelected.GetComponent<InventorySlot>().active = true;
    }
    public void MovePasiveRight(){
        pasiveSelected.GetComponent<InventorySlot>().active = false;
        itemSelectedTransform = pasiveSelected.GetComponent<InventorySlot>().itemRight;
        pasiveSelected = pasiveSelected.GetComponent<InventorySlot>().itemRight;
        pasiveSelected.GetComponent<InventorySlot>().active = true;
    }
    public void CheckAction(string itemType){
        if(canMove){
            switch(itemType){
            case "Mask":
                itemSelectedTransform = mainMask;
                itemSelectedTransform.GetComponent<InventorySlot>().active = true;
                canMove = false;
                movingMask = true;
            break;
            case"Pasive":
                itemSelectedTransform = pasiveSelected;
                itemSelectedTransform.GetComponent<InventorySlot>().active = true;
                canMove = false;
                movingPasive = true;
            break;
            case"Movement":
                itemSelectedTransform = mainMov;
                itemSelectedTransform.GetComponent<InventorySlot>().active = true;
                canMove = false;
                movingMove = true;
            break;
            case"Main":
            break;
            default:
            break;
            }
        }
        else{
            ReturnToInventory();
        }
        
    }
    public void ReturnToInventory(){
        //UnityEngine.Debug.Log("I'm at returnToInventory");
        movingPasive = false; movingMain = false; movingMask = false; movingMove = false;
        itemSelectedTransform.GetComponent<InventorySlot>().active = false;
        itemSelectedTransform = itemSelected.self;
        canMove = true;
    }
    public void ChangeDescription(bool changeState){
        if(changeState)
            if(description)
                description = false;
            else
                description = true;
        
        string[] descript = skillSelected.Descritption.Split(':');
        if(description){
            descriptionText.text = descript[1];
            
        }  
        else{
            descriptionText.text = descript[0];
            
        }
        delayMovePassed = false;  
    }
    #endregion
    public void ChangeEquipedSkill(){
        DisableEquiped(itemSelectedTransform.GetComponent<InventorySlot>().skillName);
        itemSelectedTransform.GetComponent<InventorySlot>().skillName = itemSelected.skillName;
        //itemSelectedTransform.GetComponent<InventorySlot>().equiped = false;
        itemSelectedTransform.GetComponent<Image>().sprite = lastItemSelected.GetComponent<Image>().sprite;
        itemSelected.equiped = true;
        ReturnToInventory();
        

    }
    public void SwitchMainAction(){

    }
    public void DisableEquiped(string itemName){
        int index = inventorySelector.transform.childCount;
        for(int i = 0; i< index; i++){
            InventorySlot invToCheck = inventorySelector.GetChild(i).GetComponent<InventorySlot>();
            if(invToCheck.skillName == itemName){
                invToCheck.equiped = false;
            }
        }
    }
    public void GetAllInventorySlots(){
        int index = inventorySelector.transform.childCount;
        List<InventorySlot> invList = new List<InventorySlot>();
        for(int i = 0; i< index; i++){
            InventorySlot actualInv = inventorySelector.GetChild(i).GetComponent<InventorySlot>();
            invList.Add(actualInv);
            allNames.Add(actualInv.skillName);
        }
        allSlots = invList;
    }
    public void SetAllImages(){
        //UnityEngine.Debug.Log(pasiveSkill1.GetComponentInParent<Image>() != null);
        Skill auxiliar = SkillDB.GetSkillByName(pasiveSkill1.skillName);
        pasive1Image.sprite = auxiliar.skillSprite;
        auxiliar = SkillDB.GetSkillByName(pasiveSkill2.skillName);
        pasive2Image.sprite = auxiliar.skillSprite;
        auxiliar = SkillDB.GetSkillByName(maskSkill.skillName);
        maskImage.sprite = auxiliar.skillSprite;
        auxiliar = SkillDB.GetSkillByName(movSkill.skillName);
        moveImage.sprite = auxiliar.skillSprite;
        //pasiveSkill1.GetComponentInParent<Image>().sprite = auxiliar.skillSprite;
        //InventorySlot auxiliar2 = allSlots.Find("Hermit");
    }
    public InventorySlot FindItemSlot(string itemName){
        InventorySlot invSlot = null;
        foreach(InventorySlot auxSlot in allSlots){
            if(auxSlot.skillName == itemName){
                invSlot = auxSlot;
            }
        }
        return invSlot;
    }
    public void UnlockItem(string itemName){
        if(allNames.Contains(itemName)){
            InventorySlot updatedSlot = FindItemSlot(itemName);
            updatedSlot.unlocked = true;
            controller.dataManager.SaveGameData();
        }
        
    }
    private void UpdateImagesLockedUnlocked(){
        for(int i = 0; i< allNames.Count; i++){
            InventorySlot updatedSlot = FindItemSlot(allNames[i]);
            if(updatedSlot.unlocked){
                Skill auxiliar = SkillDB.GetSkillByName(allNames[i]);
                updatedSlot.transform.GetComponent<Image>().sprite = auxiliar.skillSprite;
            }
            else{
                Skill auxiliar = SkillDB.GetSkillByName("NoSkill");
                updatedSlot.transform.GetComponent<Image>().sprite = auxiliar.skillSprite;
            }
        }
        SetMainSkillsImages();
    }
    private void SetMainSkillsImages(){
        InventorySlot target = FindItemSlot("Hermit");
        if(target.unlocked){
            Skill auxiliar = SkillDB.GetSkillByName("Hermit");
            
            mainSkill1.transform.GetComponent<Image>().sprite = auxiliar.skillSprite;
        }
        else{
            Skill auxiliar = SkillDB.GetSkillByName("NoSkill");
            mainSkill1.transform.GetComponent<Image>().sprite = auxiliar.skillSprite;
        }
    }
    public void UpdatePlayerSkills(){
         InventorySlot target = FindItemSlot("Hermit");
        if(target.unlocked){
            controller.resources.mainSkill = SkillDB.GetSkillByName("Hermit");
        }
        else{
            controller.resources.mainSkill = SkillDB.GetSkillByName("NoSkill");
        }
        if(pasiveSkill1.skillName == "None"){
            controller.resources.passiveSkills[0] = SkillDB.GetSkillByName("NoSkill");
        }
        else{
            controller.resources.passiveSkills[0] = SkillDB.GetSkillByName(pasiveSkill1.skillName);
        }
        if(pasiveSkill2.skillName == "None"){
            controller.resources.passiveSkills[1] = SkillDB.GetSkillByName("NoSkill");
        }
        else{
            controller.resources.passiveSkills[1] = SkillDB.GetSkillByName(pasiveSkill2.skillName);
        }
        
    }
}

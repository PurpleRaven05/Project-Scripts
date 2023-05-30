using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPresetActivator : MonoBehaviour
{
    public GameObject itemToActivate;
    public List<GameObject> itemsToGetActive;
    public GameObject platform;
    public bool activateItem;
    public bool bossfight, dontNeedPlatform;
    // Start is called before the first frame update
    void Start()
    {
        activateItem = false;
        itemToActivate.SetActive(false);
        if(!dontNeedPlatform){
            platform.SetActive(false);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        CheckItemActive();
        itemToActivate.SetActive(activateItem);
    }
    private void CheckItemActive(){
        foreach(GameObject objeto in itemsToGetActive){
            if(bossfight){
                if(objeto.active == true){
                    activateItem = false;
                return;
            }
            }
            else if(objeto.active == false){
                activateItem = false;
                return;
            }
        }
        activateItem = true;
        if(!dontNeedPlatform){
            platform.SetActive(true);
        }
        
    }
}

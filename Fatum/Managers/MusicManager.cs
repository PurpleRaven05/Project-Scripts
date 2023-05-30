using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioStorer storer;
    private bool AlreadyLoaded;
    public int level;
    private bool musicLoaded = false;
    void Awake(){
        switch(level){
            case 0:
            case 3:
            case 5:
                AudioStorer[] musicObj = GameObject.FindObjectsOfType<AudioStorer>();
                for(int i = 0; i < musicObj.Length; i ++){
                    Destroy(musicObj[i].gameObject);
                }
                /*if(musicObj.Length > 1){
                    storer = musicObj[0].GetComponent<AudioStorer>();
                    AlreadyLoaded = true;
                    Destroy(musicObj[0]);
                }*/
            break;
            case 1:
            case 2:
            case 4:
                GameObject[] musicObj2 = GameObject.FindGameObjectsWithTag("AudioStorer");
                if(musicObj2.Length > 1){
                    storer = musicObj2[0].GetComponent<AudioStorer>();
                    AlreadyLoaded = true;
                }
                else{
                    Instantiate(Resources.Load("MusicManager"),Vector3.zero, Quaternion.identity);
                    GameObject storerObject = GameObject.FindWithTag("AudioStorer");
                    storer = storerObject.GetComponent<AudioStorer>();
                    AlreadyLoaded = false;
                    }
            break;
        }
        
    }
    void Start(){
        switch(level){
            case 0:
            case 3:
            case 5:
                GameObject[] musicObj = GameObject.FindGameObjectsWithTag("AudioStorer");
                if(musicObj.Length > 1){
                    storer = musicObj[0].GetComponent<AudioStorer>();
                    AlreadyLoaded = true;
                    Destroy(musicObj[0]);
                }
            break;
            case 1:
            case 2:
            case 4:
                GameObject[] musicObj2 = GameObject.FindGameObjectsWithTag("AudioStorer");
                if(musicObj2.Length > 1){
                    storer = musicObj2[0].GetComponent<AudioStorer>();
                    AlreadyLoaded = true;
                }
                else{
                    Instantiate(Resources.Load("MusicManager"),Vector3.zero, Quaternion.identity);
                    GameObject storerObject = GameObject.FindWithTag("AudioStorer");
                    storer = storerObject.GetComponent<AudioStorer>();
                    AlreadyLoaded = false;
                    }
            break;
        }
    }
    
}

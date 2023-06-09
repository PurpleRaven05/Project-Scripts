using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStorer : MonoBehaviour
{
    public static AudioSource instance;
    private void Awake(){
       GameObject[] musicObj = GameObject.FindGameObjectsWithTag("AudioStorer");
       if(musicObj.Length > 1){
           Destroy(this.gameObject);
       }
       DontDestroyOnLoad(this.gameObject);
   }
}


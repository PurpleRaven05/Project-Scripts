using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public BossHermitController hermitController;
	public AudioSource source;
	public GameObject MusicHolder, BossObject;
	public bool activated = false;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			hermitController.enabled = true;
			if(!source.isPlaying)
			source.PlayDelayed(3f);
			activated = true;
		}
	}
	void Update(){
		if(activated && !BossObject.activeInHierarchy){
			//MusicHolder.SetActive(false);
			if(source.volume >0){
				source.volume -= 0.001f;
			}
		}
	}
}

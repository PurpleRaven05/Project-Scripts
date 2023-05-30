using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionsItem : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI description;
    public bool PlayerIn;
    public string textToWrite;
    public int timesShowingInfo;
    public AudioSource source;
   
    // Start is called before the first frame update
    void Update()
    {
        if(PlayerIn&&Input.GetButton("Collect")){
                //UnityEngine.Debug.Log("Papoi");
                canvas.gameObject.SetActive(false);
                transform.gameObject.SetActive(false);

            }
    }
    void OnTriggerEnter(Collider other)
	{
        if(timesShowingInfo > 0){
            if (other.tag == "Player") {
			    canvas.gameObject.SetActive(true);
                description.text = textToWrite;
                PlayerIn = true;
                source.Play();
		    }
        }
		
	}
    void OnTriggerExit(Collider other)
	{
        if(timesShowingInfo > 0){
            if (other.tag == "Player") {
			    canvas.gameObject.SetActive(false);
                PlayerIn = false;
                timesShowingInfo--;
		    }
        }
        if(timesShowingInfo <=1){
            this.gameObject.SetActive(false);
        }
		
	}
}

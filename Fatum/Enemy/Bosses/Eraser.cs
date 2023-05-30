using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public float DestroyTimer = 0f, TimeToDestroy = 15f, MinDamageInterval = 1.5f, MaxDamageInterval = 2f;
    public AudioClip clip;
    private bool soundPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyTimer += Time.deltaTime;
        if(DestroyTimer >= 2f && !soundPlayed){
                AudioSource.PlayClipAtPoint(clip, transform.position,0.3f);
                soundPlayed = true;
            }
        if(DestroyTimer >=MinDamageInterval && DestroyTimer <MaxDamageInterval){
            
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);
            foreach(Collider collider in colliders){
                if(collider.GetComponent<ThunderSlate>()){
                    UnityEngine.Debug.Log("SlateActivate");
                    collider.GetComponent<ThunderSlate>().Activate();
                }
                else if(collider.GetComponent<PlayerResources>()){
                    collider.GetComponent<PlayerResources>().TakeDamage(20f);
                    Destroy(this.gameObject);
                } 
            }
        }
            
        if(DestroyTimer >= TimeToDestroy){
            Destroy(this.gameObject);
        }
        
            
    }
}

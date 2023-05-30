using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBall : MonoBehaviour
{
    public float maxSpeed = 5f, time = 10f;
    public Transform player;
    public Transform boss;
    private Transform targetTransform;
    private bool TargetingPlayer, TargetingBoss;
    public AudioClip rock;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        boss = GameObject.Find("Boss").GetComponent<Transform>();
        targetTransform = player;
        TargetingPlayer = true;
        TargetingBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        //selfBody.MovePosition(player.position);
    }
    void FixedUpdate(){
        Vector3 a = transform.position;
        Vector3 b = targetTransform.position;
        if(TargetingPlayer){
            b = new Vector3(targetTransform.position.x, targetTransform.position.y+1f, targetTransform.position.z);
        }
        if(TargetingBoss){
             b = new Vector3(targetTransform.position.x, targetTransform.position.y+6f, targetTransform.position.z);
        }
        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,time), maxSpeed);
        transform.LookAt(b);
    }
    void OnCollisionEnter(Collision collision){
        if(collision.collider.transform.GetComponent<MirrorWall>()){
            AudioSource.PlayClipAtPoint(rock, transform.position, 0.3f);
            targetTransform = boss;
            TargetingPlayer = false;
            TargetingBoss = true;
        }
        if(collision.collider.transform.GetComponent<PlayerResources>()){
            if(TargetingPlayer){
                AudioSource.PlayClipAtPoint(rock, transform.position, 0.3f);
                collision.collider.transform.GetComponent<PlayerResources>().TakeDamage(20f);
                Destroy(this.gameObject);
            }
            
        }
        if(collision.collider.transform.GetComponent<BossHermitController>()){
            if(TargetingBoss){
                AudioSource.PlayClipAtPoint(rock, transform.position, 0.3f);
                collision.collider.transform.GetComponent<BossHermitController>().TakeHit();
                Destroy(this.gameObject);
            }
            
        }
        else{
            if(TargetingPlayer){
                AudioSource.PlayClipAtPoint(rock, transform.position, 0.3f);
                Destroy(this.gameObject);
            }
                
        }
    }
}

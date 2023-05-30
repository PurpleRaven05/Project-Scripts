using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoutines : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyView POV;
    public List<Transform> patrolPoints;
    public Transform playerTransform, projectileStart, BackWards;
    private Transform lastWayPoint;
    private Rigidbody selfBody;
    public ResourcesManager resources;
    public Animator animator;
    public float timeBetweenPatrols = 5f, timeToReturnPatrol = 
    2f, timerPatrol = 0, timerSearch = 0, reloadTimer = 0, loadTime = 0.25f, 
    reloadSpeed = 3f, shotingDistance = 10f, maxRange = 20f, shotTimer;
    public bool miniStop = false, patroling = false, isInAnimation = false,
     chasing = false, searching = false, 
     playerDetected = false, shootReady = false, posibleShot = false, loadingShot = false;
    private int index = 0;
    public LayerMask obstacle;
    public AudioSource moveSource, detectSource, attackSource;
    // Start is called before the first frame update
    void Start()
    {
        patroling = true;
        agent.SetDestination(patrolPoints[index].position);
        selfBody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
                
        //Debug.Log(agent.speed);
        Debug.DrawLine(transform.position, playerTransform.position, Color.blue);
        DetectPlayer();
        if(playerDetected){
            FollowPlayer();
        }
        else if(patroling)
            Patrol();

        posibleShot = playerDetected && CheckAttackRange();
        if(posibleShot&&!isInAnimation)
            Shoot();
        ManageTimers();
        animator.SetFloat("Speed",agent.speed);
    }
    private void DetectPlayer(){
        if(POV.playerDetected){

            //UnityEngine.Debug.Log(Physics.Raycast(transform.position, playerTransform.position, 100f, obstacle));
            RaycastHit hit = new RaycastHit();
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            Physics.Raycast(transform.position, playerTransform.position, out hit, distance);
            //if(hit.collider.gameObject.tag == "Player"){
                if(!playerDetected)
                    detectSource.Play();
                playerDetected = true;
                
                
            //}    
        }
        else{
            playerDetected = false;
            if(!patroling){
                miniStop = true;
                patroling = true;
            }
                
        }
    }
    private void Shoot(){
        if(shootReady){
            isInAnimation = true;
            StartCoroutine(stopAnimation(1f));
            StartCoroutine(WeildAttack(0.5f));
            //Instantiate(Resources.Load("Projectile") as GameObject, projectileStart.position, Quaternion.identity);
            shootReady = false;
            
            
            
        }
        else{
        }
        
    }
    private void Patrol(){
        agent.isStopped = false;
        if(!miniStop){
            moveSource.Play();
            if(agent.remainingDistance <= agent.stoppingDistance){
                agent.isStopped = true;
                agent.speed = 0;
                index ++;
                miniStop = true;
                if(index >= patrolPoints.Count)
                    index = 0;
            
            }
            else{
                agent.isStopped = false;
                agent.speed =3.5f;
            }
        }
    }
    private void FollowPlayer(){
        lastWayPoint = patrolPoints[index];
        patroling = false;
        chasing = true;
        transform.LookAt(playerTransform);
        agent.SetDestination(playerTransform.position);
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if(distance < shotingDistance){
            moveSource.Play();
            agent.SetDestination(BackWards.position);
            agent.speed = 6.5f;
            isInAnimation = true;
            agent.isStopped = false;
            selfBody.isKinematic = false;
        }
        if(distance >= shotingDistance && distance < maxRange){
            agent.speed = 0f;
            agent.isStopped = true;
            isInAnimation = false;
            selfBody.isKinematic = true;
        }
        else{
            moveSource.Play();
            agent.speed = 4.5f;
            agent.isStopped = false;
            selfBody.isKinematic = false;
            isInAnimation = true;
        }
            

    }
    private void ReturnToPatrol(){
        
        agent.SetDestination(lastWayPoint.position);
    }
    private bool CheckAttackRange(){
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        return (distance < maxRange && distance >= shotingDistance);
    }
    private void ManageTimers(){

        if(miniStop){
            animator.SetFloat("Speed",0);
            timerPatrol+=Time.deltaTime;
            if(timerPatrol >timeBetweenPatrols){
                timerPatrol = 0f;
                miniStop = false;
                agent.SetDestination(patrolPoints[index].position);
                transform.LookAt(patrolPoints[index]);
            }
        }
        if(!playerDetected && chasing){
            timerSearch += Time.deltaTime;
            if(timerSearch > timeToReturnPatrol){
                timerSearch = 0f;
                chasing = false;
                agent.SetDestination(lastWayPoint.position);
            }
        }
        if(!shootReady){
            if(reloadTimer >= reloadSpeed){
                reloadTimer = 0f;
                shootReady = true;
            }
            else
                reloadTimer += Time.deltaTime;
        }
    }
    public IEnumerator stopAnimation(float length)
	{
		yield return new WaitForSeconds(length); 
		isInAnimation = false;
	}
    public IEnumerator WeildAttack(float length)
	{
        animator.SetTrigger("Attack");
        attackSource.Play();
		yield return new WaitForSeconds(length); 
        animator.SetTrigger("ReturnToIdle");
        Instantiate(Resources.Load("Projectile") as GameObject, projectileStart.position, Quaternion.identity);
		isInAnimation = false;
	}
}

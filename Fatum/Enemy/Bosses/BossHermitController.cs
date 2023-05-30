using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHermitController : MonoBehaviour
{
    public List<Transform> TeleportZones = new List<Transform>();
    public Transform playerTransform, groundTransform, projectileCreator;
    private Transform Attack;
    public GameObject bossArmor1, bossArmor2;
    public Animator bossAnimator;
    public int hits = 3;
    public int maxShots = 1, attacksWithoutRock = 0;
    private int remainingShots;
    private float TimeBetweenAttacks = 5f, TimeBetweenTeleports = 15f, TimerTeleport = 0, TimerAttack =0, TimerOnAttack = 0, TimeAttacking = 1f, distanceWithPlayer, randomProb, animationShots, TimerDeath, TimerToDie = 2.7f;
    private bool Teleporting, Attacking, AttackSecuence, TeleportOnCooldown, AttacksOnCooldown, isDead, lightning, changeAttack;
    private int TeleportIndex = -1;
    public AudioSource damageRec, launch, castThunder, teleport, die;

    // Start is called before the first frame update
    void Start()
    {
        bossAnimator.SetBool("IsDead",false);
        TeleportOnCooldown = true;
        AttacksOnCooldown = true;
        Teleporting = false;
        Attacking = false;
        isDead = false;
        changeAttack = true;
        UpdatePhase();
        animationShots = maxShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead){
            transform.LookAt(playerTransform);
            if(!Attacking){
                if(TeleportOnCooldown){
                    TimerTeleport += Time.deltaTime;
                if(TimerTeleport >=TimeBetweenTeleports){
                    TimerTeleport = 0f;
                    TeleportOnCooldown = false;
                }
            

            }
                else{
                    Teleporting = true;
                    Teleport();
                }
            }
            if(!Teleporting){
                if(AttacksOnCooldown){
                    TimerAttack +=Time.deltaTime;
                    if(TimerAttack >= TimeBetweenAttacks)
                    {
                        TimerAttack = 0f;
                        AttacksOnCooldown = false;
                        remainingShots = maxShots;
                        animationShots = maxShots;
                        Attacking = true;
                    }
                }
                if(Attacking){
                    float rng = Random.Range(0f,1f);
                    if(changeAttack){
                        if(rng <=randomProb){
                            lightning = false;
                        }
                        else{
                            lightning = true;
                        }
                        changeAttack = false;
                    }
                    
                    TimerOnAttack += Time.deltaTime;
                    if(TimerOnAttack >= TimeAttacking){
                        TimerOnAttack = 0;
                        AttackCast();
                        
                    }
                    else{
                        if(remainingShots < 1){
                            Attacking = false;
                            AttacksOnCooldown = true;
                            changeAttack = true;
                            
                        }
                    }
                    
                }
            }
            distanceWithPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if(distanceWithPlayer <= 20f){
                Teleport();
                TimerTeleport = 0f;
                TeleportOnCooldown = true;
            }
        }
        else{
            if(TimerDeath >= TimerToDie ){
                transform.gameObject.SetActive(false);
            }
            else{
                TimerDeath+= Time.deltaTime;
            }
        }
        CheckDeath();
        
    }
    public void Teleport(){
        int index;
        do{
            index = Random.Range(0, TeleportZones.Count);
        }while(index == TeleportIndex);

        teleport.Play();
        transform.position = TeleportZones[index].position;
        TeleportIndex = index;
        TeleportOnCooldown = true;
        Teleporting = false;
    }
    private void AttackCast(){
        if(lightning){
            
            AttackLightning();
            attacksWithoutRock++;
        }
        else{
            AttackBall();
            attacksWithoutRock = 0;
        }
    }
    public void AttackLightning(){
        StartCoroutine(SetLapse(TimeAttacking));
        Vector3 realPosition = new Vector3(0,0,0);
        Attack = playerTransform;
        realPosition = new Vector3(Attack.position.x, groundTransform.position.y, Attack.position.z);
        castThunder.Play();
        Instantiate(Resources.Load("LightningCombo") as GameObject, realPosition, Quaternion.identity);
        //Attacking = false;
        remainingShots --;
        //bossAnimator.SetTrigger("ReturnToIdle");
    }
    public void AttackBall(){
        StartCoroutine(RockLaunch(1.3f));
        
    }
    public void TakeHit(){
        if(hits >0){
            if(hits > 1)
                damageRec.Play();
            else{
                die.Play();
            }
            hits--;
            Teleport();
        }
        UpdatePhase();
    }
    private void CheckDeath(){
        if(hits <= 0){
            isDead = true;
            StopAllCoroutines();
            
            bossAnimator.SetTrigger("Die");
            bossAnimator.SetBool("IsDead", true);
        }
    }
    private void UpdatePhase(){
        switch(hits){
            case 3:
                maxShots = 4;
                if(attacksWithoutRock >= 24){
                    randomProb = 1f;
                }
                else{
                    randomProb = 0.35f;
                }
                
                bossArmor1.SetActive(true);
                bossArmor2.SetActive(true);
            break;
            case 2:
                maxShots = 5;
                if(attacksWithoutRock >= 20){
                    randomProb = 1f;
                }
                else{
                    randomProb = 0.45f;
                }
                bossArmor1.SetActive(false);
            break;
            case 1:
                maxShots = 6;
                if(attacksWithoutRock >= 18){
                    randomProb = 1f;
                }
                else{
                randomProb = 0.7f;
                }
                bossArmor2.SetActive(false);
            break;
            default:
                maxShots = 0;
            break;
        }
    }

    public IEnumerator SetLapse(float lapse){
        bossAnimator.SetTrigger("Lightning");
        yield return new WaitForSeconds(lapse);
        if(animationShots >1){
            bossAnimator.SetBool("UsingChain",true);
        }
        else{
            bossAnimator.SetBool("UsingChain",false);
        }
        bossAnimator.SetTrigger("ReturnToIdle");
        animationShots --;
    }
    public IEnumerator RockLaunch(float lapse){
        bossAnimator.SetTrigger("Rock");
        remainingShots = 0;
        yield return new WaitForSeconds(lapse/2);
        launch.Play();
        Instantiate(Resources.Load("BossBall") as GameObject, projectileCreator.position, Quaternion.identity);
        yield return new WaitForSeconds(lapse/2);
        bossAnimator.SetTrigger("ReturnToIdle");
    }
}

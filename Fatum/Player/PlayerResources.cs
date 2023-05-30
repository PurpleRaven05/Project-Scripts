using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerResources : MonoBehaviour
{
    public float health, maxHealth = 100f;
    public float mana, maxMana = 100f;
    private float lerpSpeed;
    public Transform lookAt;
    public Transform self;
    public Skill mainSkill;
    public Skill mask;
    public Skill movementSkill;
    public Skill[] passiveSkills = new Skill[2];
    private float timer =0, timeToRecoverMana = 1f;
    public Material healthMaterial;
    public Material manaMaterial;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        mana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > maxHealth) health = maxHealth;
        if(mana > maxMana) mana = maxMana;
        lerpSpeed = 3f* Time.deltaTime;
        //BarFillers();
        ColorChanger();
        if(timer >= timeToRecoverMana){
            ConsumeMana(-5f);
            timer = 0f;
        }
        else{
            timer+= Time.deltaTime;
        }
        
    }
    void FixedUpdate(){
        //FaceCamera();
    }
    void ColorChanger(){
        Color healthColor = Color.Lerp(Color.red, Color.green,(health/maxHealth));
        Color manaColor = Color.Lerp(Color.white, Color.blue,(mana/maxMana));
        healthMaterial.color = healthColor;
        healthMaterial.SetColor("_EmissionColor", healthColor);
        manaMaterial.color = manaColor;
        manaMaterial.SetColor("_EmissionColor", manaColor);
    }
    public void TakeDamage(float quantity){
        UnityEngine.Debug.Log("Damage!" + quantity);
        if(health > 0 && health <= maxHealth){
            health -= quantity;
            if(quantity > 0){
                source.pitch = Random.Range(0.8f, 1.1f);
                source.Play();
            }
            
        }
    }
    public void ConsumeMana(float quantity){
        if(mana>=0&& mana <= maxMana){
            UnityEngine.Debug.Log("Mana used "+quantity);
            mana -= quantity;
        }
    }
    
    
    
}

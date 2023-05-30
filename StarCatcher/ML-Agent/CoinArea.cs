using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinArea : MonoBehaviour
{
    public int numberOfPickups{get; private set;}
    private int totalPickups;
    public GameObject[] coins;
    public bool[] coinVisited;
    public GameObject[] walls;
    public GameObject[] bounds;
    private float timer=0f;
    private float timer2=0f;
    public bool trainingMode = false;


    void Awake () {

		numberOfPickups = GameObject.FindGameObjectsWithTag ("pickup").Length;
        coins = GameObject.FindGameObjectsWithTag ("pickup");
        walls = GameObject.FindGameObjectsWithTag("Wall");
        bounds = GameObject.FindGameObjectsWithTag("Boundary");
        coinVisited = new bool[numberOfPickups];
        totalPickups = numberOfPickups;

        for(int i =0; i< numberOfPickups; i++){
            coinVisited[i]=false;
        }
	}
    public void CollectCoin(Collider2D coin){
        coin.gameObject.SetActive(false);
        if(!trainingMode){
            numberOfPickups--;
        }
        
    }
    void ResetCoins(){
        if(timer >= 300f){
            timer = 0f;
            foreach(GameObject coin in coins){
                coin.SetActive(true);
            }
        }
    }
    void ResetCoinsVisited(){
        if(timer >= 900f){
            timer = 0f;
            for(int i = 0; i<coinVisited.Length; i++){
                coinVisited[i]= false;
            }
        }
    }
    void Update(){
        if(trainingMode){
            ResetCoins();
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;
        }
        
    }
    public int FindCoinIndex(Collider2D coin){
        for(int i = 0; i<totalPickups; i++){
            if(coins[i]==coin.gameObject){
                return i;
            }
        }
        return -1;
    }
    
}

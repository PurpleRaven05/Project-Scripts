using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class CollectCoinsAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    private Rigidbody2D rb;
    private CoinArea coinArea;
    private GameObject nearestCoin;
    private GameObject lastCoinObtained;
    private GameObject nearestWall;
    private GameObject nearestBound;
	public float speed;
    private float timeWithoutColliding = 0f, timeSinceLastCoinCollected =0f, rewardModifier =1f;
    public Camera agentCamera;
    public bool trainingMode;
    private bool frozen =false;
    public int coinsObtained {get; private set;}


    public override void Initialize(){
        rb =GetComponent<Rigidbody2D>();
        coinArea = GetComponentInParent<CoinArea>();
        if(!trainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin(){
        if(trainingMode){
            coinArea.trainingMode = true;
        }

        rb.velocity= Vector2.zero;
        rb.angularVelocity = 0f;

        bool inFrontOfCoin = true;

        if(trainingMode){
            inFrontOfCoin = UnityEngine.Random.value > .5f;
        }
        //MoveToSafeRandomPosition(inFrontOfCoin);

        UpdateNearestCoin();
        UpdateNearestWall();
        UpdateNearestBound();
    }
    
    //It can go Up or Down(1) and Right or Left(2)
   public override void OnActionReceived(float[] vectorAction){
       if(frozen) return;

       Vector2 move = new Vector2(vectorAction[0],vectorAction[1]);

       rb.AddForce(move*speed);
   }


   public override void CollectObservations(VectorSensor sensor)
    {
        if(nearestCoin == null){
           sensor.AddObservation(new float[9]);
           return; 
        }
        sensor.AddObservation(transform.position);
        
        Vector2 toCoin = nearestCoin.transform.position - transform.position;
        sensor.AddObservation(toCoin.normalized);

        Vector2 toWall = nearestWall.transform.position - transform.position;

        sensor.AddObservation(toWall.normalized);

        Vector2 toBound = nearestBound.transform.position - transform.position;

        sensor.AddObservation(toBound.normalized);
    }
    public override void Heuristic(float[] actionsOut)
    {
        Vector2 forward = Vector2.zero;
        Vector2 left = Vector2.zero;
        if(Input.GetKey(KeyCode.W)) forward = transform.up;
        else if(Input.GetKey(KeyCode.S)) forward = -transform.up;
        else if(Input.GetKey(KeyCode.A)) left = -transform.right;
        else if(Input.GetKey(KeyCode.D)) left = transform.right;

        Vector2 combined = (forward + left).normalized;
        actionsOut[0] = combined.x;
        actionsOut[1] = combined.y;
    }

    public  void FreezeAgent(){
        UnityEngine.Debug.Assert(trainingMode == false, "freeze/unfreeze no disponible en entrenamiento");
        frozen = true;
        rb.Sleep();
    }
    public void UnfreezeAgent(){
        UnityEngine.Debug.Assert(trainingMode == false, "freeze/unfreeze no disponible en entrenamiento");
        frozen = false;
        rb.WakeUp();
    }

    public void MoveToSafeRandomPosition(bool inFrontOfCoin){
        bool safePositionFound = false;
        int attemptsRemaining = 100;
        Vector2 potentialPosition = Vector2.zero;

        while(!safePositionFound && attemptsRemaining > 0){
            attemptsRemaining --;
            if(inFrontOfCoin){
                GameObject randomCoin = coinArea.coins[UnityEngine.Random.Range(0, coinArea.coins.Length)];
                float distanceFromCoin = UnityEngine.Random.Range(0.5f, 1f);
                potentialPosition = randomCoin.transform.position * distanceFromCoin;
            }
            else{
                float radius = UnityEngine.Random.Range(1f, 2f);
                potentialPosition = coinArea.transform.position * radius;
            }
            Collider2D collider = Physics2D.OverlapCircle(potentialPosition, 0.5f);

            safePositionFound = collider == null;

        }
        UnityEngine.Debug.Assert(safePositionFound, "No se ha encontrado posición segura de Spawn");
        transform.position=potentialPosition;
    }

    private void UpdateNearestCoin(){
        foreach(GameObject coin in coinArea.coins){
            if(nearestCoin == null && coin.activeSelf){
                nearestCoin = coin;
            }
            else if(coin.activeSelf){
                float distanceToCurrentCoin = Vector2.Distance(nearestCoin.transform.position, transform.position);
                float distanceToCoin = Vector2.Distance(coin.transform.position, transform.position);
                if(!nearestCoin.activeSelf || distanceToCoin <distanceToCurrentCoin){
                    nearestCoin = coin;
                   
                }
            }
        }
    }

    private void UpdateNearestWall(){
        foreach(GameObject wall in coinArea.walls){
            if(nearestWall == null && wall.activeSelf){
                nearestWall = wall;
            }
            else if(wall.activeSelf){
                float distanceToCurrentWall = Vector2.Distance(nearestWall.transform.position, transform.position);
                float distanceToWall = Vector2.Distance(wall.transform.position, transform.position);
                if(!nearestWall.activeSelf || distanceToWall <distanceToCurrentWall){
                    nearestWall = wall;
                   
                }
            }
        }
    }

    private void UpdateNearestBound(){
        foreach(GameObject bound in coinArea.bounds){
            if(nearestBound == null && bound.activeSelf){
                nearestBound = bound;
            }
            else if(bound.activeSelf){
                float distanceToCurrentBound = Vector2.Distance(nearestBound.transform.position, transform.position);
                float distanceToBound = Vector2.Distance(bound.transform.position, transform.position);
                if(!nearestBound.activeSelf || distanceToBound <distanceToCurrentBound){
                    nearestBound = bound;
                   
                }
            }
        }
    }
    private void ResetCoinsVisited(){
        bool allCoinsVisited = true;
        foreach(bool coin in coinArea.coinVisited){
            allCoinsVisited = coin;
            if(coin == false){
                break;
            }
        }
        if(allCoinsVisited){
            AddReward(10f);
            UnityEngine.Debug.Log("All Coins Collected!");
            for(int index = 0; index <coinArea.coinVisited.Length; index++){
                coinArea.coinVisited[index] = false;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other){
        TriggerEnterOrStay(other);
    }
    private void OnTriggerStay2D(Collider2D other){
        TriggerEnterOrStay(other);
    }

    private void TriggerEnterOrStay(Collider2D collider){
        //¿Colisiona con moneda?
        if(collider.CompareTag("pickup")){
            coinsObtained++;
            int indexOfCoin = coinArea.FindCoinIndex(collider);
            //UnityEngine.Debug.Log("coinVisited:"+indexOfCoin);
            coinArea.CollectCoin(collider);

            if(trainingMode){
                if(lastCoinObtained != null && lastCoinObtained != collider.gameObject){
                    AddReward(1f*rewardModifier);
                }
                else{
                    AddReward(0.5f*rewardModifier);
                }
                //UnityEngine.Debug.Log("coinVisited2:"+coinArea.coinVisited[indexOfCoin]);
                if(!coinArea.coinVisited[indexOfCoin]){
                    AddReward(1f);
                    coinArea.coinVisited[indexOfCoin]=true;
                }
                timeSinceLastCoinCollected = 0f;
                
                
            }
            ResetCoinsVisited();
            UpdateNearestCoin();
            UpdateNearestWall();
            UpdateNearestBound();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(trainingMode && collision.collider.CompareTag("Boundary")){
            AddReward(-0.6f);
            timeWithoutColliding = 0f;
        }

        if(trainingMode && collision.collider.CompareTag("Wall")){
            AddReward(-0.15f);
        }
    }
    private void Update(){
        if(trainingMode && timeWithoutColliding <= 10f){
            AddReward(0.015f);
        }
       /* if(trainingMode && timeSinceLastCoinCollected >=15){
            AddReward(-0.01f);
        }
        else{
            if(trainingMode && timeSinceLastCoinCollected >=5){
                AddReward(-0.005f);
            }
        }
        if(trainingMode &&(Mathf.Abs(rb.velocity.x)+Mathf.Abs(rb.velocity.y)>25f)){
            AddReward(0.02f);
            UnityEngine.Debug.Log("Speed more than 25");
        }
        if(trainingMode && timeSinceLastCoinCollected<0.75f){
            rewardModifier = 2f;
        }
        else{
            if(trainingMode && timeSinceLastCoinCollected<1.25f){
                rewardModifier = 1.5f;
            }
            else{
                rewardModifier = 1f;
            }
        }*/
        if(nearestCoin != null){
            UnityEngine.Debug.DrawLine(transform.position, nearestCoin.transform.position, Color.green);
        }
        if(nearestWall != null){
            UnityEngine.Debug.DrawLine(transform.position, nearestWall.transform.position, Color.red);
        }
        if(nearestBound != null){
            UnityEngine.Debug.DrawLine(transform.position, nearestBound.transform.position, Color.blue);
        }
        //UnityEngine.Debug.Log("Vel:"+ Mathf.Abs(rb.velocity.x)+Mathf.Abs(rb.velocity.y));
        
        UpdateNearestCoin();
        UpdateNearestWall();
        UpdateNearestBound();
        timeWithoutColliding += Time.deltaTime;
        timeSinceLastCoinCollected +=Time.deltaTime;
    }
}

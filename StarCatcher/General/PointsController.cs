using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsController : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI BotText;
    public TextMeshProUGUI PlayerText;
    public CollectCoinsAgent jugador;
    public CollectCoinsAgent bot;
    public CoinArea area;
    public LevelManager manager;
    

    // Update is called once per frame
    void Update()
    {
        BotText.text = "BotPoints: "+bot.coinsObtained;
        PlayerText.text = "PlayerPoints: "+jugador.coinsObtained;
        checkEndGame();
    }
    public void checkEndGame(){
        if(area.numberOfPickups <1){
            if(bot.coinsObtained<jugador.coinsObtained){
                manager.LoadWin();
            }
            else if(bot.coinsObtained>jugador.coinsObtained){
                manager.LoadLose();
            }
        }
        
    }
}

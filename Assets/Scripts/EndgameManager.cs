using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameType
{
    Moves, 
    Time
}

[System.Serializable]
public class EndgameRequirements
{
    public GameType gameType;
    public int counterValue;
}
public class EndgameManager : MonoBehaviour
{
    public GameObject movesLabel;
    public GameObject timeLabel;
    public GameObject youWinLabel;
    public GameObject tryAgainPanel;
    public Text counter;
    public EndgameRequirements requirements;
    public int currentCounterValue;
    private Board board;
    private float timerSecond;
    
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        SetupGame(); 
    }

    void SetupGame()
    {

        currentCounterValue = requirements.counterValue;
        if (requirements.gameType == GameType.Moves)
        {
            movesLabel.SetActive(true);
            timeLabel.SetActive(false);
        }
        else
        {
            timerSecond = 1;
            movesLabel.SetActive(false);
            timeLabel.SetActive(true);
        }
        counter.text = "" + currentCounterValue;
    }

    public void DecreaseCounterValue()
    {
        if (board.currentState != GameState.pause)
        {
            currentCounterValue--;
            counter.text = "" + currentCounterValue;
            if (currentCounterValue <= 0)
            {
                LoseGame();
            }
        }    
    }

    public void WinGame()
    {
        youWinLabel.SetActive(true);
        board.currentState = GameState.win;
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    public void LoseGame()
    {
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        Debug.Log("You Lose!");
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (requirements.gameType == GameType.Time && currentCounterValue > 0)
        {
            timerSecond -= Time.deltaTime;
            if (timerSecond <= 0)
            {
                DecreaseCounterValue();
                timerSecond = 1;
            }
        }
    }
}

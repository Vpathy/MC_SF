
/***********************************************************************************************
 * Game Manager is the main manager of the script that controls the gameloop for the entire Game. 
 * GameManager intitalizes all the other managers and it contains a satemachine for the game flow
 ************************************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : GenericSingleton<GameManager>
{

    #region public
    public GameState curr_GameState;
    public UnityAction OnTimerComplete;
    public UnityAction OnTimerStart;
    public int TotalDuration = 60;
    public int Score = 0;
    public int HighScore = 0;
    int prev_score = 0;
    public int CountDownDuration = 3;
    public int RemainingDuration = 1;
    public int CorrectScore = 100;
    public int WrongScore = -200;
    public int currt_input_value = 0;
    public ShapeScriptableObjects curr_shape;
    #endregion

    // Start is to intialize all the managers, TODO: Create a loading bar based on manger's intialization
    void Start()
    {
        EventManager.Instance.Init();
        TimeManager.Instance.Init();
        MenuManager.Instance.Init();
        SpwanManager.Instance.Init();
        InputManager.Instance.Init();
        SfxManager.Instance.Init();
      Instance.Init();
       
      
    }
    // Playerprefs is used for saving and loading highscore. TODO: Binary format
    void Init()
    {
        // curr_GameState = GameState.Begin;
        if(PlayerPrefs.HasKey(CName.HighscoreKey))HighScore = PlayerPrefs.GetInt(CName.HighscoreKey);
        
    }

   

    public void InitGameState()
    {
        UpdateGameState(GameState.Begin);
    }

    void GameplayState()
    {
        UpdateGameState(GameState.Gameplay);
    }

    void EndGameState()
    {
        UpdateGameState(GameState.End);
    }

    private void OnDestroy()
    {
        if(Score > HighScore)PlayerPrefs.SetInt(CName.HighscoreKey, Score);
    }


    // GameLoop
    //Update is the main loop and flow of the game. TODO: Extract Methods to simplify
    // 
    void Update()
    {
        if (curr_GameState != GameState.Selection)
        {
            
            RemainingDuration = (int)TimeManager.Instance?._timer._duration;
            MenuManager.Instance.updateUI();
            if (curr_GameState == GameState.Gameplay)
            {
                InputManager.Instance.PlayerInputUpdate();
            }
            if (curr_GameState == GameState.Gameplay || curr_GameState == GameState.Begin)
            {
                if (RemainingDuration <= 0)
                {
                    EndGameState();
                }
                else
                    TimeManager.Instance.TimerUpdate();
            }

            if (RemainingDuration == TotalDuration && curr_GameState != GameState.Gameplay)
            {

                GameplayState();
            }
        }
        
       
    }

    // State machine update for the game

    public void UpdateGameState(GameState NewState)
    {
        curr_GameState = NewState;
        switch (NewState)
        {
            case GameState.Begin:
                EventManager.TriggerEvent(CName.gameStart);
                break;
            case GameState.Gameplay:
                EventManager.TriggerEvent(CName.countdownEnd);
                break;
            case GameState.End:
                EventManager.TriggerEvent(CName.gameEND);
                break;
        }

    }



    //Validates userinput with the shown shape value to see if the answer is correct

    public void ValidateInput()
    {

        if(currt_input_value == curr_shape.value)
        {
            Score += 100;
            EventManager.TriggerEvent(CName.correct);
        }
        else
        {
            Score -= 200;
            EventManager.TriggerEvent(CName.wrong);
        }
    }


}
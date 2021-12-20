using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : GenericSingleton<GameManager>
{

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

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Init();
        TimeManager.Instance.Init();
        MenuManager.Instance.Init();
        SpwanManager.Instance.Init();
        InputManager.Instance.Init();
      Instance.Init();
       
      
    }

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


    public void ValidateInput()
    {

        if(currt_input_value == curr_shape.value)
        {
            Score += 100;
        }
        else
        {
            Score -= 200;
        }
    }


}
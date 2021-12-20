using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : GenericSingleton<MenuManager>
{
    public Canvas GameplayCanvas;
    public Canvas CountdownCanvas;
    public Canvas TitleCanvas;
    public Canvas CompleteCanvas;
    public Text TimerTxt;
    public Text CountDownTxt;
    public Image ShapeUI;
    public Text ScoreTxt;
    public Text HighScoreTxt;
    public void Init()
    {
        EventManager.AddListener(CName.gameStart, ShowCountDownUI);
        EventManager.AddListener(CName.gameStart, HideTitleUI);
        EventManager.AddListener(CName.countdownEnd, ShowGamePlayUI);
        EventManager.AddListener(CName.countdownEnd, HideCountDownUI);
        EventManager.AddListener(CName.gameEND, HideGamePlayUI);
        EventManager.AddListener(CName.gameEND, ShowGameOverUI);
        EventManager.AddListener(CName.newShape, LoadUIShape);

    }


    public void LoadUIShape()
    {
        ShapeUI.sprite = GameManager.Instance?.curr_shape.Shape.ShapeSprite;
    }


    public void HideTitleUI()
    {
        TitleCanvas.gameObject.SetActive(false);
    }
    public void ShowTitleUI()
    {
        TitleCanvas.gameObject.SetActive(true);

    }


    public void ShowGameOverUI()
    {
        CompleteCanvas.gameObject.SetActive(true);
    }


    public void HideCountDownUI()
    {
        CountdownCanvas.gameObject.SetActive(false);
    }
    public void ShowGamePlayUI()
    {
        GameplayCanvas.gameObject.SetActive(true);

    }

    public void HideGamePlayUI()
    {
        GameplayCanvas.gameObject.SetActive(false);

    }

    public void ShowCountDownUI()
    {
        CountdownCanvas.gameObject.SetActive(true);
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void Quit()
    {
        Application.Quit();
    }


    public void updateUI()
    {
        if(GameManager.Instance?.curr_GameState == GameState.Gameplay)
        {
            TimerTxt.text = GameManager.Instance?.RemainingDuration.ToString("00");
            ScoreTxt.text = GameManager.Instance?.Score.ToString("");
            HighScoreTxt.text = GameManager.Instance?.HighScore.ToString();
        }

        if(GameManager.Instance?.curr_GameState == GameState.Begin)
        {
            CountDownTxt.text = (GameManager.Instance.CountDownDuration - ((GameManager.Instance.TotalDuration + GameManager.Instance.CountDownDuration) - GameManager.Instance.RemainingDuration)).ToString("00");
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    public GameObject ghost;
    public Text scoreText;
    public Text highScoreText;
    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelGameOver;
    public Slider lifeSlider;
    

    public static GameManager Instance { get; private set; }

    public enum State {MENU, INIT, PLAY, GAMEOVER}
    //init state - this is in between state o restart score
    State _state;

    private int _lifePoints;
    private int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; 
        scoreText.text = "Score: " + _score;
        }
    }

   public int Life
    {
        get { return _lifePoints; }
        set
        {
            _lifePoints = value;
            lifeSlider.value = _lifePoints;
        }
    }


    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }


    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
    }

   
    public void SwitchState(State newState)
    {
        EndState();
        _state = newState;
        BeginState(newState); 
    }


    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true;
                highScoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore"); 
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                SwitchState(State.PLAY);
                break;
            case State.PLAY:
                break;
            case State.GAMEOVER:
                if (Score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", Score);
                }
                panelGameOver.SetActive(true);
                break;
        }
    }


    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                //lifeSlider.value = GhostHunter.lifePlayer;
                if (lifeSlider.value <= 0)
                {
                    SwitchState(State.GAMEOVER);
                }
                break;
            case State.GAMEOVER:
                if (Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }
    }


    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break; 
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    [HideInInspector]
    public float _time;
    [HideInInspector]
    public int _score;

    public GameObject _timeText;
    public GameObject _leftStar;
    public GameObject _middleStar;
    public GameObject _rightStar;

    public GameObject _winUI;
    public GameObject _looseUI;
    public GameObject _pauseUI;


    void Start()
    {
       
    }

    void Update()
    {
        _timeText.GetComponent<Text>().text = "Time: " + _time.ToString("0s");
        if (_score >= 1)
        {
            _middleStar.SetActive(true);
        }
        if (_score >= 2)
        {
            _leftStar.SetActive(true);
        }
        if (_score == 3)
        {
            _rightStar.SetActive(true);
        }
    }

    // Afficher le canevas
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // Cacher le canevas
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Gagné! afficher le score
    public void ShowWinScreen()
    {
        Invoke("Show", 0.7f);
        _winUI.SetActive(true);
        _looseUI.SetActive(false);
        _pauseUI.SetActive(false);
    }

    // Perdue! Afficher l'ecran pour recommencer..
    public void ShowLooseScreen()
    {
        Invoke("Show", 0.7f);
        _winUI.SetActive(false);
        _looseUI.SetActive(true);
        _pauseUI.SetActive(false);
    }
    
    // Niveau suivant
    public void NextLevel()
    {
        Hide();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Rejouer le niveau
    public void Restart()
    {
        Hide();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Retourner au menu principal
    public void Home()
    {
        Hide();
        SceneManager.LoadScene(0);
    }

    // Mettre le jeu en pause
    public void Pause()
    {
        Time.timeScale = 0f;
        Show();
        _winUI.SetActive(false);
        _looseUI.SetActive(false);
        _pauseUI.SetActive(true);
    }

    // Reprendre le jeu
    public void Resume()
    {
        Hide();
        Time.timeScale = 1f;
        _winUI.SetActive(false);
        _looseUI.SetActive(false);
        _pauseUI.SetActive(false);
    }
}

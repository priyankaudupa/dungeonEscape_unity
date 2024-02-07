using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject Levelcomplete;
    [SerializeField] private AudioSource gameover;
    [SerializeField] private AudioSource levelcomp;

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        gameover.Play();
        //Time.timeScale = 0f;
    }
    public void LevelComplete()
    {
        Levelcomplete.SetActive(true);
        levelcomp.Play();
    }
}

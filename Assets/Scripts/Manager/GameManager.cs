using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public PrepareUI prepareUI;
    public CardListUI cardListUI;
    public FailUI failUI;
    public WinUI winUI;
    private bool isGameEnd=false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameStart();
    }
    void GameStart()
    {
        Vector3 currentPosition=Camera.main.transform.position;
        Camera.main.transform.DOPath(
            new Vector3[] { currentPosition, new Vector3(5, 0, -10), currentPosition },
            4, PathType.Linear).OnComplete(OnCameraMoveComplete);
    }
    public void GameEndFail()
    {
        if (isGameEnd == true) return;
        isGameEnd = true;
        failUI.Show();
        ZombieManager.Instance.Pause();
        PauseAllPlants();
        cardListUI.DisableCardList();
        SunManager.Instance.StopProdeuce();
        AudioManager.Instance.PlayClip(Config.lose_music);
    }
    public void GameEndSuccess()
    {
        if (isGameEnd == true) return;
        isGameEnd = true;
        winUI.Show();
        PauseAllPlants();
        cardListUI.DisableCardList();
        SunManager.Instance.StopProdeuce();
        AudioManager.Instance.PlayClip(Config.win_music);
    }
    void PauseAllPlants()
    {
        Plant[] plants = FindObjectsOfType<Plant>();
        foreach (Plant plant in plants)
        {
            plant.TransitionToPause();
        }
    }
    void OnCameraMoveComplete()
    {
        prepareUI.Show(OnPrepareUIComplete);

    }
    void OnPrepareUIComplete()
    {
        SunManager.Instance.StartProduce();
        ZombieManager.Instance.StartSpawn();
        cardListUI.ShowCardListUI();
        AudioManager.Instance.PlayBgm(Config.bgm1);
    }
}

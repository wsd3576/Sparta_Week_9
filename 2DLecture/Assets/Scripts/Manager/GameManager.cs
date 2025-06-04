using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerControllor player { get; private set; }
    private ResourceController playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;

    private UIManager uiManager;
    public static bool isFirstLoading = true;

    private void Awake()
    {
        Instance = this;

        player = FindAnyObjectByType<PlayerControllor>();
        player.Init(this);

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        uiManager = FindAnyObjectByType<UIManager>();

        playerResourceController = player.GetComponent<ResourceController>();
        playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }

    private void Start()
    {
        if (!isFirstLoading) StartGame();
        else isFirstLoading = false;
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex++;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
    }
}

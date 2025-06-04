using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isFirstLoading = true;

    [SerializeField] private int currentStageIndex;
    [SerializeField] private int currentWaveIndex;
    private ResourceController _playerResourceController;

    private CameraController cameraShake;

    private EnemyManager enemyManager;

    private UIManager uiManager;

    public PlayerController player { get; private set; }

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        uiManager = FindObjectOfType<UIManager>();

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);


        _playerResourceController = player.GetComponent<ResourceController>();
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        cameraShake = FindObjectOfType<CameraController>();
        MainCameraShake();
    }

    private void Start()
    {
        if (!isFirstLoading)
            StartGame();
        else
            isFirstLoading = false;
    }

    public void MainCameraShake()
    {
        cameraShake.ShakeCamera(1, 1, 1);
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
        // StartNextWave();
        StartStage();
    }

    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        // StartNextWave();
        StartNextWaveInStage();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
    }


    public void StartStage()
    {
        var stageInfo = GetStageInfo(currentStageIndex);

        if (stageInfo == null)
        {
            Debug.Log("스테이지 정보가 없습니다.");
            return;
        }

        uiManager.ChangeWave(currentStageIndex + 1);

        enemyManager.StartStage(stageInfo.waves[currentWaveIndex]);
    }

    public void StartNextWaveInStage()
    {
        var stageInfo = GetStageInfo(currentStageIndex);
        if (stageInfo.waves.Length - 1 > currentWaveIndex)
        {
            currentWaveIndex++;
            StartStage();
        }
        else
        {
            CompleteStage();
        }
    }

    public void CompleteStage()
    {
        currentStageIndex++;
        currentWaveIndex = 0;
        StartStage();
    }

    private StageInfo GetStageInfo(int stageKey)
    {
        foreach (var stage in StageData.stages)
            if (stage.stageKey == stageKey)
                return stage;
        return null;
    }
}
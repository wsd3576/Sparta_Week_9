using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isFirstLoading = true;

    [SerializeField] private int currentStageIndex;
    [SerializeField] private int currentWaveIndex;
    private ResourceController _playerResourceController;

    private CameraController cameraShake;

    private StageInstance currentStageInstance;

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

        LoadOrStartNewStage();
    }

    private void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        StartNextWaveInStage();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
        StageSaveManager.ClearStageInstance();
    }

    private void LoadOrStartNewStage()
    {
        var savedInstance = StageSaveManager.LoadStageInstance();

        if (savedInstance != null)
            currentStageInstance = savedInstance;
        else
            currentStageInstance = new StageInstance(0, 0);

        StartStage(currentStageInstance);
    }


    public void StartStage(StageInstance stageInstance)
    {
        currentStageIndex = stageInstance.stageKey;
        currentWaveIndex = stageInstance.currentWave;

        var stageInfo = GetStageInfo(stageInstance.stageKey);

        if (stageInfo == null)
        {
            Debug.Log("스테이지 정보가 없습니다.");
            StageSaveManager.ClearStageInstance();
            currentStageInstance = null;
            return;
        }

        stageInstance.SetStageInfo(stageInfo);

        uiManager.ChangeWave(currentStageIndex + 1);

        enemyManager.StartStage(currentStageInstance);

        StageSaveManager.SaveStageInstance(currentStageInstance);
    }

    public void StartNextWaveInStage()
    {
        // var stageInfo = GetStageInfo(currentStageIndex);
        // if (stageInfo.waves.Length - 1 > currentWaveIndex)
        if (currentStageInstance.CheckEndOfWave())
        {
            currentStageInstance.currentWave += 1;
            StartStage(currentStageInstance);
        }
        else
        {
            CompleteStage();
        }
    }

    public void CompleteStage()
    {
        StageSaveManager.ClearStageInstance();

        if (currentStageInstance == null) return;

        currentStageInstance.stageKey += 1;
        currentStageInstance.currentWave = 0;

        StartStage(currentStageInstance);
    }

    private StageInfo GetStageInfo(int stageKey)
    {
        foreach (var stage in StageData.stages)
            if (stage.stageKey == stageKey)
                return stage;
        return null;
    }
}
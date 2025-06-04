using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] private List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new(1, 0, 0, .3f);

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;
    private readonly List<EnemyController> activeEnemies = new();

    private Dictionary<string, GameObject> enemyPrefabDic;

    private bool enemySpawnComplite;

    private GameManager gameManager;
    private Coroutine waveRoutine;

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            var center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            var size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        enemyPrefabDic = new Dictionary<string, GameObject>();
        foreach (var prefab in enemyPrefabs) enemyPrefabDic[prefab.name] = prefab;
    }

    public void StartWave(int waveCount)
    {
        if (waveCount <= 0)
        {
            gameManager.EndOfWave();
            return;
        }

        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);

        for (var i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy(string prefabName = null)
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        GameObject randomPrefab;
        if (prefabName == null)
            randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        else
            randomPrefab = enemyPrefabDic[prefabName];


        var randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        var randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax));

        var spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y),
            Quaternion.identity);
        var enemyController = spawnEnemy.GetComponent<EnemyController>();
        enemyController.Init(this, gameManager.player.transform);

        activeEnemies.Add(enemyController);
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
            gameManager.EndOfWave();
    }

    public void StartStage(WaveData waveData)
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);

        waveRoutine = StartCoroutine(SpawnStart(waveData));
    }

    private IEnumerator SpawnStart(WaveData waveData)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);

        for (var i = 0; i < waveData.monsters.Length; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);

            var monsterSpawnData = waveData.monsters[i];
            for (var j = 0; j < monsterSpawnData.spawnCount; j++) SpawnRandomEnemy(monsterSpawnData.monsterType);
        }

        if (waveData.hasBoss)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);

            gameManager.MainCameraShake();
            SpawnRandomEnemy(waveData.bossType);
        }

        enemySpawnComplite = true;
    }
}
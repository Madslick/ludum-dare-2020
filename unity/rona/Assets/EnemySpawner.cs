/* EnemySpawner.cs: class to spawn enemies. All prefabs need to be in scene before this can be used
 * 
 * Will save Prefabs object as a Prefab to drag into screens
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    //I'm taking for granted that I am making a Prefab and these will exist in the scene
    [SerializeField]
    private Scenario[] level1Spawns;
    [SerializeField]
    private Scenario[] level2Spawns;
    [SerializeField]
    private Scenario[] level3Spawns;
    [SerializeField]
    private Scenario[] level4Spawns;
    [SerializeField]
    private Scenario[] level5Spawns;
    [SerializeField]
    private Scenario[] level6Spawns;

    private Scenario[][] allLevelSpawns;

    private Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameController.Player.transform;

        allLevelSpawns = new Scenario[6][];
        allLevelSpawns[0] = level1Spawns;
        allLevelSpawns[1] = level2Spawns;
        allLevelSpawns[2] = level3Spawns;
        allLevelSpawns[3] = level4Spawns;
        allLevelSpawns[4] = level5Spawns;
        allLevelSpawns[5] = level6Spawns;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SpawnScenario(GetRandomScenario(1));
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SpawnScenario(GetRandomScenario(2));
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SpawnScenario(GetRandomScenario(3));
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SpawnScenario(GetRandomScenario(4));
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SpawnScenario(GetRandomScenario(5));
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            SpawnScenario(GetRandomScenario(6));
        }
    }

    Scenario GetRandomScenario(int level)
    {
        //decrement to account for off by 1 with arrays
        level--;
        Scenario[] l_scenarios = allLevelSpawns[level];
        return l_scenarios[UnityEngine.Random.Range(0, level6Spawns.Length)];

    }

    void SpawnScenario(Scenario scenario)
    {
        Vector2 origin = GetRandomPositionRelativeToPlayer(scenario.distanceFromPlayer);

        foreach(SpawnLocation spawn in scenario.spawns)
        {
            Vector2 currSpawn = origin + spawn.positionFromOrigin;
            GameObject obj = Instantiate(spawn.prefab);

            obj.transform.position = currSpawn;

            obj.SetActive(true);
        }
    }

    Vector2 GetRandomPositionRelativeToPlayer(float distance)
    {

        Vector2 randomVector = UnityEngine.Random.insideUnitCircle;
        randomVector.Normalize();

        randomVector.x = randomVector.x * distance;
        randomVector.y = randomVector.y * distance;

        //relative to player
        randomVector.x += playerPosition.position.x;
        randomVector.y += playerPosition.position.y;

        return randomVector;
    }
    
    [Serializable]
    public struct Scenario
    {
        [SerializeField]
        public SpawnLocation[] spawns;

        [SerializeField]
        public float distanceFromPlayer;
    }

    [Serializable]
    public struct SpawnLocation
    {
        [SerializeField]
        public Vector2 positionFromOrigin;
    
        [SerializeField]
        public GameObject prefab;
    }
}

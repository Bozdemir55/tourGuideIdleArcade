using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomSpawner : MonoBehaviour
{
    private GameManager _gameManager;
    
    public GameObject spawnPoint;
    public GameObject peoplePrefab;
    private float timer;
    public ParticleSystem spawnPar;
    //public List<GameObject> spawnObjectsList;
    private int spawnCount;

    private Building[] buildings;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        //_building = _gameManager._kikiPrefab.gameData
//        unlockPrice = _gameManager._kikiPrefab.gameData.buildings[buildingId].unlockPrice;
//        buildingTypeId = _gameManager._kikiPrefab.gameData.buildings[buildingId].buildingTypeId;
//        levelId = _gameManager._kikiPrefab.gameData.buildings[buildingId].levelId;
//        spawnTimer = _gameManager._kikiPrefab.gameData.buildings[buildingId].spawnTimer;
//        colorId = _gameManager._kikiPrefab.gameData.buildings[buildingId].colorId;
        // spawnPoint.transform.position;

        //buildings = FindObjectsOfType<Building>().ToList();
        
        // if (_building.levelId!=0)
        // {
        //     timer = 100;
        // }
  
    }

    // Update is called once per frame
    void Update()
    {
        // if (_building.levelId==0)
        //     return;
        //
        
        SpawnMethod();
    }

    public void SpawnMethod()
    {
        timer += Time.deltaTime;
        
        
        if (timer> 10)
        {
            int activeBuildingCount_Color1 = 0;
            int activeBuildingCount_Color2 = 0;
            int activeBuildingCount_Color3 = 0;
        
            for (int i = 0; i < _gameManager._kikiPrefab.gameData.buildings.Count; i++)
            {
                if (_gameManager._kikiPrefab.gameData.buildings[i].levelId > 0 && _gameManager._kikiPrefab.gameData.buildings[i].colorId == 0)
                {
                    activeBuildingCount_Color1++;
                }
            
                if (_gameManager._kikiPrefab.gameData.buildings[i].levelId > 0 && _gameManager._kikiPrefab.gameData.buildings[i].colorId == 1)
                {
                    activeBuildingCount_Color2++;
                }
            
                if (_gameManager._kikiPrefab.gameData.buildings[i].levelId > 0 && _gameManager._kikiPrefab.gameData.buildings[i].colorId == 2)
                {
                    activeBuildingCount_Color3++;
                }
            }
            
            GameObject spawnedObject = Instantiate(peoplePrefab, spawnPoint.transform.position, Quaternion.identity);
            spawnedObject.transform.position = spawnPoint.transform.position;
            spawnCount++;
            spawnPar.Play();
            spawnedObject.gameObject.SetActive(true);
            
            timer = 0;
        }
    }
}

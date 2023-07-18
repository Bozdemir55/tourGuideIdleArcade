using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour
{
    private GameManager _gameManager;
    
    public int buildingId;
    public BuildUpgradeCanvas upgradeCanvas;
    
    private int unlockPrice;
    private int buildingTypeId;
    private int levelId;
    private int colorId;
    public GameObject[] buildingParts;
    public GameObject spawnPoint;
    public GameObject peoplePrefab;
    private float timer;
    private Building _building;
    public ParticleSystem spawnPar;
    public Image[] spawnFillImages;
    private int spawnCount;
    private RandomPosSet _randomPosSet;
    public Material[] peopleColor;
    public Texture[] buildMatTexture;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _randomPosSet = FindObjectOfType<RandomPosSet>();
        _building = _gameManager._kikiPrefab.gameData.buildings[buildingId];
//        unlockPrice = _gameManager._kikiPrefab.gameData.buildings[buildingId].unlockPrice;
//        buildingTypeId = _gameManager._kikiPrefab.gameData.buildings[buildingId].buildingTypeId;
//        levelId = _gameManager._kikiPrefab.gameData.buildings[buildingId].levelId;
//        spawnTimer = _gameManager._kikiPrefab.gameData.buildings[buildingId].spawnTimer;
//        colorId = _gameManager._kikiPrefab.gameData.buildings[buildingId].colorId;
        // spawnPoint.transform.position;
        buildingParts[1].GetComponent<MeshRenderer>().material.mainTexture = buildMatTexture[buildingId];
        buildingParts[2].GetComponent<MeshRenderer>().material.mainTexture = buildMatTexture[buildingId];
        if (_building.levelId!=0)
        {
            timer = 100;
        }
  
    }

    // Update is called once per frame
    void Update()
    {
        if (_building.levelId==0)
            return;
        
        SpawnMethod();
    }

    public void SpawnMethod()
    {
        timer += Time.deltaTime;
        spawnFillImages[0].fillAmount = timer /((float)_building.spawnTimer/(float)_building.levelId) ;
        spawnFillImages[1].fillAmount = timer /((float)_building.spawnTimer/(float)_building.levelId) ;
        if (timer>_building.spawnTimer/(float)_building.levelId)
        {
            if (_gameManager._kikiPrefab.gameData.peopleCount<=79)
            {
                GameObject people = Instantiate(peoplePrefab, spawnPoint.transform.position, Quaternion.identity);
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = _randomPosSet.positionList[Random.Range(0, 50)];
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[buildingId];
                if (buildingId==0)
                {
                    people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.blue;
                }
                if (buildingId==1)
                {
                    people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.green;
                }
                if (buildingId==2)
                {
                    people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.red;
                }

                // spawnObjectsList[spawnCount].transform.position = spawnPoint.transform.position;
                // spawnCount++;
                // spawnObjectsList[spawnCount].gameObject.SetActive(true);
                spawnPar.Play();
                timer = 0;   
            }
        }
    }
}

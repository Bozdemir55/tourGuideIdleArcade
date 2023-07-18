using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourOfficeInfo : MonoBehaviour
{
    private GameManager _gameManager;
    
    public TourOfficeUpgradeCanvas upgradeCanvas;
    
    public ParticleSystem upgradePar;

    private TourOffice _tourOffice;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _tourOffice = _gameManager._kikiPrefab.gameData.tourOffice;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // if (_building.levelId==0)
    //     //     return;
    //     //
    //     // SpawnMethod();
    // }

    public void SpawnMethod()
    {
//         timer += Time.deltaTime;
//         spawnFillImages[0].fillAmount = timer /((float)_building.spawnTimer/(float)_building.levelId) ;
//         spawnFillImages[1].fillAmount = timer /((float)_building.spawnTimer/(float)_building.levelId) ;
//         if (timer>_building.spawnTimer/(float)_building.levelId)
//         {
// //            Instantiate(peoplePrefab, spawnPoint.transform.position, Quaternion.identity);
//             spawnObjectsList[spawnCount].transform.position = spawnPoint.transform.position;
//             spawnCount++;
//             spawnPar.Play();
//             spawnObjectsList[spawnCount].gameObject.SetActive(true);
//             
//             timer = 0;
//         }
    }
}

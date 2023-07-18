using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject people;
    public float HideDistance;

    public GameObject localParkPlace;
    public List<GameObject> blueTutorialPeople;
    public List<GameObject> greenTutorialPeople;
    public List<GameObject> redTutorialPeople;

    public GameObject greenCar;
    public GameObject greenBuild;

//    public GameObject localBuild;
    public bool peopleLook;

    public BuildingInfo[] buildingInfos;
    public ParkPlaceInfo[] parkPlaceInfos;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        buildingInfos = FindObjectsOfType<BuildingInfo>();
    }

    // Update is called once per frame
    void Update()
    {
//        print("Print :" + PlayerPrefs.GetInt("TutorialCar-CallPeople", 0));
//        print("Update Prefs :"+PlayerPrefs.GetInt("TutorialCar-CallPeople", 0));

//        if (PlayerPrefs.GetInt("TutorialCar-CallPeople",0)==0)
//        {
//            print("0");
//        }
//        if (PlayerPrefs.GetInt("TutorialCar-CallPeople",0)==1)
//        {
//            print("1");
//        }
//        if (PlayerPrefs.GetInt("TutorialCar-CallPeople",0)==2)
//        {
//            print("2");
//        }
//        if (PlayerPrefs.GetInt("TutorialCar-CallPeople",0)==3)
//        {
//            print("3");
//        }
//        

        if (peopleLook)
        {
            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
            }

            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            }

            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            }

            Vector3 targetPosition = people.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 2)
        {
            if (_gameManager._kikiPrefab.gameData
                    .parkPlaceData[localParkPlace.GetComponent<ParkPlaceInfo>().parkPlaceId].colorType ==
                ColorType.Blue)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
            }

            if (_gameManager._kikiPrefab.gameData
                    .parkPlaceData[localParkPlace.GetComponent<ParkPlaceInfo>().parkPlaceId].colorType ==
                ColorType.Green)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            }

            if (_gameManager._kikiPrefab.gameData
                    .parkPlaceData[localParkPlace.GetComponent<ParkPlaceInfo>().parkPlaceId].colorType == ColorType.Red)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            }

//            if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 5)
//            {
////                wefwf
//            }

            Vector3 targetPosition = localParkPlace.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 3)
        {
            if (_gameManager._kikiPrefab.gameData.parkPlaceData[greenCar.GetComponent<ParkPlaceInfo>().parkPlaceId]
                    .levelId == 0)
            {
                Vector3 targetPosition = greenCar.GetComponent<ParkPlaceInfo>().upgradeCanvas.transform.position;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);
            }
            else
            {
                if (_gameManager._kikiPrefab.gameData.buildings[greenBuild.GetComponent<BuildingInfo>().buildingId]
                        .levelId ==
                    0)
                {
                    Vector3 targetPosition = greenBuild.GetComponent<BuildingInfo>().upgradeCanvas.transform.position;
                    targetPosition.y = transform.position.y;
                    transform.LookAt(targetPosition);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    _gameManager._kikiPrefab.gameData.carTurorialActive = false;
                }
            }
//            foreach (var parkPlaceInfo in parkPlaceInfos)
//            {
//                if (parkPlaceInfo.parkPlaceId == 1)
//                {
//                    if (_gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId].levelId == 0)
//                    {
//                        Vector3 targetPosition = parkPlaceInfo.upgradeCanvas.transform.position;
//                        targetPosition.y = transform.position.y;
//                        transform.LookAt(targetPosition);
//                    }
//                    else
//                    {
//                        for (int i = 0; i < buildingInfos.Length; i++)
//                        {
//                            if (buildingInfos[i].buildingId == 1)
//                            {
//                                if (_gameManager._kikiPrefab.gameData.buildings[buildingInfos[i].buildingId].levelId ==
//                                    0)
//                                {
//                                    Vector3 targetPosition = buildingInfos[i].upgradeCanvas.transform.position;
//                                    targetPosition.y = transform.position.y;
//                                    transform.LookAt(targetPosition);
//                                }
//                                else
//                                {
//                                    transform.GetChild(0).gameObject.SetActive(false);
//                                    _gameManager._kikiPrefab.gameData.carTurorialActive = false;
//                                }
//                            }
//                        }
//                    }
//                }
//            }

//
//                if (buildingInfos[i].buildingId == 1)
//                {
//                    if (_gameManager._kikiPrefab.gameData.buildings[buildingInfos[i].buildingId].levelId == 0)
//                    {
//                        Vector3 targetPosition = buildingInfos[i].transform.position;
//                        targetPosition.y = transform.position.y;
//                        transform.LookAt(targetPosition);
//                    }
//                    else
//                    {
//                        transform.GetChild(0).gameObject.SetActive(false);
////                        PlayerPrefs.SetInt("TutorialCar-CallPeople", 4);
//                        _gameManager._kikiPrefab.gameData.carTurorialActive = false;
//                    }
//                }
        }

        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 4)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 0; i < _gameManager.heliParkUpgradeCanvases.Count - 1; i++)
            {
                if (_gameManager.heliParkUpgradeCanvases[i].parkplaceInfo.parkPlaceData.levelId == 0)
                {
                    Vector3 targetPosition = _gameManager.heliParkUpgradeCanvases[0].transform.position;
                    targetPosition.y = transform.position.y;
                    transform.LookAt(targetPosition);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    _gameManager._kikiPrefab.gameData.heliTurorialActive = false;
                }
            }
        }

        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 5)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 0; i < _gameManager.shipParkUpgradeCanvases.Count - 1; i++)
            {
                if (_gameManager.shipParkUpgradeCanvases[i].parkplaceInfo.parkPlaceData.levelId == 0)
                {
                    Vector3 targetPosition = _gameManager.shipParkUpgradeCanvases[0].transform.position;
                    targetPosition.y = transform.position.y;
                    transform.LookAt(targetPosition);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    _gameManager._kikiPrefab.gameData.shipTurorialActive = false;
                }
            }
        }

        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 6)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 0; i < _gameManager.planeParkUpgradeCanvases.Count - 1; i++)
            {
                if (_gameManager.planeParkUpgradeCanvases[i].parkplaceInfo.parkPlaceData.levelId == 0)
                {
                    Vector3 targetPosition = _gameManager.planeParkUpgradeCanvases[0].transform.position;
                    targetPosition.y = transform.position.y;
                    transform.LookAt(targetPosition);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    _gameManager._kikiPrefab.gameData.planeTurorialActive = false;
                }
            }
        }
//        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 5)
//        {
//            if (localParkPlace.GetComponent<ParkPlaceInfo>().parkPlaceData.colorType == ColorType.Blue)
//            {
//                if (blueTutorialPeople.Count == 0)
//                {
//                    PlayerPrefs.SetInt("TutorialCar-CallPeople", 3);
//                }
//                else
//                {
//                    for (int i = 0; i < blueTutorialPeople.Count-1; i++)
//                    {
//                        blueTutorialPeople[i].GetComponent<NeutralControl>().blueTutorialActive = true;
//                    }
//                    transform.GetChild(0).gameObject.SetActive(true);
//                    Vector3 targetPosition = blueTutorialPeople[0].transform.position;
//                    targetPosition.y = transform.position.y;
//                    transform.LookAt(targetPosition);
//                }
//            }
//
//            if (localParkPlace.GetComponent<ParkPlaceInfo>().parkPlaceData.colorType == ColorType.Green)
//            {
//                if (greenTutorialPeople.Count == 0)
//                {
//                    PlayerPrefs.SetInt("TutorialCar-CallPeople", 3);
//                }
//                else
//                {
//                    for (int i = 0; i < greenTutorialPeople.Count-1; i++)
//                    {
//                        greenTutorialPeople[i].GetComponent<NeutralControl>().greenTutorialActive = true;
//                    }
//                    transform.GetChild(0).gameObject.SetActive(true);
//                    Vector3 targetPosition = greenTutorialPeople[0].transform.position;
//                    targetPosition.y = transform.position.y;
//                    transform.LookAt(targetPosition);
//                }
//            }
//
//            if (localParkPlace.GetComponent<ParkPlaceInfo>().parkPlaceData.colorType == ColorType.Red)
//            {
//                if (redTutorialPeople.Count == 0)
//                {
//                    PlayerPrefs.SetInt("TutorialCar-CallPeople", 3);
//                }
//                else
//                {
//                    for (int i = 0; i < redTutorialPeople.Count-1; i++)
//                    {
//                        redTutorialPeople[i].GetComponent<NeutralControl>().redTutorialActive = true;
//                    }
//                    transform.GetChild(0).gameObject.SetActive(true);
//                    Vector3 targetPosition = redTutorialPeople[0].transform.position;
//                    targetPosition.y = transform.position.y;
//                    transform.LookAt(targetPosition);
//                }
//            }
//        }
//    }

//    public void PeopleLookAt(Transform people)
//    {
//    }
//    void SetChildrenActive(bool value)
//    {
//        foreach (Transform child in transform)
//        {
//            child.gameObject.SetActive(value);
//        }
    }
}
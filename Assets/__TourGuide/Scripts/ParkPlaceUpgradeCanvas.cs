using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ParkPlaceUpgradeCanvas : MonoBehaviour
{
    public Image fillUI;
    private float stayTimer;
    private GameManager _gameManager;
    public ParkPlaceInfo parkplaceInfo;
    public TextMeshProUGUI unlockUpgradePrice;
    public TextMeshProUGUI unlockUpgradeText;
    public GameObject unlockUpgradeCanvas;
    public TextMeshProUGUI[] peopleCountText;
    public GameObject[] parUpgrade;
    private ParkPlaceData parkPlaceData;
    private bool doneUpgrade = false;
    private PlayerControl playerControl;
    public bool touchBool = false;
    public GameObject locationPar;
    public BoxCollider meCollider;
    public CursorScript cursor;
    public List<GameObject> heliParkPlace;
    public List<GameObject> shipParkPlace;
    public List<GameObject> planeParkPlace;
    public BuildUpgradeCanvas[] buildUpgradeCanvases;
    public List<GameObject> tutorialTarget;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        playerControl = FindObjectOfType<PlayerControl>();

        cursor = FindObjectOfType<CursorScript>();

        locationPar = FindObjectOfType<Camera>().transform.GetChild(0).gameObject;
        parkPlaceData = _gameManager._kikiPrefab.gameData.parkPlaceData[parkplaceInfo.parkPlaceId];
        meCollider = transform.GetComponent<BoxCollider>();
        buildUpgradeCanvases = FindObjectsOfType<BuildUpgradeCanvas>();
        if (parkPlaceData.levelId == 0) //locked 
        {
            UnlockParkPlace(true);
        }
        else
        {
            UpgradeParkPlace(true);
            parkplaceInfo.gameObject.GetComponent<SphereCollider>().enabled = true;
        }


        // int upgradePrice = _gameManager._kikiPrefab.gameConfig
        //     .carUpgradePriceList[
        //         parkplaceInfo.parkPlaceCarTypeId].prices[parkArea.levelId/3].price[parkArea.levelId];
        // print("parkArea.levelId:" + parkArea.levelId);
        // print("upgradePrice: " + upgradePrice);

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
        {
            int a = 0;

            for (int i = 0; i < _gameManager.carParkUpgradeCanvases.Count; i++)
            {
                if (_gameManager._kikiPrefab.gameData
                        .parkPlaceData[_gameManager.carParkUpgradeCanvases[i].parkplaceInfo.parkPlaceId].levelId > 0)
                {
                    a++;
                }
            }

            if (a < 3)
            {
                unlockUpgradeCanvas.gameObject.SetActive(false);
                meCollider.enabled = false;
            }
            else
            {
                unlockUpgradeCanvas.gameObject.SetActive(true);
                meCollider.enabled = true;
            }
        }

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
        {
            int a = 0;

            for (int i = 0; i < _gameManager.heliParkUpgradeCanvases.Count; i++)
            {
                if (_gameManager._kikiPrefab.gameData
                        .parkPlaceData[_gameManager.heliParkUpgradeCanvases[i].parkplaceInfo.parkPlaceId].levelId > 0)
                {
                    a++;
                }
            }

            if (a < 6)
            {
                unlockUpgradeCanvas.gameObject.SetActive(false);
                meCollider.enabled = false;
            }
            else
            {
                unlockUpgradeCanvas.gameObject.SetActive(true);
                meCollider.enabled = true;
            }
        }

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
        {
            int a = 0;

            for (int i = 0; i < _gameManager.shipParkUpgradeCanvases.Count; i++)
            {
                if (_gameManager._kikiPrefab.gameData
                        .parkPlaceData[_gameManager.shipParkUpgradeCanvases[i].parkplaceInfo.parkPlaceId].levelId > 0)
                {
                    a++;
                }
            }

            if (a < 6)
            {
                unlockUpgradeCanvas.gameObject.SetActive(false);
                meCollider.enabled = false;
            }
            else
            {
                unlockUpgradeCanvas.gameObject.SetActive(true);
                meCollider.enabled = true;
            }
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    // }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        other.transform.GetComponent<BoxCollider>().size = Vector3.zero;
        if (playerControl.triggerActive)
        {
            touchBool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stayTimer = 0;
        fillUI.fillAmount = stayTimer;
        if (other.CompareTag("Player"))
        {
            if (playerControl.triggerActive)
            {
                touchBool = false;
            }
        }
    }

//    public void TutorialHeli(ParkPlaceInfo parkPlaceInfo,ParkPlaceData parkPlaceData)
//    {
//        if (PlayerPrefs.GetInt("TutorialCar-CallPeople",0)==4 && parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
//        {
//            PlayerPrefs.SetInt("TutorialCar-CallPeople",5);
//            NeutralControl[] peopleS = FindObjectsOfType<NeutralControl>();
//            foreach (var item in peopleS)
//            {
//                if (item.peopleColorType == PeopleColorType.blue)
//                {
//                    cursor.blueTutorialPeople.Add(item.gameObject);
//                }
//                if (item.peopleColorType == PeopleColorType.green)
//                {
//                    cursor.greenTutorialPeople.Add(item.gameObject);
//                }
//                if (item.peopleColorType == PeopleColorType.red)
//                {
//                    cursor.redTutorialPeople.Add(item.gameObject);
//                }
//            }
//            cursor.localParkPlace = parkplaceInfo.gameObject;
//        }
//    }
    public void TriggerMethod()
    {
        float fillAmountValue = 0;
        if (parkPlaceData.levelId == 0) //locked 
        {
            UnlockParkPlace(false);
        }
        else
        {
            UpgradeParkPlace(false);
        }
    }

    private void UnlockParkPlace(bool isStart)
    {
//        ManageFloors();

        unlockUpgradeText.text = "UNLOCK";
        unlockUpgradePrice.text =
            NumberExtensions.ToStringShort(parkPlaceData.unlockPrice);

        float fillAmountValue = 0;

        if (isStart)
        {
            fillAmountValue = (float) parkPlaceData.moneyPutted /
                              (float) parkPlaceData.unlockPrice;

            DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f);
            return;
        }

        int remainingCost = parkPlaceData.unlockPrice -
                            parkPlaceData.moneyPutted;

        if (remainingCost <= _gameManager._kikiPrefab.gameData.coinCount)
        {
            _gameManager._kikiPrefab.gameData.coinCount -= remainingCost;
            parkPlaceData.moneyPutted += remainingCost;
            _gameManager._gameUIManager.UpdateCoinCountText();


            fillAmountValue = (float) parkPlaceData.moneyPutted /
                              (float) parkPlaceData.unlockPrice;

            DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f).OnComplete(() =>
            {
                parkPlaceData.levelId++;

                playerControl.finalParkPlaceInfo = parkplaceInfo;
                for (int i = 0; i < _gameManager.parkPlaceUpgradeCanvases.Length; i++)
                {
                    _gameManager.parkPlaceUpgradeCanvases[i].CheckUpgradeCanvasActived();
                }

                StartCoroutine(_gameManager._gameUIManager.LocationFlag(parkplaceInfo.parkPlaceId, locationPar));
                parkplaceInfo.gameObject.GetComponent<SphereCollider>().enabled = true;
                parkPlaceData.moneyPutted = 0;
                UpgradeParkPlace(true);
//                buildingInfo.buildingParts[_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId].transform.GetChild(0).gameObject.SetActive(true);
                parUpgrade[(parkPlaceData.levelId / 5)]
                    .SetActive(true);
                parkplaceInfo.ChangeVehicleTexture();

                _gameManager._makeNoise.PlaySFX(2);
                _gameManager._makeNoise.PlaySFX(5);

                parkplaceInfo.ChangeTicketOfficeTexture();
                parkplaceInfo.UpdateVehicleAndTicketOffice(false);
                fillUI.fillAmount = 0;
                parkplaceInfo.carCanvas.gameObject.SetActive(true);

//                TutorialHeli(parkplaceInfo,parkPlaceData);
                if (parkPlaceData.colorType == ColorType.Blue)
                {
                    _gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive = true;
                    foreach (var buildUpgrade in buildUpgradeCanvases)
                    {
                        if (buildUpgrade.buildingInfo.buildingId == 0)
                        {
                            buildUpgrade.transform.GetComponent<BoxCollider>().enabled = true;
                            buildUpgrade.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }

                if (parkPlaceData.colorType == ColorType.Green)
                {
                    _gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive = true;
                    foreach (var buildUpgrade in buildUpgradeCanvases)
                    {
                        if (buildUpgrade.buildingInfo.buildingId == 1)
                        {
                            buildUpgrade.transform.GetComponent<BoxCollider>().enabled = true;
                            buildUpgrade.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }

                if (parkPlaceData.colorType == ColorType.Red)
                {
                    _gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive = true;
                    foreach (var buildUpgrade in buildUpgradeCanvases)
                    {
                        if (buildUpgrade.buildingInfo.buildingId == 2)
                        {
                            buildUpgrade.transform.GetComponent<BoxCollider>().enabled = true;
                            buildUpgrade.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
            });
        }
        else
        {
            parkPlaceData.moneyPutted +=
                _gameManager._kikiPrefab.gameData.coinCount;

            _gameManager._kikiPrefab.gameData.coinCount -= _gameManager._kikiPrefab.gameData.coinCount;
            _gameManager._gameUIManager.UpdateCoinCountText();

            fillAmountValue = (float) parkPlaceData.moneyPutted /
                              (float) parkPlaceData.unlockPrice;

            DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f);
        }
    }

    private void ManageFloors()
    {
        switch (parkPlaceData.parkPlaceType)
        {
            case ParkPlaceType.Car:

                foreach (var item in parkplaceInfo.cars)
                {
                    item.SetActive(false);
                }

                if (parkPlaceData.levelId == 0) //locked 
                {
                    parkplaceInfo.cars[0].SetActive(true);
                }
                else
                {
                    int howMany = (parkPlaceData.levelId / 5) + 1;
                    if (howMany == 1)
                    {
                        peopleCountText[0].text = parkPlaceData.levelId.ToString();
                        peopleCountText[0].gameObject.SetActive(true);
                        parkplaceInfo.cars[1].SetActive(true);
                    }
                    else
                    {
                        peopleCountText[0].gameObject.SetActive(true);
                        peopleCountText[0].text = parkPlaceData.levelId.ToString();
                        parkplaceInfo.cars[1].SetActive(true);

                        peopleCountText[1].text = parkPlaceData.levelId.ToString();
                        peopleCountText[1].gameObject.SetActive(true);
                        parkplaceInfo.cars[2].SetActive(true);
                    }
                }

                break;
        }
    }

    private void UpgradeParkPlace(bool isStart)
    {
        if (parkPlaceData.parkPlaceType == ParkPlaceType.Car && parkPlaceData.levelId == 11)
        {
            unlockUpgradeCanvas.SetActive(false);
//            ManageFloors();
            return;
        }

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Heli && parkPlaceData.levelId == 5)
        {
            unlockUpgradeCanvas.SetActive(false);
//            ManageFloors();
            return;
        }

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Ship && parkPlaceData.levelId == 8)
        {
            unlockUpgradeCanvas.SetActive(false);
//            ManageFloors();
            return;
        }

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Plane && parkPlaceData.levelId == 5)
        {
            unlockUpgradeCanvas.SetActive(false);
//            ManageFloors();
            return;
        }

        unlockUpgradeCanvas.SetActive(true);
//
        int upgradePrice = 0;


        upgradePrice = _gameManager._kikiPrefab.gameConfig
            .parkPlaceUpgradePriceList[
                (int) parkPlaceData.parkPlaceType].prices[parkPlaceData.levelId - 1];

        if (parkPlaceData.colorType == ColorType.Blue)
        {
            _gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive = true;
        }

        if (parkPlaceData.colorType == ColorType.Green)
        {
            _gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive = true;
        }

        if (parkPlaceData.colorType == ColorType.Red)
        {
            _gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive = true;
        }

        unlockUpgradeText.text = "UPGRADE";
        unlockUpgradePrice.text = NumberExtensions.ToStringShort(upgradePrice);

        float fillAmountValue = 0;

        //buildingParts
        if (isStart)
        {
            fillAmountValue = (float) parkPlaceData.moneyPutted /
                              (float) upgradePrice;
            // DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 0f).OnComplete(() =>
            // {
            //     
            // });
//            fillUI.fillAmount = 0;
//            ManageFloors();

            return;
        }

        int remainingCost = upgradePrice -
                            parkPlaceData.moneyPutted;

        if (remainingCost <= _gameManager._kikiPrefab.gameData.coinCount)
        {
            _gameManager._kikiPrefab.gameData.coinCount -= remainingCost;
            parkPlaceData.moneyPutted += remainingCost;
            _gameManager._gameUIManager.UpdateCoinCountText();
            parkPlaceData.levelId++;
            doneUpgrade = true;

            _gameManager._makeNoise.PlaySFX(3);

            if (parkPlaceData.levelId > parkPlaceData.Vehicles[parkPlaceData.activeVehicleLevelId].maxLevelId)
            {
                parkPlaceData.activeVehicleLevelId++;
            }

            if (parkPlaceData.parkPlaceType == ParkPlaceType.Car)
            {
                parkPlaceData.capacity += 2;
            }

            if (parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
            {
                parkPlaceData.capacity += 3;
            }

            if (parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
            {
                parkPlaceData.capacity += 5;
            }

            if (parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
            {
                parkPlaceData.capacity += 10;
            }

            parkplaceInfo.UpdateVehicleAndTicketOffice(false);
        }
        else
        {
            parkPlaceData.moneyPutted +=
                _gameManager._kikiPrefab.gameData.coinCount;

            _gameManager._kikiPrefab.gameData.coinCount -= _gameManager._kikiPrefab.gameData.coinCount;
            _gameManager._gameUIManager.UpdateCoinCountText();
        }

        fillAmountValue = (float) parkPlaceData.moneyPutted /
                          (float) upgradePrice;

        DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f).OnComplete(() =>
        {
            parUpgrade[(parkPlaceData.levelId / 5)]
                .SetActive(true);
            if (parkPlaceData.parkPlaceType == ParkPlaceType.Car)
            {
                if (parkPlaceData.levelId == 11)
                {
                    parkPlaceData.moneyPutted = 0;
                    unlockUpgradeCanvas.SetActive(false);
                }
                else
                {
                    parkPlaceData.moneyPutted = 0;
                }
            }
            if (parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
            {
                if (parkPlaceData.levelId == 5)
                {
                    parkPlaceData.moneyPutted = 0;
                    unlockUpgradeCanvas.SetActive(false);
                }
                else
                {
                    parkPlaceData.moneyPutted = 0;
                }
            }
            if (parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
            {
                if (parkPlaceData.levelId == 8)
                {
                    parkPlaceData.moneyPutted = 0;
                    unlockUpgradeCanvas.SetActive(false);
                }
                else
                {
                    parkPlaceData.moneyPutted = 0;
                }
            }
            if (parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
            {
                if (parkPlaceData.levelId == 5)
                {
                    parkPlaceData.moneyPutted = 0;
                    unlockUpgradeCanvas.SetActive(false);
                }
                else
                {
                    parkPlaceData.moneyPutted = 0;
                }
            }

//            ManageFloors();
            fillUI.fillAmount = 0;
            if (doneUpgrade)
            {
                upgradePrice = _gameManager._kikiPrefab.gameConfig
                    .parkPlaceUpgradePriceList[
                        (int) parkPlaceData.parkPlaceType].prices[parkPlaceData.levelId - 1];
                unlockUpgradeText.text = "UPGRADE";
                unlockUpgradePrice.text = upgradePrice.ToString();
                doneUpgrade = false;
            }
        });
    }

    public void CheckUpgradeCanvasActived()
    {
        if (parkPlaceData.parkPlaceType == ParkPlaceType.Heli && _gameManager._kikiPrefab.gameData.heliTurorialActive)
        {
            int a = 0;

            for (int i = 0; i < _gameManager.carParkUpgradeCanvases.Count; i++)
            {
                if (_gameManager._kikiPrefab.gameData
                        .parkPlaceData[_gameManager.carParkUpgradeCanvases[i].parkplaceInfo.parkPlaceId].levelId > 0)
                {
                    a++;
                }
            }

            if (a < 3)
            {
                unlockUpgradeCanvas.gameObject.SetActive(false);
                meCollider.enabled = false;
            }
            else
            {
                unlockUpgradeCanvas.gameObject.SetActive(true);
                ParkPlaceInfo[] parkPlaceInfos = FindObjectsOfType<ParkPlaceInfo>();
                foreach (var parkPlace in parkPlaceInfos)
                {
                    if (parkPlace.parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
                    {
                        heliParkPlace.Add(parkPlace.gameObject);
                    }
                }

                PlayerPrefs.SetInt("TutorialCar-CallPeople", 4);
                meCollider.enabled = true;
            }
        }

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Ship && _gameManager._kikiPrefab.gameData.shipTurorialActive)
        {
            int a = 0;

            for (int i = 0; i < _gameManager.heliParkUpgradeCanvases.Count; i++)
            {
                if (_gameManager._kikiPrefab.gameData
                        .parkPlaceData[_gameManager.heliParkUpgradeCanvases[i].parkplaceInfo.parkPlaceId].levelId > 0)
                {
                    a++;
                }
            }

            if (a < 6)
            {
                unlockUpgradeCanvas.gameObject.SetActive(false);
                meCollider.enabled = false;
            }
            else
            {
                unlockUpgradeCanvas.gameObject.SetActive(true);
                ParkPlaceInfo[] parkPlaceInfos = FindObjectsOfType<ParkPlaceInfo>();
                foreach (var parkPlace in parkPlaceInfos)
                {
                    if (parkPlace.parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                    {
                        shipParkPlace.Add(parkPlace.gameObject);
                    }
                }

                PlayerPrefs.SetInt("TutorialCar-CallPeople", 5);
                meCollider.enabled = true;
            }
        }

        if (parkPlaceData.parkPlaceType == ParkPlaceType.Plane && _gameManager._kikiPrefab.gameData.planeTurorialActive)
        {
            int a = 0;

            for (int i = 0; i < _gameManager.shipParkUpgradeCanvases.Count; i++)
            {
                if (_gameManager._kikiPrefab.gameData
                        .parkPlaceData[_gameManager.shipParkUpgradeCanvases[i].parkplaceInfo.parkPlaceId].levelId > 0)
                {
                    a++;
                }
            }

            if (a < 6)
            {
                unlockUpgradeCanvas.gameObject.SetActive(false);
                meCollider.enabled = false;
            }
            else
            {
                unlockUpgradeCanvas.gameObject.SetActive(true);
                ParkPlaceInfo[] parkPlaceInfos = FindObjectsOfType<ParkPlaceInfo>();
                foreach (var parkPlace in parkPlaceInfos)
                {
                    if (parkPlace.parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
                    {
                        planeParkPlace.Add(parkPlace.gameObject);
                    }
                }

                PlayerPrefs.SetInt("TutorialCar-CallPeople", 6);
                meCollider.enabled = true;
            }
        }
    }
}
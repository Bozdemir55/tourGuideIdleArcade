using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;
using TMPro;
using UnityEngine.UI;

public class PlayerCollision : BaseCollision
{
    private GameManager _gameManager;
    public RandomPosSet randomPosSet;
    public Canvas peopleInfoCanvas;
    public GameObject particleMove;
    private static PlayerCollision instance = null;
    public MoneyHolder moneyHolder;
    public PlayerControl playerControl;
    private bool isMouseUp;
    public bool triggerActive;
    public bool stackPeopleBussy;
    public int howManypeople;
    public bool CPISecondIdea;

    public static PlayerCollision Instance
    {
        get => instance;
        set => instance = value;
    }

    public int FakeCount
    {
        get => fakeCount;
        set => fakeCount = value;
    }

    [SerializeField] private GameObject handBoard;
    [SerializeField] private GameObject handBoardCollectedPeopleCountText;
    [SerializeField] private GameObject handBoardCollectedPeopleCountTextBack;

    private int fakeCount = 0;
    public List<GameObject> stackBluePeople = new List<GameObject>();
    public List<GameObject> stackGreenPeople = new List<GameObject>();
    public List<GameObject> stackRedPeople = new List<GameObject>();

    public int bluepeopleCount;
    public int greenpeopleCount;
    public int redpeopleCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        stackBluePeople = new List<GameObject>();
        stackGreenPeople = new List<GameObject>();
        stackRedPeople = new List<GameObject>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Setup()
    {
        _gameManager = FindObjectOfType<GameManager>();
        UpdateCollectedPeopleCountText();
        randomPosSet = FindObjectOfType<RandomPosSet>();
        base.Start();
    }

    private ParkPlaceInfo localParkPlaceInfo;
    private ParkPlaceData localPaekPlaceData;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (CPISecondIdea)
            {
                CPISecondIdea = false;
            }

            if (!CPISecondIdea)
            {
                CPISecondIdea = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CPISecondIdea)
            {
//                ParkPlaceInfo parkPlaceInfo = other.GetComponent<ParkPlaceInfo>();
//
//                ParkPlaceData parkPlaceData = _gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId];
                localParkPlaceInfo.upgradeCanvas.GetComponent<BoxCollider>().enabled = false;
                localParkPlaceInfo.transform.GetComponent<SphereCollider>().enabled = false;
                if (localPaekPlaceData.parkPlaceType == ParkPlaceType.Car)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        localParkPlaceInfo.carsHolder.GetComponent<SplineFollower>().follow = true;
                        localParkPlaceInfo.carsHolder.GetComponent<CarsHolder>().peopleCanvas.gameObject
                            .SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[0]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId] ;
                        localParkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        localParkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            localPaekPlaceData.Vehicles[localPaekPlaceData.activeVehicleLevelId].capacity,
                            localParkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

                if (localPaekPlaceData.parkPlaceType == ParkPlaceType.Ship)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        localParkPlaceInfo.shipsHolder.GetComponent<SplineFollower>().follow = true;
                        localParkPlaceInfo.shipsHolder.GetComponent<ShipsHolder>().peopleCanvas.gameObject
                            .SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[1]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];
                        localParkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        localParkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            localPaekPlaceData.Vehicles[localPaekPlaceData.activeVehicleLevelId].capacity,
                            localParkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

                if (localPaekPlaceData.parkPlaceType == ParkPlaceType.Heli)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        localParkPlaceInfo.helisHolder.GetComponent<SplineFollower>().follow = true;
                        localParkPlaceInfo.helisHolder.GetComponent<HelisHolder>().peopleCanvas.gameObject
                            .SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[2]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];
                        localParkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        localParkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            localPaekPlaceData.Vehicles[localPaekPlaceData.activeVehicleLevelId].capacity,
                            localParkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

                if (localPaekPlaceData.parkPlaceType == ParkPlaceType.Plane)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        localParkPlaceInfo.planesHolder.GetComponent<SplineFollower>().spline = _gameManager
                            ._splineManager.splineData[localParkPlaceInfo.parkPlaceId].inSpline;
                        localParkPlaceInfo.planesHolder.GetComponent<SplineFollower>().SetPercent(0);
                        localParkPlaceInfo.planesHolder.GetComponent<SplineFollower>().follow = true;
                        localParkPlaceInfo.planesHolder.GetComponent<PlanesHolder>().peopleCanvas.gameObject
                            .SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[3]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];
                        localParkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        localParkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            localPaekPlaceData.Vehicles[localPaekPlaceData.activeVehicleLevelId].capacity,
                            localParkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

//                            parkPlaceData.peopleCount = 0;
                peopleInfoCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void CrashMePlayer(GameObject newGameObject)
    {
        newGameObject.GetComponent<NeutralControl>().PlayerCrash = true;
        UpdateCollectedPeopleCountText();
    }

//    public IEnumerator BlueListPeople()
//    {
//        yield return new WaitForSeconds(.25f);
//    }
//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.transform.CompareTag("Vehicle"))
//        {
//            Debug.Log("ggeewqeqweqeqwqew");
//            StartCoroutine(MoveToPeoples());
//        }
//    }
//
//    IEnumerator MoveToPeoples()
//    {
//        for (int i = 0; i < stackGreenPeople.Count; i++)
//        {
//            stackBluePeople[i].GetComponent<NeutralControl>().MoveToVehicle();
//            yield return new WaitForSeconds(.15f);
//        }
//    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MoneyControl>()?.GetCollisionMoney();
        other.GetComponent<WallScript>()?.ReduceChangeOpacity();

        if (other.CompareTag("UpgradeCanvas"))
        {
            playerControl.upgradeCanvas = other.gameObject.GetComponent<ParkPlaceUpgradeCanvas>();
        }

        if (other.CompareTag("TicketUpgradeCanvas"))
        {
            playerControl.ticketupgradeCanvas = other.gameObject.GetComponent<TicketOfficeUpgradeCanvas>();
        }

        if (other.CompareTag("BuildUpgradeCanvas"))
        {
            playerControl.buildupgradeCanvas = other.gameObject.GetComponent<BuildUpgradeCanvas>();
        }

        if (other.CompareTag("TourUpgradeCanvas"))
        {
            playerControl.tourofficeupgradeCanvas = other.gameObject.GetComponent<TourOfficeUpgradeCanvas>();
        }

        if (other.CompareTag("ParkPlace"))
        {
            ParkPlaceInfo parkPlaceInfo = other.GetComponent<ParkPlaceInfo>();

            ParkPlaceData parkPlaceData = _gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId];

            localParkPlaceInfo = parkPlaceInfo;
            localPaekPlaceData = parkPlaceData;
            parkPlaceData.activeVehicleLevelId = 0;

            moneyHolder = other.GetComponentInChildren<MoneyHolder>();

            for (int i = 0; i < parkPlaceData.Vehicles.Length; i++)
            {
                if (parkPlaceData.levelId > parkPlaceData.Vehicles[i].maxLevelId + 1)
                {
                    parkPlaceData.activeVehicleLevelId++;
                }
            }

            if (parkPlaceData.colorType == ColorType.Blue)
            {
                howManypeople = parkPlaceData.capacity -
                                parkPlaceData.peopleCount;
                for (int i = 0; i < stackBluePeople.Count; i++)
                {
                    stackBluePeople[i].GetComponent<NeutralControl>().animScaleCount = stackBluePeople.Count;
                }

                if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 2)
                {
                    PlayerPrefs.SetInt("TutorialCar-CallPeople", 3);
                }

                if (CPISecondIdea)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (stackBluePeople.Count != 0)
                        {
//                        stackPeople[i].transform.parent = other.transform;
                            float wait = (i * 0f);
//                        stackBluePeople[0].gameObject.SetActive(false);
//                        stackBluePeople[0].GetComponent<NeutralControl>().ToIntoCrashObject(other.gameObject, _mat);
                            stackBluePeople[0].transform.LookAt(other.transform);
                            stackBluePeople[0].GetComponent<NeutralControl>().soundActive = true;
                            stackBluePeople[0].GetComponent<NeutralControl>()
                                .MoveVehicle(parkPlaceInfo, parkPlaceData, other.gameObject);

                            stackBluePeople[0].GetComponent<NeutralControl>()._parkPlaceInfo = parkPlaceInfo;
                            stackBluePeople[0].GetComponent<NeutralControl>()._parkPlaceData = parkPlaceData;

                            stackBluePeople.RemoveAt(0);
                            parkPlaceData.peopleCount++;
//                    stackPeople[i].SetActive(false);
//                    parkPlaceInfo.cars[parkPlaceData.activeVehicleLevelId].GetComponent<Animator>().SetTrigger("Active");
//                    parkPlaceData.peopleCount++;
//                    CheckType(other.gameObject,parkPlaceData.parkPlaceType);
//                    SetTextCanvas(other.gameObject);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < howManypeople; i++)
                    {
                        if (stackBluePeople.Count != 0)
                        {
//                        stackPeople[i].transform.parent = other.transform;
                            float wait = (i * 0f);
//                        stackBluePeople[0].gameObject.SetActive(false);
//                        stackBluePeople[0].GetComponent<NeutralControl>().ToIntoCrashObject(other.gameObject, _mat);
                            stackBluePeople[0].transform.LookAt(other.transform);
                            stackBluePeople[0].GetComponent<NeutralControl>().soundActive = true;
                            stackBluePeople[0].GetComponent<NeutralControl>()
                                .MoveVehicle(parkPlaceInfo, parkPlaceData, other.gameObject);

                            stackBluePeople[0].GetComponent<NeutralControl>()._parkPlaceInfo = parkPlaceInfo;
                            stackBluePeople[0].GetComponent<NeutralControl>()._parkPlaceData = parkPlaceData;

                            stackBluePeople.RemoveAt(0);
                            parkPlaceData.peopleCount++;
//                    stackPeople[i].SetActive(false);
//                    parkPlaceInfo.cars[parkPlaceData.activeVehicleLevelId].GetComponent<Animator>().SetTrigger("Active");
//                    parkPlaceData.peopleCount++;
//                    CheckType(other.gameObject,parkPlaceData.parkPlaceType);
//                    SetTextCanvas(other.gameObject);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            if (parkPlaceData.colorType == ColorType.Green)
            {
                howManypeople = parkPlaceData.capacity -
                                parkPlaceData.peopleCount;
                for (int i = 0; i < stackGreenPeople.Count; i++)
                {
                    stackGreenPeople[i].GetComponent<NeutralControl>().animScaleCount = stackGreenPeople.Count;
                }

                for (int i = 0; i < howManypeople; i++)
                {
                    if (stackGreenPeople.Count != 0)
                    {
//                        stackPeople[i].transform.parent = other.transform;
                        float wait = (i * 0f);
//                        stackGreenPeople[0].gameObject.SetActive(false);
//                        stackGreenPeople[0].GetComponent<NeutralControl>().ToIntoCrashObject(other.gameObject, _mat);

                        stackGreenPeople[0].transform.LookAt(other.transform);
                        stackGreenPeople[0].GetComponent<NeutralControl>().soundActive = true;
                        stackGreenPeople[0].GetComponent<NeutralControl>()
                            .MoveVehicle(parkPlaceInfo, parkPlaceData, other.gameObject);

                        stackGreenPeople[0].GetComponent<NeutralControl>()._parkPlaceInfo = parkPlaceInfo;
                        stackGreenPeople[0].GetComponent<NeutralControl>()._parkPlaceData = parkPlaceData;

                        stackGreenPeople.RemoveAt(0);
                        parkPlaceData.peopleCount++;
//                    stackPeople[i].SetActive(false);
//                    parkPlaceInfo.cars[parkPlaceData.activeVehicleLevelId].GetComponent<Animator>().SetTrigger("Active");
//                    parkPlaceData.peopleCount++;
//                    CheckType(other.gameObject,parkPlaceData.parkPlaceType);
//                    SetTextCanvas(other.gameObject);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (parkPlaceData.colorType == ColorType.Red)
            {
                howManypeople = parkPlaceData.capacity -
                                parkPlaceData.peopleCount;
                for (int i = 0; i < stackRedPeople.Count; i++)
                {
                    stackRedPeople[i].GetComponent<NeutralControl>().animScaleCount = stackRedPeople.Count;
                }

                for (int i = 0; i < howManypeople; i++)
                {
                    if (stackRedPeople.Count != 0)
                    {
//                        stackPeople[i].transform.parent = other.transform;
                        float wait = (i * 0f);
//                        stackRedPeople[0].gameObject.SetActive(false);
//                        stackRedPeople[0].GetComponent<NeutralControl>().ToIntoCrashObject(other.gameObject, _mat);
                        stackRedPeople[0].transform.LookAt(other.transform);
                        stackRedPeople[0].GetComponent<NeutralControl>().soundActive = true;
                        stackRedPeople[0].GetComponent<NeutralControl>()
                            .MoveVehicle(parkPlaceInfo, parkPlaceData, other.gameObject);

                        stackRedPeople[0].GetComponent<NeutralControl>()._parkPlaceInfo = parkPlaceInfo;
                        stackRedPeople[0].GetComponent<NeutralControl>()._parkPlaceData = parkPlaceData;


                        stackRedPeople.RemoveAt(0);
                        parkPlaceData.peopleCount++;
//                    stackPeople[i].SetActive(false);
//                    parkPlaceInfo.cars[parkPlaceData.activeVehicleLevelId].GetComponent<Animator>().SetTrigger("Active");
//                    parkPlaceData.peopleCount++;
//                    CheckType(other.gameObject,parkPlaceData.parkPlaceType);
//                    SetTextCanvas(other.gameObject);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (parkPlaceData.peopleCount ==
                parkPlaceData.capacity)
            {
                parkPlaceInfo.upgradeCanvas.GetComponent<BoxCollider>().enabled = false;
                parkPlaceInfo.transform.GetComponent<SphereCollider>().enabled = false;
                if (parkPlaceData.parkPlaceType == ParkPlaceType.Car)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        parkPlaceInfo.carsHolder.GetComponent<SplineFollower>().follow = true;
                        parkPlaceInfo.carsHolder.GetComponent<CarsHolder>().peopleCanvas.gameObject.SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[0]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];
                        parkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        parkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            parkPlaceData.capacity, parkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

                if (parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        parkPlaceInfo.shipsHolder.GetComponent<SplineFollower>().follow = true;
                        parkPlaceInfo.shipsHolder.GetComponent<ShipsHolder>().peopleCanvas.gameObject.SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[1]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];
                        parkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        parkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            parkPlaceData.capacity, parkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

                if (parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        parkPlaceInfo.helisHolder.GetComponent<SplineFollower>().follow = true;
                        parkPlaceInfo.helisHolder.GetComponent<HelisHolder>().peopleCanvas.gameObject.SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[2]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];
                        parkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        parkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            parkPlaceData.capacity, parkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

                if (parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
                {
                    DOVirtual.DelayedCall(1, () =>
                    {
                        parkPlaceInfo.planesHolder.GetComponent<SplineFollower>().spline = _gameManager._splineManager
                            .splineData[parkPlaceInfo.parkPlaceId].inSpline;
                        parkPlaceInfo.planesHolder.GetComponent<SplineFollower>().SetPercent(0);
                        parkPlaceInfo.planesHolder.GetComponent<SplineFollower>().follow = true;
                        parkPlaceInfo.planesHolder.GetComponent<PlanesHolder>().peopleCanvas.gameObject
                            .SetActive(false);
                        moneyHolder.officeTicketPrice =
                            _gameManager._kikiPrefab.gameConfig.ticketOfficeTicketPriceList[3]
                                .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];
                        parkPlaceInfo.upgradeCanvas.gameObject.SetActive(false);
                        parkPlaceInfo.particleMove.SetActive(true);
                        StartCoroutine(moneyHolder.MoveMoneyToPut(
                            parkPlaceData.capacity, parkPlaceInfo));
                        randomPosSet.callSpawner = true;
                    });
                }

//                            parkPlaceData.peopleCount = 0;
                peopleInfoCanvas.gameObject.SetActive(false);
            }

//            parkPlaceData.Vehicles[parkPlaceData.levelId - 1].capacity;
//            parkPlaceData.peopleCount
        }
    }

    void OnApplicationQuit()
    {
        for (int i = 0; i < _gameManager._kikiPrefab.gameData.parkPlaceData.Count; i++)
        {
            _gameManager._kikiPrefab.gameData.parkPlaceData[i].peopleCount = 0;
            _gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive = false;
            _gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive = false;
            _gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive = false;
        }
    }

    public void StackPeopleAnimCar(GameObject people, ParkPlaceInfo parkPlaceInfo, ParkPlaceData parkPlaceData,
        float wait, List<GameObject> stackPeople)
    {
        print(_gameManager._kikiPrefab.gameData.tourOffice.levelId / 3);
    }

    public void CheckType(GameObject placeObject, ParkPlaceType type)
    {
        ParkPlaceInfo parkPlaceInfo = placeObject.GetComponent<ParkPlaceInfo>();
        ParkPlaceData parkPlaceData = _gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId];

        switch (type)
        {
            case ParkPlaceType.Car:
                peopleInfoCanvas = placeObject.GetComponent<ParkPlaceInfo>().carCanvas;
                break;
            case ParkPlaceType.Ship:
                peopleInfoCanvas = placeObject.GetComponent<ParkPlaceInfo>().shipCanvas;
                break;
            case ParkPlaceType.Heli:
                peopleInfoCanvas = placeObject.GetComponent<ParkPlaceInfo>().heliCanvas;
                break;
            case ParkPlaceType.Plane:
                peopleInfoCanvas = placeObject.GetComponent<ParkPlaceInfo>().planeCanvas;
                break;
        }
    }

    public void SetTextCanvas(GameObject other)
    {
        other.GetComponent<ParkPlaceInfo>().SetTextCanvas(other);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<WallScript>()?.PlusChangeOpacity();
    }


//    public void WriteMoneyCount()
//    {
//        handBoardCollectedPeopleCountText.GetComponent<TextMeshPro>().text = MoneyCount.ToString();
//    }

    public void UpdateCollectedPeopleCountText()
    {
        int howManyCanICarry = _gameManager._kikiPrefab.gameData.tourOffice.capacity;

        if (howManyCanICarry <= (stackBluePeople.Count + stackGreenPeople.Count + stackRedPeople.Count))
        {
            handBoardCollectedPeopleCountText.GetComponent<TextMeshPro>().text = "MAX";
            handBoardCollectedPeopleCountTextBack.GetComponent<TextMeshPro>().text = "MAX";
        }
        else
        {
            handBoardCollectedPeopleCountText.GetComponent<TextMeshPro>().text =
                (stackBluePeople.Count + stackGreenPeople.Count + stackRedPeople.Count).ToString();
            handBoardCollectedPeopleCountTextBack.GetComponent<TextMeshPro>().text =
                (stackBluePeople.Count + stackGreenPeople.Count + stackRedPeople.Count).ToString();
        }


        // handBoardCollectedPeopleCountText.GetComponent<TextMeshPro>().text =
        //     (howManyCanICarry - stackPeople.Count).ToString();
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParkPlaceInfo : MonoBehaviour
{
    private GameManager _gameManager;
    public PlayerCollision playerCollision;
    public MoneyHolder moneyHolder;
    public int parkPlaceId;
    public ParkPlaceUpgradeCanvas upgradeCanvas;
    public TicketOfficeUpgradeCanvas ticketOfficeCanvas;
    public GameObject blendCar;
    private int unlockPrice;
    private int levelId;
    private int colorId;
    public GameObject[] cars;
    public GameObject[] ships;
    public GameObject[] helis;
    public GameObject[] planes;

    public GameObject[] carRenderers;
    public GameObject[] shipRenderers;
    public GameObject[] heliRenderers;

    public GameObject[] planeRenderers;

    public GameObject carsHolder;
    public GameObject shipsHolder;
    public GameObject helisHolder;

    public GameObject planesHolder;

//    public GameObject spawnPoint;
//    public GameObject peoplePrefab;
    private float timer;
    public ParkPlaceData parkPlaceData;

    public ParticleSystem carUpgradePar;
//    public List<GameObject> spawnObjectsList;

    public GameObject ticketOffice;
    public GameObject ticketOfficeForRenderer;
    public Texture[] ticketOfficeTextures;
    public Texture[] carTextures;

    public Canvas carCanvas;
    public Canvas shipCanvas;
    public Canvas heliCanvas;
    public Canvas planeCanvas;
    public bool moneyActive;
    public GameObject particleMove;

    public void Setup()
    {
        _gameManager = FindObjectOfType<GameManager>();
        playerCollision = FindObjectOfType<PlayerCollision>();
        moneyHolder = GetComponentInChildren<MoneyHolder>();
        parkPlaceData = _gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceId];

        ChangeVehicleTexture();
        ChangeTicketOfficeTexture();
        UpdateVehicleAndTicketOffice(true);
        playerCollision.CheckType(gameObject, parkPlaceData.parkPlaceType);
        SetTextCanvas(gameObject);

        //parkPlaceData.ticketOffice.ticketPrice.ToString();
//        unlockPrice = _gameManager._kikiPrefab.gameData.buildings[buildingId].unlockPrice;
//        buildingTypeId = _gameManager._kikiPrefab.gameData.buildings[buildingId].buildingTypeId;
//        levelId = _gameManager._kikiPrefab.gameData.buildings[buildingId].levelId;
//        spawnTimer = _gameManager._kikiPrefab.gameData.buildings[buildingId].spawnTimer;
//        colorId = _gameManager._kikiPrefab.gameData.buildings[buildingId].colorId;
        // spawnPoint.transform.position;
        if (parkPlaceData.levelId != 0)
        {
            timer = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parkPlaceData.levelId == 0)
            return;

        if (moneyActive)
        {
            moneyActive = false;
//            StartCoroutine(moneyHolder.MoneyEnumerator(this, parkPlaceData, .1f, parkPlaceData.peopleCount));
//            StartCoroutine(moneyHolder.MoveMoneyToPut(playerCollision.howManypeople,this));
        }

        //SpawnMethod();
    }

    public void SetTextCanvas(GameObject other)
    {
        ParkPlaceInfo parkPlaceInfo = other.GetComponent<ParkPlaceInfo>();
        ParkPlaceData parkPlaceData = _gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId];

        int activeCarLevelId = 0;
        for (int i = 0; i < parkPlaceData.Vehicles.Length; i++)
        {
            if (parkPlaceData.levelId > parkPlaceData.Vehicles[i].maxLevelId + 1)
            {
                activeCarLevelId++;
                carCanvas.transform.DOMoveY(activeCarLevelId + 2.5f, .2f);
            }
        }

        float fillAmountValue =
            (float) _gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId].peopleCount /
            (float) parkPlaceData.capacity;

        DOTween.To(() => carCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount,
                x => carCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = x, fillAmountValue,
                1f)
            .OnComplete(() =>
            {
                if (_gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId].peopleCount >=
                    parkPlaceData.capacity && playerCollision.CPISecondIdea)
                {
                    carCanvas.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "MAX";
                }
                else
                {
                    carCanvas.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        _gameManager._kikiPrefab.gameData.parkPlaceData[parkPlaceInfo.parkPlaceId].peopleCount
                            .ToString() +
                        " / " +
                        parkPlaceData.capacity.ToString();
                }


//                carCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;
            });


        carCanvas.gameObject.SetActive(true);
    }

    public void ChangeVehicleTexture()
    {
        switch (parkPlaceData.parkPlaceType)
        {
            case ParkPlaceType.Car:

                foreach (var car in carRenderers)
                {
                    SkinnedMeshRenderer vehicleRenderer = car.GetComponent<SkinnedMeshRenderer>();

                    switch (parkPlaceData.colorType)
                    {
                        case ColorType.Blue:
                            vehicleRenderer.materials[0].mainTexture = carTextures[0];
                            break;
                        case ColorType.Green:
                            vehicleRenderer.materials[0].mainTexture = carTextures[1];
                            break;
                        case ColorType.Red:
                            vehicleRenderer.materials[0].mainTexture = carTextures[2];
                            break;
                    }
                }

                break;

            case ParkPlaceType.Heli:

                foreach (var vehicle in heliRenderers)
                {
                    SkinnedMeshRenderer vehicleRenderer = vehicle.GetComponent<SkinnedMeshRenderer>();

                    switch (parkPlaceData.colorType)
                    {
                        case ColorType.Blue:
                            vehicleRenderer.materials[0].mainTexture = carTextures[0];
                            break;
                        case ColorType.Green:
                            vehicleRenderer.materials[0].mainTexture = carTextures[1];
                            break;
                        case ColorType.Red:
                            vehicleRenderer.materials[0].mainTexture = carTextures[2];
                            break;
                    }
                }

                break;

            case ParkPlaceType.Plane:

                foreach (var vehicle in planeRenderers)
                {
                    SkinnedMeshRenderer vehicleRenderer = vehicle.GetComponent<SkinnedMeshRenderer>();

                    switch (parkPlaceData.colorType)
                    {
                        case ColorType.Blue:
                            vehicleRenderer.materials[0].mainTexture = carTextures[0];
                            break;
                        case ColorType.Green:
                            vehicleRenderer.materials[0].mainTexture = carTextures[1];
                            break;
                        case ColorType.Red:
                            vehicleRenderer.materials[0].mainTexture = carTextures[2];
                            break;
                    }
                }

                break;

            case ParkPlaceType.Ship:

                foreach (var vehicle in shipRenderers)
                {
                    SkinnedMeshRenderer vehicleRenderer = vehicle.GetComponent<SkinnedMeshRenderer>();

                    switch (parkPlaceData.colorType)
                    {
                        case ColorType.Blue:
                            vehicleRenderer.materials[0].mainTexture = carTextures[0];
                            break;
                        case ColorType.Green:
                            vehicleRenderer.materials[0].mainTexture = carTextures[1];
                            break;
                        case ColorType.Red:
                            vehicleRenderer.materials[0].mainTexture = carTextures[2];
                            break;
                    }
                }

                break;
        }
    }

    public void ChangeTicketOfficeTexture()
    {
        if (parkPlaceData.levelId != 0)
        {
            Renderer ticketOfficeRenderer = ticketOfficeForRenderer.GetComponent<Renderer>();

            switch (parkPlaceData.colorType)
            {
                case ColorType.Blue:
                    ticketOfficeRenderer.materials[0].mainTexture = ticketOfficeTextures[0];
                    break;
                case ColorType.Red:
                    ticketOfficeRenderer.materials[0].mainTexture = ticketOfficeTextures[1];
                    break;
                case ColorType.Green:
                    ticketOfficeRenderer.materials[0].mainTexture = ticketOfficeTextures[2];
                    break;
            }

            ticketOfficeCanvas.gameObject.SetActive(true);
            ticketOffice.SetActive(true);
        }
    }

    public void UpdateVehicleAndTicketOffice(bool isStart)
    {
        if (parkPlaceData.levelId != 0)
        {
            switch (parkPlaceData.parkPlaceType)
            {
                case ParkPlaceType.Car:

                    helisHolder.SetActive(false);
                    planesHolder.SetActive(false);
                    shipsHolder.SetActive(false);
                    carsHolder.SetActive(true);

                    foreach (var vehicle in cars)
                    {
                        vehicle.SetActive(false);
                    }

                    if (!isStart)
                    {
                        carUpgradePar.Play();
                    }

//                print(parkPlaceData.levelId-1);
                    cars[parkPlaceData.activeVehicleLevelId].SetActive(true);
                    SetTextCanvas(gameObject);
                    break;

                case ParkPlaceType.Heli:

                    helisHolder.SetActive(true);
                    planesHolder.SetActive(false);
                    shipsHolder.SetActive(false);
                    carsHolder.SetActive(false);

                    foreach (var vehicle in helis)
                    {
                        vehicle.SetActive(false);
                    }

                    if (!isStart)
                        carUpgradePar.Play();
                    helis[parkPlaceData.activeVehicleLevelId].SetActive(true);
                    SetTextCanvas(gameObject);
                    break;

                case ParkPlaceType.Plane:

                    helisHolder.SetActive(false);
                    planesHolder.SetActive(true);
                    shipsHolder.SetActive(false);
                    carsHolder.SetActive(false);

                    foreach (var vehicle in planes)
                    {
                        vehicle.SetActive(false);
                    }

                    if (!isStart)
                        carUpgradePar.Play();
                    planes[parkPlaceData.activeVehicleLevelId].SetActive(true);

                    break;

                case ParkPlaceType.Ship:

                    helisHolder.SetActive(false);
                    planesHolder.SetActive(false);
                    shipsHolder.SetActive(true);
                    carsHolder.SetActive(false);

                    foreach (var vehicle in ships)
                    {
                        vehicle.SetActive(false);
                    }

                    if (!isStart)
                        carUpgradePar.Play();
                    ships[parkPlaceData.activeVehicleLevelId].SetActive(true);
                    SetTextCanvas(gameObject);
                    break;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TicketOfficeUpgradeCanvas : MonoBehaviour
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
    public TextMeshProUGUI ticketPriceText;
    public TextMeshProUGUI ticketLocationText;
    private PlayerControl playerControl;
    public bool touchBool = false;
    public SpriteRenderer parkLine;
    public bool meanTicketOffice;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
//        StartCoroutine(Unlit());
        parkPlaceData = _gameManager._kikiPrefab.gameData.parkPlaceData[parkplaceInfo.parkPlaceId];
        if (parkPlaceData.parkPlaceType != ParkPlaceType.Heli)
        {
            if (parkPlaceData.colorType == ColorType.Blue)
            {
                parkLine.color = Color.blue;
            }

            if (parkPlaceData.colorType == ColorType.Green)
            {
                parkLine.color = Color.green;
            }

            if (parkPlaceData.colorType == ColorType.Red)
            {
                parkLine.color = Color.red;
            }
        }

        UpgradeParkPlace(true);

        // if (parkPlaceData.levelId == 0) //locked 
        // {
        //     UnlockParkPlace(true);
        // }
        // else
        // {
        //     UpgradeParkPlace(true);
        // }

        // int upgradePrice = _gameManager._kikiPrefab.gameConfig
        //     .carUpgradePriceList[
        //         parkplaceInfo.parkPlaceCarTypeId].prices[parkArea.levelId/3].price[parkArea.levelId];
        // print("parkArea.levelId:" + parkArea.levelId);
        // print("upgradePrice: " + upgradePrice);
    }

    IEnumerator Unlit()
    {
        yield return new WaitForSeconds(1f);
        playerControl = FindObjectOfType<PlayerControl>();
    }

    public void Setup()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    // void Update()
    // {
    // }
    public void TicketTriggerMethod()
    {
        UpgradeParkPlace(false);

        //UnlockParkPlace(false);

        // float fillAmountValue = 0;
        // if (parkPlaceData.levelId == 0) //locked 
        // {
        //     UnlockParkPlace(false);
        // }
        // else
        // {
        //     UpgradeParkPlace(false);
        // }
    }

    public void OnTriggerEnter(Collider other)
    {
//        print(other.name);
        if (playerControl == null)
        {
            playerControl = FindObjectOfType<PlayerControl>();
        }

        if (playerControl.triggerActive)
        {
            touchBool = true;
        }

        if (!other.transform.CompareTag("Player"))
            return;
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
//     private void UnlockParkPlace(bool isStart)
//     {
// //        ManageFloors();
//
//         unlockUpgradeText.text = "UNLOCK";
//         unlockUpgradePrice.text =
//             parkPlaceData.unlockPrice.ToString();
//
//         float fillAmountValue = 0;
//
//         if (isStart)
//         {
//             fillAmountValue = (float) parkPlaceData.moneyPutted /
//                               (float) parkPlaceData.unlockPrice;
//
//             DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f);
//             return;
//         }
//
//         int remainingCost = parkPlaceData.unlockPrice -
//                             parkPlaceData.moneyPutted;
//
//         if (remainingCost <= _gameManager._kikiPrefab.gameData.coinCount)
//         {
//             _gameManager._kikiPrefab.gameData.coinCount -= remainingCost;
//             parkPlaceData.moneyPutted += remainingCost;
//             _gameManager._gameUIManager.UpdateCoinCountText();
//             parkPlaceData.levelId++;
//
//             fillAmountValue = (float) parkPlaceData.moneyPutted /
//                               (float) parkPlaceData.unlockPrice;
//
//             DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f).OnComplete(() =>
//             {
//                 parkPlaceData.moneyPutted = 0;
//                 UpgradeParkPlace(true);
// //                buildingInfo.buildingParts[_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId].transform.GetChild(0).gameObject.SetActive(true);
//                 parUpgrade[(parkPlaceData.levelId / 5) ]
//                     .SetActive(true);
//                 fillUI.fillAmount = 0;
//             });
//         }
//         else
//         {
//             parkPlaceData.moneyPutted +=
//                 _gameManager._kikiPrefab.gameData.coinCount;
//
//             _gameManager._kikiPrefab.gameData.coinCount -= _gameManager._kikiPrefab.gameData.coinCount;
//             _gameManager._gameUIManager.UpdateCoinCountText();
//
//             fillAmountValue = (float) parkPlaceData.moneyPutted /
//                               (float) parkPlaceData.unlockPrice;
//
//             DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f);
//         }
//     }

    // private void ManageFloors()
    // {
    //     foreach (var item in parkplaceInfo.carParts)
    //     {
    //         item.SetActive(false);
    //     }
    //
    //     if (parkPlaceData.levelId == 0) //locked 
    //     {
    //         parkplaceInfo.carParts[0].SetActive(true);
    //     }
    //     else
    //     {
    //         int howMany = (parkPlaceData.levelId / 5)+1;
    //         if (howMany == 1)
    //         {
    //             peopleCountText[0].text = parkPlaceData.levelId.ToString();
    //             peopleCountText[0].gameObject.SetActive(true);
    //             parkplaceInfo.carParts[1].SetActive(true);
    //         }
    //         else
    //         {
    //             peopleCountText[0].gameObject.SetActive(true);
    //             peopleCountText[0].text = parkPlaceData.levelId.ToString();
    //             parkplaceInfo.carParts[1].SetActive(true);
    //             
    //             peopleCountText[1].text = parkPlaceData.levelId.ToString();
    //             peopleCountText[1].gameObject.SetActive(true);
    //             parkplaceInfo.carParts[2].SetActive(true);
    //         }
    //     }
    // }

    private void UpgradeParkPlace(bool isStart)
    {
        ticketPriceText.text =  (_gameManager._kikiPrefab.gameConfig
                                     .ticketOfficeTicketPriceList[(int) parkPlaceData.parkPlaceType]
                                     .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId] +
                                 0).ToString();
        //      ticketLocationText.text = _gameManager._kikiPrefab.gameConfig.placeList[parkplaceInfo.parkPlaceId].name;
        ticketLocationText.text = "Ticket-Office";
        if (_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId == _gameManager._kikiPrefab.gameConfig.ticketOfficeMaxUpgradeLevelId)
        {
            unlockUpgradeCanvas.SetActive(false);
//            ManageFloors();
            return;
        }

        unlockUpgradeCanvas.SetActive(true);
//
        int upgradePrice = 0;

        //
        GameConfig.TicketOfficeUpgrade ticketOfficeUpgrade =
            _gameManager._kikiPrefab.gameConfig.ticketOfficeUpgradePriceList.First(r =>
                r.parkPlaceType == parkPlaceData.parkPlaceType);

        upgradePrice = ticketOfficeUpgrade.prices[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId];


        unlockUpgradeText.text = "UPGRADE";
        unlockUpgradePrice.text = NumberExtensions.ToStringShort(upgradePrice);

        float fillAmountValue = 0;

        if (isStart)
        {
            fillAmountValue = (float) _gameManager._kikiPrefab.gameData.ticketOfficeAlone.moneyPutted / (float) upgradePrice;
            fillUI.fillAmount = fillAmountValue;
            // DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 0f).OnComplete(() =>
            // {
            //     
            // });
//            fillUI.fillAmount = 0;
//            ManageFloors();

            return;
        }

        int remainingCost = upgradePrice - _gameManager._kikiPrefab.gameData.ticketOfficeAlone.moneyPutted;

        if (remainingCost <= _gameManager._kikiPrefab.gameData.coinCount)
        {
            _gameManager._kikiPrefab.gameData.coinCount -= remainingCost;
            _gameManager._kikiPrefab.gameData.ticketOfficeAlone.moneyPutted += remainingCost;
            _gameManager._gameUIManager.UpdateCoinCountText();
            _gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId++;
            parUpgrade[0].SetActive(true);
        }
        else
        {
            _gameManager._kikiPrefab.gameData.ticketOfficeAlone.moneyPutted += _gameManager._kikiPrefab.gameData.coinCount;

            _gameManager._kikiPrefab.gameData.coinCount -= _gameManager._kikiPrefab.gameData.coinCount;
            _gameManager._gameUIManager.UpdateCoinCountText();
        }

        fillAmountValue = (float) _gameManager._kikiPrefab.gameData.ticketOfficeAlone.moneyPutted / (float) upgradePrice;

        DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f).OnComplete(() =>
        {
            if (_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId == _gameManager._kikiPrefab.gameConfig.ticketOfficeMaxUpgradeLevelId)
            {
                _gameManager._kikiPrefab.gameData.ticketOfficeAlone.moneyPutted = 0;
                unlockUpgradeCanvas.SetActive(false);
            }
            else
            {
                ticketPriceText.text = (_gameManager._kikiPrefab.gameConfig
                                            .ticketOfficeTicketPriceList[(int) parkPlaceData.parkPlaceType]
                                            .upgradePrice[_gameManager._kikiPrefab.gameData.ticketOfficeAlone.levelId] +
                                        0).ToString();
                _gameManager._kikiPrefab.gameData.ticketOfficeAlone.moneyPutted = 0;
            }

//            ManageFloors();
            fillUI.fillAmount = 0;
        });
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BuildUpgradeCanvas : MonoBehaviour
{
    public Image fillUI;
    private float stayTimer;
    private GameManager _gameManager;
    public BuildingInfo buildingInfo;
    public TextMeshProUGUI unlockUpgradePrice;
    public TextMeshProUGUI unlockUpgradeText;
    public GameObject unlockUpgradeCanvas;
    public TextMeshProUGUI[] spawnCountText;
    public GameObject[] parUpgrade;
    private PlayerControl playerControl;
    public List<GameObject> spawnCanvasList;
    public bool touchBool = false;

    void Start()
    {

    }

    public void Setup()
    {
        _gameManager = FindObjectOfType<GameManager>();
        playerControl = FindObjectOfType<PlayerControl>();
        DOVirtual.DelayedCall(1, () =>
        {
            if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].colorId == 0 &&
                _gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive ||
                _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].colorId == 1 &&
                _gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive ||
                _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].colorId == 2 &&
                _gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive)
            {
                if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId == 0) //locked 
                {
                    UnlockBuilding(true);
                }
                else
                {
                    UpgradeBuilding(true);
                    spawnCanvasList[0].SetActive(true);
                    if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId >= 5)
                    {
                        spawnCanvasList[1].SetActive(true);
                    }
                }
            }
            else
            {
                transform.GetComponent<BoxCollider>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        });
       
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void BuildTriggerMethod()
    {
        float fillAmountValue = 0;
        if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId == 0) //locked 
        {
            UnlockBuilding(false);
        }
        else
        {
            UpgradeBuilding(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
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

    private void UnlockBuilding(bool isStart)
    {
//        ManageFloors();

        unlockUpgradeText.text = "UNLOCK";
        unlockUpgradePrice.text =
            NumberExtensions.ToStringShort(_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId]
                .unlockPrice);

        float fillAmountValue = 0;

        if (isStart)
        {
            fillAmountValue = (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted /
                              (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].unlockPrice;

            DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f);
            return;
        }

        int remainingCost = _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].unlockPrice -
                            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted;

        if (remainingCost <= _gameManager._kikiPrefab.gameData.coinCount)
        {
            _gameManager._kikiPrefab.gameData.coinCount -= remainingCost;
            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted += remainingCost;
            _gameManager._gameUIManager.UpdateCoinCountText();
            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId++;

            fillAmountValue = (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted /
                              (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].unlockPrice;

            DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f).OnComplete(() =>
            {
                _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted = 0;
                UpgradeBuilding(true);
//                buildingInfo.buildingParts[_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId].transform.GetChild(0).gameObject.SetActive(true);
                parUpgrade[(_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId / 5)]
                    .SetActive(true);
                fillUI.fillAmount = 0;
            });
        }
        else
        {
            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted +=
                _gameManager._kikiPrefab.gameData.coinCount;

            _gameManager._kikiPrefab.gameData.coinCount -= _gameManager._kikiPrefab.gameData.coinCount;
            _gameManager._gameUIManager.UpdateCoinCountText();

            fillAmountValue = (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted /
                              (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].unlockPrice;

            DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f);
        }
    }

    private void ManageFloors()
    {
        foreach (var item in buildingInfo.buildingParts)
        {
            item.SetActive(false);
        }

        if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId == 0) //locked 
        {
            buildingInfo.buildingParts[0].SetActive(true);
        }
        else
        {
            int howMany = (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId / 5) + 1;
            if (howMany == 1)
            {
                spawnCountText[0].text = _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId
                    .ToString();
                spawnCountText[0].gameObject.SetActive(true);
                buildingInfo.buildingParts[1].SetActive(true);
            }
            else
            {
                spawnCountText[0].gameObject.SetActive(true);
                spawnCountText[0].text = _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId
                    .ToString();
                buildingInfo.buildingParts[1].SetActive(true);

                spawnCountText[1].text = _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId
                    .ToString();
                spawnCountText[1].gameObject.SetActive(true);
                buildingInfo.buildingParts[2].SetActive(true);
            }
        }
    }

    private void UpgradeBuilding(bool isStart)
    {
        if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId == 10)
        {
            unlockUpgradeCanvas.SetActive(false);
            ManageFloors();
            return;
        }

        unlockUpgradeCanvas.SetActive(true);

        int upgradePrice = _gameManager._kikiPrefab.gameConfig
            .buildingUpgradePriceList[
                _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].buildingTypeId]
            .price[_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId];

        unlockUpgradeText.text = "UPGRADE";
        unlockUpgradePrice.text = NumberExtensions.ToStringShort(upgradePrice);

        float fillAmountValue = 0;

        //buildingParts
        if (isStart)
        {
            fillAmountValue = (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted /
                              (float) upgradePrice;
            // DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 0f).OnComplete(() =>
            // {
            //     
            // });
            fillUI.fillAmount = 0;
            ManageFloors();

            return;
        }

        int remainingCost = upgradePrice -
                            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted;

        if (remainingCost <= _gameManager._kikiPrefab.gameData.coinCount)
        {
            _gameManager._kikiPrefab.gameData.coinCount -= remainingCost;
            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted += remainingCost;
            _gameManager._gameUIManager.UpdateCoinCountText();
            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId++;
        }
        else
        {
            _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted +=
                _gameManager._kikiPrefab.gameData.coinCount;

            _gameManager._kikiPrefab.gameData.coinCount -= _gameManager._kikiPrefab.gameData.coinCount;
            _gameManager._gameUIManager.UpdateCoinCountText();
        }

        fillAmountValue = (float) _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted /
                          (float) upgradePrice;

        DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f).OnComplete(() =>
        {
            parUpgrade[(_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId / 5)]
                .SetActive(true);
            if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId == 10)
            {
                _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted = 0;
                unlockUpgradeCanvas.SetActive(false);
            }
            else
            {
                _gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].moneyPutted = 0;
            }

            if (_gameManager._kikiPrefab.gameData.buildings[buildingInfo.buildingId].levelId >= 5)
            {
                spawnCanvasList[1].SetActive(true);
            }

            ManageFloors();
            fillUI.fillAmount = 0;
        });
    }
}
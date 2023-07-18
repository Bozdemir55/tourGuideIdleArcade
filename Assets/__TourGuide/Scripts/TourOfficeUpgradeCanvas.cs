using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TourOfficeUpgradeCanvas : MonoBehaviour
{
    public Image fillUI;
    private float stayTimer;
    private GameManager _gameManager;
    public TourOfficeInfo tourOfficeInfo;
    public TextMeshProUGUI unlockUpgradePrice;
    public TextMeshProUGUI unlockUpgradeText;
    public GameObject unlockUpgradeCanvas;
    public GameObject parUpgrade;
    private PlayerControl playerControl;
    public bool touchBool = false;
    public TextMeshPro tourOfficeLevelIdText;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        playerControl = FindObjectOfType<PlayerControl>();
        UpgradeTourOffice(true,null);
    }

    public void TourOfficeTriggerMethod(GameObject player)
    {
        UpgradeTourOffice(false, player);
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
    private void UpgradeTourOffice(bool isStart, GameObject collider)
    {
   
//        tourOfficeLevelIdText.text = "+"+_gameManager._kikiPrefab.gameData.tourOffice.capacity.ToString();
        tourOfficeLevelIdText.text ="+5";

        if (_gameManager._kikiPrefab.gameData.tourOffice.levelId == _gameManager._kikiPrefab.gameConfig.tourOfficeMaxUpgradeLevelId)
        {
            unlockUpgradeCanvas.SetActive(false);
            return;
        }

        unlockUpgradeCanvas.SetActive(true);

        int upgradePrice = 0;

        upgradePrice = _gameManager._kikiPrefab.gameConfig.tourOfficeUpgradePriceList[_gameManager._kikiPrefab.gameData.tourOffice.levelId];
        

        unlockUpgradeText.text = "UPGRADE";
        unlockUpgradePrice.text = NumberExtensions.ToStringShort(upgradePrice);

        float fillAmountValue = 0;
        
        if (isStart)
        {
            fillAmountValue = (float) _gameManager._kikiPrefab.gameData.tourOffice.moneyPutted / (float) upgradePrice;
            fillUI.fillAmount = fillAmountValue;
            // DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 0f).OnComplete(() =>
            // {
            //     
            // });
//            fillUI.fillAmount = 0;
//            ManageFloors();

            return;
        }

        int remainingCost = upgradePrice - _gameManager._kikiPrefab.gameData.tourOffice.moneyPutted;

        if (remainingCost <= _gameManager._kikiPrefab.gameData.coinCount)
        {
            _gameManager._kikiPrefab.gameData.coinCount -= remainingCost;
            _gameManager._kikiPrefab.gameData.tourOffice.moneyPutted += remainingCost;
            _gameManager._gameUIManager.UpdateCoinCountText();
            _gameManager._kikiPrefab.gameData.tourOffice.levelId++;
            
            _gameManager._kikiPrefab.gameData.tourOffice.capacity += 5;
//            tourOfficeLevelIdText.text ="+"+_gameManager._kikiPrefab.gameData.tourOffice.capacity.ToString();
            tourOfficeLevelIdText.text ="+5";

            collider.GetComponent<PlayerCollision>().UpdateCollectedPeopleCountText();
        }
        else
        {
            _gameManager._kikiPrefab.gameData.tourOffice.moneyPutted += _gameManager._kikiPrefab.gameData.coinCount;

            _gameManager._kikiPrefab.gameData.coinCount -= _gameManager._kikiPrefab.gameData.coinCount;
            _gameManager._gameUIManager.UpdateCoinCountText();
        }
        
        fillAmountValue = (float) _gameManager._kikiPrefab.gameData.tourOffice.moneyPutted / (float) upgradePrice;

        DOTween.To(() => fillUI.fillAmount, x => fillUI.fillAmount = x, fillAmountValue, 1f).OnComplete(() =>
        {
            if (_gameManager._kikiPrefab.gameData.tourOffice.levelId == _gameManager._kikiPrefab.gameConfig.tourOfficeMaxUpgradeLevelId)
            {
                _gameManager._kikiPrefab.gameData.tourOffice.moneyPutted = 0;
                unlockUpgradeCanvas.SetActive(false);
            }
            else
            {
                _gameManager._kikiPrefab.gameData.tourOffice.moneyPutted = 0;
                parUpgrade.GetComponent<ParticleSystem>().Play();
            }

//            ManageFloors();
            fillUI.fillAmount = 0;
        });
    }

}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicketIssueDevice : MonoBehaviour
{
    [HideInInspector] private GameManager _gameManager;

    public Animator ticketIssueDeviceAnimatior;

    public Canvas canvas;
    public CanvasGroup canvasGroup;

    public TextMeshProUGUI parkedAtTimeText;

    public TextMeshProUGUI option1CauseText;
    public TextMeshProUGUI option2CauseText;
    public TextMeshProUGUI option3CauseText;

    public TextMeshProUGUI option1PriceText;
    public TextMeshProUGUI option2PriceText;
    public TextMeshProUGUI option3PriceText;

    public TextMeshPro ticketPriceText;

    public Image[] buttonImages;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Option1Selected()
    {
        print("option1 selected");

        _gameManager._kikiPlayerController.totalAnswerCount++;

        if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 0)
        {
            _gameManager._kikiPlayerController.trueAnswerCount++;

            _gameManager._kikiPrefab.vibrationController.Vibrate(HapticTypes.Success);
            _gameManager._kikiPrefab.vibrationController.Vibrate(.4f, .4f, 15f);

            buttonImages[0].color = Color.green;

            _gameManager._kikiPlayerController.TicketWrite();
        }
        else
        {
            //Yanlış seçim
            _gameManager._kikiPrefab.vibrationController.Vibrate(HapticTypes.Failure);

            gameObject.transform.DOShakeRotation(2f, 7f);

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 0)
                buttonImages[0].color = Color.green;
            else
                buttonImages[0].color = Color.red;

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 1)
                buttonImages[1].color = Color.green;
            else
                buttonImages[1].color = Color.red;

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 2)
                buttonImages[2].color = Color.green;
            else
                buttonImages[2].color = Color.red;

            ParkingAreaInfo parkingAreaInfo =
                _gameManager._kikiPlayerController.zoomedCar.GetComponent<ParkingAreaInfo>();

            if (parkingAreaInfo != null)
            {
                parkingAreaInfo.angryFX.SetActive(true);
                parkingAreaInfo.driverAnimator.SetTrigger("Angry");
            }

            _gameManager._kikiPlayerController.WrongOptionSelected();
        }
    }

    public void Option2Selected()
    {
        print("option2 selected");

        _gameManager._kikiPlayerController.totalAnswerCount++;

        if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 1)
        {
            _gameManager._kikiPlayerController.trueAnswerCount++;
            _gameManager._kikiPrefab.vibrationController.Vibrate(HapticTypes.Success);
            _gameManager._kikiPrefab.vibrationController.Vibrate(.4f, .4f, 15f);

            buttonImages[1].color = Color.green;

            _gameManager._kikiPlayerController.TicketWrite();
        }
        else
        {
            _gameManager._kikiPrefab.vibrationController.Vibrate(HapticTypes.Failure);

            //Yanlış seçim
            gameObject.transform.DOShakeRotation(2f, 7f);

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 0)
                buttonImages[0].color = Color.green;
            else
                buttonImages[0].color = Color.red;

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 1)
                buttonImages[1].color = Color.green;
            else
                buttonImages[1].color = Color.red;

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 2)
                buttonImages[2].color = Color.green;
            else
                buttonImages[2].color = Color.red;

            ParkingAreaInfo parkingAreaInfo =
                _gameManager._kikiPlayerController.zoomedCar.GetComponent<ParkingAreaInfo>();

            if (parkingAreaInfo != null)
            {
                parkingAreaInfo.angryFX.SetActive(true);
                parkingAreaInfo.driverAnimator.SetTrigger("Angry");
            }

            _gameManager._kikiPlayerController.WrongOptionSelected();
        }
    }

    public void Option3Selected()
    {
        print("option3 selected");
        _gameManager._kikiPlayerController.totalAnswerCount++;

        if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 2)
        {
            _gameManager._kikiPlayerController.trueAnswerCount++;

            _gameManager._kikiPrefab.vibrationController.Vibrate(HapticTypes.Success);
            _gameManager._kikiPrefab.vibrationController.Vibrate(.4f, .4f, 15f);

            buttonImages[2].color = Color.green;

            _gameManager._kikiPlayerController.TicketWrite();
        }
        else
        {
            _gameManager._kikiPrefab.vibrationController.Vibrate(HapticTypes.Failure);

            //Yanlış seçim
            gameObject.transform.DOShakeRotation(2f, 7f);

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 0)
                buttonImages[0].color = Color.green;
            else
                buttonImages[0].color = Color.red;

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 1)
                buttonImages[1].color = Color.green;
            else
                buttonImages[1].color = Color.red;

            if (_gameManager._kikiPlayerController.zoomedCarTrueOption == 2)
                buttonImages[2].color = Color.green;
            else
                buttonImages[2].color = Color.red;


            ParkingAreaInfo parkingAreaInfo =
                _gameManager._kikiPlayerController.zoomedCar.GetComponent<ParkingAreaInfo>();

            if (parkingAreaInfo != null)
            {
                parkingAreaInfo.angryFX.SetActive(true);
                parkingAreaInfo.driverAnimator.SetTrigger("Angry");
            }


            _gameManager._kikiPlayerController.WrongOptionSelected();
        }
    }

    public void DeviceOpenFinished()
    {
        print("DeviceOpenFinished ");
    }

    public void DeviceCloseFinished()
    {
        print("DeviceCloseFinished ");
        _gameManager._kikiPlayerController.ticketIssueDevice.gameObject.SetActive(false);

        _gameManager._gameUIManager.looksFineButton.GetComponent<Button>().enabled = true;
        _gameManager._gameUIManager.issueATicketButton.GetComponent<Button>().enabled = true;
        _gameManager._gameUIManager.HideDeviceButton.GetComponent<Button>().enabled = true;
    }

    public void PrintTicketFinished()
    {
        _gameManager._kikiPrefab.vibrationController.StopContinuousHaptic();

        _gameManager._gameUIManager.looksFineButton.GetComponent<Button>().enabled = true;
        _gameManager._gameUIManager.issueATicketButton.GetComponent<Button>().enabled = true;
        _gameManager._gameUIManager.HideDeviceButton.GetComponent<Button>().enabled = true;

        _gameManager._gameUIManager.HideDeviceButton.SetActive(false);
        _gameManager._gameUIManager.looksFineButton.SetActive(false);
        _gameManager._gameUIManager.issueATicketButton.SetActive(false);

        ParkingAreaInfo parkingAreaInfo = _gameManager._kikiPlayerController.zoomedCar.GetComponent<ParkingAreaInfo>();

        if (parkingAreaInfo.hasDriverSpeech && parkingAreaInfo.speechType != SpeechType.None)
        {
            print("openSpeechUI");

            //_gameManager._gameUIManager.actionHolder.GetComponent<Image>().enabled = false;
            _gameManager._gameUIManager.speechChooseLeftButton.SetActive(true);
            _gameManager._gameUIManager.speechChooseRightButton.SetActive(true);

            parkingAreaInfo.speechBubble.GetComponent<BillboardRotation>().TargetToLook =
                _gameManager._kikiPlayerController.transform;


            SpeechBubbleShowPerDistance disrr = _gameManager._kikiPlayerController.zoomedCar
                .GetComponentInChildren<SpeechBubbleShowPerDistance>();

            if (disrr != null)
                disrr.gameObject.SetActive(false);

            _gameManager._gameUIManager.carHUDAnimation.SetTrigger("Open");

            _gameManager._kikiPlayerController.OpenSpeechUI();

            _gameManager._kikiPlayerController.ZoomToCharacterForSpeech();

            parkingAreaInfo = _gameManager._kikiPlayerController.zoomedCar.GetComponent<ParkingAreaInfo>();

            if (parkingAreaInfo.speechType == SpeechType.Rusvet)
            {
                //Solda kabul etmeler
                _gameManager._gameUIManager.speechChooseLeftButtonText.text = KikiGamesUtility.answerTextsRusvet[0];
                _gameManager._gameUIManager.speechChooseRightButtonText.text = KikiGamesUtility.answerTextsRusvet[1];
            }
            else if (parkingAreaInfo.speechType == SpeechType.Tehdit)
            {
                //Solda kabul etmeler
                _gameManager._gameUIManager.speechChooseLeftButtonText.text = KikiGamesUtility.answerTextsTehdit[0];
                _gameManager._gameUIManager.speechChooseRightButtonText.text = KikiGamesUtility.answerTextsTehdit[1];
            }
            else if (parkingAreaInfo.speechType == SpeechType.Yavsama)
            {
                //Solda kabul etmeler
                _gameManager._gameUIManager.speechChooseLeftButtonText.text = KikiGamesUtility.answerTextsYavsama[0];
                _gameManager._gameUIManager.speechChooseRightButtonText.text = KikiGamesUtility.answerTextsYavsama[1];
            }
            else if (parkingAreaInfo.speechType == SpeechType.Karen)
            {
                //Solda kabul etmeler
                _gameManager._gameUIManager.speechChooseLeftButtonText.text = KikiGamesUtility.answerTextsKaren[0];
                _gameManager._gameUIManager.speechChooseRightButtonText.text = KikiGamesUtility.answerTextsKaren[1];
            }

            //_gameManager._gameUIManager.actionHolder.GetComponent<Image>().enabled = false;
            _gameManager._gameUIManager.speechChooseLeftButton.SetActive(true);
            _gameManager._gameUIManager.speechChooseRightButton.SetActive(true);

            print("PrintTicketFinished");

            _gameManager._kikiPlayerController.ticketIssueDevice.gameObject.SetActive(false);


            // if (parkingAreaInfo.hasDriver && parkingAreaInfo.hasDriverSpeech)
            // {
            //     //parkingAreaInfo.speechBubbleText.text = KikiGamesUtility.speechTexts[(int) parkingAreaInfo.speechType]; 
            //     parkingAreaInfo.speechBubble.GetComponent<SpeechBubbleShowPerDistance>().Dissappear();
            //     //parkingAreaInfo.speechBubble.SetActive(false);
            // }

            //_gameManager._kikiPlayerController.WakAgain();


            // if (_gameManager._kikiPlayerController.zoomedCar != null)
            // {
            //     if (parkingAreaInfo.moneyParFX != null)
            //     {
            //         parkingAreaInfo.moneyParFX.SetActive(true);
            //     }
            //
            //     if (parkingAreaInfo.carLevelUpFX != null)
            //     {
            //         parkingAreaInfo.carLevelUpFX.SetActive(true);
            //     }
            // }
        }
        else
        {
            _gameManager._gameUIManager.looksFineButton.GetComponent<Button>().enabled = true;
            _gameManager._gameUIManager.issueATicketButton.GetComponent<Button>().enabled = true;
            _gameManager._gameUIManager.HideDeviceButton.GetComponent<Button>().enabled = true;

            // // _gameManager._gameUIManager.actionHolder.GetComponent<Image>().enabled = false;
            // _gameManager._gameUIManager.speechChooseLeftButton.SetActive(true);
            // _gameManager._gameUIManager.speechChooseRightButton.SetActive(true);
            //
            // _gameManager._gameUIManager.carHUDAnimation.SetTrigger("Open");
            //
            print("PrintTicketFinished 2");
            _gameManager._kikiPlayerController.ticketIssueDevice.gameObject.SetActive(false);

            //
            // if (parkingAreaInfo.hasDriver && parkingAreaInfo.hasDriverSpeech)
            // {
            //     //parkingAreaInfo.speechBubbleText.text = KikiGamesUtility.speechTexts[(int) parkingAreaInfo.speechType]; 
            //     parkingAreaInfo.speechBubble.GetComponent<SpeechBubbleShowPerDistance>().Dissappear();
            //     //parkingAreaInfo.speechBubble.SetActive(false);
            // }

            _gameManager._kikiPlayerController.WakAgain(true);


            if (_gameManager._kikiPlayerController.zoomedCar != null)
            {
                if (parkingAreaInfo.moneyParFX != null)
                {
                    parkingAreaInfo.moneyParFX.SetActive(true);
                }

                if (parkingAreaInfo.carLevelUpFX != null)
                {
                    parkingAreaInfo.carLevelUpFX.SetActive(true);
                }
            }
        }

        // _gameManager._kikiPlayerController.TicketWrite();
    }
}
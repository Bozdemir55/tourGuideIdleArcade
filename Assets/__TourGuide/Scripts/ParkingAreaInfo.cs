using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fine
{
    public FineType fineType;
    // public int fineAmount;
}

public enum FineType
{
    SideWalkParking = 0,
    OverStayParking = 1,
    HandicappedParking = 2,
    WrongDayParking = 3,
    LoudMusic = 4,
    NoTaxiParking = 5,
    TaxiStopParking = 6,
    NoTruckParking = 7,
    BusStopParking = 8,
    StudentParkingOnly = 9,
    PermitParkingOnly = 10,
    VisitorParkingOnly = 11,
    OnlyHumanParking = 12,
    FireHydrantParking = 13,
    BikeLaneParking = 14,
    PayParking = 15,
    NoSpaceCraftParking = 16,
    NoMoonWalk = 17,
    NoFormula1Parking = 18,
    NoDirtyParking = 19,
    WrongColor = 20,
    WrongDirection = 21,
    NoEntranceParking = 22,
    NoElonMusking = 23,
    NoF16Parking =24
}

// public enum SpeechType
// {
//     YavsamaTip1 = 0,
//     YavsamaTip2 = 1,
//     Rusvet1 = 2,
//     Rusvet2 = 3,
//     Rusvet3 = 4,
//     Tehdit1 = 5,
//     Tehdit2 = 6
// }

public enum SpeechType
{
    None = 0,
    Yavsama = 1,
    Rusvet = 2,
    Tehdit = 3,
    Karen = 4,
    Random
}

public class ParkingAreaInfo : MonoBehaviour
{
    // public List<Fine> fineOptions;
    //
    // public int trueOption = 0;

    public bool hasDriver;
    
    public GameObject driver;
    public Animator driverAnimator;
    public Animator carAnimator;
    public GameObject carLevelUpFX;
    public GameObject moneyParFX;
    public GameObject angryFX;
    
     public bool hasDriverSpeech;
     public SpeechType speechType = SpeechType.None;
     public GameObject speechBubble;
     public TextMeshPro speechBubbleText;
     public bool makeMoonwalk;
     public GameObject poffFX;

     public bool hasDetective = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!hasDriver)
        {
            driver.SetActive(false);
            speechBubble.SetActive(false);
        }
        else
        {
            // if(hasDriverSpeech)
            //     speechBubble.SetActive(true);
        }
    }
}




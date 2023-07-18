using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplineManager : MonoBehaviour
{
    private GameManager _gameManager;

    public SplineData[] splineData;
}

[System.Serializable]
public class SplineData
{
    public int parkPlaceId;
    public SplineComputer inSpline;
    public SplineComputer outSpline;//levelId artırdıkça mevcut arabamın maxLevelId'sinden büyükse araba upgrade et
}


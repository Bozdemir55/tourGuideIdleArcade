using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamteck.Splines;
using TMPro;

public class PlanesHolder : MonoBehaviour
{
    private GameManager _gameManager;
    private ParkPlaceInfo _parkPlaceInfo;
    [SerializeField] private SplineFollower _splineFollower;
    public Canvas peopleCanvas;
    public Canvas carUpgradeCanvas;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _parkPlaceInfo = transform.parent.GetComponent<ParkPlaceInfo>();
        _splineFollower = gameObject.GetComponent<SplineFollower>();
        _splineFollower.spline = _gameManager._splineManager.splineData[_parkPlaceInfo.parkPlaceId].outSpline;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnterSpline()
    {
        StartCoroutine(SetSplineEnter());
    }
    public void ExitSpline()
    {
        StartCoroutine(SetSplineExit());
    }

    IEnumerator  SetSplineExit()
    {
        _splineFollower.follow = false;
        _parkPlaceInfo.particleMove.SetActive(false);
        peopleCanvas.gameObject.SetActive(false);
        peopleCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;
        _gameManager._kikiPrefab.gameData.parkPlaceData[_parkPlaceInfo.parkPlaceId].peopleCount = 0;
        peopleCanvas.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            _gameManager._kikiPrefab.gameData.parkPlaceData[_parkPlaceInfo.parkPlaceId].peopleCount.ToString() + " / " +
            _gameManager._kikiPrefab.gameData.parkPlaceData[_parkPlaceInfo.parkPlaceId].Vehicles[_gameManager._kikiPrefab.gameData.parkPlaceData[_parkPlaceInfo.parkPlaceId].activeVehicleLevelId].capacity.ToString();
        _splineFollower.spline = _gameManager._splineManager.splineData[_parkPlaceInfo.parkPlaceId].outSpline;
        yield return new WaitForSeconds(1f);
        _splineFollower.SetPercent(0);
        _splineFollower.follow = true;
//        _gameManager._kikiPrefab.gameData.parkPlaceData[_parkPlaceInfo.parkPlaceId].peopleCount = 0;
        _parkPlaceInfo.particleMove.SetActive(true);
        peopleCanvas.gameObject.SetActive(true);
    }
    IEnumerator  SetSplineEnter()
    {
        _splineFollower.follow = false;
        _parkPlaceInfo.particleMove.SetActive(false);
        _splineFollower.spline = _gameManager._splineManager.splineData[_parkPlaceInfo.parkPlaceId].inSpline;
        yield return new WaitForSeconds(.1f);
        carUpgradeCanvas.gameObject.SetActive(true);
        carUpgradeCanvas.GetComponent<BoxCollider>().enabled = true;
        _parkPlaceInfo.GetComponent<SphereCollider>().enabled = true;
        _splineFollower.SetPercent(0);
        carUpgradeCanvas.gameObject.SetActive(true);
    }
}

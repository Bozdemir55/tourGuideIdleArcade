using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarsHolder : MonoBehaviour
{

    
    private GameManager _gameManager;
    private ParkPlaceInfo _parkPlaceInfo;
    [SerializeField] private SplineFollower _splineFollower;
    public Canvas peopleCanvas;
    public Canvas carUpgradeCanvas;
    public GameObject bombParticleSystem;
    private bool emojiparBool=true;
    private int emojiİnt;
    public GameObject emojiParticleGameObject;
    public List<ParticleSystem> emojiParticleList;

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
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (emojiparBool)
            {
                StartCoroutine(EmojiPar());
            }
            _splineFollower.followSpeed = 0;
            bombParticleSystem.SetActive(true);
            _gameManager._gameUIManager.GameOver(0);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (emojiparBool)
            {
                StartCoroutine(EmojiPar());
            }
        }
        
    }

    private IEnumerator EmojiPar()
    {
        emojiparBool = false;
        if (emojiİnt<(emojiParticleGameObject.transform.childCount-1))
        {
            emojiİnt++;
            emojiParticleList[Random.Range(0, (emojiParticleGameObject.transform.childCount - 1))].Play();
            yield return new WaitForSeconds(.25f);
            StartCoroutine(EmojiPar());
        }
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
        _splineFollower.spline = _gameManager._splineManager.splineData[_parkPlaceInfo.parkPlaceId].inSpline;
        yield return new WaitForSeconds(.25f);
        _splineFollower.SetPercent(0);
        if (_parkPlaceInfo.parkPlaceData.activeVehicleLevelId > 1)
        {
            _parkPlaceInfo.carRenderers[_parkPlaceInfo.parkPlaceData.activeVehicleLevelId]
                .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
        }
        _splineFollower.follow = true;
        _parkPlaceInfo.SetTextCanvas(_parkPlaceInfo.gameObject);
        _parkPlaceInfo.particleMove.SetActive(true);
        peopleCanvas.gameObject.SetActive(true);
    }
    IEnumerator  SetSplineEnter()
    {
        _splineFollower.follow = false;
        _parkPlaceInfo.particleMove.SetActive(false);
        _splineFollower.spline = _gameManager._splineManager.splineData[_parkPlaceInfo.parkPlaceId].outSpline;
        yield return new WaitForSeconds(.1f);
        carUpgradeCanvas.gameObject.SetActive(true);
        carUpgradeCanvas.GetComponent<BoxCollider>().enabled = true;
//        _splineFollower.result.
        _splineFollower.SetPercent(0);
        _parkPlaceInfo.GetComponent<SphereCollider>().enabled = true;
        carUpgradeCanvas.gameObject.SetActive(true);
    }
}

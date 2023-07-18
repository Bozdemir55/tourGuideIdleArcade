using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using Dreamteck.Splines;

public enum PeopleColorType
{
    blue,
    green,
    red
}

public class NeutralControl : BasePlayerControl
{
    private GameManager _gameManager;
    private RandomPosSet _randomPosSet;
    [SerializeField] private GameObject _neutralBody;

    [SerializeField] private bool _instantiateControl = false;


    public PeopleColorType peopleColorType;
    public ParkPlaceInfo _parkPlaceInfo;
    public ParkPlaceData _parkPlaceData;

    private float _distanceValue = 2;
    private float distance = 0;
    private float _colliderDistance;
    private int _playerCount = 0;

    private bool _playerCrash = false;
    private bool _fakeCrash = false;
    private bool _isCrashed = false;
    private bool _neutralInPlayer = true;

    private GameObject followObject;
    private GameObject rotaterObject;

    private CapsuleCollider _capsulControl;
    private PlayerCollision playerCollision;
    private Material _bodyMaterial;
    private Quaternion _newRotate;

    public bool soundActive = false;
    float wanderRadius = 40f;
    public float wanderTimer;
    public Vector3 newPos;
    private float timer;
    private bool runAwayToWander;
    private NavMeshAgent _agent;
    private bool isArrivedToFirstPos;
    public bool stackPeople;
    private int scaleCount;
    public bool scaleBool;
    public int animScaleCount;
    public GameObject cursor;
    public ParticleSystem spawnParticle;
    public List<ParticleSystem> emojiParticel;
    public List<ParticleSystem> emojiHappyParticle;

    public bool blueTutorialActive;
    public bool greenTutorialActive;

    public bool redTutorialActive;
    //public GameObject referansCube;

    public bool IsCrashed
    {
        get => _isCrashed;
        set => _isCrashed = value;
    }

    public bool PlayerCrash
    {
        get => _playerCrash;
        set => _playerCrash = value;
    }

    public bool FakeCrash
    {
        get => _fakeCrash;
        set => _fakeCrash = value;
    }

    public bool InstantiateControl
    {
        get => _instantiateControl;
        set => _instantiateControl = value;
    }

    private bool isMouseUp;


    protected override void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _randomPosSet = FindObjectOfType<RandomPosSet>();
        base.Start();
        playerCollision = PlayerCollision.Instance;
        _bodyMaterial = _neutralBody.GetComponent<Renderer>().material;
        _capsulControl = GetComponent<CapsuleCollider>();
        cursor = FindObjectOfType<CursorScript>().gameObject;
        speed = 1;
        anim.SetInteger("AnimStatus", 1);

        wanderTimer = 0f; //Random.Range(0f, 2f);

//        newPos = RandomNavSphere(transform.transform.position, Random.Range(7, 15), 1);
        //referansCube.transform.position = newPos;
        // print(newPos);
    }

    protected override void FixedUpdate()
    {
        DistanceControl();
        base.FixedUpdate();
    }

    private void DistanceControl()
    {
        if (IsCrashed)
        {
            distance = Vector3.Distance(transform.position, followObject.transform.position);

            if (distance > _distanceValue)
            {
                speed = 8;
                _neutralInPlayer = false;
            }
            else if (distance < _distanceValue)
            {
                speed = 4.9f;
                _neutralInPlayer = true;
            }

            if (isMouseUp)
            {
                speed = 0;
            }
            else
            {
                speed = 8;
            }


            // if (distance > _colliderDistance)
            // {
            //     _capsulControl.enabled = false;
            // }
            // else if (!_capsulControl.enabled)
            // {
            //     _capsulControl.enabled = true;
            // }

            if (!_neutralInPlayer)
            {
                transform.LookAt(followObject.transform.position);
            }
            else
            {
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, rotaterObject.transform.localRotation, 0.1f);
            }
        }
        else
        {
            Vector3 targetDirection = newPos - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (!isArrivedToFirstPos)
            {
                if (Utilitiess.Distance(transform.position, newPos) <= 1f) //Pozisyona ulaştı
                {
                    isArrivedToFirstPos = true;
                    speed = 0;
                    anim.SetInteger("AnimStatus", 2);
                }
            }
        }

        // _newRotate = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
        // _newRotate = Quaternion.Euler(0,(newPos-transform.position).y,0);
        //transform.localRotation = _newRotate;
    }

    private void Update()
    {
        if (!IsCrashed)
            return;

        if (Input.GetMouseButtonUp(0))
        {
            isMouseUp = true;
            anim.SetInteger("AnimStatus", 2);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isMouseUp = false;
            anim.SetInteger("AnimStatus", 1);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            for (int i = 0; i < playerCollision.stackBluePeople.Count; i++)
            {
                playerCollision.stackBluePeople[i].GetComponent<NeutralControl>()
                    .emojiParticel[Random.Range(0, emojiParticel.Count - 1)].Play();
            }

            for (int i = 0; i < playerCollision.stackGreenPeople.Count; i++)
            {
                playerCollision.stackGreenPeople[i].GetComponent<NeutralControl>()
                    .emojiParticel[Random.Range(0, emojiParticel.Count - 1)].Play();
            }

            for (int i = 0; i < playerCollision.stackRedPeople.Count; i++)
            {
                playerCollision.stackRedPeople[i].GetComponent<NeutralControl>()
                    .emojiParticel[Random.Range(0, emojiParticel.Count - 1)].Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            for (int i = 0; i < playerCollision.stackBluePeople.Count; i++)
            {
                playerCollision.stackBluePeople[i].GetComponent<NeutralControl>()
                    .emojiHappyParticle[Random.Range(0, emojiHappyParticle.Count - 1)].Play();
            }

            for (int i = 0; i < playerCollision.stackGreenPeople.Count; i++)
            {
                playerCollision.stackGreenPeople[i].GetComponent<NeutralControl>()
                    .emojiHappyParticle[Random.Range(0, emojiHappyParticle.Count - 1)].Play();
            }

            for (int i = 0; i < playerCollision.stackRedPeople.Count; i++)
            {
                playerCollision.stackRedPeople[i].GetComponent<NeutralControl>()
                    .emojiHappyParticle[Random.Range(0, emojiHappyParticle.Count - 1)].Play();
            }
        }
    }

    public void MoveVehicle(ParkPlaceInfo parkPlaceInfo, ParkPlaceData parkPlaceData, GameObject parkPlaceCollider)
    {
        stackPeople = true;
        anim.SetTrigger("Runing");
        anim.speed = 2f;
        transform.DOMove(new Vector3(parkPlaceCollider.transform.position.x, .3f,
            parkPlaceCollider.transform.position.z), 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            //------------------ burada işlem yapıyorum --------------------------------
            gameObject.SetActive(false);
        });
    }

    private void ScaleActive(ParkPlaceInfo parkPlaceInfo, ParkPlaceData parkPlaceData, float scaleValue,
        Transform vehicleTransform, PeopleColorType _peopleColorType)
    {
//        vehicleTransform = parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform;
        vehicleTransform.DOScale(vehicleTransform.localScale.x * scaleValue, .15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            vehicleTransform.DOScale(vehicleTransform.localScale.x / scaleValue, .15f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    scaleCount++;
                    if (scaleCount < 2)
                    {
                        ScaleActive(parkPlaceInfo, parkPlaceData, scaleValue, vehicleTransform, peopleColorType);
                    }
                    else
                    {
                        vehicleTransform.DOScale(vehicleTransform.localScale.x / vehicleTransform.localScale.x, .15f)
                            .SetEase(Ease.Linear);
                    }
                });
        });
    }

//collison
    void OnCollisionEnter(Collision collision)
    {
        GameObject _other = collision.gameObject;

        if (_other.GetComponent<PlayerCollision>())
        {
            ;
            if (_gameManager._kikiPrefab.gameData.tourOffice.capacity -
                (_other.GetComponent<PlayerCollision>().stackBluePeople.Count +
                 _other.GetComponent<PlayerCollision>().stackGreenPeople.Count +
                 _other.GetComponent<PlayerCollision>().stackRedPeople.Count) > 0)
            {

                if (!IsCrashed)
                {
//                    print("carTurorialActive :"+_gameManager._kikiPrefab.gameData.carTurorialActive);
                    speed = 8;
                    anim.SetInteger("AnimStatus", 1);


                    
                    if (peopleColorType == PeopleColorType.blue)
                    {
                        _other.GetComponent<PlayerCollision>().stackBluePeople.Add(gameObject);
                        _gameManager._makeNoise.PlaySFX(9);
                        _other.GetComponent<PlayerControl>().cursor.GetComponent<CursorScript>().peopleLook = false;
//                        _other.GetComponent<PlayerControl>().cursor.transform.GetChild(0).gameObject.SetActive(false);
                        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 1 && blueTutorialActive)
                        {
                            if (_gameManager._kikiPrefab.gameData.carTurorialActive)
                            {
                                PlayerPrefs.SetInt("TutorialCar-CallPeople", 2);
                            }
                        }
                    }

                    if (peopleColorType == PeopleColorType.green)
                    {
                        _other.GetComponent<PlayerCollision>().stackGreenPeople.Add(gameObject);
                        _gameManager._makeNoise.PlaySFX(9);
                        _other.GetComponent<PlayerControl>().cursor.GetComponent<CursorScript>().peopleLook = false;
//                        _other.GetComponent<PlayerControl>().cursor.transform.GetChild(0).gameObject.SetActive(false);
                        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 1 && greenTutorialActive)
                        {
                            if (_gameManager._kikiPrefab.gameData.carTurorialActive)
                            {
                                PlayerPrefs.SetInt("TutorialCar-CallPeople", 2);
                            }
                        }
                    }

                    if (peopleColorType == PeopleColorType.red)
                    {
                        _other.GetComponent<PlayerCollision>().stackRedPeople.Add(gameObject);
                        _gameManager._makeNoise.PlaySFX(9);
                        _other.GetComponent<PlayerControl>().cursor.GetComponent<CursorScript>().peopleLook = false;
//                        _other.GetComponent<PlayerControl>().cursor.transform.GetChild(0).gameObject.SetActive(false);
                        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 1 && redTutorialActive)
                        {
                            if (_gameManager._kikiPrefab.gameData.carTurorialActive)
                            {
                                PlayerPrefs.SetInt("TutorialCar-CallPeople", 2);
                            }
                        }
                    }

                    _other.GetComponent<PlayerCollision>()?.CrashMePlayer(gameObject);
                    _other.GetComponent<FakeCollision>()?.CrashMeFake(gameObject);
                    _other.GetComponent<BaseCollision>()?.CrashNeutral(gameObject);

                    // _other.GetComponent<PlayerCollision>().stackPeople.Count;

                    // if (!InstantiateControl)
                    // {
                    //     _other.GetComponent<BaseCollision>().MoneyCount--;
                    // }
                    _capsulControl.isTrigger = false;
                }
            }
        }

        if (collision.transform.CompareTag("Vehicle"))
        {
            if (peopleColorType == PeopleColorType.blue &&
                _gameManager._kikiPrefab.gameData
                    .parkPlaceData[collision.transform.parent.parent.GetComponent<ParkPlaceInfo>().parkPlaceId]
                    .colorType == ColorType.Blue)
            {
                if (stackPeople)
                {
                    //------------------ burada işlem yapıyorum --------------------------------
//                    gameObject.SetActive(false);
                    //------------------ burada işlem yapıyorum --------------------------------


                    _gameManager._kikiPrefab.gameData.peopleCount--;
                    StartCoroutine(_randomPosSet.CallSpawner(peopleColorType));
                    _gameManager._gameUIManager.UpdatePeopleCountText();
                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Car)
                    {
//                    _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
//                    _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform    
//                        .DOPunchScale(new Vector3(.2f,.2f,.2f), .75f, 5,elasticity:.4f).OnComplete(() =>
//                        {
//                           _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform
//                                .DOScale(Vector3.one, .1f);
//                        });
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
                    {
//                    _parkPlaceInfo.helis[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.helis[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                    {
//                    _parkPlaceInfo.ships[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.025f,
                            _parkPlaceInfo.ships[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
                    {
//                    _parkPlaceInfo.planes[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.planes[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (playerCollision.CPISecondIdea)
                    {
                        float blendShapeValuecar = (float) _parkPlaceData.peopleCount /
                                                   (float) 20 * 100f;

                        if (_parkPlaceData.parkPlaceType == ParkPlaceType.Car &&
                            _parkPlaceData.activeVehicleLevelId == 0)
                        {
                            _parkPlaceInfo.carRenderers[_parkPlaceData.activeVehicleLevelId]
                                .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShapeValuecar);
                        }
                    }

                    float blendShapeValue = (float) _parkPlaceData.peopleCount /
                                            (float) _parkPlaceData.Vehicles[_parkPlaceData.activeVehicleLevelId]
                                                .capacity * 100f;
                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Car ||
                        _parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                    {
                        if (_parkPlaceData.activeVehicleLevelId > 1)
                            _parkPlaceInfo.carRenderers[_parkPlaceData.activeVehicleLevelId]
                                .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShapeValue);
                    }
                    else
                    {
//                    _parkPlaceInfo.carRenderers[_parkPlaceData.activeVehicleLevelId]
//                        .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShapeValue);
                    }
//                StartCoroutine(
//                    _parkPlaceInfo.moneyHolder.MoneyEnumerator(_parkPlaceInfo, _parkPlaceData, .01f));

                    playerCollision.UpdateCollectedPeopleCountText();
                    playerCollision.CheckType(_parkPlaceInfo.gameObject, _parkPlaceData.parkPlaceType);
                    playerCollision.SetTextCanvas(_parkPlaceInfo.gameObject);
                }
            }

            if (peopleColorType == PeopleColorType.green &&
                _gameManager._kikiPrefab.gameData
                    .parkPlaceData[collision.transform.parent.parent.GetComponent<ParkPlaceInfo>().parkPlaceId]
                    .colorType == ColorType.Green)
            {
                if (stackPeople)
                {
                    StartCoroutine(_randomPosSet.CallSpawner(peopleColorType));
                    gameObject.SetActive(false);
                    _gameManager._kikiPrefab.gameData.peopleCount--;

                    _gameManager._gameUIManager.UpdatePeopleCountText();
                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Car)
                    {
//                    _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
//                    _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform    
//                        .DOPunchScale(new Vector3(.2f,.2f,.2f), .75f, 5,elasticity:.4f).OnComplete(() =>
//                        {
//                           _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform
//                                .DOScale(Vector3.one, .1f);
//                        });
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
                    {
//                    _parkPlaceInfo.helis[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.helis[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                    {
//                    _parkPlaceInfo.ships[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.025f,
                            _parkPlaceInfo.ships[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
                    {
//                    _parkPlaceInfo.planes[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.planes[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    float blendShapeValue = (float) _parkPlaceData.peopleCount /
                                            (float) _parkPlaceData.Vehicles[_parkPlaceData.activeVehicleLevelId]
                                                .capacity * 100f;

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Car ||
                        _parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                    {
                        if (_parkPlaceData.activeVehicleLevelId > 1)
                            _parkPlaceInfo.carRenderers[_parkPlaceData.activeVehicleLevelId]
                                .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShapeValue);
                    }
                    else
                    {
//                    _parkPlaceInfo.carRenderers[_parkPlaceData.activeVehicleLevelId]
//                        .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShapeValue);
                    }
//                StartCoroutine(
//                    _parkPlaceInfo.moneyHolder.MoneyEnumerator(_parkPlaceInfo, _parkPlaceData, .01f));

                    playerCollision.UpdateCollectedPeopleCountText();
                    playerCollision.CheckType(_parkPlaceInfo.gameObject, _parkPlaceData.parkPlaceType);
                    playerCollision.SetTextCanvas(_parkPlaceInfo.gameObject);
                }
            }

            if (peopleColorType == PeopleColorType.red &&
                _gameManager._kikiPrefab.gameData
                    .parkPlaceData[collision.transform.parent.parent.GetComponent<ParkPlaceInfo>().parkPlaceId]
                    .colorType == ColorType.Red)
            {
                if (stackPeople)
                {
                    StartCoroutine(_randomPosSet.CallSpawner(peopleColorType));
                    gameObject.SetActive(false);
                    _gameManager._kikiPrefab.gameData.peopleCount--;

                    _gameManager._gameUIManager.UpdatePeopleCountText();
                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Car)
                    {
//                    _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
//                    _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform    
//                        .DOPunchScale(new Vector3(.2f,.2f,.2f), .75f, 5,elasticity:.4f).OnComplete(() =>
//                        {
//                           _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform
//                                .DOScale(Vector3.one, .1f);
//                        });
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.cars[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Heli)
                    {
//                    _parkPlaceInfo.helis[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.helis[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                    {
//                    _parkPlaceInfo.ships[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.025f,
                            _parkPlaceInfo.ships[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Plane)
                    {
//                    _parkPlaceInfo.planes[_parkPlaceData.activeVehicleLevelId].transform.parent
//                        .GetComponent<Animator>()
//                        .SetTrigger("Active");
                        ScaleActive(_parkPlaceInfo, _parkPlaceData, 1.2f,
                            _parkPlaceInfo.planes[_parkPlaceData.activeVehicleLevelId].transform.parent.transform,
                            peopleColorType);
                    }

                    float blendShapeValue = (float) _parkPlaceData.peopleCount /
                                            (float) _parkPlaceData.Vehicles[_parkPlaceData.activeVehicleLevelId]
                                                .capacity * 100f;

                    if (_parkPlaceData.parkPlaceType == ParkPlaceType.Car ||
                        _parkPlaceData.parkPlaceType == ParkPlaceType.Ship)
                    {
                        if (_parkPlaceData.activeVehicleLevelId > 1)
                            _parkPlaceInfo.carRenderers[_parkPlaceData.activeVehicleLevelId]
                                .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShapeValue);
                    }
                    else
                    {
//                    _parkPlaceInfo.carRenderers[_parkPlaceData.activeVehicleLevelId]
//                        .GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShapeValue);
                    }
//                StartCoroutine( 
//                    _parkPlaceInfo.moneyHolder.MoneyEnumerator(_parkPlaceInfo, _parkPlaceData, .01f));

                    playerCollision.UpdateCollectedPeopleCountText();
                    playerCollision.CheckType(_parkPlaceInfo.gameObject, _parkPlaceData.parkPlaceType);
                    playerCollision.SetTextCanvas(_parkPlaceInfo.gameObject);
                }
            }

            if (soundActive)
            {
                _gameManager._makeNoise.PlaySFX(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // artık kullanmıyoruz collisona çektim bu kısmı 

        //---------------------------------------------------------------------------------------------------

        GameObject _other = other.gameObject;

        if (_other.GetComponent<PlayerCollision>())
        {
//            print("carTurorialActive :"+_gameManager._kikiPrefab.gameData.carTurorialActive);
            if (_gameManager._kikiPrefab.gameData.tourOffice.capacity -
                (_other.GetComponent<PlayerCollision>().stackBluePeople.Count +
                 _other.GetComponent<PlayerCollision>().stackGreenPeople.Count +
                 _other.GetComponent<PlayerCollision>().stackRedPeople.Count) > 0)
            {
                if (!IsCrashed)
                {
                    speed = 8;
                    anim.SetInteger("AnimStatus", 1);

                    if (peopleColorType == PeopleColorType.blue)
                    {
                        _other.GetComponent<PlayerCollision>().stackBluePeople.Add(gameObject);
                        _gameManager._makeNoise.PlaySFX(9);
                        cursor.GetComponent<CursorScript>().peopleLook = false;
//                        print("carTurorialActive"+_gameManager._kikiPrefab.gameData.carTurorialActive);
//                        print(blueTutorialActive);
                        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 1 &&
                            _gameManager._kikiPrefab.gameData.carTurorialActive)
                        {
                            PlayerPrefs.SetInt("TutorialCar-CallPeople", 2);
                        }
                    }

                    if (peopleColorType == PeopleColorType.green)
                    {
                        _other.GetComponent<PlayerCollision>().stackGreenPeople.Add(gameObject);
                        _gameManager._makeNoise.PlaySFX(9);
                        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 1 &&
                            _gameManager._kikiPrefab.gameData.carTurorialActive)
                        {
                            PlayerPrefs.SetInt("TutorialCar-CallPeople", 2);
                        }
                    }

                    if (peopleColorType == PeopleColorType.red)
                    {
                        _other.GetComponent<PlayerCollision>().stackRedPeople.Add(gameObject);
                        _gameManager._makeNoise.PlaySFX(9);
                        if (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 1 &&
                            _gameManager._kikiPrefab.gameData.carTurorialActive)
                        {
                            PlayerPrefs.SetInt("TutorialCar-CallPeople", 2);
                        }
                    }

                    _other.GetComponent<PlayerCollision>()?.CrashMePlayer(gameObject);
                    _other.GetComponent<FakeCollision>()?.CrashMeFake(gameObject);
                    _other.GetComponent<BaseCollision>()?.CrashNeutral(gameObject);

                    // _other.GetComponent<PlayerCollision>().stackPeople.Count;

                    // if (!InstantiateControl)
                    // {
                    //     _other.GetComponent<BaseCollision>().MoneyCount--;
                    // }
                    _capsulControl.isTrigger = false;
                }
            }
        }


//-----------------------------------------------------------------------------------------------------


//
//        if (_other.CompareTag("ParkPlace"))
//        {
//
//        }
//        if (_other.CompareTag("ParkPlace"))
//        {
//            anim.SetTrigger("Runing");
//            anim.speed = 2f;
//            stackPeople = true;
//            ToIntoCrashObject(_other, _bodyMaterial);
//            PlayerCrash = false;
//        }

//        if (_other.CompareTag("ParkPlace"))
//        {
//            if (peopleColorType == PeopleColorType.blue &&
        //                _gameManager._kikiPrefab.gameData.parkPlaceData[_other.GetComponent<ParkPlaceInfo>().parkPlaceId]
        //                    .colorType == ColorType.Blue)
//            {
//                anim.SetTrigger("Runing");
//                anim.speed = 2f;
//                stackPeople = true;
//                ToIntoCrashObject(_other, _bodyMaterial);
//                PlayerCrash = false;
//            }
//
//            if (peopleColorType == PeopleColorType.green &&
//                _gameManager._kikiPrefab.gameData.parkPlaceData[_other.GetComponent<ParkPlaceInfo>().parkPlaceId]
//                    .colorType == ColorType.Green)
//            {
//                anim.SetTrigger("Runing");
//                anim.speed = 2f;
//                stackPeople = true;
//                ToIntoCrashObject(_other, _bodyMaterial);
//                PlayerCrash = false;
//            }
//
//            if (peopleColorType == PeopleColorType.red &&
//                _gameManager._kikiPrefab.gameData.parkPlaceData[_other.GetComponent<ParkPlaceInfo>().parkPlaceId]
//                    .colorType == ColorType.Red)
//            {
//                anim.SetTrigger("Runing");
//                anim.speed = 2f;
//                stackPeople = true;
//                ToIntoCrashObject(_other, _bodyMaterial);
//                PlayerCrash = false;
//            }
//        }
        // if (IsCrashed && transform.parent.GetChild(0) != null)
        // {
        //     ShowScore(transform.parent.GetChild(0).gameObject);
        // }
        //
        // if (IsCrashed && transform.parent.GetChild(1) != null)
        // {
        //     ShowScore(transform.parent.GetChild(1).gameObject);
        // }

//        if (IsCrashed && _other.GetComponent<BaseCollision>()?.MyPlayerCount > _playerCount)
//        {
//            _other.GetComponent<BaseCollision>()?.CrashNeutral(gameObject);
//        }
        //Adamları listeye aldığım yer
        // if (other.CompareTag("NeutralPlayer"))
        // {
        //     print("_playerCount : " + _playerCount);
        //     print("tourOffice.levelId : " + _gameManager._kikiPrefab.gameData.tourOffice.levelId);
        //     if (_playerCount < _gameManager._kikiPrefab.gameData.tourOffice.levelId)
        //     {
        //         _other.GetComponent<BaseCollision>()?.CrashNeutral(gameObject);
        //     }
        // }
    }

    // private void ShowScore(GameObject newGameObject)
    // {
    //     newGameObject.GetComponent<BaseCollision>()?.WriteScore();
    // }

    public void ToIntoCrashObject(GameObject newGameObject, Material newMat)
    {
        rotaterObject = newGameObject;

        if (PlayerCrash)
        {
            followObject = newGameObject.transform.GetChild(0).gameObject;
        }
        else if (FakeCrash)
        {
            followObject = newGameObject.transform.parent.GetChild(0).gameObject;
        }

//        if (stackPeople)
//        {
//            followObject = newGameObject.gameObject;
//            newGameObject.GetComponent<NeutralControl>()?.ToIntoCrashObject(gameObject, newMat);
//            transform.DOMove(new Vector3(followObject.transform.position.x, .3f, followObject.transform.position.z),
//                1.5f).SetEase(Ease.Linear).OnComplete(
//                () =>
//                {
//                    _gameManager._kikiPrefab.gameData
//                        .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId].peopleCount++;
//                    if (peopleColorType == PeopleColorType.blue)
//                    {
//                        playerCollision.stackBluePeople.RemoveAt(0);
//                    }
//
//                    if (peopleColorType == PeopleColorType.green)
//                    {
//                        playerCollision.stackGreenPeople.RemoveAt(0);
//                    }
//
//                    if (peopleColorType == PeopleColorType.red)
//                    {
//                        playerCollision.stackRedPeople.RemoveAt(0);
//                    }
//
//                    playerCollision.UpdateCollectedPeopleCountText();
//                    playerCollision.CheckType(followObject.GetComponent<ParkPlaceInfo>().gameObject, _gameManager
//                        ._kikiPrefab.gameData
//                        .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId].parkPlaceType);
//                    playerCollision.SetTextCanvas(followObject.GetComponent<ParkPlaceInfo>().gameObject);
//                    Destroy(gameObject);
//                    if (_gameManager._kikiPrefab.gameData
//                            .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId].peopleCount ==
//                        _gameManager._kikiPrefab.gameData
//                            .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId]
//                            .Vehicles[
//                                _gameManager._kikiPrefab.gameData
//                                    .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId]
//                                    .activeVehicleLevelId].capacity)
//                    {
//                        followObject.GetComponent<ParkPlaceInfo>().upgradeCanvas.gameObject.SetActive(false);
//                        followObject.GetComponent<ParkPlaceInfo>().particleMove.SetActive(true);
//                        StartCoroutine(followObject.GetComponent<ParkPlaceInfo>().moneyHolder.MoneyEnumerator(
//                            followObject.GetComponent<ParkPlaceInfo>(), _gameManager._kikiPrefab.gameData
//                                .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId], .01f));
//                        if (_gameManager._kikiPrefab.gameData
//                                .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId].parkPlaceType ==
//                            ParkPlaceType.Car)
//                        {
//                            followObject.GetComponent<ParkPlaceInfo>().carsHolder.GetComponent<SplineFollower>()
//                                .follow = true;
//                        }
//
//                        if (_gameManager._kikiPrefab.gameData
//                                .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId].parkPlaceType ==
//                            ParkPlaceType.Ship)
//                        {
//                            followObject.GetComponent<ParkPlaceInfo>().shipsHolder.GetComponent<SplineFollower>()
//                                .follow = true;
//                        }
//
//                        if (_gameManager._kikiPrefab.gameData
//                                .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId].parkPlaceType ==
//                            ParkPlaceType.Heli)
//                        {
//                            followObject.GetComponent<ParkPlaceInfo>().helisHolder.GetComponent<SplineFollower>()
//                                .follow = true;
//                        }
//
//                        if (_gameManager._kikiPrefab.gameData
//                                .parkPlaceData[followObject.GetComponent<ParkPlaceInfo>().parkPlaceId].parkPlaceType ==
//                            ParkPlaceType.Plane)
//                        {
//                            followObject.GetComponent<ParkPlaceInfo>().planesHolder.GetComponent<SplineFollower>()
//                                .follow = true;
//                        }
//
//                        playerCollision.peopleInfoCanvas.gameObject.SetActive(false);
//                    }
//                });
//        }

        newGameObject.gameObject.GetComponent<BaseCollision>()?.UpdateBoxSize();
        //ShowScore(newGameObject);

//        _neutralBody.GetComponent<Renderer>().material = newMat;

        rb.mass = 1;
        rb.drag = 0;
        anim.SetInteger("AnimStatus", 1);
        _isCrashed = true;
        stackPeople = false;
    }

    public void UpdatePlayerCountParent(int newPlayerCount, float newDistance)
    {
        _playerCount = newPlayerCount;
        _distanceValue = newDistance;
        _colliderDistance = newDistance * 5;
    }

    public void ChangeRotation()
    {
        if (!PlayerCrash && !FakeCrash)
        {
            Quaternion newRotate = transform.rotation;
            newRotate.y += 180;
            transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f, RotateMode.Fast).SetRelative(true);
        }
    }


    public void InstantiateTrue()
    {
        _instantiateControl = true;
    }

    public void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            timer = -8f;
            newPos = RandomNavSphere(transform.transform.position, Random.Range(3, 8), 1);
            //referansCube.transform.position = newPos;
            //_agent.SetDestination(newPos);
        }

        if (Vector3.Distance(transform.transform.position, newPos) <= .3f)
        {
            timer = 0;
        }

// if (Vector3.Distance(_myTransform.transform.position, newPos) <= .3f)
// {
//     timer = -0f;
// }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}

public enum PeopleStatus
{
    idle,
    Following,
    Hit,
    Attack,
    Death,
    Down,
    Standing,
    RunAway,
    Wander,
    Dancing
}
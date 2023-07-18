using System.Collections;
using System.Collections.Generic;
using MText;
using UnityEngine;

public enum BreakableState
{
    Live,
    Dead
}

public class BreakableOnTheWayController : MonoBehaviour
{
    private GameManager _gameManager;

    [HideInInspector] public BreakableState _state = BreakableState.Live;
    public Modular3DText modular3DText = null;

    //private Animator _animator;
    //public AnimationClip[] _liveAnimationClips;
    //public AnimationClip[] _dieAnimationClips;

    //private AnimatorOverrideController _animatorOverrideController;
//    [HideInInspector] public DeactivateMe deactivateMe;
//    private float deactivateDelay = 3;
//
//    private EnemyController _enemyController;

    [HideInInspector] public int _id;
    public int _health = 1;
    [HideInInspector] public int _healthStarted;
    private int _lifeCount;

    [HideInInspector] public Transform _myTransform;

    public bool hasGem = false;
    public float gemYPos = 2;

    public bool hasCoin = false;
    public float coinYPos = 2;

    public bool isFinalObject;

//    private bool _hit;
//    private bool _attackLock;
//
//    private CoinHit _coinHit;
//
//    [HideInInspector] public Flicker _flicker;
//
//    [HideInInspector] public bool limitOneDamgerPerFrame = true; //when enabled the esc can only take damage once per frame
//
    private bool canTakeDamage = true;

//
    private int damageCount = 0;

    [HideInInspector] public Collector _connectedGem;

    [HideInInspector] public Collector _connectedCoin;
    //[HideInInspector] public CapsuleCollider _collider;

//    public Color fillColor;
//
//    private float _startTime;
//
//    public Texture2D shaderTexture;
//    private Material shaderMaterial;
//    private Material shaderMaterial2;
//    public float ySize = 0;
//
//    public float tilingX = 1;
//    public float tilingY = 1;
//
//    private float fillRate = 0;
//
//    //private float fillSpeed = .18f;
//    private float deFillSpeed = 0f;
//
//    //private float fillSpeed = .18f;
//    private float fillSpeed = .10f;
//    private float FPSfillSpeed = .05f;
//    private bool inside = false;
//
//    private int insidePainterCount = 0;
//
//    public bool playSuccessFillSoundFX = true;
//    [HideInInspector] public FillSoundSFXType fillSoundSfxType;
//
//    [HideInInspector] public bool painted = false;
//
//    [HideInInspector] public List<PaintingPoint> paintingPoints = new List<PaintingPoint>();
//
////    public List<PainterController> painters = new List<PainterController>();
//
//    [HideInInspector] public bool thisIsActive = false;
//
//    private bool playPainting = false;
//
//    private int _colorableLocalPaintingFXIndex = 0;
//    private int _colorableLocalConfettiFXIndex = 0;
//    private int _colorableLocalDollarFXIndex = 0;
//
//
//    private int rewardThrowForceX = 150;
//    private int rewardThrowForceY = 100;
//    private int rewardThrowForceZ = 150;

    private void Awake()
    {
        // _gameManager = FindObjectOfType<_gameManager>();

        // _collider = GetComponent<CapsuleCollider>();

//        _skins = new GameObject[_skinsHolder.childCount];
//        for (int i = 0; i < _skins.Length; i++)
//            _skins[i] = _skinsHolder.GetChild(i).gameObject;

//        _animator = GetComponentInChildren<Animator>();
//        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
//        _animator.runtimeAnimatorController = _animatorOverrideController;

        // _coinHit = GetComponent<CoinHit>();

        // _flicker = GetComponent<Flicker>();

        _myTransform = transform;
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        Reset();
    }

    public void Setup()
    {
       // FindObjectOfType<GameManager>()._allBreakableOnTheWayObjects.Add(this);
        //_gameManager._playerController._breakableOnTheWayObjects.Add(this);
    }

    private void Reset()
    {
        //_collider = GetComponent<CapsuleCollider>();

        if (modular3DText != null)
        {
            modular3DText.Text = _health.ToString();
            modular3DText.UpdateText();
        }

        _state = BreakableState.Live;
        _healthStarted = _health;
    }

    private int rewardThrowForceX = 150;
    private int rewardThrowForceY = 200;
    private int rewardThrowForceZ = 150;


    public void Hit(int damage)
    {
        if (_state == BreakableState.Dead)
            return;


        //bool attack = _state == EnvironmentState.Attack;

        //CancelInvoke();

        //_state = ColorableState.Dead;

        _health -= damage;

        if (hasCoin)
        {
            if (_health == 0)
                _gameManager._makeNoise.PlaySFX(104); // Saçılma sesi

//            _gameManager._breakableCoinFXs[_gameManager._breakableCoinFXIndex].SetActive(true);
//            _gameManager._breakableCoinFXs[_gameManager._breakableCoinFXIndex].GetComponent<Collector>().canCollectible = true;
//            _gameManager._breakableCoinFXs[_gameManager._breakableCoinFXIndex].transform.position = _myTransform.position + new Vector3(0, coinYPos, 0);
//
//            _gameManager._breakableCoinFXs[_gameManager._breakableCoinFXIndex].GetComponent<Rigidbody>().AddForce(
//                new Vector3(
//                    Random.Range(-.8f, .8f) * rewardThrowForceX,
//                    2 * rewardThrowForceY,
//                    Random.Range(1f, 2f) * rewardThrowForceZ
//                ));
//
//
//            Debug.Log(_gameManager._breakableCoinFXIndex);
//            _gameManager._breakableCoinFXIndex++;
        }

//        Debug.Log("damage: " + damage);
//        Debug.Log("health: " + _health);

        if (modular3DText != null)
        {
            modular3DText.Text = _health.ToString();
            modular3DText.UpdateText();
        }

        if (_health <= 0)
        {
            _state = BreakableState.Dead;

            _gameManager._makeNoise.PlaySFX(102); // KIRILMA sesi

            if (modular3DText != null)
            {
                modular3DText.Text = "";
                modular3DText.UpdateText();
            }

            if (_connectedGem != null)
                _connectedGem.canCollectible = true;

//            if (_gameManager._allBreakableOnTheWayObjects.Contains(this))
//            {
//                _gameManager._allBreakableOnTheWayObjects.Remove(this);
//            }
//
//            if (_gameManager._playerController._breakableOnTheWayObjects.Contains(this))
//            {
//                _gameManager._playerController._breakableOnTheWayObjects.Remove(this);
//            }

            //_gameManager._camSlowMotionDelay.StartSlowMotionDelayNew(.05f);
            _gameManager._camShake.ShakeNew(.6f, false, .5f);
            //_gameManager._camSlowMotionDelay.StartSlowMotionDelayNew(1f);
            //_gameManager._kikiPrefab.vibrationController.Vibrate(.5f, .5f, .2f);

            //_gameManager._camShake.ShakeNew(3f, false, .4f);
        }
//        
        //_gameManager.FindShortestColorableObject(false);

        //_gameManager.CheckStageEnd(_spawnEnemyType, false);

//        _animator.enabled = false;

//        BoxCollider[] fpsTargetCols = GetComponentsInChildren<BoxCollider>();
//        for (int i = 0; i < fpsTargetCols.Length; i++)
//            fpsTargetCols[i].enabled = false;

        //deactivateMe.DeactivateMeProc(deactivateDelay);
        //enabled = false;
    }
}
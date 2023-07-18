using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BaseCollision : MonoBehaviour
{

    [SerializeField]
    protected GameObject materialObject;
    [SerializeField]
    protected GameObject distanceObject;
    // [SerializeField]
    // protected Text scoreText;

    protected float _colliderSizeX = 0;
    protected float _colliderSizeZ = 0;
    protected float _followDistance;
    protected float _distanceValueBase = 2f;
    protected float _boxCenterDistance;
    protected int _myPlayerCount = 1;
    protected int _moneyCount = 0;

    protected CameraControl cameraControl;
    protected ObjectManager objectManager;
    protected Material _mat;
    
    protected GameObject _other;
    protected GameObject _particleObjectBlue;
    protected GameObject _particleObjectGreen;
    protected GameObject _particleObjectRed;
    protected GameObject _transientParticle;

    protected BoxCollider boxColliderObject;
    protected Transform _parent;
    protected ParticleSystem _particle1;
    protected ParticleSystem _particle2;

    

    public int MyPlayerCount { get => _myPlayerCount; set => _myPlayerCount = value; }
    
    // public int MoneyCount { get => _moneyCount; set => _moneyCount = value; }

    protected virtual void Start()
    {
        boxColliderObject = GetComponents<BoxCollider>()[0];
        _mat = materialObject.GetComponent<Renderer>().material;
        cameraControl = CameraControl.Instance;
        objectManager = ObjectManager.Instance;
        
        _parent = transform.parent;
        _particleObjectBlue = objectManager.ParticleObjectBlue;
        _particleObjectGreen = objectManager.ParticleObjectGreen;
        _particleObjectRed = objectManager.ParticleObjectRed;
    }


    public void CrashNeutral(GameObject newGameObject)
    {
        newGameObject.GetComponent<NeutralControl>()?.ToIntoCrashObject(gameObject, _mat);
        if (newGameObject.GetComponent<NeutralControl>().peopleColorType == PeopleColorType.blue)
        {
            newGameObject.GetComponent<PlayerCollision>()?.stackBluePeople.Add(gameObject);
            Instantiate(_particleObjectBlue, newGameObject.transform);
        }
        if (newGameObject.GetComponent<NeutralControl>().peopleColorType ==  PeopleColorType.green)
        {
            newGameObject.GetComponent<PlayerCollision>()?.stackGreenPeople.Add(gameObject);
            Instantiate(_particleObjectGreen, newGameObject.transform);
        }
        if (newGameObject.GetComponent<NeutralControl>().peopleColorType ==  PeopleColorType.red)
        {
            newGameObject.GetComponent<PlayerCollision>()?.stackRedPeople.Add(gameObject);
            Instantiate(_particleObjectRed, newGameObject.transform);
        }

        transform.parent = newGameObject.transform.parent;
        
        //UpdatePlayerCount(newGameObject.transform.parent.gameObject);
        
//        Instantiate(_particleObject, newGameObject.transform);
        
        _transientParticle = newGameObject.transform.GetChild(2).gameObject;
        _particle1 = _transientParticle.GetComponent<ParticleSystem>();
        _particle2 =_transientParticle.transform.GetChild(0).GetComponent<ParticleSystem>();
        _particle1.startColor = _mat.color;
        _particle2.startColor = _mat.color;


        Invoke(nameof(DestroyParticle), 3f);

        if (newGameObject.GetComponent<NeutralControl>()?.PlayerCrash == true)
        {
            cameraControl.ZoomOut();
        }
        
        //WriteScore();
    }

    private void DestroyParticle()
    {
        Destroy(_transientParticle);
    }

    private void OnTriggerStay(Collider other)
    {
//        if (other.gameObject.transform.parent.childCount < 3)
//        {
//            DestroyFake(other.gameObject);
//        }
    }

    private void OnTriggerEnter(Collider other)
    {
//        if (other.gameObject.transform.parent.childCount < 3)
//        {
//            DestroyFake(other.gameObject);
//        }
    }

    private void DestroyFake(GameObject newGameObject)
    {
        newGameObject.GetComponent<FakeCollision>()?.DestroyFake(_parent.childCount, _mat, gameObject);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.childCount < 3)
        {
            other.gameObject.GetComponent<FakeCollision>()?.OpenCrashLock();
        }
    }

    private void UpdatePlayerCount(GameObject pranetObject)
    {
        for (int i = 1; i < transform.parent.childCount; i++)
        {
            _myPlayerCount = pranetObject.transform.childCount - 1;
            pranetObject.transform.GetChild(i).GetComponent<NeutralControl>()?.UpdatePlayerCountParent(_myPlayerCount, _distanceValueBase);
        }
    }

    public void UpdateBoxSize()
    {
        float parentCount = _parent.childCount;

        _followDistance = distanceObject.transform.localPosition.z;
        _distanceValueBase = 0.04f * _parent.childCount + 2f;
        _followDistance += 0.1f;
        _colliderSizeX = 0.1f * parentCount;
        _colliderSizeZ = 0.3f * parentCount;
        _boxCenterDistance = -0.02f * parentCount;

        Vector3 distancePos = distanceObject.transform.localPosition;
        distanceObject.transform.localPosition = new Vector3(distancePos.x, distancePos.y, _followDistance);
        boxColliderObject.size = new Vector3(_colliderSizeX + 6, 2, _colliderSizeZ + 6);
        boxColliderObject.center = new Vector3(0, 1, _boxCenterDistance);
        
    }


    // public void WriteScore()
    // {
    //     Invoke(nameof(LateText), 0.2f);
    // }

    // private void LateText()
    // {
    //     scoreText.text = (_parent.childCount - 1).ToString();
    // }

    //public void CrashSetPlayerCount(GameObject newGameObject)
    //{

    //    if (newGameObject.GetComponent<NeutralControl>().PlayerCount != MyPlayerCount)
    //    {
    //        newGameObject.GetComponent<NeutralControl>().PlayerCount = MyPlayerCount;
    //        
    //    }

    //}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : BasePlayerControl
{
    private GameManager _gameManager;
    public GameObject runParticle;
    private CanvasManager canvasManager;
    private float _eularObject;
    private float _eularStatic = 90;
    private float _horizantal;
    private float _vertical;
    private Quaternion _newRotate;
    public GameObject triggerObject;
    private CapsuleCollider triggerCapsule;
    private bool isMouseUp;
    [HideInInspector] public ParkPlaceUpgradeCanvas upgradeCanvas;
    [HideInInspector] public TicketOfficeUpgradeCanvas ticketupgradeCanvas;
    [HideInInspector] public BuildUpgradeCanvas buildupgradeCanvas;
    [HideInInspector] public TourOfficeUpgradeCanvas tourofficeupgradeCanvas;
    public bool triggerActive;
    public ParkPlaceInfo finalParkPlaceInfo;
    public GameObject cursor;

    protected override void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        base.Start();
        //anim.SetInteger("AnimStatus", 1);
        canvasManager = CanvasManager.Instance;
        speed = 0;
        triggerCapsule = triggerObject.GetComponent<CapsuleCollider>();

        TicketOfficeUpgradeCanvas[] ticketOfficeUpgradeCanvases = FindObjectsOfType<TicketOfficeUpgradeCanvas>();
        for (int i = 0; i < ticketOfficeUpgradeCanvases.Length; i++)
        {
            ticketOfficeUpgradeCanvases[i].Setup();
        }

        DOVirtual.DelayedCall(2.3f, () =>
        {
            if (!PlayerPrefs.HasKey("TutorialCar-CallPeople") && _gameManager._kikiPrefab.gameData.carTurorialActive)
            {
                cursor.transform.GetChild(0).gameObject.SetActive(true);
                ArrowLookAt();

//            NeutralControl neutralControls = FindObjectOfType<NeutralControl>();
//            cursor.transform.LookAt(neutralControls.transform);
//            cursor.SetActive(true);
//            print("a");
//            });
            }
        });
    }


    protected override void FixedUpdate()
    {
        //rb.velocity = transform.forward * 5;

        if (_vertical > 0)
        {
            _eularObject = _horizantal * _eularStatic;
        }
        else if (_vertical * _horizantal < 0)
        {
            _eularObject = 90 + _vertical * -_eularStatic;
        }
        else if (_vertical * _horizantal > 0)
        {
            _eularObject = 180 + _horizantal * -_eularStatic;
        }

        base.FixedUpdate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("c");
            PlayerPrefs.DeleteKey("TutorialCar-CallPeople");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(_gameManager._gameUIManager.CpıIdeaFlag());
        }

//        if (PlayerPrefs.HasKey("TutorialCar-CallPeople")) //&& finalParkPlaceInfo!=null)
//        {
//            cursor.transform.LookAt(finalParkPlaceInfo.transform);
//            print("b");
//        }


//        cursor.transform.LookAt();
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            if (upgradeCanvas != null)
            {
                if (upgradeCanvas.touchBool)
                {
                    upgradeCanvas.TriggerMethod();
                    upgradeCanvas.touchBool = false;
                    triggerActive = true;
                }
            }

            if (ticketupgradeCanvas != null)
            {
                if (ticketupgradeCanvas.touchBool)
                {
                    ticketupgradeCanvas.TicketTriggerMethod();
                    ticketupgradeCanvas.touchBool = false;
                    triggerActive = true;
                }
            }

            if (buildupgradeCanvas != null)
            {
                if (buildupgradeCanvas.touchBool)
                {
                    buildupgradeCanvas.BuildTriggerMethod();
                    buildupgradeCanvas.touchBool = false;
                    triggerActive = true;
                }
            }

            if (tourofficeupgradeCanvas != null)
            {
                if (tourofficeupgradeCanvas.touchBool)
                {
                    tourofficeupgradeCanvas.TourOfficeTriggerMethod(gameObject);
                    tourofficeupgradeCanvas.touchBool = false;
                    triggerActive = true;
                }
            }

            //         triggerCapsule.enabled = true;
            transform.GetComponent<CapsuleCollider>().enabled = false;
            runParticle.SetActive(false);
            isMouseUp = true;
            anim.SetInteger("AnimStatus", 2);
            speed = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            transform.GetComponent<BoxCollider>().enabled = true;
            transform.GetComponent<BoxCollider>().size = new Vector3(6, 2, 6);
            triggerActive = true;
            transform.GetComponent<CapsuleCollider>().enabled = true;
            triggerCapsule.enabled = false;
            runParticle.SetActive(true);
            isMouseUp = false;
            anim.SetInteger("AnimStatus", 1);
            speed = 8;
        }


        _horizantal = canvasManager.JoystickObject.Horizontal;
        _vertical = canvasManager.JoystickObject.Vertical;

        //dir = Vector3.right * _horizantal + Vector3.forward * _vertical;

        //transform.LookAt(rb.velocity);
        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        //TODO : transformda sadece y rotasyon alınacak. diğerleri sabit kalacak.

        _newRotate = Quaternion.Euler(0, _eularObject, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _newRotate, .65f);
    }

    public void ArrowLookAt()
    {
        while (PlayerPrefs.GetInt("TutorialCar-CallPeople", 0) == 0)
        {
            NeutralControl neutralControls = FindObjectOfType<NeutralControl>();
            PlayerPrefs.SetInt("TutorialCar-CallPeople", 1);
//            print(PlayerPrefs.GetInt("TutorialCar-CallPeople",1));
//            print("as");
//            print(neutralControls.transform.name);
            cursor.SetActive(true);
            cursor.GetComponent<CursorScript>().people = neutralControls.gameObject;
            cursor.GetComponent<CursorScript>().peopleLook = true;
        }
    }
}
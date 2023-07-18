using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public enum MoneyHolderPutType
{
    putbussy,
    putnotBussy
}
public enum MoneyHolderGetType
{
    getbussy,
    getnotBussy
}

public class MoneyHolder : MonoBehaviour
{
    private GameManager _gameManager;
    private ParkPlaceInfo _parkPlaceInfo;
    public MoneyHolderPutType moneyHolderputType;
    public MoneyHolderGetType moneyHolderGetType;
    public GameObject moneyPrefab;
    [SerializeField] private List<GameObject> moneyPointList;
    private int pointIndex;
    [HideInInspector] public int p;
    [HideInInspector] public float y;
    private int particleIndex;
    [SerializeField] private List<GameObject> particleList;
    [SerializeField] private List<GameObject> stackList;
    [SerializeField] private List<GameObject> removeStackList;
    [HideInInspector] public int officeTicketPrice;
    public bool moneyTime = false;


    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _parkPlaceInfo = transform.parent.GetComponent<ParkPlaceInfo>();
        moneyHolderputType = MoneyHolderPutType.putnotBussy;
        for (int i = 0; i < transform.childCount; i++)
        {
            moneyPointList.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ParticlePlay(Transform player)
    {
        if (particleList.Count != 0)
        {
            particleList[0].gameObject.SetActive(true);

            particleList[0].transform.parent.transform.DOJump(player.position, 1f, 1, 1f, false).SetEase(Ease.Linear);
            particleList[0].transform.parent.transform.DOScale(0, .13f).SetEase(Ease.Linear).OnComplete(() =>
            {
                particleList.RemoveAt(0);
//                Destroy(particleList[0]);
            });
            DOVirtual.DelayedCall(.2f, () => { ParticlePlay(player); });
        }
        else
        {
//            print(_gameManager._kikiPrefab.gameData.parkPlaceData[_parkPlaceInfo.parkPlaceId].ticketOffice.ticketPrice);
            _gameManager._gameUIManager.UpdateCoinCountText();
        }
    }

    private IEnumerator DelayPutMoney(int howMany, ParkPlaceInfo parkPlaceInfo)
    {
        yield return new WaitForSeconds(1f);
        if (moneyHolderputType == MoneyHolderPutType.putnotBussy)
        {
            p = 0;
            y = 0;
            StartCoroutine(MoveMoneyToPut(howMany, parkPlaceInfo));
        }
        else
        {
            StartCoroutine(DelayPutMoney(howMany, parkPlaceInfo));
        }
    }
    private IEnumerator DelayGetMoney(GameObject player)
    {
        yield return new WaitForSeconds(1f);
        if (moneyHolderGetType == MoneyHolderGetType.getnotBussy)
        {
            p = 0;
            y = 0;
            StartCoroutine(MoneyPar(player));
        }
        else
        {
            StartCoroutine(DelayGetMoney(player));
        }
    }
    private IEnumerator MoneyPar(GameObject player)
    {
        if (moneyHolderGetType == MoneyHolderGetType.getbussy)
        {
            StartCoroutine(DelayGetMoney(player));
        }
        else
        {
            moneyHolderputType = MoneyHolderPutType.putbussy;
            for (int i = removeStackList.Count - 1; i >= 0; i--)
            {
                removeStackList[i].transform.GetChild(0).gameObject.SetActive(true);
                removeStackList[i].transform.DOScale(Vector3.zero, .25f);
                removeStackList[i].transform.DOJump(player.transform.position, 1, 1, .1f).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        removeStackList[i].SetActive(false);
                        _gameManager._kikiPrefab.gameData.coinCount += officeTicketPrice;
                        _gameManager.UpdateCoinCount();
                        _gameManager._makeNoise.PlaySFX(7);
                    });
                yield return new WaitForSeconds(0.12f);
                if (i == 0)
                {
                    moneyHolderputType = MoneyHolderPutType.putnotBussy;
                    y = 0;
                    p = 0;
                    removeStackList.Clear();
                    moneyTime = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !moneyTime&& removeStackList.Count>0)
        {
            moneyTime = true;
            StartCoroutine(MoneyPar(other.gameObject));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && moneyTime)
        {
            moneyTime = false;
        }
    }

    public IEnumerator MoveMoneyToPut(int howMany, ParkPlaceInfo parkPlaceInfo)
    {
        if (moneyHolderputType == MoneyHolderPutType.putbussy)
        {
            
            StartCoroutine(DelayPutMoney(howMany, parkPlaceInfo));
            
        }
        else
        {
            moneyHolderGetType = MoneyHolderGetType.getbussy;
            for (int i = 0; i < howMany; i++)
            {
                GameObject money = Instantiate(moneyPrefab, parkPlaceInfo.transform.position, quaternion.identity);
                money.transform.parent = parkPlaceInfo.transform;
                money.SetActive(false);
                stackList.Add(money);
            }

//        _gameManager._kikiPrefab.vibrationController.ContinuousHaptics(1f, 1f, 3f);
            for (int i = stackList.Count - 1; i >= 0; i--)
            {
//            AudioController.Instance.audioSource.clip = AudioController.Instance.audioClipArray[5];
//            AudioController.Instance.audioSource.PlayOneShot(AudioController.Instance.audioSource.clip);
                GameObject obj = stackList[stackList.Count - 1];
                obj.SetActive(true);
                //stackList[stackList.Count - 1].transform.SetParent(finishController.transform.GetChild(0).transform, true);
                obj.transform.DORotate(new Vector3(0f, 90f, 0f), .2f);

                obj.transform.DOMove(moneyPointList[p].transform.position + Vector3.up * y, .2f).OnComplete(() =>
                {
//                obj.transform.GetChild(0).gameObject.SetActive(true);
                    obj.transform.DOScale(Vector3.one * 3f, .2f).OnComplete(() =>
                    {
                        obj.transform.DOScale(Vector3.one * 2f, .2f).SetUpdate(UpdateType.Fixed);
//                        _gameManager._makeNoise.PlaySFX(1);
                    }).SetUpdate(UpdateType.Fixed);
                });


//            collectedCountFinish++;

                removeStackList.Add(obj);
                stackList.RemoveAt(stackList.Count - 1);

                p++;
                if (p > moneyPointList.Count - 1)
                {
                    p = 0;
                    y += .35f;
                }

                if (i==0)
                {
                    moneyHolderGetType = MoneyHolderGetType.getnotBussy;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
//        _gameManager._kikiPrefab.vibrationController.StopContinuousHaptic();

//        if (collectedCountFinish < 60)
//        {
//            finishController.PlayText("WORK HARDER!", new Color32(255, 18, 0, 255), false);
//        }
//        else if (collectedCountFinish > 60)
//        {
//            finishController.PlayText("YOU ARE PROMOTED!", new Color32(255, 141, 0, 255), true);
//        }
    }

    public IEnumerator MoneyEnumerator(ParkPlaceInfo parkPlaceInfo, ParkPlaceData parkPlaceData, float waitTime,
        int howManyMoney)
    {
        _parkPlaceInfo = parkPlaceInfo;

        if (pointIndex < moneyPointList.Count)
        {
            if (howManyMoney != 0)
            {
                howManyMoney--;
                GameObject money = Instantiate(moneyPrefab, parkPlaceInfo.transform.position, quaternion.identity);
                money.transform.parent = transform;
                particleList.Add(money.transform.GetChild(0).gameObject);
                money.transform.DOJump(moneyPointList[pointIndex].transform.position, 1f, 1, 1f, false)
                    .SetEase(Ease.Linear);
                pointIndex++;
                yield return new WaitForSeconds(waitTime);
                StartCoroutine(MoneyEnumerator(parkPlaceInfo, parkPlaceData, waitTime + .085f, howManyMoney));
            }
        }

        if (pointIndex >= moneyPointList.Count)
        {
            transform.DOMoveY(transform.position.y + .3f, .01f).SetEase(Ease.Linear).OnComplete(() =>
            {
                pointIndex = 0;
                StartCoroutine(MoneyEnumerator(parkPlaceInfo, parkPlaceData, waitTime, howManyMoney));
            });
        }
    }
}
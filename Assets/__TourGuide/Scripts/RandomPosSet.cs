using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class RandomPosSet : MonoBehaviour
{
    private GameManager _gameManager;

    public List<Vector3> positionList;

    public int genaralSpawnCount;
    private bool isStart = false;
    public bool callSpawner = false;
    public GameObject peoplePrefab;
    public Material[] peopleColor;
    public bool spawnerCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            positionList.Add(transform.GetChild(i).transform.position);
        }

        DOVirtual.DelayedCall(.5f, () =>
        {
            CheckColorActived(_gameManager._kikiPrefab.gameData.colorActivedStatuses, 20);
        });
    }

    // Update is called once per frame
    void Update()
    {

        if (!isStart)
            return;
        
        
//        if (!spawnerCalled)
//        {
//            Invoke("CheckSpawner", 1f);   
//        }
        if (_gameManager._kikiPrefab.gameData.peopleCount < 19 && callSpawner)
        {
            CheckColorActived(_gameManager._kikiPrefab.gameData.colorActivedStatuses,
                (20 - _gameManager._kikiPrefab.gameData.peopleCount));
            callSpawner = false;
        }
    }

    private void CheckSpawner()
    {
        print("asdf");
        spawnerCalled = true;
        CheckColorActived(_gameManager._kikiPrefab.gameData.colorActivedStatuses, 20);
    }

    public void CheckColorActived(ColorActivedStatus colorActivedStatus, int spawnCount)
    {
        if (colorActivedStatus.blueStatuses.blueActive && colorActivedStatus.greenStatuses.greenActive &&
            colorActivedStatus.redStatuses.redActive)
        {
            StartCoroutine(SpawnMethodThirdBool(spawnCount));
        }
        else
        {
            if (colorActivedStatus.blueStatuses.blueActive && colorActivedStatus.greenStatuses.greenActive)
            {
                StartCoroutine(SpawnMethodSecondBool(colorActivedStatus.blueStatuses.blueActive,
                    colorActivedStatus.greenStatuses.greenActive, spawnCount));
            }
            else
            {
                if (colorActivedStatus.redStatuses.redActive && colorActivedStatus.greenStatuses.greenActive)
                {
                    StartCoroutine(SpawnMethodSecondBool(colorActivedStatus.redStatuses.redActive,
                        colorActivedStatus.greenStatuses.greenActive, spawnCount));
                }
                else
                {
                    if (colorActivedStatus.blueStatuses.blueActive && colorActivedStatus.redStatuses.redActive)
                    {
                        StartCoroutine(SpawnMethodSecondBool(colorActivedStatus.blueStatuses.blueActive,
                            colorActivedStatus.redStatuses.redActive, spawnCount));
                    }
                    else
                    {
                        if (colorActivedStatus.blueStatuses.blueActive)
                        {
                            StartCoroutine(SpawnMethodOneBool(colorActivedStatus.blueStatuses.blueActive, spawnCount));
                        }

                        if (colorActivedStatus.greenStatuses.greenActive)
                        {
                            StartCoroutine(SpawnMethodOneBool(colorActivedStatus.greenStatuses.greenActive,
                                spawnCount));
                        }

                        if (colorActivedStatus.redStatuses.redActive)
                        {
                            StartCoroutine(SpawnMethodOneBool(colorActivedStatus.redStatuses.redActive, spawnCount));
                        }
                    }
                }
            }
        }
    }

    public IEnumerator SpawnMethodOneBool(bool firstbool, int spawnCount)
    {
        if (firstbool == _gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive)
        {
            for (int i = spawnCount; i > 0; i--)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                spawnerCalled = true;
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[0];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.blue;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
                yield return new WaitForSeconds(0.12f);
                if (i == 1)
                {
                    isStart = true;
                }
            }
        }

        if (firstbool == _gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive)
        {
            for (int i = spawnCount; i > 0; i--)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[1];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.green;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
                yield return new WaitForSeconds(0.12f);
                if (i == 1)
                {
                    isStart = true;
                }
            }
        }

        if (firstbool == _gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive)
        {
            for (int i = spawnCount; i > 0; i--)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[2];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.red;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
                yield return new WaitForSeconds(0.12f);
                if (i == 1)
                {
                    isStart = true;
                }
            }
        }
    }

    public IEnumerator SpawnMethodSecondBool(bool firstBool, bool secondBool, int spawnCount)
    {
        for (int i = spawnCount / 2; i > 0; i--)
        {
            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                spawnerCalled = true;
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[0];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.blue;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;
                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
            }

            yield return new WaitForSeconds(0.12f);
            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[1];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.green;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;
                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
            }

            yield return new WaitForSeconds(0.12f);
            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[2];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.red;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
            }

            if (i == 1)
            {
                isStart = true;
            }
        }
    }

    public IEnumerator SpawnMethodThirdBool(int spawnCount)
    {
        for (int i = spawnCount; i > 0; i--)
        {
            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.blueStatuses.blueActive)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                spawnerCalled = true;
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[0];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.blue;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;
                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
                if (i == 1)
                {
                    isStart = true;
                }

                i--;
                if (i == 0)
                {
                    isStart = true;
                    break;
                }
            }

            yield return new WaitForSeconds(0.12f);
            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.greenStatuses.greenActive)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[1];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.green;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
                if (i == 1)
                {
                    isStart = true;
                }

                i--;
                if (i == 0)
                {
                    isStart = true;
                    break;
                }
            }

            yield return new WaitForSeconds(0.12f);
            if (_gameManager._kikiPrefab.gameData.colorActivedStatuses.redStatuses.redActive)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[2];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.red;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
            }

            if (i == 1)
            {
                isStart = true;
            }
        }

        yield return new WaitForSeconds(0.12f);
    }

    public IEnumerator CallSpawner(PeopleColorType peopleColorType)
    {
        if (_gameManager._kikiPrefab.gameData.peopleCount < 20)
        {
            if (peopleColorType == PeopleColorType.blue)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[0];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.blue;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;
                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
            }

            if (peopleColorType == PeopleColorType.green)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[1];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.green;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;
                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
            }

            if (peopleColorType == PeopleColorType.red)
            {
                GameObject people = Instantiate(peoplePrefab, positionList[Random.Range(0, 50)], Quaternion.identity);
                people.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = peopleColor[2];
                people.GetComponent<NeutralControl>().peopleColorType = PeopleColorType.red;
                people.GetComponent<NeutralControl>().spawnParticle.Play();
                _gameManager._kikiPrefab.gameData.peopleCount++;

                _gameManager._gameUIManager.UpdatePeopleCountText();
                people.GetComponent<NeutralControl>().newPos = positionList[Random.Range(0, 50)];
            }
        }

        yield return new WaitForSeconds(0.01f);
    }
}
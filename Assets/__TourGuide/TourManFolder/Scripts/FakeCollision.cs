using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeCollision : BaseCollision
{
    private bool _destroyControl = false;
    
    private GameObject newNeutralObject;

    private PlayerCollision playerCollision;



    protected override void Start()
    {
        base.Start();
        objectManager = ObjectManager.Instance;
        playerCollision = PlayerCollision.Instance;
        newNeutralObject = objectManager.NeutralObject;
    }


    public void CrashMeFake(GameObject newGameObject)
    {
        newGameObject.GetComponent<NeutralControl>().FakeCrash = true;
    }

    public void DestroyFake(int getPlayerCount, Material newMaterial, GameObject newGameObject)
    {
        if(transform.parent.childCount < getPlayerCount && !_destroyControl)
        {
            Instantiate(newNeutralObject, transform.position, transform.rotation, newGameObject.transform.parent);
            newNeutralObject.transform.GetChild(1).GetComponent<Renderer>().material = newMaterial;

            newNeutralObject.GetComponent<NeutralControl>().InstantiateTrue();
            playerCollision.FakeCount++;
            if(playerCollision.FakeCount > 4)
            {
                GetComponent<BoxCollider>().enabled = false;
                transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
                transform.GetChild(2).gameObject.SetActive(false);
                Invoke(nameof(LateStop),3f);
            }
            else
            {
                Destroy(gameObject);
            }
            _destroyControl = true;
            
        }
    }

    public void OpenCrashLock()
    {
        _destroyControl = false;
    }

    private void LateStop()
    {
        SceneManager.LoadScene("GameScene");
    }

}
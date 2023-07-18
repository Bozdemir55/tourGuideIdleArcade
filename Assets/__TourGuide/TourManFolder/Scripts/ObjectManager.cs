using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance = null;

    public static ObjectManager Instance { get => instance; set => instance = value; }
    public GameObject NeutralObject { get => neutralObject; set => neutralObject = value; }
    public GameObject ParticleObjectBlue { get => particleObjectBlue; set => particleObjectBlue = value; }
    public GameObject ParticleObjectGreen { get => particleObjectGreen; set => particleObjectGreen = value; }
    public GameObject ParticleObjectRed { get => particleObjectRed; set => particleObjectRed = value; }
    public GameObject MoneyParticleObject { get => moneyParticleObject; set => moneyParticleObject = value; }

    [SerializeField]
    private GameObject neutralObject;
    [SerializeField]
    private GameObject particleObjectBlue;
    [SerializeField]
    private GameObject particleObjectGreen;
    [SerializeField]
    private GameObject particleObjectRed;
    [SerializeField]
    private GameObject moneyParticleObject;
 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Status1,
    Status2
}

public class GameManagerKadir : MonoBehaviour
{
    private static GameManagerKadir instance = null;
    public static GameManagerKadir Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private State currentState;
    public State CurrentState { get => currentState; set => currentState = value; }


}

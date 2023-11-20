using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        InCity,
        InDungeon,
        Menu,
        Battle,
    }

    private State state;

    private void Start()
    {
        state = State.InDungeon;
    }
    public void SetState(State state)
    {
        this.state = state;
    }
    public State GetState()
    {
        return state;
    }
}

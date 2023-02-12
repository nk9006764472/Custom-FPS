using UnityEngine;
using System.Collections.Generic;

public class GunStateController : MonoBehaviour
{
    public GunBaseState CurrentState => currentState;

    [SerializeField] private GunBaseState[] _valideStates;
    private GunBaseState currentState;
    private Dictionary<GunStateType, GunBaseState> states = new Dictionary<GunStateType, GunBaseState>();

    private void OnEnable() 
    {
        Initialize();

        EnterState(GunStateType.IDLE);
    }

    private void Initialize()
    {        
        for(int i = 0; i < _valideStates.Length; i++)
        {
            states.Add(_valideStates[i].Type, _valideStates[i]);
        }
    }

    private void Update() 
    {
        UpdateState();    
    }

    public void EnterState(GunStateType stateType)
    {
        currentState?.ExitState();

        if(states.ContainsKey(stateType))
        {
            currentState = states[stateType];
            currentState.EnterState(stateType);
        }
    }

    private void UpdateState()
    {
        if(currentState == null) return;

        currentState.UpdateState();
    }

    private void ExitState()
    {

    }
}

public enum GunStateType
{
    IDLE = 0,
    FIRE = 1,
    RELOAD = 2
}

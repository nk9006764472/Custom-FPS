using UnityEngine;
using System.Collections.Generic;

public class GunStateController : MonoBehaviour
{
    public GunBaseState CurrentState => currentState;
    public Gun Gun => _gun;
    public Dictionary<GunStateType, GunBaseState> States => states;


    [SerializeField] private Gun _gun;
    [SerializeField] private GunBaseState[] _valideStates;
    

    private GunBaseState currentState;
    private Dictionary<GunStateType, GunBaseState> states = new Dictionary<GunStateType, GunBaseState>();


    private void OnEnable() 
    {
        Initialize();

    }

    private void Initialize()
    {        
        for(int i = 0; i < _valideStates.Length; i++)
        {
            _valideStates[i].Initialize(this);
            states.Add(_valideStates[i].Type, _valideStates[i]);
        }
        
        EnterState(GunStateType.IDLE);
    }

    private void Update() 
    {
        UpdateState();    
    }

    public void EnterState(GunStateType stateType)
    {
        if(currentState != null) 
        {
            if(currentState == states[stateType])
            return;

            ExitState();
        }

        if(states.ContainsKey(stateType))
        {
            currentState = states[stateType];
            currentState.EnterState();
        }
    }

    private void UpdateState()
    {
        if(currentState == null) return;
        currentState.UpdateState();
    }

    private void ExitState()
    {
        currentState.ExitState();
    }
}

public enum GunStateType
{
    IDLE = 0,
    FIRE = 1,
    RELOAD = 2
}

using UnityEngine;

public abstract class GunBaseState : MonoBehaviour
{
    public GunStateType Type{ get { return type; } }
    protected GunStateType type;
    protected abstract void Initialize();
    public virtual void EnterState(GunStateType stateType)
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {

    }
}

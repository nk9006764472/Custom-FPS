using UnityEngine;

public abstract class GunBaseState : MonoBehaviour
{
    public GunStateType Type{ get { return type; } }
    protected GunStateType type;
    protected GunStateController controller;
    
    public abstract void Initialize(GunStateController _controller);

    public virtual void EnterState()
    {
        //Debug.Log("Enter state : " + type);
    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {
        //Debug.Log("Exit state : " + type);
    }
}

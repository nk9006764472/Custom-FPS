using UnityEngine;
using UnityEngine.EventSystems;

public class LookTouchPanel : MonoBehaviour, IDragHandler
{
    public float HorizonatlTouch {get; private set;}
    public float VerticalTouch {get; private set;}

    private float sensitivityMultiplier = 5f;
    public void OnDrag(PointerEventData eventData)
    {
        HorizonatlTouch = Mathf.Lerp(HorizonatlTouch, eventData.delta.x * sensitivityMultiplier, 0.01f);
        VerticalTouch = Mathf.Lerp(VerticalTouch, eventData.delta.y * sensitivityMultiplier, 0.01f);
    }

    private void Update() 
    {
        HorizonatlTouch = 0f;
        VerticalTouch = 0f;    
    }
}

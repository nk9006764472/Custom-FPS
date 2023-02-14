using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public bool IsPressed {get; private set;}
    public bool IsClicked {get; private set;}   
    public bool IsBegin{get; private set;}
    public bool IsEnd{get; private set;}

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;

        IsBegin = true;
        StartCoroutine(SwitchBegin());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;

        IsEnd = true;
        StartCoroutine(SwitchEnd());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsClicked = true;
    }

    IEnumerator SwitchBegin()
    {
        yield return new WaitForEndOfFrame();
        IsBegin = !IsBegin;
    }

    IEnumerator SwitchEnd()
    {
        yield return new WaitForEndOfFrame();
        IsEnd = !IsEnd;
    }

    ////TODO : Improve the above code.
}

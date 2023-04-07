using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CCTV : MonoBehaviour
{
    [Header("Referenes")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cctvJoint;

    [Header("Values")]
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float oneCycleTime;
    [SerializeField] private float range;
    [SerializeField] private float detectingTime;
    [SerializeField] private Renderer alertLight;

    private CCTVState currentState;

    private void OnEnable()
    {
        EnterState(CCTVState.IDLE);
    }

    private void Update()
    {
        UpdateState();
    }

    private void CCTVMovement()
    { 
        cctvJoint.transform.localRotation = Quaternion.Euler(0f, maxAngle, 0f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cctvJoint.DOLocalRotateQuaternion(Quaternion.Euler(0f, minAngle, 0f), oneCycleTime / 2));
        sequence.Append(cctvJoint.DOLocalRotateQuaternion(Quaternion.Euler(0f, maxAngle, 0f), oneCycleTime / 2));
        sequence
            .SetLoops(-1, LoopType.Restart)
            .Play();
    }

    private void Detect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Vector3 targetScreenPos = cam.WorldToScreenPoint(collider.transform.position);
                if (targetScreenPos.z > 0 && targetScreenPos.x > 0 && targetScreenPos.x < Screen.width && targetScreenPos.y > 0 && targetScreenPos.y < Screen.height)
                {
                    Debug.Log("Player Detected");
                    //alertLight.material.color = Color.red;
                }
            }
        }

    }

    private void EnterState(CCTVState _state)
    {
        currentState = _state;

        switch (currentState)
        {
            case CCTVState.IDLE:
                CCTVMovement();
                break;
            case CCTVState.ALERT:
                CCTVMovement();
                break;
            case CCTVState.DETECTING:
                CCTVMovement();
                break;
            case CCTVState.DISABLED:
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case CCTVState.IDLE:
                Detect();
                break;
            case CCTVState.ALERT:
                break;
            case CCTVState.DETECTING:
                break;
            case CCTVState.DISABLED:
                break;
        }
    }

    enum CCTVState
    { 
        IDLE,
        DETECTING,
        ALERT,
        DISABLED
    }
}

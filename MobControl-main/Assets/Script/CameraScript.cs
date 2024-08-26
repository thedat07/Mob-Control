using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform originalCameraTransform;
    float shakeDuration = 0.0f;
    float shakeAmount = 0.3f;
    float decreaseFactor = 1.0f;

    public Vector3 originalPos;
    private MoveStage moveStage = null;



    void Start()
    {
        originalCameraTransform = GetComponent<Transform>();
        originalPos = originalCameraTransform.localPosition;
        moveStage = GameObject.Find("GameObject").GetComponent<MoveStage>();
    }

    void Update()
    {
        if(moveStage.state != MoveStage.State.Normal)
        {
            return;
        }
        else
        {
            originalPos = transform.position;
        }

        if (shakeDuration > 0)
        {
            originalCameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            if (shakeDuration < 0 ) 
            {
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            shakeDuration = 0f;
            originalCameraTransform.position = originalPos;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void CameraShakeTime(float time)
    {
        shakeDuration = time;
    }
}
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Bar : MonoBehaviour
{

    public float totalRotation = 90.0f;     // 目標の合計回転角度（度）
    public bool rotateZ = false;

    private float initialRotationY;
    private float initialRotationZ;
    private float targetRotation;
    private bool isRotatingForward = true;


    private int stopFrame = 0;

    void Start()
    {
        initialRotationY = transform.rotation.eulerAngles.y;
        initialRotationZ = transform.rotation.eulerAngles.z;
        if(rotateZ )
        {
            targetRotation = initialRotationZ + totalRotation;
        }
        else
        {
            targetRotation = initialRotationY + totalRotation;
        }

    }

    void Update()
    {
        if(stopFrame > 0)
        {
            --stopFrame;
            return;
        }

        if (isRotatingForward)
        {
            RotateForward();
        }
        else
        {
            RotateBackward();
        }
    }

    void RotateForward()
    {
        float step = totalRotation / 90;
        float newRotation;
        if(rotateZ)
        {
            newRotation = transform.rotation.eulerAngles.z + step;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newRotation);
        }
        else
        {
            newRotation = transform.rotation.eulerAngles.y + step;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotation, transform.rotation.eulerAngles.z);
        }

        if (Mathf.Abs(newRotation - targetRotation) <= 1.0f)
        {
            isRotatingForward = false;
            stopFrame = 200;
        }
    }

    void RotateBackward()
    {
        float step = totalRotation / 90;
        float newRotation;
        if (rotateZ)
        {
            newRotation = transform.rotation.eulerAngles.z - step;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newRotation);
            if (Mathf.Abs(newRotation - initialRotationZ) <= 1.0f)
            {
                isRotatingForward = true;
                stopFrame = 200;
            }

        }
        else
        {
            newRotation = transform.rotation.eulerAngles.y - step;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotation, transform.rotation.eulerAngles.z);
            if (Mathf.Abs(newRotation - initialRotationY) <= 1.0f)
            {
                isRotatingForward = true;
                stopFrame = 200;
            }

        }

    }
}
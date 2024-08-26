using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float rotationSpeed = 20.0f;  // ��]���x�i�x/�b�j
    private float rotationInterval = 7.0f;  // ��]�Ԋu�i�b�j

    private float nextRotationTime;
    private bool rotateClockwise = true;

    // Start is called before the first frame update
    void Start()
    {
        nextRotationTime = Time.time + rotationInterval;
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̎��Ԃ���]�Ԋu�𒴂������]������؂�ւ���
        if (Time.time >= nextRotationTime)
        {
            rotateClockwise = !rotateClockwise;
            nextRotationTime = Time.time + rotationInterval;
        }

        // ���v���܂��͔����v���ɉ�]
        float rotationDirection = rotateClockwise ? 1.0f : -1.0f;
        float rotationAngle = rotationSpeed * rotationDirection * Time.deltaTime;

        // �I�u�W�F�N�g����]
        transform.Rotate(Vector3.up, rotationAngle);
    }
}

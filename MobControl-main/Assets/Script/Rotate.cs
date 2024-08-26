using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float rotationSpeed = 20.0f;  // ‰ñ“]‘¬“xi“x/•bj
    private float rotationInterval = 7.0f;  // ‰ñ“]ŠÔŠui•bj

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
        // Œ»Ý‚ÌŽžŠÔ‚ª‰ñ“]ŠÔŠu‚ð’´‚¦‚½‚ç‰ñ“]•ûŒü‚ðØ‚è‘Ö‚¦‚é
        if (Time.time >= nextRotationTime)
        {
            rotateClockwise = !rotateClockwise;
            nextRotationTime = Time.time + rotationInterval;
        }

        // ŽžŒv‰ñ‚è‚Ü‚½‚Í”½ŽžŒv‰ñ‚è‚É‰ñ“]
        float rotationDirection = rotateClockwise ? 1.0f : -1.0f;
        float rotationAngle = rotationSpeed * rotationDirection * Time.deltaTime;

        // ƒIƒuƒWƒFƒNƒg‚ð‰ñ“]
        transform.Rotate(Vector3.up, rotationAngle);
    }
}

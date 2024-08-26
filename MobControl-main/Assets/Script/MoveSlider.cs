using UnityEngine;

public class MoveSlider : MonoBehaviour
{
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Cannon").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var targetWorldPos = target.position;
        var targetScreenPos = Camera.main.WorldToScreenPoint(targetWorldPos);
    }
}

using System.ComponentModel;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    public GameObject Mob = null;
    public GameObject BigMob = null;

    public float MoveRange = 3.5f;

    private Vector3 startPos = Vector3.zero;
    public Vector3 initPos { get; set; }
    private Vector3 touchPos = Vector3.zero;
    private int FrameCount = 0;

    GameObject gameoverUI;
    private MoveStage moveStage = null;

    Slider slider;
    Image image;
    Color color;


    // Start is called before the first frame update
    void Start()
    {
        gameoverUI = GameObject.Find("GameOver");
        gameoverUI.SetActive(false);

        slider = GameObject.Find("Slider").GetComponent<Slider>();
        image = GameObject.Find("Fill").GetComponent<Image>();
        color = image.color;


        moveStage = GameObject.Find("GameObject").GetComponent<MoveStage>();

    }

    // Update is called once per frame
    void Update()
    {
        if(moveStage.state != MoveStage.State.Normal)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            startPos = transform.position;
            touchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            float moveX = Input.mousePosition.x - touchPos.x;
            transform.position = startPos + moveX * transform.right * 0.028f;
            //à⁄ìÆêßå¿
            Vector3 dist = transform.position - initPos;
            if (dist.magnitude > MoveRange)
            {
                if(Vector3.Dot(transform.right, dist) > 0)
                {
                    transform.position = initPos + transform.right * MoveRange;
                }
                else
                {
                    transform.position = initPos + transform.right * -MoveRange;
                }
            }


            //Mobèoåª
            ++FrameCount;
            if (FrameCount % 30 == 0)
            {
                GameObject ob = Instantiate(Mob, transform.position + transform.forward * 0.5f, Quaternion.Euler(0.0f, 0.0f + transform.rotation.eulerAngles.y, 0.0f));

                Rigidbody rb = ob.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 5.0f, ForceMode.Impulse);
            }

            slider.value += 0.003f;
            if(slider.value == 1.0f)
            {
                image.color = Color.yellow;
            }
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            if(slider.value == 1.0f)
            {
                slider.value = 0.0f;
                GameObject ob = Instantiate(BigMob, transform.position + transform.forward * 0.8f, Quaternion.Euler(0.0f, 0.0f + transform.rotation.eulerAngles.y, 0.0f));
                Rigidbody rb = ob.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 5.0f, ForceMode.Impulse);

                image.color = color;
            }
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            gameoverUI.SetActive(true);
            GameObject.Find("Main Camera").GetComponent<CameraScript>().CameraShakeTime(0.5f);
        }
    }
}

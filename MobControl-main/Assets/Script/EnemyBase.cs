using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public bool isActive {  get; set; }

    public GameObject enemy;
    public GameObject bigenemy;
    public GameObject perOne;
    public GameObject perBreak;

    public int instTime = 30;
    public int noinstTime = 300;

    private TextMesh textmesh;
    private int frameCount = 0;
    private bool inst= false;

    private Vector3 initPos;
    private float shakeTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponentInChildren<TextMesh>();
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

        ++frameCount;
        if(inst)
        {
            if(frameCount % 5 == 0)
            {
                Vector3 pos = transform.position + transform.forward * -1.0f;
                pos.x += Random.Range(-0.9f, 0.9f);
                pos.z += Random.Range(-0.9f, 0.9f);

                GameObject ob;
                if(Random.Range(0.0f, 1.0f) > 0.95f)
                {
                    ob = Instantiate(bigenemy, pos, Quaternion.Euler(0.0f, 180.0f + transform.rotation.eulerAngles.y, 0.0f));

                }
                else
                {
                    ob = Instantiate(enemy, pos, Quaternion.Euler(0.0f, 180.0f + transform.rotation.eulerAngles.y, 0.0f));
                }

                Rigidbody rb = ob.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * -5.0f, ForceMode.Impulse);
            }

            if(frameCount > instTime)
            {
                frameCount = 0;
                inst = false;
            }
        }
        else
        {
            if(frameCount > noinstTime)
            {
                frameCount = 0;
                inst = true;
            }
        }


        if (shakeTimer > 0)
        {
            // ランダムな位置にオブジェクトを移動
            transform.position = initPos + Random.insideUnitSphere * 0.03f;

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0.0f;
            // 揺れ終了時に元の位置に戻す
            transform.position = initPos;
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            int.TryParse(textmesh.text, out int num);
            --num;
            shakeTimer = 0.3f;
            GameObject perO = Instantiate(perOne, transform.position, Quaternion.identity);
            Destroy(perO, 1.0f);

            if (num <= 0)
            {
                //GameObject perB = Instantiate(perBreak, transform.position, Quaternion.identity);
                //Destroy(perB, 1.0f);

                num = 0;
                Destroy(gameObject, 0.5f);

                //GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Mob");
                //foreach (GameObject obj in objectsToDestroy)
                //{
                //    Destroy(obj, 0.5f);
                //}

                //GameObject[] destroyob = GameObject.FindGameObjectsWithTag("Enemy");
                //foreach (GameObject obj in destroyob)
                //{
                //    Destroy(obj, 0.5f);
                //}
            }

            textmesh.text = num.ToString();
        }

    }

}

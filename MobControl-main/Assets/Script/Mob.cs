using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Color color = Color.white;

    public int frameCount = 0;
    private GameObject goal = null;
    private List<string> increaseNames = new List<string>();
    private Rigidbody rb = null;

    public int HP = 1;

    private int hozon;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hozon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ++frameCount;

        if (rb == null)
        {
            return;
        }

        if (goal == null)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("EnemyBase");
            float nearestDistance = 100.0f;
            foreach (GameObject obj in objectsWithTag)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance < nearestDistance)
                {
                    goal = obj;
                    nearestDistance = distance;
                }
            }
            //goal = GameObject.FindGameObjectWithTag("EnemyBase");
            if(goal == null )
            {
                return;
            }
        }

        if (frameCount > 60)
        {
            if ((goal.transform.position - transform.position).magnitude < 5.0f)
            {
                rb.velocity = (goal.transform.position - transform.position).normalized * 1.0f;
                rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z) * 1.0f;

            }
            else
            {
                rb.velocity = transform.forward * 1.0f;
            }
        }
    }

    public bool ExistIncreaseName(string name)
    {
        if(increaseNames.Contains(name))
        {
            return true;
        }
        else
        {
            increaseNames.Add(name);
            return false;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("EnemyBase") || collision.gameObject.CompareTag("Syogai"))
        {
            if(frameCount - hozon > 30)
            {
                --HP;
                if (HP == 0)
                {
                    // すべての子オブジェクトを取得
                    Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

                    // 各子オブジェクトのRendererコンポーネントの色を変更
                    foreach (Renderer renderer in childRenderers)
                    {
                        renderer.material.color = color;
                    }
                    GetComponent<CapsuleCollider>().enabled = false;
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(gameObject, 0.1f);
                }
                else
                {
                    transform.localScale *= 0.9f;

                }
                hozon = frameCount;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (frameCount - hozon > 30)
            {
                --HP;
                if (HP == 0)
                {
                    // すべての子オブジェクトを取得
                    Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

                    // 各子オブジェクトのRendererコンポーネントの色を変更
                    foreach (Renderer renderer in childRenderers)
                    {
                        renderer.material.color = color;
                    }
                    GetComponent<CapsuleCollider>().enabled = false;
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(gameObject, 0.1f);
                }
                else
                {
                    transform.localScale *= 0.8f;

                }
                hozon = frameCount;
            }
        }

    }
}

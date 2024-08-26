using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color color = Color.white;

    private Rigidbody rb = null;
    private int frameCount = 0;

    public int HP = 1;
    private int hozon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ++frameCount;

        if(rb == null)
        {
            return;
        }

        if (frameCount > 60)
        {
            rb.velocity = transform.forward * 1.0f;
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            if (frameCount - hozon > 30)
            {
                --HP;
                if (HP == 0)
                {
                    // ���ׂĂ̎q�I�u�W�F�N�g���擾
                    Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

                    // �e�q�I�u�W�F�N�g��Renderer�R���|�[�l���g�̐F��ύX
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

}

using UnityEngine;

public class Syogai : MonoBehaviour
{
    public GameObject perOne;
    public GameObject perBreak;

    private TextMesh textmesh;

    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            int.TryParse(textmesh.text, out int num);
            --num;
            GameObject perO = Instantiate(perOne, transform.position, Quaternion.identity);
            Destroy(perO, 1.0f);

            if (num <= 0)
            {
                GameObject perB = Instantiate(perBreak, transform.position, Quaternion.identity);
                Destroy(perB, 1.0f);

                num = 0;
                Destroy(gameObject);
            }

            textmesh.text = num.ToString();
        }

    }

}

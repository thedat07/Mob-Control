using UnityEngine;

public class SetBool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBase"))
        {
            EnemyBase eb = other.gameObject.GetComponent<EnemyBase>();
            eb.isActive = true;
        }
        else if(other.gameObject.CompareTag("Increase"))
        {
            Increase eb = other.gameObject.GetComponent<Increase>();
            eb.isActive = true;

        }
    }
}

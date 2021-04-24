using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Finish : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameWin();
            Destroy(this);
        }
    }
}

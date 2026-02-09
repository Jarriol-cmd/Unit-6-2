using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 1f;
    public float minDist = 1f;
    public Transform target;
    public Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (target == null)
        {

            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsWalking", false);

        if (target == null)
            return;


        transform.LookAt(target);


        float distance = Vector3.Distance(transform.position, target.position);


        if (distance > minDist)
            transform.position += transform.forward * speed * Time.deltaTime;
            anim.SetBool("IsWalking", true);


    }


    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }
    */
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

}


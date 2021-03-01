using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject hammer;
    public float maxRange = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), hammer.GetComponent<Collider2D>());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z));
        Vector3 mouseDir = Vector3.ClampMagnitude(mouseWorldPos - transform.position, maxRange);

        Vector3 hammerPos = transform.position + mouseDir;
        Vector3 newhamerPos = Vector3.Lerp(hammer.transform.position, hammerPos, 0.2f);

        hammer.GetComponent<Rigidbody2D>().MovePosition(newhamerPos);
        hammer.transform.rotation = Quaternion.FromToRotation(Vector3.right, newhamerPos - transform.position);

        ContactFilter2D filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.layerMask = 1 << LayerMask.NameToLayer("Default");
        List<Collider2D> colliders = new List<Collider2D>();

        if(hammer.GetComponent<Rigidbody2D>().OverlapCollider(filter, colliders) > 0)
        {
            Vector3 newPlayerPos = hammer.transform.position - mouseDir;

            Vector3 force = (newPlayerPos - transform.position) * 80f;
            GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
}

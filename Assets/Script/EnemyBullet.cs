using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int _attack = 3;
    [SerializeField] float _speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, _speed * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Barrier")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Damaged(_attack);
            Destroy(this.gameObject);
        }
    }
}

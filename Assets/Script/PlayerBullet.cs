using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] int _attack = 3;
    [SerializeField] float _speed = 5;
    [SerializeField] float _lifeTime = 1;
    [SerializeField] int _power = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, _speed * Time.deltaTime));
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Damaged(_attack);
            if (_power < 2) Destroy(this.gameObject);
        }
        
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            if (_power < 1) Destroy(this.gameObject);
        }
    }
    
}

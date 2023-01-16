using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryArea : MonoBehaviour
{
    [SerializeField,Tooltip("‘±ŠÔ")] float _lifeTime = 3;
    [SerializeField,Tooltip("‰ñ•œ—Ê")] float _recovery = 3;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Destroy(this.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Recovery(_recovery * Time.deltaTime);
            Debug.Log("OK");
        }
    }
}

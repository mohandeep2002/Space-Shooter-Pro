using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed = 9f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -15f)
        {
            int min = -13;
            int max = 13;
            transform.position = new Vector3(Random.Range(min,max), 15f, transform.position.z);
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.transform.tag);
        if (other.transform.name == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}

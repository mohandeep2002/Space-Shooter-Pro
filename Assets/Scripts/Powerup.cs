using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 15f;
    [SerializeField]
    private int _powerUpID;
    [SerializeField]
    private AudioClip _audioClip;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -15.74)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.UpdateTripleLaser();
                        break;
                    case 1:
                        Debug.Log("Speed");
                        player.sppedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Shield");
                        player.ActivateShield(); 
                        break;
                    default: Debug.Log("Default Value");break;
                 
                }
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Null Returened");
            }
        }
    }
}

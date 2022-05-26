using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed = 15f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private float _lives = 3;

    void Start()
    {
        // take the current postion  
        transform.position = new Vector3(-0.1f, -1.7f, 27.7f);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.up * _speed * Time.deltaTime * verticalInput);
        //transform.Translate(Vector3.right * _speed * Time.deltaTime * horizontalInput);
        //transform.Translate(Vector3.up * _speed * Time.deltaTime * verticalInput);
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime;
        transform.Translate(direction);
        /*if (transform.position.y >= 14f)
        {
            transform.position = new Vector3(transform.position.x, -14f, transform.position.z);
        }
        else if (transform.position.y <= -14f)
        {
            transform.position = new Vector3(transform.position.x, 14f, transform.position.z);
        }*/
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -14.11475f, 14.11475f), transform.position.z);
        if (transform.position.x >= 31f)
        {
            transform.position = new Vector3(-31f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -31f)
        {
            transform.position = new Vector3(31f, transform.position.y, transform.position.z);
        }
    }
    void ShootLaser()
    {
        Debug.Log("Space Key Pressed");
        _canFire = _canFire + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
    }
    public void Damage()
    {
        _lives--;
        if (_lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

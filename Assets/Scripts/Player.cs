using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update\
    [SerializeField]
    private float _speed = 15f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject shieldVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    void Start()
    {
        // take the current postion  
        transform.position = new Vector3(-0.1f, -1.7f, 27.7f);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is Null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.Log("UI Manager is NULL");
        }
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
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        /*if (transform.position.y >= 14f)
        {
            transform.position = new Vector3(transform.position.x, -14f, transform.position.z);
        }
        else if (transform.position.y <= -14f)
        {
            transform.position = new Vector3(transform.position.x, 14f, transform.position.z);
        }*/
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -10.4f, 10.4f), transform.position.z);
        if (transform.position.x >= 28.8f)
        {
            transform.position = new Vector3(-28.5f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -28.5f)
        {
            transform.position = new Vector3(28.8f, transform.position.y, transform.position.z);
        }
    }
    void ShootLaser()
    {
        _canFire = _canFire + _fireRate;
        Debug.Log("Space Key Pressed");
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 3.3f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }
    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
        _lives--;
        
        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        if (_lives == 1)
        {
            _rightEngine.SetActive(true); 
        }
        _uiManager.UpdateLives(_lives);
        if (_lives <= 0)
        {
            _spawnManager.OnplayerDeath();
            Destroy(this.gameObject); 
        }
    }
    public void UpdateTripleLaser()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotDownRoutine());
    }
    IEnumerator TripleShotDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }    
    public void sppedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= 2;
    }
    public void ActivateShield()
    {
        _isShieldActive = true;
        shieldVisualizer.SetActive(true);
        StartCoroutine(HoldingShield());
    }
    IEnumerator HoldingShield()
    {
        yield return new WaitForSeconds(5f);
        _isShieldActive = false;
        shieldVisualizer.SetActive(false);
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}

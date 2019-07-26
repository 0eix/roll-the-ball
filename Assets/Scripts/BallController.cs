using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float _speed;

    public GameObject _scoreMenuUI;
    [HideInInspector]
    public ScoreMenu _scoreMenu;

    private Rigidbody _rigidbody;
    private int _direction = 1;

    public bool _isPaused;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreMenu = _scoreMenuUI.GetComponent<ScoreMenu>();
        _scoreMenu.Hide();
        _scoreMenu._score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                _isPaused = false;
                _scoreMenu.Resume();
            }
            else
            {
                _isPaused = true;
                _scoreMenu.Pause();
            }
        }
    }

    private void FixedUpdate()
    {
        // Au cas où on est sur PC
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rigidbody.AddForce(movement * _speed * _direction * Time.deltaTime);


        // Au cas où on est sur téléphone
        Vector3 accelerometerMovement = Quaternion.Euler(90, 0, 0) * Input.acceleration;
        _rigidbody.AddForce(accelerometerMovement * _speed * _direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 movement = new Vector3(0.0f, 0.8f, 0.0f);
        switch (other.gameObject.tag)
        {
            case "Goal":
                _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                _rigidbody.AddForce(movement * _speed * Time.deltaTime);
                FindObjectOfType<AudioManager>().Play("Goal");
                _scoreMenu._time = Time.timeSinceLevelLoad;
                _scoreMenu.ShowWinScreen();
                break;

            case "Hole":
                _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                _rigidbody.AddForce(movement * _speed * Time.deltaTime);
                FindObjectOfType<AudioManager>().Play("Loose");
                _scoreMenu.ShowLooseScreen();
                break;

            case "Star":
                _scoreMenu._score += 1;
                FindObjectOfType<AudioManager>().Play("PickUpStar");
                other.gameObject.SetActive(false);
                break;

            case "Reverse":
                _direction *= -1;
                FindObjectOfType<AudioManager>().Play("PickUpReverse");
                other.gameObject.SetActive(false);
                break;
        }
    }
}

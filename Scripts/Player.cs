using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private int lives;
    private int shooting;
    private bool hasShield;

    public GameManager gameManager;

    public GameObject bullet;
    public GameObject explosion;
    public GameObject thruster;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        lives = 3;
        shooting = 1;
        hasShield = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        gameManager.UpdateLivesText(lives);
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput,0) * Time.deltaTime * speed);
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, -0.001f, 0);
        }

        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -3.99f, 0);
        }

        if (transform.position.x > 9.3f)
        {
            transform.position = new Vector3(9.299f, transform.position.y, 0);
        }

        if (transform.position.x < -9.3f)
        {
            transform.position = new Vector3(-9.299f, transform.position.y, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (shooting)
            {
                case 1:
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 30f)); 
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -30f));
                    break;
            }
        }
    }

    public void LoseALife()
    {
        if (hasShield == false)
        {
            lives--;
        } else if (hasShield == true)
        {
            //lose the shield
            hasShield = false;
            shield.gameObject.SetActive(false);
            Debug.Log("shield down!");
            gameManager.UpdatePowerupText("");
<<<<<<< Updated upstream
=======
            gameManager.PlayPowerDown();
>>>>>>> Stashed changes
            //no longer have a shield
        }

        if (lives == 0)
        {
            gameManager.GameOver();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 6f;
        thruster.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShootingPowerDown()
    {
        yield return new WaitForSeconds(3f);
        shooting = 1;
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(3f);
        hasShield = false;
        shield.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(3f);
        hasShield = false;
        shield.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Powerup")
        {
            gameManager.PlayPowerUp();
            int powerupType = Random.Range(1, 5); //this can be 1, 2, 3, or 4
            switch(powerupType)
            {
                case 1:
                    //speed powerup
                    speed = 9f;
                    gameManager.UpdatePowerupText("Picked up Speed!");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //double shot
                    shooting = 2;
                    gameManager.UpdatePowerupText("Picked up Double Shot!");
                    StartCoroutine (ShootingPowerDown());
                    break;
                case 3:
                    //triple shot
                    shooting = 3;
                    gameManager.UpdatePowerupText("Picked up Triple Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 4:
                    //shield
                    gameManager.UpdatePowerupText("Picked up Shield!");
                    shield.gameObject.SetActive(true);
                    hasShield = true;
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
            Destroy(whatIHit.gameObject);
        }
    }
}

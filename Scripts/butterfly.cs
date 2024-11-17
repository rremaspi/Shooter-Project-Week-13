using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{

    public GameObject explosion;
    public float FlyDirection;

    // Start is called before the first frame update
    void Start()
    {
        FlyDirection = Random.Range(-0.4f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(FlyDirection, 0, 0) * Time.deltaTime);
        transform.Translate(-new Vector3(0, -1, 0) * Time.deltaTime * 2.5f);

        if (transform.position.y < -8.5f || transform.position.y > 8.5 || transform.position.x > 12)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            //I hit the Player!
            whatIHit.GetComponent<Player>().LoseALife();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (whatIHit.tag == "Weapon")
        {
            //I am shot!
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(5);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }
}
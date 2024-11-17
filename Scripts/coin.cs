using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float lifetime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9f, 9f), Random.Range(-4.5f, 1f), 0);
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Destroy after lifetime of 3 seconds
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collection)
    {
        if (collection.tag == "Player")
        {
            Debug.Log("Coin picked up");
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
            Destroy(this.gameObject);
        }
    }

}
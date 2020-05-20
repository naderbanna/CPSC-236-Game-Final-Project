using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentController : MonoBehaviour
{
    public GameObject bullet;

    float fireRate;
    float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2.5f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position + new Vector3(0,0.23f,0), Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        gun.UpgradeGun();
        Destroy(gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
    }

}

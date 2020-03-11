using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform camera = null;

    float range = 100f; // for now
    public int scoreVal = 0;
    public UnityEngine.UI.Text score;
    public GameObject bulletHolePrefab;



    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        RaycastHit hit;

        LayerMask wall = LayerMask.GetMask("Structures");
        LayerMask enemy = LayerMask.GetMask("Enemies");

        if (Physics.Raycast(camera.position, camera.forward, out hit, range, enemy))
        {
            Debug.Log("Shot the following enemy: " + hit.collider.name);
            Health enemyHealth = hit.collider.GetComponent<Health>();
            enemyHealth.TakeDamage(10);
            Instantiate(bulletHolePrefab,
                        hit.point + (0.01f * hit.normal),
                        Quaternion.LookRotation(-1 * hit.normal, hit.transform.up));
        
        if (enemyHealth.isDead)
            {
                hit.collider.gameObject.SetActive(false);
                scoreVal += 1;
                score.text = scoreVal.ToString();
            }
        }
        else if (Physics.Raycast(camera.position, camera.forward, out hit, range, wall))
        {
            Debug.Log("Shot the following wall: " + hit.collider.name);

            // display a bullet hole decal
            Instantiate(bulletHolePrefab,
                        hit.point + (0.01f * hit.normal),
                        Quaternion.LookRotation(-1 * hit.normal, hit.transform.up));
        }

        // animate the gun
        //gunAnimator.SetTrigger("Fire");
    }

}
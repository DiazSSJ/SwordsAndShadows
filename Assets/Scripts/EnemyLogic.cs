using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyLogic : MonoBehaviour
{
    public int hp;
    public int HandHurt;

    public Animator enemy1;
    // Start is called before the first frame update
    void Start()
    {
        enemy1 = enemy1.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // hand.enabled = true;
        Debug.Log("poniendo en true el colider");

        if (other.CompareTag("handImpact"))
        {
            Debug.Log("tocnado al enemigo");
            Debug.Log("vida del man este " + hp);
            if (enemy1 != null)
            {
                Debug.Log("entré al enemigo");
                Debug.Log("vida del man este abajo " + hp);
                enemy1.SetBool("IsDamage", true);
                // enemy1.SetBool("", true);
                enemy1.SetBool("isIdle", false);
            }
            hp -= HandHurt;
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", true);
        }
        // hand.enabled = false;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}

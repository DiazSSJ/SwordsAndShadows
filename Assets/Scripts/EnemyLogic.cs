using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyLogic : MonoBehaviour
{
    public int hp;
    public int HandHurt;

    public int level;

    public GameObject gema;

    public Animator enemy1;
    // Start is called before the first frame update
    void Start()
    {
        enemy1 = enemy1.GetComponent<Animator>();
        gema.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Actualiza el estado idle continuamente si no está en otro estado
        if (enemy1 != null && !enemy1.GetBool("IsDamage") && !enemy1.GetBool("IsAttacking") && !enemy1.GetBool("IsDead"))
        {
            enemy1.SetBool("isIdle", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // hand.enabled = true;
        Debug.Log("Activando el collider");

        if (other.CompareTag("handImpact"))
        {
            if (enemy1 != null)
            {
                Debug.Log("Entré en el enemigo");
                Debug.Log("Vida del enemigo: " + hp);
                if (hp == 0)
                {
                    StartCoroutine(HandleDeadAnimation());
                }
                else
                {
                    StartCoroutine(HandleDamageAnimation());
                    hp -= HandHurt;
                }

            }

        }

    }

    private IEnumerator HandleDamageAnimation()
    {
        if(level ==1)
        {
            enemy1.SetBool("IsDamage", true);
            enemy1.SetBool("isIdle", false);
            yield return new WaitForSeconds(1f); // Ajusta el tiempo según la duración de tu animación de daño
            enemy1.SetBool("IsAttacking", true);
            enemy1.SetBool("IsDamage", false);
            yield return new WaitForSeconds(1f); // Ajusta el tiempo según la duración de tu animación de ataque
            enemy1.SetBool("IsAttacking", false);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", true);
        }
        else if (level == 2)
        {
            enemy1.SetBool("IsDamage", true);
            enemy1.SetBool("isIdle", false);
            yield return new WaitForSeconds(0.5f); // Ajusta el tiempo según la duración de tu animación de daño
            enemy1.SetBool("IsAttacking", true);
            enemy1.SetBool("IsDamage", false);
            yield return new WaitForSeconds(0.5f); // Ajusta el tiempo según la duración de tu animación de ataque
            enemy1.SetBool("IsAttacking", false);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", true);
        }
        else if (level == 3)
        {
            enemy1.SetBool("IsDamage", true);
            enemy1.SetBool("isIdle", false);
            yield return new WaitForSeconds(1f); // Ajusta el tiempo según la duración de tu animación de daño
            enemy1.SetBool("IsAttacking", true);
            enemy1.SetBool("IsDamage", false);
            yield return new WaitForSeconds(1f); // Ajusta el tiempo según la duración de tu animación de ataque
            enemy1.SetBool("IsAttacking", false);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", true);
        }
        else
        {
            enemy1.SetBool("IsDamage", true);
            enemy1.SetBool("isIdle", false);
            yield return new WaitForSeconds(1f); // Ajusta el tiempo según la duración de tu animación de daño
            enemy1.SetBool("IsAttacking", true);
            enemy1.SetBool("IsDamage", false);
            yield return new WaitForSeconds(1f); // Ajusta el tiempo según la duración de tu animación de ataque
            enemy1.SetBool("IsAttacking", false);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", true);
        }
        
    }


    private IEnumerator HandleDeadAnimation()
    {
        if(level ==1)
        {
            enemy1.SetBool("IsDead", true);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", false);
            enemy1.SetBool("IsAttacking", false);
            yield return new WaitForSeconds(1.2f);
            Destroy(gameObject);
            gema.SetActive(true);
        }

        else if (level == 2)
        {
            enemy1.SetBool("IsDead", true);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", false);
            enemy1.SetBool("IsAttacking", false);
            yield return new WaitForSeconds(4.5f);
            Destroy(gameObject);
            gema.SetActive(true);
        }
        else if (level == 3)
        {
            enemy1.SetBool("IsDead", true);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", false);
            enemy1.SetBool("IsAttacking", false);
            yield return new WaitForSeconds(4.5f);
            Destroy(gameObject);
            gema.SetActive(true);
        }
        else
        {
            enemy1.SetBool("IsDead", true);
            enemy1.SetBool("IsDamage", false);
            enemy1.SetBool("isIdle", false);
            enemy1.SetBool("IsAttacking", false);
            yield return new WaitForSeconds(4.5f);
            Destroy(gameObject);
            gema.SetActive(true);
        }
        
    }

}

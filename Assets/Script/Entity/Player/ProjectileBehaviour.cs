using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public LayerMask whatIsEnemies;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Collider2D[] ennemiesToDamage = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0, whatIsEnemies);
        for (int i = 0; i < ennemiesToDamage.Length; i++)
        {
            ennemiesToDamage[i].GetComponent<ControllerHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
	}
}

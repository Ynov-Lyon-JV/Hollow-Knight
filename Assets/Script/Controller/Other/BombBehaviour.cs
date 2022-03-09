using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{

    public LayerMask whatIsEnemies;
    public int damage;
    public int explosionDelay;
    private bool isExploding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Awake()
	{
        Invoke("Explode", 2);

    }

	void Explode()
	{

        GetComponentInChildren<FadeOut>().canFade = true;
        isExploding = true;
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();

    }

	private void OnTriggerStay2D(Collider2D collision)
	{
        if(isExploding && damage>0)
		{
            Collider2D[] ennemiesToDamage = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 3), 0, whatIsEnemies);
            for (int i = 0; i < ennemiesToDamage.Length; i++)
            {
                ennemiesToDamage[i].GetComponent<ControllerHealth>().TakeDamage(damage);
                damage--;
            }
        }
        
    }
}

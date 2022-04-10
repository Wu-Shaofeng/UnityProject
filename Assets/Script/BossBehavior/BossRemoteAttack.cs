using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRemoteAttack : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterController character))
            character.getDamage(damage);
    }
}

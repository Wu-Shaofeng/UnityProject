using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    public bool isOver;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            if (isOver)
            {
                isOver = false;
                character.getDamage(damage);
                StartCoroutine(trapPlayer());
            }
        }
    }

    IEnumerator trapPlayer()
    {
        yield return new WaitForSeconds(1.0f);
        isOver = true;
    }
}

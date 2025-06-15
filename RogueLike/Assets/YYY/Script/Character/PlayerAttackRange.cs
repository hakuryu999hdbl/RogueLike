using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    public Player player; // 在 Inspector 手动拖入 Player 脚本

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !player.nearbyEnemies.Contains(other.gameObject))
        {
            player.nearbyEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && player.nearbyEnemies.Contains(other.gameObject))
        {
            player.nearbyEnemies.Remove(other.gameObject);
        }
    }
}

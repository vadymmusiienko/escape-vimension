using UnityEngine;

public class BossAttackHitbox : MonoBehaviour
{
    public BossCursor bossController;

    void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的是否是玩家
        if (other.CompareTag("Player"))
        {
            // 尝试从玩家对象上获取PlayerHealth脚本
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && bossController != null)
            {
                // 通知Boss主脚本对这个玩家造成伤害
                bossController.DealDamageToPlayer(playerHealth);
            }
        }
    }
}
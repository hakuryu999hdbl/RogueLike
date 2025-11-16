using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganBossTeleport : MonoBehaviour
{

    [Header("四个传送点")]
    public Transform pointFront;
    public Transform pointBack;
    public Transform pointLeft;
    public Transform pointRight;

    [Header("Boss 四个方向图片")]
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public SpriteRenderer spriteRenderer;
    public GameObject GateEffect;

    private int lastDir = -1; // 记住上一次传送方向

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Invoke(nameof(ChangePlace), 0.5f);
        GateEffect.SetActive(true);
    }

    void ChangePlace()
    {
        // 随机决定传送方向（不能与上一次相同）
        int dir;
        do
        {
            dir = Random.Range(0, 4);
        } while (dir == lastDir);

        lastDir = dir; // 记录当前方向，供下次排除

        switch (dir)
        {
            case 0:
                TeleportSelf(pointFront, frontSprite);
                break;
            case 1:
                TeleportSelf(pointBack, backSprite);
                break;
            case 2:
                TeleportSelf(pointLeft, leftSprite);
                break;
            case 3:
                TeleportSelf(pointRight, rightSprite);
                break;
        }

        GateEffect.SetActive(false);
    }

    void TeleportSelf(Transform targetPoint, Sprite targetSprite)
    {
        if (targetPoint == null) return;

        transform.position = targetPoint.position;
        spriteRenderer.sprite = targetSprite;
    }
}

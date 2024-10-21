﻿using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    private Vector2 spawnAreaMin;  // Điểm bắt đầu của vùng spawn (x,y)
    private Vector2 spawnAreaMax;  // Điểm kết thúc của vùng spawn (x,y)
    public float moveSpeed = 2f;   // Tốc độ di chuyển
    public float restDuration = 1f; // Thời gian nghỉ sau khi di chuyển
    private Vector2 direction;  // Hướng di chuyển
    private bool isMoving = true;  // Thỏ bắt đầu di chuyển
    private float timer = 0f;

    void Start()
    {
        // Chọn một hướng di chuyển ngẫu nhiên ban đầu
        ChooseNewDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isMoving)
        {
            // Di chuyển thỏ
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // Quay thỏ theo hướng di chuyển chỉ theo trục X
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Quay sang trái
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Quay sang phải
            }

            // Kiểm tra nếu thỏ chạm tới giới hạn vùng spawn
            if (HasReachedBoundary())
            {
                // Dừng lại để nghỉ, rồi đổi hướng di chuyển
                isMoving = false;
                timer = 0f;
            }
        }
        else
        {
            // Thỏ đang nghỉ
            if (timer >= restDuration)
            {
                // Sau khi nghỉ, thỏ đổi hướng và tiếp tục di chuyển
                isMoving = true;
                timer = 0f;
                ChooseNewDirection();  // Chọn hướng mới sau khi nghỉ
            }
        }
    }

    public void SetSpawnArea(Vector2 min, Vector2 max)
    {
        spawnAreaMin = min;
        spawnAreaMax = max;
    }

    void ChooseNewDirection()
    {

        direction = new Vector2(Random.Range(-1f, 1f), 0).normalized;
    }

    bool HasReachedBoundary()
    {
        // Kiểm tra nếu thỏ vượt qua giới hạn của vùng spawn
        Vector2 currentPosition = transform.position;

        return currentPosition.x <= spawnAreaMin.x || currentPosition.x >= spawnAreaMax.x ||
               currentPosition.y <= spawnAreaMin.y || currentPosition.y >= spawnAreaMax.y;
    }
}
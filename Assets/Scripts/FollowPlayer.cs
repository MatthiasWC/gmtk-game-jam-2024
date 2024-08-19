using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float followForce = 1f;
    [SerializeField] private float offset = -10f;

    private Transform playerTransform;
    private Vector3 startPos;
    private Vector3 endPos = Vector3.up * 1000;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerTransform = PlayerSingleton.instance.transform;
        startPos = transform.position;
    }

    private void Update()
    {
        float targetY = System.Math.Min(System.Math.Max(playerTransform.position.y, startPos.y), endPos.y);
        Vector3 targetPosition = new Vector3(startPos.x, targetY, offset);
        float adjustedFollowForce = 1 / (followForce * (targetPosition - transform.position).magnitude);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, adjustedFollowForce);
    }
}

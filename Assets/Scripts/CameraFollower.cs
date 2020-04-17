using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Transform playerShip;
    Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothTime = 1.0f;
    [SerializeField] Vector2 limits = new Vector2(1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        playerShip = transform.Find("Player Ship");
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowShip(playerShip);
    }

    private void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
    }

    private void CameraFollowShip(Transform t)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = t.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x, targetLocalPos.y + 1.8f, localPos.z), ref velocity, smoothTime);
    }
}

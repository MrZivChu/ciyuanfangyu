using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static bool startShake = false;
    public static float seconds = 0.0f;
    public static bool started = false;
    public static float quake = 0.2f;
    private Vector3 camPos;
    public bool is2D;

    void Start()
    {
        camPos = transform.position;
    }

    void LateUpdate()
    {
        if (startShake)
        {
            transform.position = camPos + Random.insideUnitSphere * quake;
            if (is2D)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
            }
        }

        if (started)
        {
            StartCoroutine(WaitForSecond(seconds));
            started = false;
        }
    }

    public static void shakeFor(float tseconds, float tquake)
    {
        seconds = tseconds;
        started = true;
        quake = tquake;
    }

    IEnumerator WaitForSecond(float a)
    {
        camPos = transform.position;
        startShake = true;
        yield return new WaitForSeconds(a);
        startShake = false;
        transform.position = camPos;
    }
}

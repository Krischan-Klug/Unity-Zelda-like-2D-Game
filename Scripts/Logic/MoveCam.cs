using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform target; // Das Ziel, dem die Kamera folgen soll (in diesem Fall der Spieler).
    public float smoothSpeed = 0.125f; // Geschwindigkeit der Kameraanpassung.
    public Vector3 offset; // Optionaler Offset, um die Kamera relativ zum Spieler zu positionieren.

    void FixedUpdate()
    {
        if (target == null) return; // Wenn target nicht ausgew�hlt wird Code nicht ausgef�hrt.(Keine Kamera verfolgung)

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}

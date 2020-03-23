using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulate : MonoBehaviour
{
    private Vector3 posOriginal;
    private Quaternion rotOriginal;
    private Vector3 scaOriginal;

    void Start()
    {
        posOriginal = transform.position;
        rotOriginal = transform.rotation;
        scaOriginal = transform.localScale;
    }

    public void Size(float amount)
    {
        transform.localScale += new Vector3(amount,amount,amount);
    }

    public void SizeReset()
    {
        transform.localScale = scaOriginal;
    }

    public void Rotation(float amount)
    {
        float targetRot = transform.rotation.eulerAngles.y + amount;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRot, transform.rotation.eulerAngles.z);
    }

    public void RotationReset()
    {
        transform.rotation = rotOriginal;
    }

    public void Position(Vector3 newPlace)
    {
        transform.position = newPlace;
    }
    public void PositionReset()
    {
        transform.position = posOriginal;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public abstract void Start();
    //function for moving to be edited in tankmover
    public abstract void Move(Vector3 direction, float speed);
    //function for rotation to be edited in tankmover
    public abstract void Rotate(float speed);
}

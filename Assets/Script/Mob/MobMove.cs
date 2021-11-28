
using UnityEngine;

public abstract class MobMove : MonoBehaviour
{
    public abstract ControllerMove CM { get; set; }
    public abstract void Move();
    public abstract void Flip();
}

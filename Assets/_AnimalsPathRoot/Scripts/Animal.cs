using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void SetVisibility(bool status)
    {
        Debug.Log(gameObject.name + " | " + " isVisible: " + status);
        anim.SetBool("isVisible", status);
    }

    public void SetRunning(bool status)
    {
        Debug.Log(gameObject.name + " | " + " isRunning: " + status);
        anim.SetBool("isRunning", status);
    }
}

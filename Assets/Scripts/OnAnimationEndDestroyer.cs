using UnityEngine;

public class OnAnimationEndDestroyer : MonoBehaviour
{
    public void DestroyOnAnimationEnd()
    {
        GameObject objectForDestroy = transform.parent.gameObject ? transform.parent.gameObject : gameObject;
        Destroy(objectForDestroy);
    }
}

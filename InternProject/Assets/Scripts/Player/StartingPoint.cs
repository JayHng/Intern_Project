using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    void Start()
    {
        GameManager.gm.player.transform.position = transform.position;
    }
}

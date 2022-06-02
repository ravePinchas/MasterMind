using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovment movment;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            movment.enabled = false;
            // GetComponent<PlayerMovment>().enabled = false; SAME THING AS ABOVE
            FindObjectOfType<GameMannager>().EndGame();
        }
    }
}

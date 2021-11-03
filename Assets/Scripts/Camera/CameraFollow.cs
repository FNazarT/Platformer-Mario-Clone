using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float offset;                                             //Private variable to store the offset distance between the player and camera
    [SerializeField] private GameObject player = null;                //Variable to store a reference to the player game object

    void Start ()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = (transform.position - player.transform.position).z;
    }

    private void LateUpdate()
    {
        //Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance
        transform.position = new Vector3(player.transform.position.x, transform.position.y, offset);
    }
}
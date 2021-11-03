using UnityEngine;

public class ScaleBackground : MonoBehaviour
{

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;                      //This is how you get the width of the image
        float height = sr.sprite.bounds.size.y;                     //This is how you get the height of the image

        float worldHeight = Camera.main.orthographicSize * 2f;      //This is how you get the height of the camera in world units
        float worldWidth = worldHeight * Camera.main.aspect;        //This is how you get the width of the camera in world units

        //This last sections adjusts the image size to the camera size
        Vector3 tempScale = transform.localScale;
        tempScale.x = worldWidth / width + 0.1f;
        tempScale.y = worldHeight / height + 0.1f;

        transform.localScale = tempScale;
    }
}
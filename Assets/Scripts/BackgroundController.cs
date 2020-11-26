using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float xOffset;
    public float yOffset;
    public float speed;

    private int horizontalDir = 1;
    private int verticalDir = 1;

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(xOffset * horizontalDir, yOffset * verticalDir), Time.deltaTime * speed);
        
        if (Mathf.Abs(transform.position.x) > xOffset - xOffset / 20 || Mathf.Abs(transform.position.y) > yOffset - yOffset / 20)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                horizontalDir = -horizontalDir;
            } else
            {
                verticalDir = -verticalDir;
            }
        }
    }
}

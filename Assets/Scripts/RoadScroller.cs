using UnityEngine;

public class RoadScroller : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float height;
   public bool isEvent = false;

    private void Update()
    {
        if(isEvent == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if(transform.position.y <= -height)
            {
                transform.position = Vector3.up * height;
            }
        }
    }
}

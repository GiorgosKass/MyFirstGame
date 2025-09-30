using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        Debug.Log("Start Pos: " + startPos + " | Repeat Width: " + repeatWidth);
    }

    void Update()
    {
        // �� ����� ����� (��� ������ ���), ����� ����
        if (transform.position.x > startPos.x + repeatWidth)
        {
            transform.position = startPos;
        }
    }
}

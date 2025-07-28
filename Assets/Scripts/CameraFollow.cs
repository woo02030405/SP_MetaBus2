using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap ����� ���� �߰�

public class CameraFollow : MonoBehaviour
{
    // ���� ��� (�÷��̾�)
    public Transform target;

    // �ε巴�� ������� �ð� (�������� ������ �����)
    public float smoothTime = 0.2f;

    // SmoothDamp���� ����� ���� �ӵ� ���� ����
    private Vector3 velocity = Vector3.zero;

    // �� ��� (Tilemap���� �ڵ� ���)
    private Vector2 minPos;
    private Vector2 maxPos;

    // ������ Tilemap (Inspector���� �巡���Ͽ� ����)
    public Tilemap map;

    void Start()
    {
        // map�� ����Ǿ� ������ �ڵ����� �� ��� ���
        if (map != null)
        {
            // Tilemap�� Bound�� �������� (���� ��ü �� ����)
            BoundsInt bounds = map.cellBounds;

            // ���� �ּ�/�ִ� ��ǥ�� ���� ��ǥ�� ��ȯ
            Vector3 min = map.CellToWorld(bounds.min);
            Vector3 max = map.CellToWorld(bounds.max);

            // z���� ������� �ʰ� x, y�� ��迡 ���
            minPos = new Vector2(min.x, min.y);
            maxPos = new Vector2(max.x, max.y);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // �÷��̾� ��ġ�� ��ǥ ��ġ�� ���� (z�� ī�޶� �Ÿ� ���������� -10 ����)
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, -10f);

        // SmoothDamp�� ����� ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        // �� ��� �ȿ����� ī�޶� �����̵��� ����
        smoothPos.x = Mathf.Clamp(smoothPos.x, minPos.x, maxPos.x);
        smoothPos.y = Mathf.Clamp(smoothPos.y, minPos.y, maxPos.y);

        // ���� ���� ��ġ�� ī�޶� ����
        transform.position = smoothPos;
    }
}

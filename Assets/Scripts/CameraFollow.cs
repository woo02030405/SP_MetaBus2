using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap 사용을 위해 추가

public class CameraFollow : MonoBehaviour
{
    // 따라갈 대상 (플레이어)
    public Transform target;

    // 부드럽게 따라오는 시간 (낮을수록 빠르게 따라옴)
    public float smoothTime = 0.2f;

    // SmoothDamp에서 사용할 내부 속도 저장 변수
    private Vector3 velocity = Vector3.zero;

    // 맵 경계 (Tilemap에서 자동 계산)
    private Vector2 minPos;
    private Vector2 maxPos;

    // 참조할 Tilemap (Inspector에서 드래그하여 연결)
    public Tilemap map;

    void Start()
    {
        // map이 연결되어 있으면 자동으로 맵 경계 계산
        if (map != null)
        {
            // Tilemap의 Bound를 가져오기 (맵의 전체 셀 영역)
            BoundsInt bounds = map.cellBounds;

            // 맵의 최소/최대 좌표를 월드 좌표로 변환
            Vector3 min = map.CellToWorld(bounds.min);
            Vector3 max = map.CellToWorld(bounds.max);

            // z축은 사용하지 않고 x, y만 경계에 사용
            minPos = new Vector2(min.x, min.y);
            maxPos = new Vector2(max.x, max.y);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 플레이어 위치를 목표 위치로 설정 (z는 카메라 거리 유지용으로 -10 고정)
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, -10f);

        // SmoothDamp를 사용해 현재 위치에서 목표 위치로 부드럽게 이동
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        // 맵 경계 안에서만 카메라가 움직이도록 제한
        smoothPos.x = Mathf.Clamp(smoothPos.x, minPos.x, maxPos.x);
        smoothPos.y = Mathf.Clamp(smoothPos.y, minPos.y, maxPos.y);

        // 최종 계산된 위치를 카메라에 적용
        transform.position = smoothPos;
    }
}

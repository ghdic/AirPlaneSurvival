using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private float smooth = 5.0f;		// 카메라 모션 부드럽게하기용 변수
    public Transform mainCamera;
    public Transform standardPos;          // 카메라의 일반적인 위치
    public Transform frontPos;         // 카메라 전면 탐지기

    // 원활하게 연결되지 않을 때(빠른전환)에 대한 bool 플래그
    private bool bQuickSwitch = false;  //카메라 위치를 빠르게 변경하라(정면 카메라, 노말 카메라 변경할때 사용)

    //카메라 줌 인/아웃
    public float zoomSpeed = 0.5f; // 줌 스피드
    private float distance; // 카메라와의 거리
    private Vector3 AxisVec; //축의 벡터

    private void Start()
    {
        mainCamera = GetComponent<Transform>();



        // 카메라 시작 포지션
        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
    }

    private void FixedUpdate()
    {
    
        // 카메라를 표준 위치 및 방향으로 되돌린다.
        setCameraPositionNormalView();

        // 마우스 휠 줌/아웃
        zoomInZoomOut();

        if (Input.GetKey(KeyCode.Space))
        {
            setCameraPositionFrontView();
        }
    }

    private void setCameraPositionNormalView()
    {
        if (bQuickSwitch == false)
        {
            // 표준 위치와 방향으로 카메라를 부드럽게 움직인다.
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
        }
        else
        {
            // 표준 위치와 방향으로 카메라를 빠르게 움직인다.
            transform.position = standardPos.position;
            transform.forward = standardPos.forward;
            bQuickSwitch = false;
        }
        
    }

    private void setCameraPositionFrontView()
    {
        // 정면 카메라로 전환
        bQuickSwitch = true;
        transform.position = frontPos.position;
        transform.forward = frontPos.forward;
    }


    private void zoomInZoomOut()
    {
        distance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1;
        distance = Mathf.Clamp(distance, -10f, 20.0f);

        AxisVec = standardPos.forward * -1;
        AxisVec *= distance;
        mainCamera.position = transform.position + AxisVec;
        
    }
}
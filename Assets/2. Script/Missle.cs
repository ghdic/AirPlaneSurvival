using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    //미사일의 속도
    public float speed = 60000.0f;
    //폭발 효과 프리팹 연결 변수
    public GameObject expEffect;
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        GetComponent<Rigidbody>().AddForce(transform.forward * speed);

        //5초가 지난 후 자동으로 사라지는 코루틴
        StartCoroutine(this.ExploisonCannon(5.0f));
    }

    private void Update()
    {
        //TO DO : Physics.OverlapSphere 이용해서 일정 범위 안에 들어오면 추적하게하기
        //Player의 경우에는 마우스로 추적 Target 바로 정할수 있게끔
        //타겟 지정 될시 0.1초(주기적)으로 타겟을 향해 추적하게 설정 lerp, slerp 이용
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //지면 또는 적 탱크에 충돌한 경우 즉시 폭발하도록 코루틴 실행
            StartCoroutine(this.ExploisonCannon(0.0f));
        }
    }

    IEnumerator ExploisonCannon(float tm)
    {
        yield return new WaitForSeconds(tm);
        if (tm == 0)
        {
            //충돌 콜백 함수가 발생하지 않도록 Collider를 비활성화
            _collider.enabled = false;
            //물리엔진의 영향을 받을 필요 없음
            _rigidbody.isKinematic = true;

            //폭발 프리팹 동적 생성
            GameObject obj = (GameObject)Instantiate(expEffect, transform.position, Quaternion.identity);
            Destroy(obj, 3.0f);
        }

        Destroy(this.gameObject, 0.0f);
    }
}

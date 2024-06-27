using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    Vector3 _destPos;

    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idel,
    }

    PlayerState _state = PlayerState.Idel;

    void UpdateDie()
    {
        // 아무것도 못함.
    }
    
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;

        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idel;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // 애니메이션
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다.
        anim.SetFloat("speed", _speed);

    }

    void UpdateIdel()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

  

    // Update is called once per frame
    void Update()
    {
       switch (_state)
        {
            case PlayerState.Idel:
                UpdateIdel();
                return;
            case PlayerState.Die:
                UpdateDie();
                return;
            case PlayerState.Moving:
                UpdateMoving();
                return;
        }
    }

    
    void OnMouseClicked(Define.MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
    
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
        }
    }
}

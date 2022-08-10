using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Rigidbody rigid;
    public float Speed = 10.0f;
    public float rotateSpeed = 10.0f;    // 회전 속도
    public static Player Instance { get; private set; }
    public Vector3 position { get { return transform.position; } }

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //// position의 y값과 rotation의 x, z가 변경되지 않게 고정.
        //rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX
        //| RigidbodyConstraints.FreezeRotationZ;
    }

   // [SerializeField] private float speed = 8f;
    private Vector3 velocity = Vector3.zero;
    void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Touch.SecondaryThumbstick))
        {
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            
            if(thumbstick.x < 0)
            {
                rigid.AddForce(Vector3.left*20);
            }
            if (thumbstick.x > 0)
            {
                rigid.AddForce(Vector3.right * 20);
            }
            if (thumbstick.y < 0)
            {
                rigid.AddForce(Vector3.back * 20);
            }
            if (thumbstick.y > 0)
            {
                rigid.AddForce(Vector3.forward * 20);
            }
            float x = rigid.velocity.x;
            float z = rigid.velocity.z;

            Vector3 dir = new Vector3(x, 0, z);
            // 바라보는 방향으로 회전 후 다시 정면을 바라보는 현상을 막기 위해 설정
            if (!(x == 0 && z == 0))
            {
                // 이동과 회전을 함께 처리
                transform.position += dir * Speed * Time.deltaTime;
                // 회전하는 부분. Point 1.
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
            }
        }
    }

    public bool isLive { get { return gameObject.activeSelf; } }
    public void OnDamaged()
    {
        gameObject.SetActive(false);
    }
    public void Init()
    {
        velocity = Vector3.zero;
        gameObject.SetActive(true);
    }
}
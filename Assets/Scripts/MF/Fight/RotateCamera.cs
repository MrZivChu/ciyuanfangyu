using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour
{
    private Vector3 offsetPosition;//位置偏移
    private bool isRotating = false;
    private float distance = 0;
    public Transform player;

    public float minScrollWheel;
    public float maxScrollWheel;
    public float scrollSpeed = 3;

    public float minEulerAngles;
    public float maxEulerAngles;
    public float rotateSpeed = 2;

    void Start()
    {
        //transform.LookAt(player.position);
        offsetPosition = transform.position - player.position;
    }   

    void Update()
    {
        transform.position = offsetPosition + player.position;
        //处理视野的旋转
        RotateView();
        //处理视野的拉近和拉远效果
        ScrollView();
        //双击归位
        Recover();
    }

    private float t1;
    private float t2;
    void Recover()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isClickUI = Utils.CheckGuiRaycastObjects();
            if (isClickUI)
                return;

            t2 = Time.realtimeSinceStartup;
            if (t2 - t1 < 0.3f)
            {
                offsetPosition = offsetPosition.normalized * maxScrollWheel;
                transform.position = offsetPosition + player.position;
            }
            t1 = t2;
        }
    }

    private Touch oldTouch1;  //上次触摸点1(手指1)  
    private Touch oldTouch2;  //上次触摸点2(手指2)  
    void ScrollView()
    {
        //手机操作
        if (Input.touchCount == 2)
        {
            //多点触摸, 放大缩小  
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);

            //第2点刚开始接触屏幕, 只记录，不做处理  
            if (newTouch2.phase == TouchPhase.Began)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                return;
            }

            //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            //两个距离之差，为正表示放大手势， 为负表示缩小手势  
            float offset = newDistance - oldDistance;

            //放大因子， 一个像素按 0.01倍来算(100可调整)  
            float scaleFactor = offset / 100f;


            //记住最新的触摸点，下次使用  
            oldTouch1 = newTouch1;
            oldTouch2 = newTouch2;

            distance = offsetPosition.magnitude;
            distance += -scaleFactor * scrollSpeed;
            distance = Mathf.Clamp(distance, minScrollWheel, maxScrollWheel);
            offsetPosition = offsetPosition.normalized * distance;
        }
        else//电脑操作
        {
            float tMouseScrollWheel = -Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(tMouseScrollWheel) > 0)
            {
                distance = offsetPosition.magnitude;
                distance += tMouseScrollWheel * scrollSpeed;
                distance = Mathf.Clamp(distance, minScrollWheel, maxScrollWheel);
                offsetPosition = offsetPosition.normalized * distance;
            }
        }
    }

    void RotateView()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layMask = 1 << LayerMask.NameToLayer("inLand");
            if (Physics.Raycast(ray, out hit, 1000, layMask))
            {
                isRotating = false;
            }
            else
            {
                isRotating = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
#if !UNITY_EDITOR
            //手机操作
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
#endif
            {
                transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));
                Vector3 originalPos = transform.position;
                Quaternion originalRotation = transform.rotation;

                transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
                float x = transform.eulerAngles.x;
                if (x < minEulerAngles || x > maxEulerAngles)
                {
                    //当超出范围之后，我们将属性归位原来的，就是让旋转无效 
                    transform.position = originalPos;
                    transform.rotation = originalRotation;
                }
                offsetPosition = transform.position - player.position;
            }
#if !UNITY_EDITOR
        }
#endif
        }
    }
}

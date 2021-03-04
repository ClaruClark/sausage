using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private Vector3 tap3D;
    private Vector2 tap2D;
    private bool isMove;
    private Vector2 startPoint;
    private Vector2 endPoint;
    [SerializeField] int dotsNumber;
    public GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] Transform[] dotsList;
    [SerializeField] [Range(0.01f, 0.3f)] float dotMinScale;
    [SerializeField] [Range(0.3f, 1f)] float dotMaxScale;
    [SerializeField] float pushForce = 4f;
    Vector3 pos;
    float timeStamp;
    Vector3 direction;
    Vector3 force;
    float distance;
    public bool isBlocked { get; set; }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => dotsParent != null);
        Hide();
        //prepare dots
        PrepareDots();
    }

    public void UpdateUs()
    {
        Hide();
        isBlocked = false;
        PrepareDots();
    }
    void Update()
    {
        if (UIManager.Instance.isGame)
            CheckMove();
    }

    private void CheckMove()
    {
        if (isBlocked)
            return;

        //here
#if !UNITY_EDITOR
        if(Input.touchCount > 0)
        {
            Touch tap = Input.GetTouch(0);
            if (tap.phase == TouchPhase.Began)
            {
                tap3D = Camera.main.ScreenToWorldPoint(tap.position);
                tap2D = new Vector2(tap3D.x, tap3D.y);
                startPoint = tap2D;
        isMove = true;
            Show();
            }

            if (isMove)
            {
                if (tap.phase == TouchPhase.Moved)
                {
                    tap3D = Camera.main.ScreenToWorldPoint(tap.position);
                    tap2D = new Vector2(tap3D.x, tap3D.y);
                   distance = Vector2.Distance(startPoint, tap2D);
                direction = (startPoint - tap2D).normalized;
                force = direction * distance * pushForce;
                force = new Vector3(0, force.y, force.x);
                UpdateDots(CameraController.Instance.sausage.transform.position, force);
                }


                if (tap.phase == TouchPhase.Ended)
                {
                    tap3D = Camera.main.ScreenToWorldPoint(tap.position);
                    tap2D = new Vector2(tap3D.x, tap3D.y);
                    endPoint = tap2D;
                isMove = false;
               // CameraController.Instance.sausage.Fly(startPoint - endPoint);
                CameraController.Instance.sausage.Fly(force);
                Hide();
                isBlocked = true;
                }
            }

        }
#else
        //and here

        if (Input.GetMouseButtonDown(0))
        {
            tap3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tap2D = new Vector2(tap3D.x, tap3D.y);
            startPoint = tap2D;
            isMove = true;
            Show();
        }

        if (isMove)
        {
            if (Input.GetMouseButton(0))
            {
                tap3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tap2D = new Vector2(tap3D.x, tap3D.y);
                distance = Vector2.Distance(startPoint, tap2D);
                direction = (startPoint - tap2D).normalized;
                force = direction * distance * pushForce;
                force = new Vector3(0, force.y, force.x);
                UpdateDots(CameraController.Instance.sausage.transform.position, force);
            }

            if (Input.GetMouseButtonUp(0))
            {
                tap3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tap2D = new Vector2(tap3D.x, tap3D.y);
                endPoint = tap2D;
                isMove = false;
               // CameraController.Instance.sausage.Fly(startPoint - endPoint);
                CameraController.Instance.sausage.Fly(force);
                Hide();
                isBlocked = true;

            }
        }

#endif



       
    }

    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector3 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.z = (ballPos.z + forceApplied.z * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }

}

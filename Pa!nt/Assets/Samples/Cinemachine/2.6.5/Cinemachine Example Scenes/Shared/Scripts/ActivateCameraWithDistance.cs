using UnityEngine;
using Unity;
using System.Collections;
using System.Collections.Generic;

namespace Cinemachine.Examples
{

    [AddComponentMenu("")] // Don't display in add component menu
    public class ActivateCameraWithDistance : MonoBehaviour
    {
        public List<GameObject> objectToCheck;
        [SerializeField]
        private GameObject Player;
        public float distanceToObject = 15f;
        public CinemachineVirtualCameraBase initialActiveCam;
        public List<CinemachineVirtualCameraBase> switchCameraTo;
        private CinemachineVirtualCameraBase temp;
        [SerializeField]
        private CinemachineVirtualCameraBase Fullview;
        private bool FullviewEnded;
        CinemachineBrain brain;
        [SerializeField]
        private float FullViewTime = 3;
        float zoomSpeed;
        float originalMinSize = 0.0f;
        float originalMaxSize = 0.0f;

        void Start()
        {
            FullviewEnded = false;
            brain = Camera.main.GetComponent<CinemachineBrain>();
            originalMinSize = ((CinemachineVirtualCamera)initialActiveCam).m_Lens.OrthographicSize;
            originalMaxSize = ((CinemachineVirtualCamera)Fullview).m_Lens.OrthographicSize;
            zoomSpeed = (originalMaxSize - originalMinSize) / 5.0f;
            SwitchCam(Fullview);
            Invoke("FullViewEnd", FullViewTime);
        }

        // Update is called once per frame
        void Update()
        {
            if (FullviewEnded) {
                float distance = distanceToObject;
                temp = initialActiveCam;
                for (int i = 0; i < objectToCheck.Count; i++)
                {
                    if (objectToCheck[i] && switchCameraTo[i])
                    {

                        if (Vector3.Distance(Player.transform.position, objectToCheck[i].transform.position) < distanceToObject)
                        {

                            if (distance > Vector3.Distance(Player.transform.position, objectToCheck[i].transform.position))
                            {
                                distance = Vector3.Distance(Player.transform.position, objectToCheck[i].transform.position);
                                temp = switchCameraTo[i];
                            }
                        }
                    }
                }
                SwitchCam(temp);
                MouseZoom();
                //TouchZoom();
            }
        }

        private void FullViewEnd() {
            SwitchCam(initialActiveCam);
            FullviewEnded = true;
            GameObject.Find("Title").SetActive(false);
        }

        public void SwitchCam(CinemachineVirtualCameraBase vcam)
        {
            if (brain == null || vcam == null)
                return;
            if (brain.ActiveVirtualCamera != (ICinemachineCamera)vcam)
                vcam.MoveToTopOfPrioritySubqueue();      
        }


        public void CheckSize(CinemachineVirtualCamera initial)
        {
            if(initial.m_Lens.OrthographicSize < originalMinSize)
            {
                initial.m_Lens.OrthographicSize = originalMinSize;
            }

            else if(initial.m_Lens.OrthographicSize > originalMaxSize)
            {
                initial.m_Lens.OrthographicSize = originalMaxSize;
            }
        }

        public void TouchZoom() // 터치로 줌인아웃
        {
            if(Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                CinemachineVirtualCamera initial = (CinemachineVirtualCamera)initialActiveCam;
                initial.m_Lens.OrthographicSize += deltaMagnitudeDiff * zoomSpeed;

                CheckSize(initial);
                otherZoom(deltaMagnitudeDiff * zoomSpeed);
            }
        }

        public void ButtonZoomIn() // 버튼으로 줌인
        {
            CinemachineVirtualCamera initial = (CinemachineVirtualCamera)initialActiveCam;
            initial.m_Lens.OrthographicSize += -zoomSpeed;
            CheckSize(initial);
            otherZoom(-zoomSpeed);
        }
        
        public void ButtonZoomOut()
        {
            CinemachineVirtualCamera initial = (CinemachineVirtualCamera)initialActiveCam;
            initial.m_Lens.OrthographicSize += zoomSpeed;
            CheckSize(initial);
            otherZoom(zoomSpeed);
        }

        public void MouseZoom() // 마우스로 줌인아웃
        {
            float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;

            if(distance != 0)
            {
                CinemachineVirtualCamera initial = (CinemachineVirtualCamera)initialActiveCam;
                initial.m_Lens.OrthographicSize += distance;
                CheckSize(initial);
                otherZoom(distance);
                
            }
        }

        public void otherZoom(float distance)
        {
             for(int i = 0; i < switchCameraTo.Count; i++)
            {
                CinemachineVirtualCamera scam = (CinemachineVirtualCamera)switchCameraTo[i];
                scam.m_Lens.OrthographicSize += distance;
                CheckSize(scam);
            }
        }
    }
}
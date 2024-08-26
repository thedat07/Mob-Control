using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveStage : MonoBehaviour
{
    public enum State
    {
        Normal,
        CannonMove,
        StageMove
    }

    public Camera mainCamera;

    private bool flg = false;
    private int count = 0;

    private GameObject cam;
    private GameObject cannonOb = null;
    private Cannon cannon;
    private Vector3 distCam;

    private int stageNum = 1;
    public State state = State.Normal;
    public GameObject startPoint1 = null;
    public GameObject startPoint2 = null;
    public GameObject startPoint3 = null;

    GameObject ClearUI;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        cannonOb = GameObject.Find("Cannon");
        startPoint1 = GameObject.Find("startPoint (1)");
        startPoint2 = GameObject.Find("startPoint (2)");
        startPoint3 = GameObject.Find("startPoint (3)");
        stageNum = 1;

        cannon = cannonOb.GetComponent<Cannon>();
        cannon.initPos = startPoint1.transform.position;

        distCam = cam.transform.position - cannon.transform.position;
        ClearUI = GameObject.Find("StageClear");
        ClearUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemybase = GameObject.FindGameObjectWithTag("EnemyBase");
        Debug.Log(enemybase);
        if (enemybase == null)
        {
            ClearUI.SetActive(true);
        }


        Vector3 nmov;
        Vector3 amov;

        switch (state)
        {
            case State.Normal:
                --count;
                if (count == 0)
                {
                    flg = false;
                }
                if (flg)
                {
                    return;
                }
                // カメラの視野に入るすべてのコライダーを取得
                Collider[] collidersInView = Physics.OverlapBox(mainCamera.transform.position, mainCamera.transform.localScale * 17);

                EnemyBase eb = null;
                // カメラの視野に入るオブジェクトを順にチェック
                foreach (Collider collider in collidersInView)
                {
                    if (collider.CompareTag("EnemyBase"))
                    {
                        // 指定したタグを持つオブジェクトがカメラの視野に入っている
                        eb = collider.gameObject.GetComponent<EnemyBase>();
                    }
                }

                if (eb == null)
                {
                    //ステージ移動
                    foreach (Collider collider in collidersInView)
                    {
                        if (collider.CompareTag("Mob") || collider.CompareTag("Enemy") || collider.CompareTag("Increase") || collider.CompareTag("StageOb")|| collider.CompareTag("Syogai"))
                        {
                            Destroy(collider.gameObject);
                        }

                    }
                    flg = true;
                    count = 600;
                    state = State.CannonMove;
                }

                break;
            case State.CannonMove:
                switch (stageNum)
                {
                    case 1:
                        nmov = (startPoint1.transform.position - cannonOb.transform.position).normalized;
                        cannonOb.transform.position += nmov * 0.1f;
                        if((startPoint1.transform.position - cannonOb.transform.position).magnitude < 0.5f)
                        {
                            state = State.StageMove;
                        }

                        break;
                    case 2:
                        if (startPoint2 == null)
                        {
                            return;
                        }
                        nmov = (startPoint2.transform.position - cannonOb.transform.position).normalized;
                        cannonOb.transform.position += nmov * 0.1f;
                        if ((startPoint2.transform.position - cannonOb.transform.position).magnitude < 0.5f)
                        {
                            state = State.StageMove;
                        }

                        break;
                    case 3:
                        if (startPoint3 == null)
                        {
                            return;
                        }

                        nmov = (startPoint3.transform.position - cannonOb.transform.position).normalized;
                        cannonOb.transform.position += nmov * 0.1f;
                        if ((startPoint3.transform.position - cannonOb.transform.position).magnitude < 0.5f)
                        {
                            state = State.StageMove;
                        }

                        break;
                    default:

                        break;
                }
                break;
            case State.StageMove:
                switch (stageNum)
                {
                    case 1:
                        if (startPoint2 == null)
                        {
                            return;
                        }
                        nmov = (startPoint2.transform.position - cannonOb.transform.position).normalized;
                        amov = ((Quaternion.AngleAxis(startPoint2.transform.rotation.eulerAngles.y, Vector3.up) * distCam) + startPoint2.transform.position - cam.transform.position).normalized;
                        nmov.y = 0;
                        cannonOb.transform.position += nmov * 0.2f;
                        cam.transform.position += amov * 0.21f;
                        if ((startPoint2.transform.position - cannonOb.transform.position).magnitude < 0.5f)
                        {
                            cannon.transform.position = startPoint2.transform.position;
                            cam.transform.position = cannon.transform.position + Quaternion.AngleAxis(startPoint2.transform.rotation.eulerAngles.y, Vector3.up) * distCam;
                            cannonOb.transform.rotation = Quaternion.Euler(cannonOb.transform.rotation.eulerAngles.x, startPoint2.transform.rotation.eulerAngles.y, cannonOb.transform.rotation.eulerAngles.z);
                            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, startPoint2.transform.rotation.eulerAngles.y, cam.transform.rotation.eulerAngles.z);
                            state = State.Normal;
                            cannon.initPos = startPoint2.transform.position;
                            ++stageNum;
                        }

                        break;
                    case 2:
                        if (startPoint3 == null)
                        {
                            return;
                        }
                        nmov = (startPoint3.transform.position - cannonOb.transform.position).normalized;
                        amov = ((Quaternion.AngleAxis(startPoint3.transform.rotation.eulerAngles.y, Vector3.up) * distCam) + startPoint3.transform.position - cam.transform.position).normalized;
                        nmov.y = 0;
                        cannonOb.transform.position += nmov * 0.2f;
                        cam.transform.position += amov * 0.21f;
                        if ((startPoint3.transform.position - cannonOb.transform.position).magnitude < 0.5f)
                        {
                            cannon.transform.position = startPoint3.transform.position;
                            cam.transform.position = cannon.transform.position + Quaternion.AngleAxis(startPoint3.transform.rotation.eulerAngles.y, Vector3.up) * distCam;
                            cannonOb.transform.rotation = Quaternion.Euler(cannonOb.transform.rotation.eulerAngles.x, startPoint3.transform.rotation.eulerAngles.y, cannonOb.transform.rotation.eulerAngles.z);
                            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, startPoint3.transform.rotation.eulerAngles.y, cam.transform.rotation.eulerAngles.z);
                            state = State.Normal;
                            cannon.initPos = startPoint3.transform.position;
                            ++stageNum;
                        }

                        break;
                    case 3:


                        break;
                    default:

                        break;
                }

                break;
            default:

                break;
        }

    }
}

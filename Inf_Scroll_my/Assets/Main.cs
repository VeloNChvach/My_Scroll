using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform center;
    public Button[] bttn;

    private int bttnDistance;
    private float[] bttnDistToCenter;
    private int step;
    private float newX;
    private float newY;
    private bool dragging = false;
    private float MinDistance;
    private float[] nMin;
    private float nMinDist;
    private int N_bttn_min;
    private string bttnTag;
    private Vector2 mousePos;
    private float[] diffMousePos;
    private int nMinDiffMP;


    void Start ()
    {
        bttnDistToCenter = new float[bttn.Length];
        nMin = new float[bttn.Length];
        diffMousePos = new float[bttn.Length];
        bttnDistance = (int)Mathf.Abs(bttn[0].transform.position.x - bttn[1].transform.position.x);
    }

    void Update ()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            bttnDistToCenter[i] = bttn[i].transform.position.x - center.transform.position.x;
            nMin[i] = Mathf.Abs(bttnDistToCenter[i]);

            if (bttnDistToCenter[i] < -1000)
            {
                step = bttn.Length * bttnDistance;
                newX = bttn[i].transform.position.x + step;
                newY = bttn[i].transform.position.y;
                bttn[i].transform.position = new Vector3(newX, newY, 0);
            }

            if (bttnDistToCenter[i] > 1000)
            {
                step = bttn.Length * bttnDistance;
                newX = bttn[i].transform.position.x - step;
                newY = bttn[i].transform.position.y;
                bttn[i].transform.position = new Vector3(newX, newY, 0);
            }
        }

        nMinDist = Mathf.Min(nMin);

        for (int a = 0; a < bttn.Length; a++)
        {
            if (nMinDist == nMin[a])
            {
                N_bttn_min = a;
            }
        }

        if (!dragging)
        {
            LerpToBttn(-bttnDistToCenter[N_bttn_min]);
        }

    }

    void LerpToBttn(float distance)
    {
        float toX = Mathf.Lerp(panel.transform.position.x, panel.transform.position.x + distance, Time.deltaTime * 5f);
        Vector2 newPosition = new Vector2(toX, panel.transform.position.y);

        panel.transform.position = newPosition;
    }

    public void StartDrag ()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }


    void OnGUI()
    {
        Event e = Event.current;

        if (e.button == 0 && e.isMouse)
        {
            if (e.type == EventType.MouseDown)
            {
                mousePos = e.mousePosition;


                for (int c = 0; c < bttn.Length; c++)
                {
                    diffMousePos[c] = Mathf.Abs(bttn[c].transform.position.x - mousePos.x);
                }

                float minDiffMP = Mathf.Min(diffMousePos);

                for (int d = 0; d < bttn.Length; d++)
                {
                    if (minDiffMP == diffMousePos[d])
                    {
                        nMinDiffMP = d;
                    }
                }

                float distClick = bttn[nMinDiffMP].transform.position.x - center.transform.position.x;
                //print(nMinDiffMP);
                panel.transform.position = new Vector2(panel.transform.position.x - distClick, panel.transform.position.y);
            }
        }

    }

}

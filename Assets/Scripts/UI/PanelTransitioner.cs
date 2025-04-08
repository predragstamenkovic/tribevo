using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTransitioner : MonoBehaviour
{
    public List<APanel> panels;
    public float transitionDuration;

    private static PanelTransitioner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance.Equals(this))
        {
            Destroy(gameObject);
        }
    }

    private static void CheckInstance()
    {
        if (instance == null)
        {
            var obj = FindObjectOfType<PanelTransitioner>();
            if (obj != null)
            {
                var pt = obj.GetComponent<PanelTransitioner>();
                if (pt != null)
                    instance = pt;
            }
        }
    }

    public static PanelTransitioner Instance
    {
        get
        {
            CheckInstance();
            return instance;
        }
    }

    public void Transition(string from, string to)
    {
        APanel fromPanel = null;
        APanel toPanel = null;

        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i].PanelName == from)
                fromPanel = panels[i];
            if (panels[i].PanelName == to)
                toPanel = panels[i];
        }

        if (fromPanel != null && toPanel != null)
            StartCoroutine(TransitionCoroutine(fromPanel, toPanel));
        else
            Debug.LogError("Panel not found: " + from + " -> " + to);
    }

    public void Transition(APanel from, APanel to)
    {
        StartCoroutine(TransitionCoroutine(from, to));
    }

    private IEnumerator TransitionCoroutine(APanel from, APanel to)
    {
        var phase = 0.0f;
        while (phase < transitionDuration)
        {

            phase += Time.deltaTime;
            yield return null;
        }

        from.BeforeHide();
        to.BeforeShow();
        yield return null;
        from.Hide();
        to.Show();
        yield return null;
        from.AfterHide();
        to.AfterShow();

        phase = 0.0f;
        while (phase < transitionDuration)
        {

            phase += Time.deltaTime;
            yield return null;
        }
    }
}

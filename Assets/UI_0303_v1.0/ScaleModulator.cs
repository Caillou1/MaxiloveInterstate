using UnityEngine;

public class ScaleModulator : MonoBehaviour
{

    [SerializeField, Range(0, 5)]
    float swellTime;
    [SerializeField]
    AnimationCurve swellCurve;
    [SerializeField]
    bool swellFromStart;

    [SerializeField]
    bool swellLoop;


    bool canSwell;
    bool canSwellCooldown;
    float timer;

    Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        if (swellFromStart)
        {
            TriggerSwell();
        }
    }

    void OnEnable()
    {
        initialScale = transform.localScale;
        if (swellFromStart)
        {
            TriggerSwell();
        }
    }


    public void TriggerSwell()
    {
        canSwell = true;

    }

    public void TriggerSwell(float cooldownTime)
    {
        canSwellCooldown = true;
        swellTime = cooldownTime;
    }


    void Update()
    {
        /*
		if (Input.GetKeyDown (KeyCode.N)) {
			TriggerSwell ();
		}
		*/


        if (canSwell)
        {
            Swell();
        }

        if (canSwellCooldown)
        {
            Swell(Vector3.one);
        }

    }

    void Swell()
    {
        timer += Time.unscaledDeltaTime;

        float swellPercent = timer / swellTime;
        swellPercent = Mathf.Clamp01(swellPercent);
        float swellAmount = swellCurve.Evaluate(swellPercent);

        transform.localScale = initialScale * swellAmount; ;

        if (swellPercent >= 1)
        {
            if (!swellLoop)
            {
                canSwell = false;
            }

            timer = 0;
        }
    }

    void Swell(Vector3 v)
    {
        timer += Time.unscaledDeltaTime;

        float swellPercent = timer / swellTime;
        swellPercent = Mathf.Clamp01(swellPercent);
        float swellAmount = swellCurve.Evaluate(swellPercent);

        transform.localScale = v * swellAmount; ;

        if (swellPercent >= 1)
        {
            if (!swellLoop)
            {
                canSwellCooldown = false;
            }

            timer = 0;
        }

    }
}

using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float maxProtectionTime = 3f;
    public GameObject shield;

    private Slider slider;
    private float protectionTime;

    public bool IsActive { get; private set; }

    void Start()
    {
        slider = GetComponent<Slider>();
        protectionTime = maxProtectionTime;

        if (slider != null)
        {
            slider.value = 1.0f;
        }

        if (shield != null)
        {
            shield.SetActive(false);
        }

        IsActive = false;
    }

    void Update()
    {
        if (shield == null)
        {
            return;
        }

        if (slider != null)
        {
            slider.value = Mathf.Clamp(protectionTime / maxProtectionTime, 0f, 1f);
        }

        if (UserInput.Instance.input.Shield.IsPressed())
        {
            if (protectionTime > 0f)
            {
                protectionTime -= Time.deltaTime;
                IsActive = true;
                shield.SetActive(true);
            }
            else
            {
                protectionTime = 0f;
                IsActive = false;
                shield.SetActive(false);
            }
        }
        else
        {
            protectionTime += Time.deltaTime;

            if (protectionTime > maxProtectionTime)
            {
                protectionTime = maxProtectionTime;
            }

            IsActive = false;
            shield.SetActive(false);
        }
    }

    public void RefillShield()
    {
        protectionTime = maxProtectionTime;

        if (slider != null)
        {
            slider.value = 1.0f;
        }
    }
}
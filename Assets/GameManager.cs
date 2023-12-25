using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float damageIntensity = 0;
    public PostProcessVolume damageVolume;
    public Vignette damageVignette;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (damageVolume != null)
        {
            damageVolume.profile.TryGetSettings(out damageVignette);
            if (damageVignette != null)
            {
                damageVignette.enabled.Override(false);
            }
        }
    }

    public void ApplyDamageEffect()
    {
        StartCoroutine(TakeDamageEffect());
    }

    private IEnumerator TakeDamageEffect()
    {
        damageIntensity = 1.5f;

        if (damageVignette != null)
        {
            damageVignette.enabled.Override(true);
            damageVignette.intensity.Override(damageIntensity);
        }

        yield return new WaitForSeconds(0.1f);

        while (damageIntensity > 0)
        {
            damageIntensity -= 0.1f;

            if (damageIntensity < 0)
            {
                damageIntensity = 0;
            }

            if (damageVignette != null)
            {
                damageVignette.intensity.Override(damageIntensity);
            }

            yield return new WaitForSeconds(0.1f);
        }

        if (damageVignette != null)
        {
            damageVignette.enabled.Override(false);
        }

        yield break;
    }
}

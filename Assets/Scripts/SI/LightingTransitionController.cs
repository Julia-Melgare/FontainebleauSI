using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LightingTransitionController : MonoBehaviour
{
    [Header("Post Processing Volumes")]
    public Volume sunsetPostProcess;
    public Volume nightPostProcess;
    public Volume morningPostProcess;
    
    [Header("Sky and Fog Volumes")]
    public Volume sunsetSkyAndFog;
    public Volume nightSkyAndFog;
    public Volume morningSkyAndFog;

    [Header("Directional Lights")]
    public Light sunsetSun;
    public Light nightMoon;
    public Light morningSun;

    public Material fadeMaterial;
    
    void Start()
    {
        //StartCoroutine(Test());
    }

    public void TransitionToNight()
    {
        StartCoroutine(ChangeLighting(sunsetPostProcess, nightPostProcess, sunsetSkyAndFog, nightSkyAndFog, sunsetSun,
            nightMoon, 1.5f, Color.black, 16f));
    }

    public void TransitionToMorning()
    {
        StartCoroutine(ChangeLighting(nightPostProcess, morningPostProcess, nightSkyAndFog, morningSkyAndFog, nightMoon,
            morningSun, 4f, Color.white, 14f));
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(30f);
        //Debug.Log("Starting lighting transition...");
        StartCoroutine(ChangeLighting(sunsetPostProcess, nightPostProcess, sunsetSkyAndFog, nightSkyAndFog, sunsetSun,
            nightMoon, 1.5f, Color.black, 5f));
        yield return new WaitForSeconds(60f);
        StartCoroutine(ChangeLighting(nightPostProcess, morningPostProcess, nightSkyAndFog, morningSkyAndFog, nightMoon,
            morningSun, 4f, Color.white,5f));
        yield return null;
    }

    private IEnumerator ChangeLighting(Volume oldPostProcessVol, Volume newPostProcessVol, Volume oldSkyFogVol, Volume newSkyFogVol, Light oldLight, Light newLight, float newLightIntensity, Color fadeImageColor, float transitionDuration)
    {
        float time = 0f;
        float oldLightIntensity = oldLight.intensity;
        fadeMaterial.SetColor("_UnlitColor", new Color(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b, 0f));
        
        while (time < transitionDuration)
        {
            float t = time / transitionDuration;

            oldPostProcessVol.weight = Mathf.Lerp(1f, 0f, t);
            newPostProcessVol.weight = Mathf.Lerp(0f, 1f, t);
            oldLight.intensity = Mathf.Lerp(oldLightIntensity, 0, t);
            newLight.intensity = Mathf.Lerp(0, newLightIntensity, t);
            fadeMaterial.SetColor("_UnlitColor", new Color(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b, Mathf.Lerp(0f, 1f, t)));
            time += Time.deltaTime;
            yield return null;
        }

        oldSkyFogVol.weight = 0f;
        newSkyFogVol.weight = 1f;

        time = 0f;
        while (time < transitionDuration)
        {
            float t = time / transitionDuration;

            fadeMaterial.SetColor("_UnlitColor", new Color(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b, Mathf.Lerp(1f, 0f, t)));
            time += Time.deltaTime;
            yield return null;
        }
    }

}


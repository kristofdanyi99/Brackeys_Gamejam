using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    // Resource values
    public float water = 100f;
    public float oxygen = 100f;
    public float fuel = 100f;
    public float energy = 100f;

    // Resource depletion rates
    public float waterDepletionRate = 1f;
    public float oxygenDepletionRate = 1f;
    public float fuelDepletionRate = 1f;
    public float energyDepletionRate = 1f;

    // UI Sliders for resource bars
    public Slider waterBar;
    public Slider oxygenBar;
    public Slider fuelBar;
    public Slider energyBar;

    // TV Canvas Image
    public Image fillImage;

    public Color32 highColor = new Color32(0, 80, 7, 255);
    public Color32 mediumColor = new Color32(245, 163, 0, 75);
    public Color32 lowColor = new Color32(63, 7, 0, 255);


    // Update is called once per frame
    void Update()
    {
        // Deplete resources over time
        water -= waterDepletionRate * Time.deltaTime;
        oxygen -= oxygenDepletionRate * Time.deltaTime;
        fuel -= fuelDepletionRate * Time.deltaTime;
        energy -= energyDepletionRate * Time.deltaTime;

        // Clamp resource values between 0 and 100
        water = Mathf.Clamp(water, 0f, 100f);
        oxygen = Mathf.Clamp(oxygen, 0f, 100f);
        fuel = Mathf.Clamp(fuel, 0f, 100f);
        energy = Mathf.Clamp(energy, 0f, 100f);

        // Update the UI sliders
        waterBar.value = Mathf.Lerp(waterBar.value, water, Time.deltaTime * 5f);
        oxygenBar.value = Mathf.Lerp(oxygenBar.value, oxygen, Time.deltaTime * 5f);
        fuelBar.value = Mathf.Lerp(fuelBar.value, fuel, Time.deltaTime * 5f);
        energyBar.value = Mathf.Lerp(energyBar.value, energy, Time.deltaTime * 5f);

        UpdateScreenColor();
    }

    public void UpdateScreenColor()
    {
        if (water > 50f && oxygen > 50f && fuel > 50f && energy > 50f)
        {
            fillImage.color = highColor;
        }
        else if (water > 20f && oxygen > 20f && fuel > 20f && energy > 20f)
        {
            fillImage.color = mediumColor;
        }
        else
        {
            fillImage.color = lowColor;
        }
    }

    // Example method to add resources (e.g., when collecting items)
    public void AddWater(float amount)
    {
        water += amount;
        water = Mathf.Clamp(water, 0f, 100f);
    }

    public void AddOxygen(float amount)
    {
        oxygen += amount;
        oxygen = Mathf.Clamp(oxygen, 0f, 100f);
    }

    public void AddFuel(float amount)
    {
        fuel += amount;
        fuel = Mathf.Clamp(fuel, 0f, 100f);
    }

    public void AddEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0f, 100f);
    }
}

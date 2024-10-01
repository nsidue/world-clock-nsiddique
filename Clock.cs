using System;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;

    [SerializeField] Transform nyHoursPivot, nyMinutesPivot, nySecondsPivot;
    [SerializeField] Transform caHoursPivot, caMinutesPivot, caSecondsPivot;
    [SerializeField] Transform bdHoursPivot, bdMinutesPivot, bdSecondsPivot;
    [SerializeField] Transform lyHoursPivot, lyMinutesPivot, lySecondsPivot;
    
    [SerializeField] Text nyDigitalClockText;  // Optional for displaying time digitally
    [SerializeField] Text caDigitalClockText;
    [SerializeField] Text bdDigitalClockText;
    [SerializeField] Text lyDigitalClockText;

    // Time zones
    TimeZoneInfo nyTimeZone;
    TimeZoneInfo caTimeZone;
    TimeZoneInfo bdTimeZone;
    TimeZoneInfo lyTimeZone;

    void Start()
    {
        // Initialize time zones for each city
        nyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");    // New York
        caTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");    // California
        bdTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time"); // Bangladesh
        lyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Libya Standard Time");      // Libya
    }

    void Update()
    {
        DateTime utcTime = DateTime.UtcNow;

        // Update clocks for each city
        DateTime nyTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, nyTimeZone);
        UpdateClock(nyTime, nyHoursPivot, nyMinutesPivot, nySecondsPivot, nyDigitalClockText);

        DateTime caTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, caTimeZone);
        UpdateClock(caTime, caHoursPivot, caMinutesPivot, caSecondsPivot, caDigitalClockText);

        DateTime bdTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, bdTimeZone);
        UpdateClock(bdTime, bdHoursPivot, bdMinutesPivot, bdSecondsPivot, bdDigitalClockText);

        DateTime lyTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, lyTimeZone);
        UpdateClock(lyTime, lyHoursPivot, lyMinutesPivot, lySecondsPivot, lyDigitalClockText);
    }

    void UpdateClock(DateTime localTime, Transform hoursPivot, Transform minutesPivot, Transform secondsPivot, Text digitalClockText)
    {
        // Calculate rotations for each hand
        float hourRotation = hoursToDegrees * (localTime.Hour % 12 + localTime.Minute / 60f);
        float minuteRotation = minutesToDegrees * localTime.Minute;
        float secondRotation = secondsToDegrees * localTime.Second;

        // Apply rotations to the clock hands
        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, hourRotation);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minuteRotation);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, secondRotation);

        // Update digital time display (optional)
        if (digitalClockText != null)
        {
            digitalClockText.text = localTime.ToString("HH:mm:ss");
        }
    }
}

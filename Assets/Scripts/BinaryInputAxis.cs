using System;
using UnityEngine;

/// <summary>
/// Class for turning an input axis into a pair of buttons.
/// </summary>
public class BinaryInputAxis
{
    const float onThreshold = 0.75f;
    const float offThreshold = 0.25f;
    readonly string axisName;
    bool isReset;
    int state;

    /// <summary>
    /// Returns +1 or -1 if the axis was moved during this update.
    /// </summary>
    /// <value>The value.</value>
    public int Value { get; private set; }

    public BinaryInputAxis(string axisName)
    {
        this.axisName = axisName;
    }

    public void Reset()
    {
        this.isReset = false;
        this.state = 0;
    }

    public void Update()
    {
        float axisValue = Input.GetAxis(this.axisName);
        if (!isReset)
        {
            if (Mathf.Abs(axisValue) < offThreshold)
                isReset = true;
            return;
        }
        int lastState = state;
        float posThreshold = lastState > 0 ? offThreshold : onThreshold;
        float negThreshold = lastState < 0 ? offThreshold : onThreshold;
        if (axisValue > posThreshold)
            state = 1;
        else if (axisValue < -negThreshold)
            state = -1;
        else
            state = 0;
        Value = state == lastState ? 0 : state;
    }
}

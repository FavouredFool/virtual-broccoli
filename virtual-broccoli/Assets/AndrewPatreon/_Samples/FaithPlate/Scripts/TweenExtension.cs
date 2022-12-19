using System;
using Pixelplacement;
using Pixelplacement.TweenSystem;

public class TweenExtension : Tween
{
    public static TweenBase FaithCurve(Action startCallback, Action completeCallback, Action<float> valueUpdatedCallback, float duration, float delay)
    {
        ValueFloat tween = new ValueFloat(0.0f, 1.0f, valueUpdatedCallback, duration, delay, true, Tween.EaseIn, LoopType.None, startCallback, completeCallback);
        Instance.ExecuteTween(tween);
        return tween;
    }
}

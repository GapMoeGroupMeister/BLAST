using System;

public interface IDetectable
{
    event Action OnDetectEvent;
    event Action OnUnDetectEvent;
    void Detect();
    void UnDetect();
}
using System;

namespace CannonShootingPrototype.Infrastructure.Services.Input
{
    public interface IMouseInputService
    {
        event Action<float> AxisXChanged;
        event Action<float> AxisYChanged;
        event Action<float> WheelChanged;
    }
}
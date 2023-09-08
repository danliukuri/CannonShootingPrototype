using System;

namespace CannonShootingPrototype.Infrastructure.Services.Input
{
    public interface IMouseInputService
    {
        event Action<float> OnMouseAxisXChanged;
        event Action<float> OnMouseAxisYChanged;
    }
}
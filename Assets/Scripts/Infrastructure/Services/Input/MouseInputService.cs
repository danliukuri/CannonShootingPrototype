using System;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Services.Input
{
    public class MouseInputService : ITickable, IMouseInputService
    {
        private const string MouseXAxisName = "Mouse X";
        private const string MouseYAxisName = "Mouse Y";
        
        private Vector2 _mouseAxis;

        public event Action<float> OnMouseAxisXChanged;
        public event Action<float> OnMouseAxisYChanged;

        public void Tick(float deltaTime)
        {
            float newMouseAxisX = UnityEngine.Input.GetAxis(MouseXAxisName);
            float newMouseAxisY = UnityEngine.Input.GetAxis(MouseYAxisName);

            if (!Mathf.Approximately(newMouseAxisX, _mouseAxis.x))
            {
                _mouseAxis.x = newMouseAxisX;
                OnMouseAxisXChanged?.Invoke(_mouseAxis.x);
            }

            if (!Mathf.Approximately(newMouseAxisY, _mouseAxis.x))
            {
                _mouseAxis.y = newMouseAxisY;
                OnMouseAxisYChanged?.Invoke(_mouseAxis.y);
            }
        }
    }
}
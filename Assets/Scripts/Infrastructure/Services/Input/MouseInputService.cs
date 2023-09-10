using System;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Services.Input
{
    public class MouseInputService : ITickable, IMouseInputService
    {
        private const string MouseXAxisName = "Mouse X";
        private const string MouseYAxisName = "Mouse Y";
        private const string MouseWheelAxisName = "Mouse ScrollWheel";
        
        private Vector2 _mouseAxis;
        private float _mouseWheelAxis;

        public event Action<float> AxisXChanged;
        public event Action<float> AxisYChanged;
        public event Action<float> WheelChanged; 

        public void Tick(float deltaTime)
        {
            float newMouseAxisX = UnityEngine.Input.GetAxis(MouseXAxisName);
            if (!Mathf.Approximately(newMouseAxisX, _mouseAxis.x))
            {
                _mouseAxis.x = newMouseAxisX;
                AxisXChanged?.Invoke(_mouseAxis.x);
            }

            float newMouseAxisY = UnityEngine.Input.GetAxis(MouseYAxisName);
            if (!Mathf.Approximately(newMouseAxisY, _mouseAxis.x))
            {
                _mouseAxis.y = newMouseAxisY;
                AxisYChanged?.Invoke(_mouseAxis.y);
            }

            float newMouseWheelAxis = UnityEngine.Input.GetAxis(MouseWheelAxisName);
            if (!Mathf.Approximately(newMouseWheelAxis, _mouseWheelAxis))
            {
                _mouseWheelAxis = newMouseWheelAxis;
                WheelChanged?.Invoke(newMouseWheelAxis);
            }
        }
    }
}
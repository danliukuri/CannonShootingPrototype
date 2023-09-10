using System;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class CannonFirepowerChanger : IInitializable, IDisposable
    {
        private readonly CannonData _cannon;
        private readonly CannonConfig _cannonConfig;
        private readonly IMouseInputService _mouseInputService;

        public CannonFirepowerChanger(CannonData cannon, CannonConfig cannonConfig,
            IMouseInputService mouseInputService)
        {
            _cannon = cannon;
            _cannonConfig = cannonConfig;
            _mouseInputService = mouseInputService;
        }

        public void Initialize() => _mouseInputService.WheelChanged += ChangeFirepower;

        public void Dispose() => _mouseInputService.WheelChanged -= ChangeFirepower;

        private void ChangeFirepower(float mouseWheelAxis)
        {
            float newFirepower = _cannon.Firepower + mouseWheelAxis * _cannonConfig.FirepowerChangingSpeed;
            _cannon.Firepower = Mathf.Clamp(newFirepower, _cannonConfig.MinFirepower, _cannonConfig.MaxFirepower);
        }
    }
}
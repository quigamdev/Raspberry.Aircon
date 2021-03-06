﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace Raspberry.Aircon.PiController.Facades
{
    public class LedLightsFacade :FacadeBase
    {
        protected bool GpioPowerOffSignalValue { get; set; } = true;
        public void SwitchOn()
        {
            Logger.LogInformation("GPIO 26 ON");
            
            // Get a reference to the pin you need to use.
            // All 3 methods below are exactly equivalent
            var blinkingPin = Pi.Gpio.Pin26;

            // Configure the pin as an output
            blinkingPin.PinMode = GpioPinDriveMode.Output;

            // perform writes to the pin by toggling the isOn variable
            blinkingPin.Write(!GpioPowerOffSignalValue);
            Logger.LogInformation("GPIO 26 Output written");
        }

        public void SwitchOff()
        {
            Logger.LogInformation("GPIO 26 OFF");

            // Get a reference to the pin you need to use.
            // All 3 methods below are exactly equivalent
            var blinkingPin = Pi.Gpio.Pin26;

            // Configure the pin as an output
            blinkingPin.PinMode = GpioPinDriveMode.Output;

            // perform writes to the pin by toggling the isOn variable
            blinkingPin.Write(GpioPowerOffSignalValue);
            Logger.LogInformation("GPIO 26 Output written");
        }
    }
}

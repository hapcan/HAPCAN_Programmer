using Hapcan.General;

namespace Hapcan.Messages;

    class Msg304_Temperature
    {
        private HapcanFrame _frame;

        public Msg304_Temperature(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDesription()
        {
            var subframe = _frame.Data[6];
            switch (subframe)
            {
                case 0x11: return new Msg304_11(_frame).GetDesription();
                case 0x12: return new Msg304_12(_frame).GetDesription();
                case 0x13: return new Msg304_13(_frame).GetDesription();
                case 0xF0: return new Msg304_F0(_frame).GetDesription();
                default: return "TEMPERATURE - unknown data";
            }
        }
    }
    class Msg304_11
    {
        private HapcanFrame _frame;

        public Msg304_11(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDesription()
        {
            var temp = GetTemperature();
            var setp = GetSetPoint();
            var hyster = GetHysteresis();

            return string.Format("TEMPERATURE - Temperature: {0}°C, Setpoint: {1}°C, Hysteresis: {2}°C", temp, setp, hyster);
        }

        private string GetTemperature()
        {
            var tempH = _frame.Data[7];
            var tempL = _frame.Data[8];
            return ((float)((short)(tempH * 256 + tempL) * 0.0625)).ToString("N4");
        }
        private string GetSetPoint()
        {
            var setH = _frame.Data[9];
            var setL = _frame.Data[10];
            return ((float)((short)(setH * 256 + setL) * 0.0625)).ToString("N4");
        }
        private string GetHysteresis()
        {
            var hyster = _frame.Data[11];
            return ((float)((short)(hyster + 1) * 0.0625)).ToString("N4");
        }
    }
    class Msg304_12
    {
        private HapcanFrame _frame;

        public Msg304_12(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDesription()
        {
            var thermSt = GetThermostatState();
            var thermEn = GetThermostatEnabled();

            return string.Format("THERMOSTAT - {0}, {1}", thermSt, thermEn);
        }
        private string GetThermostatState()
        {
            var therm = _frame.Data[7];
            if (therm == 0x00)
                return "Temperature below setpoint";
            else if (therm == 0xFF)
                return "Temperature above setpoint";
            else if (therm == 0x80)
                return "State not defined yet";
            else
                return "Unknow state";
        }
        private string GetThermostatEnabled()
        {
            var therm = _frame.Data[11];
            if (therm == 0x00)
                return "Thermostat turned off";
            else if (therm == 0xFF)
                return "Thermostat turned on";
            else
                return "Unknow state";
        }
    }
    class Msg304_13
    {
        private HapcanFrame _frame;

        public Msg304_13(HapcanFrame frame)
        {
            _frame = frame;
        }
        public string GetDesription()
        {
            var heatPwmState = GetHeatPwmState();
            var heatPwmDuty = GetHeatPwmDuty();
            var coolPwmState = GetCoolPwmState();
            var coolPwmDuty = GetCoolPwmDuty();
            var ctrlEn = GetControllerEnabled();

            return string.Format("PWM CONTROLLER - {0}, {1}, {2}, {3}, {4}", heatPwmState, heatPwmDuty, coolPwmState, coolPwmDuty, ctrlEn);
        }
        private string GetHeatPwmState()
        {
            var pwm = _frame.Data[7];
            if (pwm == 0x00)
                return "Heating PWM pulse off";
            else if (pwm == 0xFF)
                return "Heating PWM pulse on";
            else
                return "Heating PWM unknow state";
        }
        private string GetHeatPwmDuty()
        {
            return string.Format("Heating PWM duty: {0}/255", _frame.Data[9]);
        }
        private string GetCoolPwmState()
        {
            var pwm = _frame.Data[9];
            if (pwm == 0x00)
                return "Cooling PWM pulse off";
            else if (pwm == 0xFF)
                return "Cooling PWM pulse on";
            else
                return "Cooling PWM unknow state";
        }
        private string GetCoolPwmDuty()
        {
            return string.Format("Cooling PWM duty: {0}/255", _frame.Data[11]);
        }
        private string GetControllerEnabled()
        {
            var ctrl = _frame.Data[11];
            if (ctrl == 0x00)
                return "Controller turned off";
            else if (ctrl == 0xFF)
                return "Controller turned on";
            else
                return "Unknow state";
        }
    }
class Msg304_F0
{
    private HapcanFrame _frame;

    public Msg304_F0(HapcanFrame frame)
    {
        _frame = frame;
    }

    public string GetDesription()
    {
        var error = GetError();

        return string.Format("TEMPERATURE ERROR - {0}", error);
    }
    private string GetError()
    {
        var error = _frame.Data[7];
        if (error == 0x01)
            return "Sensor not connected";
        else if (error == 0x02)
            return "Connected more than one sensor or connected wrong type of sensor";
        else if (error == 0x03)
            return "Connected wrong type of sensor";
        else if (error == 0x04)
            return "Communication problem on 1-wire network (CRC problem)";
        else
            return "Unknow state";
    }
}

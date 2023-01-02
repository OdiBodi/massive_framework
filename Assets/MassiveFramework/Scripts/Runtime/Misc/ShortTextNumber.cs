using System.Globalization;

namespace MassiveCore.Framework
{
    public class ShortTextNumber
    {
        private readonly int _number;

        public ShortTextNumber(int number)
        {
            _number = number;
        }

        public string Text()
        {
            var result = string.Empty;
            if (_number >= 1000000)
            {
                var number = _number / 1000000f;
                result = number.ToString("#.#m", NumberFormatInfo.InvariantInfo);
            }
            else if (_number >= 1000)
            {
                var number = _number / 1000f;
                result = number.ToString("#.#k", NumberFormatInfo.InvariantInfo);
            }
            else
            {
                result = _number.ToString();
            }
            return result;
        }
    }
}

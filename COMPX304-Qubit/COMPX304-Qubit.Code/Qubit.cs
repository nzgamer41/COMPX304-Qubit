using System;
using System.Collections.Generic;
using System.Text;

namespace COMPX304_Qubit
{
    public class Qubit
    {
        private int _value;
        private int _polarization;

        public Qubit(int value, int polarization)
        {
            if ((polarization == 0 || polarization == 1) && (value == 0 || value == 1))
            {
                _value = value;
                _polarization = polarization;
            }
            else throw new QubitPolarizationInvalidException(polarization);
        }

        /// <summary>
        /// Sets the value and polarization of the Qubit.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="polarization"></param>
        public void set(int value, int polarization)
        {
            if ((polarization == 0 || polarization == 1) && (value == 0 || value == 1))
            {
                _value = value;
                _polarization = polarization;
            }
            else throw new QubitPolarizationInvalidException(polarization);
        }

        public int measure(int polarization)
        {
            if ((polarization == 0 || polarization == 1))
            {
                if(polarization == _polarization)
                {
                    return _value;
                }
                else
                {
                    Random random = new Random();
                    int num = random.Next(100);
                    if (num > 50)
                    {
                        set(1, polarization);
                    }
                    else
                    {
                        set(0, polarization);
                    }
                    return _value;
                }
            }
            else throw new QubitPolarizationInvalidException(polarization);
        }
    }

    public class QubitPolarizationInvalidException : Exception
    {
        public QubitPolarizationInvalidException(int invalidPol) : base(String.Format("{0} is an invalid option. Valid options are 0 or 1.", invalidPol))
        {

        }

        public QubitPolarizationInvalidException(string message)
            : base(message)
        {
        }

        public QubitPolarizationInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

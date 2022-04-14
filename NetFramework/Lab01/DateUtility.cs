using System;

namespace Lab01
{
    public class DateUtility
    {
        public bool IsPayday()
        {
            if (DateTime.Today.Day == 5)
            {
                return true;
            }

            return false;
        }
    }
}
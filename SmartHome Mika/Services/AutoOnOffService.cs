namespace SmartHome_Mika.Services
{
    public class AutoOnOffService
    {
        public bool AutoOnOff()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan start = new TimeSpan(19, 0, 0);
            TimeSpan end = new TimeSpan(23, 0, 0);

            return now >= start && now <= end;
        }

        public TimeSpan TimeUntill()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan start = new TimeSpan(19, 0, 0);
            TimeSpan end = new TimeSpan(23, 0, 0);
            TimeSpan timeuntill = new TimeSpan(0, 0, 0);

            if (now < start)
            {
                timeuntill = start - now;
            }
            else if (now >= start && now <= end) 
            {
                timeuntill = new TimeSpan(0, 0, 0);
            }
            else if (now > end)
            {
                timeuntill = end - now + start;
            }
            return timeuntill;
        }
    }

}

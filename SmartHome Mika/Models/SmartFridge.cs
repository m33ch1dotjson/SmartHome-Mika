namespace SmartHome_Mika.Models
{
    public class SmartFridge : SmartDevice
    {
        public int Temperature { get; set; }
        public bool FreezeMode { get; set; }
        public SmartFridge(int id, Room room, string name, int temprature) : base(id, room, name)
        {
        }

        public void FreezeModeOnOff()
        {
            FreezeMode = !FreezeMode;
        }

    }
}

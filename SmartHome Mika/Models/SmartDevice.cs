namespace SmartHome_Mika.Models
{
    public class SmartDevice
    {
        //Properties 
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public Room Room { get; set; }

        //Constructor

        public SmartDevice(int id, Room room, string name)
        {
            Id = id;
            Room = room;
            Name = name;
        }

        //Method

        public void ChangeIsOn()
        {
            IsOn = !IsOn;
        }
    }
}

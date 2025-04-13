namespace SmartHome_Mika.Models
{
    public class HomeViewModel
    {
        public SmartLamp Lamp { get; set; }
        public SmartFridge Fridge { get; set; }

        public string Zoekterm { get; set; }
        public List<SmartDevice> ZoekResultaten { get; set; }
    }

}

namespace PostalSystem.Models
{
    public class PrintingHouse
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public PrintingHouse() { }

        public PrintingHouse(int key, string name, string address)
        {
            Key = key;
            Name = name;
            Address = address;
        }
    }
}
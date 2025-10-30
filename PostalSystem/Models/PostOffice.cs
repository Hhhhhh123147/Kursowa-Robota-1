namespace PostalSystem.Models
{
    public class PostOffice
    {
        public int Key { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public int NewspaperKey { get; set; }
        public int PrintingHouseKey { get; set; }

        public PostOffice() { }

        public PostOffice(int key, string number, string address, int newspaperKey, int printingHouseKey)
        {
            Key = key;
            Number = number;
            Address = address;
            NewspaperKey = newspaperKey;
            PrintingHouseKey = printingHouseKey;
        }
    }
}
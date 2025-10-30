namespace PostalSystem.Models
{
    public class Newspaper
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }
        public string Editor { get; set; }
        public int Circulation { get; set; }
        public double Price { get; set; }

        public Newspaper() { }

        public Newspaper(int key, string name, string index, string editor, int circulation, double price)
        {
            Key = key;
            Name = name;
            Index = index;
            Editor = editor;
            Circulation = circulation;
            Price = price;
        }
    }
}
namespace Autopark.DAL.Entities
{
    public class Component
    {
        public int ComponentId { get; set; }
        public string Name { get; set; }
        public Component()
        {

        }

        public Component(string name)
        {
            Name = name;
        }
    }
}

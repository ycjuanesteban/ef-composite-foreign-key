namespace ForeignKeyComposite.api.Domain
{
    public class Entity01
    {
        public int Key01 { get; set; }
        public int Key02 { get; set; }

        public ICollection<Entity02> Entities02 { get; set; }
    }
}

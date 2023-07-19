namespace ForeignKeyComposite.api.Domain
{
    public class Entity02
    {
        public int Key01 { get; set; }
        public int Foreign01 { get; set; }
        public int Foreign02 { get; set; }
        public string? OtherProperty { get; set; }
        public Entity01 Entity01 { get; set; }
    }
}

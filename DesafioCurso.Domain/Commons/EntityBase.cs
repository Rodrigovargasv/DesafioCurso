namespace DesafioCurso.Domain.Commons
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Identifier { get; set; }
    }
}
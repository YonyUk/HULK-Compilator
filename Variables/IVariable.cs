namespace Compilator
{
    public interface IVariable<T>
    {
        string Name { get; }
        T Value { get; set; }
        bool IsAssignated { get; }
    }
}
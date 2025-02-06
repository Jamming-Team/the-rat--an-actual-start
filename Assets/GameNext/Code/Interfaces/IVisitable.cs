namespace MeatAndSoap
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }

    public interface IVisitableMC<T> : IVisitable
    {
        void FillData(T data);
    }
}
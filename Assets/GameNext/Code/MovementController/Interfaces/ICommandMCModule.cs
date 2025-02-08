namespace MeatAndSoap
{
    public interface ICommandMCModule : ICommand
    {
        void Init(IPC_States state);
    }
}
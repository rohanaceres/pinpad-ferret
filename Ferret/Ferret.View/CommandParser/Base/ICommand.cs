namespace Ferret.View.CommandParser.Base
{
    internal interface ICommand
    {
        // TODO: Ação a ser feita, ligada a um comando específico.
        void Execute(string[] args);
    }
}

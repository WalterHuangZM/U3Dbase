
public interface ICommand : IEventDispatcher
{
	void Execute(object commandParams=null);
    void Cancel();
}


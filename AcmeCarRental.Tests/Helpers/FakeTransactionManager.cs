namespace AcmeCarRental;

public enum TransactionState
{
    None,
    Started,
    Commit,
    Rollback
}

/// <summary>
/// Fake implementation of transaction scope management
/// </summary>
public class FakeTransactionManager : ITransactionManager
{
    private Scope? _lastScope;

    public ITransactionScope CreateScope()
    {
        return _lastScope = new Scope();
    }

    public TransactionState LastScopeState => _lastScope?.State ?? TransactionState.None;

    internal sealed class Scope : ITransactionScope
    {
        private TransactionState _state = TransactionState.Started;

        internal TransactionState State => _state;

        public bool Complete()
        {
            _state = TransactionState.Commit;
            return true;
        }

        public void Dispose()
        {
            if (_state == TransactionState.Started)
                _state = TransactionState.Rollback;
        }
    }
}

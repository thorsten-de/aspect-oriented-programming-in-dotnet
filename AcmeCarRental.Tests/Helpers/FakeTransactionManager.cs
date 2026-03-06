namespace AcmeCarRental;

internal enum TransactionState
{
    None,
    Started,
    Commit,
    Rollback
}

/// <summary>
/// Fake implementation of transaction scope management that supports nested and context-aware transaction scopes.
/// Nested scopes propagate rollback upward: if an inner scope is disposed without being completed, all enclosing
/// scopes are marked as rolled back. The ambient (current) scope can be inspected via <see cref="CurrentScope"/>.
/// </summary>
internal class FakeTransactionManager : ITransactionManager
{
    private readonly Stack<Scope> _scopeStack = new();
    private Scope? _lastDisposedScope;

    public ITransactionScope CreateScope()
    {
        var scope = new Scope(this);
        _scopeStack.Push(scope);
        return scope;
    }

    /// <summary>
    /// Gets the innermost currently active transaction scope (ambient transaction), or <c>null</c> if no scope is active.
    /// </summary>
    public ITransactionScope? CurrentScope => _scopeStack.Count > 0 ? _scopeStack.Peek() : null;

    /// <summary>
    /// Gets the number of currently active (nested) transaction scopes.
    /// </summary>
    public int ActiveScopeCount => _scopeStack.Count;

    /// <summary>
    /// Gets the state of the innermost active scope, or the state of the last disposed scope when no scope is active.
    /// </summary>
    public TransactionState LastScopeState
    {
        get
        {
            if (_scopeStack.Count > 0)
            {
                return _scopeStack.Peek().State;
            }
            return _lastDisposedScope?.State ?? TransactionState.None;
        }
    }

    internal void OnScopeDisposed(Scope scope, bool rolledBack)
    {
        if (rolledBack)
        {
            // Propagate rollback to all enclosing scopes so the root transaction reflects the failure.
            foreach (var s in _scopeStack)
            {
                s.ForceRollback();
            }
        }

        _scopeStack.TryPop(out _);
        _lastDisposedScope = scope;
    }

    internal sealed class Scope : ITransactionScope
    {
        private readonly FakeTransactionManager _manager;
        private TransactionState _state = TransactionState.Started;
        private bool _disposed;

        internal Scope(FakeTransactionManager manager)
        {
            _manager = manager;
        }

        internal TransactionState State => _state;

        internal void ForceRollback() => _state = TransactionState.Rollback;

        public bool Complete()
        {
            if (_state == TransactionState.Started)
            {
                _state = TransactionState.Commit;
            }
            return true;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            bool rolledBack = _state == TransactionState.Started;
            if (rolledBack)
            {
                _state = TransactionState.Rollback;
            }
            _manager.OnScopeDisposed(this, rolledBack);
        }
    }
}

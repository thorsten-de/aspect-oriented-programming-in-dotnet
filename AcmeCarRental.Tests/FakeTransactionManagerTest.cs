namespace AcmeCarRental;

public class FakeTransactionManagerTest
{
    private readonly FakeTransactionManager _transactions = new();

    // ----- Single scope -----

    [Fact]
    public void CreateScope_ShouldReturnStartedScope()
    {
        using var scope = _transactions.CreateScope();

        Assert.Equal(TransactionState.Started, _transactions.LastScopeState);
    }

    [Fact]
    public void SingleScope_ShouldCommit_WhenCompleted()
    {
        using (var scope = _transactions.CreateScope())
        {
            scope.Complete();
        }

        Assert.Equal(TransactionState.Commit, _transactions.LastScopeState);
    }

    [Fact]
    public void SingleScope_ShouldRollback_WhenNotCompleted()
    {
        using (_transactions.CreateScope())
        {
            // disposed without calling Complete
        }

        Assert.Equal(TransactionState.Rollback, _transactions.LastScopeState);
    }

    [Fact]
    public void LastScopeState_ShouldBeNone_WhenNoScopeEverCreated()
    {
        Assert.Equal(TransactionState.None, _transactions.LastScopeState);
    }

    // ----- ActiveScopeCount -----

    [Fact]
    public void ActiveScopeCount_ShouldTrackNestingDepth()
    {
        Assert.Equal(0, _transactions.ActiveScopeCount);

        using (var outer = _transactions.CreateScope())
        {
            Assert.Equal(1, _transactions.ActiveScopeCount);

            using (var inner = _transactions.CreateScope())
            {
                Assert.Equal(2, _transactions.ActiveScopeCount);
            }

            Assert.Equal(1, _transactions.ActiveScopeCount);
        }

        Assert.Equal(0, _transactions.ActiveScopeCount);
    }

    // ----- CurrentScope (context-aware ambient transaction) -----

    [Fact]
    public void CurrentScope_ShouldBeNull_WhenNoActiveScopeExists()
    {
        Assert.Null(_transactions.CurrentScope);
    }

    [Fact]
    public void CurrentScope_ShouldReturnInnermostActiveScope()
    {
        using (var outer = _transactions.CreateScope())
        {
            Assert.Same(outer, _transactions.CurrentScope);

            using (var inner = _transactions.CreateScope())
            {
                Assert.Same(inner, _transactions.CurrentScope);
            }

            Assert.Same(outer, _transactions.CurrentScope);
        }

        Assert.Null(_transactions.CurrentScope);
    }

    // ----- Nested scope commit / rollback propagation -----

    [Fact]
    public void NestedScopes_ShouldCommit_WhenAllScopesCompleted()
    {
        using (var outer = _transactions.CreateScope())
        {
            using (var inner = _transactions.CreateScope())
            {
                inner.Complete();
            }

            outer.Complete();
        }

        Assert.Equal(TransactionState.Commit, _transactions.LastScopeState);
    }

    [Fact]
    public void NestedScopes_ShouldRollback_WhenInnerScopeNotCompleted()
    {
        using (var outer = _transactions.CreateScope())
        {
            using (_transactions.CreateScope())
            {
                // inner scope disposed without Complete — should roll back outer too
            }

            outer.Complete(); // this should have no effect: outer is already rolled back
        }

        Assert.Equal(TransactionState.Rollback, _transactions.LastScopeState);
    }

    [Fact]
    public void NestedScopes_ShouldRollback_WhenOuterScopeNotCompleted()
    {
        using (var outer = _transactions.CreateScope())
        {
            using (var inner = _transactions.CreateScope())
            {
                inner.Complete();
            }

            // outer disposed without Complete
        }

        Assert.Equal(TransactionState.Rollback, _transactions.LastScopeState);
    }

    [Fact]
    public void NestedScopes_ShouldSupportThreeLevels_WithRollbackPropagation()
    {
        using (var outer = _transactions.CreateScope())
        {
            using (var middle = _transactions.CreateScope())
            {
                using (_transactions.CreateScope())
                {
                    // innermost disposed without Complete
                }

                middle.Complete(); // no-op: already rolled back by innermost
            }

            outer.Complete(); // no-op: already rolled back
        }

        Assert.Equal(TransactionState.Rollback, _transactions.LastScopeState);
    }

    // ----- Double-dispose guard -----

    [Fact]
    public void Scope_ShouldNotChangeState_WhenDisposedTwice()
    {
        var scope = _transactions.CreateScope();
        scope.Complete();
        scope.Dispose();
        scope.Dispose(); // second dispose should be a no-op

        Assert.Equal(TransactionState.Commit, _transactions.LastScopeState);
        Assert.Equal(0, _transactions.ActiveScopeCount);
    }
}

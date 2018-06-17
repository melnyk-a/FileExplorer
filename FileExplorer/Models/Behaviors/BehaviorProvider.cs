namespace FileExplorer.Models.Behaviors
{
    internal class BehaviorProvider
    {
        public BehaviorProvider(
            InitializeRootBehavior initializeRootBehavior,
            UpdateRepositoryBehavior updateRepositoryBehavior,
            ExpandRepositoryBehavior expandRepositoryBehavior
            )
        {
            UpdateRepositoryBehavior = updateRepositoryBehavior;
            InitializeRootBehavior = initializeRootBehavior;
            ExpandRepositoryBehavior = expandRepositoryBehavior;
        }

        public ExpandRepositoryBehavior ExpandRepositoryBehavior { get; }

        public InitializeRootBehavior InitializeRootBehavior { get; }

        public UpdateRepositoryBehavior UpdateRepositoryBehavior { get; }
    }
}
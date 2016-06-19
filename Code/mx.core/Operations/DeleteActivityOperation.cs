namespace mx.core.Operations
{
    using System;
    using mx.core.Patterns;
    using Persistance;
    using Specifications;

    public class DeleteActivityOperation : IOperation
    {
        private Activity activityToDelete;
        private IProjectPersistor persistor;

        private ActivityExistsSpecification activityExistsSpec;

        public DeleteActivityOperation(Activity activityToDelete, IProjectPersistor persistor)
        {
            this.activityToDelete = activityToDelete;
            this.persistor = persistor;

            this.activityExistsSpec = new ActivityExistsSpecification(persistor);
        }

        public void Execute()
        {
            ISpecification<Activity> allowedTDeleteToSpecification = activityExistsSpec;

            if (allowedTDeleteToSpecification.IsSatisfiedBy(this.activityToDelete))
            {
                try
                {
                    persistor.DeleteActivity(this.activityToDelete);
                }
                catch (Exception)
                {
                    // If the persistance fails, don't perform the delete
                    throw;
                }

                // Now remove it logically
                Group parent = this.activityToDelete.Parent;
                if (parent != null)
                {
                    parent.RemoveItem(this.activityToDelete);
                }
            }
        }
    }
}

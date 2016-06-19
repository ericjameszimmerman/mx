
namespace mx.core.Operations
{
    using System;
    using mx.core.Patterns;
    using Persistance;
    using Specifications;

    public class AddActivityOperation : IOperation
    {
        private Activity activityToAdd;
        private Group parent;
        private IProjectPersistor persistor;

        private ValidActivityNameSpecification validNameSpec;
        private ActivityDoesNotExistSpecification activityDoesNotExistSpec;

        public AddActivityOperation(Activity activityToAdd, Group parent, IProjectPersistor persistor)
        {
            this.activityToAdd = activityToAdd;
            this.parent = parent;
            this.persistor = persistor;

            this.validNameSpec = new ValidActivityNameSpecification();
            this.activityDoesNotExistSpec = new ActivityDoesNotExistSpecification(activityToAdd.Name, persistor);
        }

        public void Execute()
        {
            ISpecification<Activity> allowedToAddSpecification = validNameSpec;
            ISpecification<Group> allowedToAddToSpecification = activityDoesNotExistSpec;

            if (allowedToAddSpecification.IsSatisfiedBy(this.activityToAdd) &&
                allowedToAddToSpecification.IsSatisfiedBy(this.parent))
            {
                activityToAdd.Parent = parent;
                parent.AddItem(activityToAdd);

                try
                {
                    persistor.CreateActivity(activityToAdd);
                }
                catch (Exception)
                {
                    // If the persistance fails, undo the logical addition
                    parent.RemoveItem(activityToAdd);
                    activityToAdd.Parent = null;
                    throw;
                }
            }
        }
    }
}

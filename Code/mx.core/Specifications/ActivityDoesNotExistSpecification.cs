
namespace mx.core.Specifications
{
    using mx.core.Patterns;
    using Persistance;

    public class ActivityDoesNotExistSpecification : CompositeSpecification<Group>
    {
        private string activityName;
        private IProjectPersistor persistor;

        public ActivityDoesNotExistSpecification(string activityName, IProjectPersistor persistor)
        {
            this.activityName = activityName;
            this.persistor = persistor;
        }

        public override bool IsSatisfiedBy(Group entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (entity.FindItemByName(activityName) != null)
            {
                return false;
            }

            string path = ProjectPath.Combine(entity.Path, activityName);
            if (this.persistor.ActivityExists(path))
            {
                return false;
            }
            
            return true;
        }
    }
}

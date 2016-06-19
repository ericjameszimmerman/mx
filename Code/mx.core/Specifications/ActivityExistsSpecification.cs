namespace mx.core.Specifications
{
    using mx.core.Patterns;
    using Persistance;

    public class ActivityExistsSpecification : CompositeSpecification<Activity>
    {
        private IProjectPersistor persistor;

        public ActivityExistsSpecification(IProjectPersistor persistor)
        {
            this.persistor = persistor;
        }

        public override bool IsSatisfiedBy(Activity entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (!this.persistor.ActivityExists(entity.Path))
            {
                return false;
            }

            return true;
        }
    }
}

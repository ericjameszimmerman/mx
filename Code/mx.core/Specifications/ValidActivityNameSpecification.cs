namespace mx.core.Specifications
{
    using System;
    using System.IO;
    using mx.core.Patterns;

    public class ValidActivityNameSpecification : CompositeSpecification<Activity>
    {
        public ValidActivityNameSpecification()
        {

        }

        public override bool IsSatisfiedBy(Activity entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                return false;
            }

            char[] invalidChars = Path.GetInvalidFileNameChars();
            if (entity.Name.IndexOfAny(invalidChars) >= 0)
            {
                return false;
            }

            if (entity.Name.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}

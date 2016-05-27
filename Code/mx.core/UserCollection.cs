using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class UserCollection
    {
        private Dictionary<string, User> userLookup;

        public UserCollection()
        {
            this.userLookup = new Dictionary<string, User>();
        }

        public User LookupOrAdd(string identifier)
        {
            User user;
            if (userLookup.TryGetValue(identifier, out user))
            {
                return user;
            }

            // Not found, so parse and add.
            // format should be "Name <email>"
            if (string.IsNullOrEmpty(identifier))
                throw new ArgumentNullException();

            int emailStartIndex;
            int emailEndIndex;
            emailStartIndex = identifier.IndexOf('<');
            emailEndIndex = identifier.LastIndexOf('>');
            string email = string.Empty;
            string name = string.Empty;

            if (emailStartIndex > 0 && emailEndIndex > 0)
            {
                email = identifier.Substring(emailStartIndex + 1, (emailEndIndex - emailStartIndex - 1));
                name = identifier.Substring(0, emailStartIndex);
            }
            else
            {
                name = identifier;
            }

            user = new User(name, email);
            this.userLookup.Add(identifier, user);

            return user;
        }
    }
}

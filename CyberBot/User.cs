
using System;

namespace CyberBot.Models
{
    /// <summary>
    /// Represents the user interacting with CyberBot.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a new User with the specified name.
        /// </summary>
        /// <param name="name">The user's name.</param>
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "User name cannot be empty.",
                    nameof(name));
            }

            Name = name.Trim();
        }
    }
}

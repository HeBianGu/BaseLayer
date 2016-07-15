using System;

namespace OPT.PCOCCenter.Manager {
    class User {
        string name;
        string password;
        string role;
        string memo;

        public User(string Name)
        {
            this.name = Name;
            this.password = String.Empty;
            this.Role = String.Empty;
            this.Memo = String.Empty;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
    }
}
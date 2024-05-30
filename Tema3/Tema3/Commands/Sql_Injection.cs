using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tema3.Commands
{
    public class Sql_Injection
    {
        public Sql_Injection() { }

        public bool hasForbiddenWords(string word)
        { 
            var forbiddenKeywords = new[] { "@", "from", "select", "*", "where" , "drop" , "join" , "update" , "delete" , "alter" , "database" , "table"};
            word.ToLower();
            if(forbiddenKeywords.Any(keyword => word.Contains(keyword)))
            {
                return true;
            }
            return false;
        }

        public bool CanExecuteLogin(string Username, string Password)
        {
            if(Username == null || Password == null)
            { return false; }

            if(hasForbiddenWords(Username) || hasForbiddenWords(Password)) { return false; }

            return true;
        }
        
    }
}

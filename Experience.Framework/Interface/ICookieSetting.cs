using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public interface ICookieSetting
    {
        string CookieName { get; }
        bool HttpOnly { get; }
        double Expired { get; }
    }
}

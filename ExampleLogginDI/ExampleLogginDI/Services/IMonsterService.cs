using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExampleLogginDI.Models;

namespace ExampleLogginDI.Services
{
    public interface IMonsterService
    {

        String Log(Object msg);
        void Error(Object msg, Exception e);

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SDK {
    public interface IPlugin {

        string title { get; }
        void doSomething();
    }
}

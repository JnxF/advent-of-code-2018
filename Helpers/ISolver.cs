using System;

namespace Helpers
{
    public interface ISolver<T>
    {
        T Solve();
    }
}

namespace Matheparser.Util
{
    using System.Collections.Generic;
    using Matheparser.Values;

    public interface ISet : IEnumerable<IValue>
    {
        int Count
        {
            get;
        }

        ISet Union(ISet other);
        ISet Cut(ISet other);
        ISet Except(ISet other);
    }
}

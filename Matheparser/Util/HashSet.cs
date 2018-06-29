using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matheparser.Values;

namespace Matheparser.Util
{
    public class HashSet : ISet
    {
        private HashSet<IValue> values;

        public int Count
        {
            get
            {
                return this.values.Count;
            }
        }

        public ISet Cut(ISet other)
        {
            var res = new HashSet<IValue>(this.values);

            return this;
        }

        public ISet Except(ISet other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IValue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public ISet Union(ISet other)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

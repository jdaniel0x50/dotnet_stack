using restauranter.Models;
using System.Collections.Generic;

namespace restauranter.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}
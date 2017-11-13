using dapper_trails.Models;
using System.Collections.Generic;

namespace dapper_trails.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}
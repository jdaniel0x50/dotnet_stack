using message_wall.Models;
using System.Collections.Generic;

namespace message_wall.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}
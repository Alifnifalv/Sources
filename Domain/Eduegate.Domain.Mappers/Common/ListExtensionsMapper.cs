using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers.Common
{
    public class ListExtensionsMapper : DTOEntityDynamicMapper
    {
        public static ListExtensionsMapper Mapper(CallContext context)
        {
            var mapper = new ListExtensionsMapper();
            mapper._context = context;
            return mapper;
        }

        public List<T> ShuffleIDList<T>(List<T> list)
        {
            var random = new Random();
            var newShuffledList = new List<T>();
            var listcCount = list.Count;
            for (int i = 0; i < listcCount; i++)
            {
                var randomElementInList = random.Next(0, list.Count);
                newShuffledList.Add(list[randomElementInList]);
                list.Remove(list[randomElementInList]);
            }

            return newShuffledList;
        }

        public List<T> ShuffleIDListWithCount<T>(List<T> list, int listCount)
        {
            var random = new Random();
            var newShuffledList = new List<T>();
            for (int i = 0; i < listCount; i++)
            {
                var randomElementInList = random.Next(0, list.Count);
                newShuffledList.Add(list[randomElementInList]);
                list.Remove(list[randomElementInList]);
            }

            return newShuffledList;
        }

    }
}
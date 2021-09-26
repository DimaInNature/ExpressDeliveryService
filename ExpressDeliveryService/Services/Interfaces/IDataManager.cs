using System;
using System.Collections.Generic;

namespace ExpressDeliveryService.Services.Interfaces
{
    interface IDataManager
    {
        void Create<T>(string table, T record);

        List<T> GetAll<T>(string table);

        T Find<T>(string table, Guid id);

        void Edit<T>(string table, Guid id, T record);

        void Delete<T>(string table, Guid id);
    }
}

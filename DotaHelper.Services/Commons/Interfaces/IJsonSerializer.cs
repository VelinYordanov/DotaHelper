using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Services.Commons.Interfaces
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T model);

        T Deserialize<T>(string json);
    }
}

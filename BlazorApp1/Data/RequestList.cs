using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace BlazorApp1.Data
{
    public class RequestList : List<RequestEx>
    {
        public RequestList() { } 

        public RequestList(IEnumerable<RequestEx> items)
        {
            this.AddRange(items);
        }


        public RequestList Filter(string search)
        {
            if (string.IsNullOrEmpty(search))
                return this;

            var items = new RequestList();

            foreach( var request in this)
            {
                var json = JsonSerializer.Serialize(request);

                if (json.Contains(search, StringComparison.OrdinalIgnoreCase))
                    items.Add(request);
            }

            return items;
        }
    }
}

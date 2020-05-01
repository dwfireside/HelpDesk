using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorApp1.Shared
{
    public enum TicketViewMode
    {
        Open, 
        Closed,
        All,
        Archived
    }


    public class AppState
    {
        public event Action OnSelectedRequestChanged;
        public event Action OnViewModeChanged;
       

        public RequestList Requests = new RequestList();
        public IEnumerable<RequestEx> ViewRequests;
        

        private RequestEx _selectedRequest;
        private TicketViewMode _viewMode;
        private string _searchText;


        public string SearchText
        {
            get { return _searchText; }
            set { 
                _searchText = value;
                GetRequests(value);
            }
        }

        public RequestEx SelectedRequest { 
            get { return _selectedRequest; } 
            set { 
                 _selectedRequest = value;
                OnSelectedRequestChanged?.Invoke();
            } 
        }

        public TicketViewMode ViewMode
        {
            get { return _viewMode; }
            set { 
                _viewMode = value;
                OnViewModeChanged?.Invoke();
            }
        }


        public Task<RequestList> GetRequests(string filter)
        {
            var service = new RequestService();
            return Task.FromResult(service.GetRequests().Filter(filter));
        }

        public RequestList GetRequestsForView()
        {
            switch (ViewMode)
            {
                case TicketViewMode.Open:
                    return new RequestList(this.Requests.Where(r => r.IsResolved == false));
                case TicketViewMode.Closed:
                    return new RequestList(this.Requests.Where(r => r.IsResolved == true));
                default:
                    return this.Requests;
            }
        }

        public bool IsInitialised()
        {
            return OnViewModeChanged != null;
        }
    }
}

﻿using BlazorApp1.Data;
using System;
using System.Collections.Generic;


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
        public event Action OnSearchTextChanged;
       
        public RequestList Requests = new RequestList();
        
        private RequestEx _selectedRequest;
        private TicketViewMode _viewMode;
        private string _searchText;


        public string SearchText
        {
            get { return _searchText; }
            set { 
                _searchText = value;
                OnSearchTextChanged?.Invoke();
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
    }
}

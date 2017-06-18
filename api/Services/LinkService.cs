using System;
using System.Collections.Generic;
using SchedulePath.Repository;
using SchedulePath.Models;

namespace SchedulePath.Services
{
    public class LinkService : ILinkService
    {
        private ICepRepository _repository;
        public LinkService(ICepRepository repository)
        {
            _repository = repository;
        }
        public void AddLink(Link request)
        {
            _repository.AddLink(request);
        }

        public void DeleteLink(int id)
        {
            _repository.DeleteLink(id);
        }

        public LinkWithActivity GetLink()
        {
            return _repository.GetLink();
        }

        public void UpdateLink(Link request)
        {
            _repository.UpdateLink(request);
        }
    }
}

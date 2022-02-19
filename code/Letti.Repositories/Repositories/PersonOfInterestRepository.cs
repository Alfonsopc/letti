using Letti.Data;
using Letti.Model;
using Letti.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zorbek.Essentials.Core.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Letti.Repositories.Helpers;
using System.Linq.Expressions;
using Letti.Repositories.Entities;

namespace Letti.Repositories.Repositories
{
    public class PersonOfInterestRepository : GenericRepository<Model.PersonOfInterest, LettiContext>, IPersonOfInterestRepository
    {
        public PersonOfInterestRepository(LettiContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<PersonOfInterest>> GetAll()
        {
            List<PersonOfInterest> poi = await base.GetList().ToListAsync();
            return poi;
        }

        public async Task<PersonOfInterest> GetById(int id)
        {
            PersonOfInterest poi = await base.Read(id);
            return poi;
        }

        public Task<PersonOfInterest> GetByNumber(string number)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PersonOfInterest>> SearchInName(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return new List<Model.PersonOfInterest>();
            }
            ICollection<Expression<Func<Model.PersonOfInterest, bool>>> queryFilter = new List<Expression<Func<Model.PersonOfInterest, bool>>>();
            FilterParser filterParser = new FilterParser();
            SearchParameter searchParameter = filterParser.Parse(filter);
            queryFilter.Add(p => p.LastName.StartsWith(searchParameter.LastName));
            if (!string.IsNullOrEmpty(searchParameter.MotherLastName))
            {
                queryFilter.Add(p => p.SecondLastName.StartsWith(searchParameter.MotherLastName));
            }


           // ICollection<Expression<Func<Model.PersonOfInterest, object>>> includes = new List<Expression<Func<Model.PersonOfInterest, object>>>();
           // includes.Add(p => p.LaborProfile);
           // includes.Add(p => p.LaborProfile.Organization);
            Func<IQueryable<Model.PersonOfInterest>, IOrderedQueryable<Model.PersonOfInterest>> orderBy = p => p.OrderBy(c => c.FirstName);
            var response = await base.GetList(queryFilter, orderBy, null).ToListAsync();
            return response;
        }

        public new async Task<PersonOfInterest> Create(PersonOfInterest poi)
        {
            await base.Create(poi);
            return poi;
        }

        public new async Task<PersonOfInterest> Update(PersonOfInterest poi)
        {
            await base.Update(poi);
            return poi;
        }

        public Task<ICollection<PersonOfInterest>> SearchInCURP(string filter)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PersonOfInterest>> SearchInRFC(string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Property> AddProperty(Property property)
        {
            property.ScrappedTimestamp = DateTime.UtcNow;
            _dbContext.Properties.Add(property);
            await _dbContext.SaveChangesAsync();
            return property;
        }

        public async Task<ICollection<Property>> GetProperties(int poiId)
        {
            ICollection<Property> properties = await _dbContext.Properties.Where(c => c.OwnerId == poiId).ToListAsync();
            return properties;
        }
    }
}

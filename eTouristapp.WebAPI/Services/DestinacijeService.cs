﻿using AutoMapper;
using eTouristapp.Models.Request;
using eTouristapp.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTouristapp.WebAPI.Services
{
    public class DestinacijeService : BaseCRUDService<Models.Destinacija, DestinacijaSearchRequest, Database.Destinacija,DestinacijaInsertRequest,DestinacijaInsertRequest>
    {
        public DestinacijeService(eTourist1Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Models.Destinacija> Get(DestinacijaSearchRequest search)
        {
            var query = _touristcontext.Set<Database.Destinacija>().AsQueryable();
            if(search.Naziv!=null)
            {
                query = query.Where(x => x.Naziv.Contains(search.Naziv));
            }
            var list = query.ToList();
            return _mapper.Map<List<Models.Destinacija>>(list);
        }
    }
}
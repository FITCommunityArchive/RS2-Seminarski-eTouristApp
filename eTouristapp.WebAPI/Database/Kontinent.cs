﻿using System;
using System.Collections.Generic;

namespace eTouristapp.WebAPI.Database
{
    public partial class Kontinent
    {
        public Kontinent()
        {
            Drzava = new HashSet<Drzava>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Drzava> Drzava { get; set; }
    }
}

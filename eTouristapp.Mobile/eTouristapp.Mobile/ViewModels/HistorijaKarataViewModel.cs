﻿using eProdaja.Mobile;
using eTouristapp.Mobile.Models;
using eTouristapp.Models;
using eTouristapp.Models.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTouristapp.Mobile.ViewModels
{
    public class HistorijaKarataViewModel:BaseViewModel
    {

        private readonly APIService _karteservice = new APIService("Karte");
        private readonly APIService _hoteliservice = new APIService("Hoteli");
        private readonly APIService _destinacijeservice = new APIService("Destinacije");
        private readonly APIService _terminiservice = new APIService("Termini");

        private readonly APIService _korisniciservice = new APIService("Korisnici");

        public HistorijaKarataViewModel()
        {

        }

        DateTime _selectedDatumOd=DateTime.Now;
        public DateTime SelectedDatumOd
        {
            get { return _selectedDatumOd; }
            set
            {
                SetProperty(ref _selectedDatumOd, value);

                //if (value != null)
                //{
                //    Initial.Execute(null);
                //}
            }

        }
        DateTime _selectedDatumDo=DateTime.Now;
        public DateTime SelectedDatumDo
        {
            get { return _selectedDatumDo; }
            set
            {
                SetProperty(ref _selectedDatumDo, value);

                //if (value != null)
                //{
                //    Initial.Execute(null);
                //}
            }

        }

        public async Task LoadHistorijaKarte()
        {
            KorisniciSearchRequest request = new KorisniciSearchRequest()
            {
                KorisnickoIme = APIService.Username
            };
            var korisnik = await _korisniciservice.Get<List<Korisnik>>(request);

            int idkorisnik = korisnik.FirstOrDefault().Id;

            KartaSearchRequest search = new KartaSearchRequest()
            {
                KorisnikID = idkorisnik

            };
            var karte = await _karteservice.Get<IEnumerable<Karta>>(search);

            HistorijaKarteList.Clear();

            foreach (var x in karte)
            {
                var termin = await _terminiservice.GetById<Termin>(x.TerminId);
                if (x.Ponistena == false && termin.AktivanTermin == false)
                {



                    var hotel = await _hoteliservice.GetById<Hotel>(termin.HotelId);
                    var destinacija = await _destinacijeservice.GetById<Destinacija>(termin.DestinacijaId);
                    MojeKarte k = new MojeKarte()
                    {
                        KartaId = x.Id,
                        DatumPolaska = termin.DatumPolaska,
                        DatumDolaska = termin.DatumDolaska,
                        BrojZvjezdica = hotel.BrojZvjezdica,
                        NazivHotela = hotel.Naziv,
                        NazivPutovanja = destinacija.Naziv,
                        Cijena = termin.Cijena

                    };
                    HistorijaKarteList.Add(k);
                }

            }
        }
        public ObservableCollection<MojeKarte> HistorijaKarteList { get; set; } = new ObservableCollection<MojeKarte>();

        public async Task Pretraga()
        {
            KorisniciSearchRequest request = new KorisniciSearchRequest()
            {
                KorisnickoIme = APIService.Username
            };
            var korisnik = await _korisniciservice.Get<List<Korisnik>>(request);

            int idkorisnik = korisnik.FirstOrDefault().Id;

            KartaSearchRequest search = new KartaSearchRequest()
            {
                KorisnikID = idkorisnik

            };
            var karte = await _karteservice.Get<IEnumerable<Karta>>(search);

            HistorijaKarteList.Clear();

            foreach (var x in karte)
            {
                var termin = await _terminiservice.GetById<Termin>(x.TerminId);
                if (x.Ponistena == false && termin.AktivanTermin == false && termin.DatumPolaska.Date>=SelectedDatumOd.Date && termin.DatumDolaska.Date<=SelectedDatumDo.Date)
                {



                    var hotel = await _hoteliservice.GetById<Hotel>(termin.HotelId);
                    var destinacija = await _destinacijeservice.GetById<Destinacija>(termin.DestinacijaId);
                    MojeKarte k = new MojeKarte()
                    {
                        KartaId = x.Id,
                        DatumPolaska = termin.DatumPolaska,
                        DatumDolaska = termin.DatumDolaska,
                        BrojZvjezdica = hotel.BrojZvjezdica,
                        NazivHotela = hotel.Naziv,
                        NazivPutovanja = destinacija.Naziv,
                        Cijena = termin.Cijena

                    };
                    HistorijaKarteList.Add(k);
                }

            }
        }



    }
}

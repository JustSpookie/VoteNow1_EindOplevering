#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_View.Data;
using ASP_View.Models;
using BusinessLogic;
using DataAccessLayer;
using System.Data;

namespace ASP_View.Controllers
{
    public class VerkiezingViewModelsController : Controller
    {
        VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new VerkiezingDAL());
        KandidaatContainer kandidaatContainer = new KandidaatContainer(new KandidaatDAL());

        public VerkiezingViewModelsController()
        {
        }

        // GET: VerkiezingViewModels
        public IActionResult Index()
        {
            if ((int)HttpContext.Session.GetInt32("UserID") == 0) { return RedirectToAction("Index", "Home"); }

            DataTable dataTable1 = new DataTable();
            List<Verkiezing> verkiezingList = verkiezingContainer.GetAllVerkiezingen((int)HttpContext.Session.GetInt32("UserID"));

            dataTable1.Columns.Add("VerkiezingID");
            dataTable1.Columns.Add("VerkiezingNaam");

            foreach (Verkiezing verkiezing in verkiezingList)
            {
                dataTable1.Rows.Add(verkiezing.VerkiezingID,verkiezing.VerkiezingNaam);
            }

            return View(dataTable1);
        }

       

        
        // GET: VerkiezingViewModels/AddOrEdit/5
        public IActionResult AddOrEdit(int id)
        {
            if ((int)HttpContext.Session.GetInt32("UserID") == 0) { return RedirectToAction("Index", "Home"); }

            List<Kandidaat> lijstKandidaten = kandidaatContainer.GetKandidaten((int)HttpContext.Session.GetInt32("UserID"));

            VerkiezingViewModel verkiezingViewModel = new();
            if (id > 0)
            {
                if (verkiezingContainer.GetVerkiezing(id).UserID != (int)HttpContext.Session.GetInt32("UserID"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Verkiezing verkiezing = verkiezingContainer.GetVerkiezing(id);
                    List<Kandidaat> lijstKandidatenVerkijzing = kandidaatContainer.GetKandidatenFromVerkiezing(id);
                    foreach (Kandidaat kandidaat in lijstKandidaten)
                    {
                        foreach (Kandidaat kandidaat1 in lijstKandidatenVerkijzing)
                        {
                            if (kandidaat.KandidaatID == kandidaat1.KandidaatID)
                            {
                                kandidaat.Selected = true;
                            }
                        }
                    }

                    verkiezingViewModel.VerkiezingID = verkiezing.VerkiezingID;
                    verkiezingViewModel.VerkiezingNaam = verkiezing.VerkiezingNaam;

                }
            }


            AddVerkiezingViewModel addVerkiezingViewModel = new AddVerkiezingViewModel();
            List<KandidaatViewModel> kandidaatViewModels = new List<KandidaatViewModel>();

            foreach(Kandidaat kandidaat in lijstKandidaten)
            {
                kandidaatViewModels.Add(new KandidaatViewModel(kandidaat.KandidaatID, kandidaat.KandidaatNaam, kandidaat.Selected));
            }

            //kandidaatViewModels[3].Selected = true;

            addVerkiezingViewModel.ListKandidaten = kandidaatViewModels;
            addVerkiezingViewModel.verkiezingViewModel= verkiezingViewModel;

            return View(addVerkiezingViewModel);
        }

        // POST: VerkiezingViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(AddVerkiezingViewModel addVerkiezingViewModel)
        {
            List<Kandidaat> Kandidaten = kandidaatContainer.GetKandidaten((int)HttpContext.Session.GetInt32("UserID"));
            List<Kandidaat> KandidatenInVerkiezing = new List<Kandidaat>();

            if (!ModelState.IsValid)
            {
                for (int i = 0; i < addVerkiezingViewModel.ListKandidaten.Count; i++)
                {
                    if(addVerkiezingViewModel.ListKandidaten[i].Selected == true)
                    {
                        KandidatenInVerkiezing.Add(Kandidaten[i]);

                    }

                }

                if (addVerkiezingViewModel.verkiezingViewModel.VerkiezingID > 0)
                {

                    if (KandidatenInVerkiezing.Count < 2)
                    {
                        return RedirectToAction("AddOrEdit", "VerkiezingViewModels");
                    }
                    else
                    {
                        if (verkiezingContainer.GetVerkiezing(addVerkiezingViewModel.verkiezingViewModel.VerkiezingID).UserID != (int)HttpContext.Session.GetInt32("UserID"))
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        else
                        {
                            Verkiezing verkiezing = new Verkiezing(addVerkiezingViewModel.verkiezingViewModel.VerkiezingID, addVerkiezingViewModel.verkiezingViewModel.VerkiezingNaam);
                            verkiezing.UpdateVerkiezing(new VerkiezingDAL());
                            verkiezing.UpdateVerkiezingKandidaten(new VerkiezingDAL(), KandidatenInVerkiezing);
                        }
                    }
                }
                else
                {
                    if(KandidatenInVerkiezing.Count < 2)
                    {
                        return RedirectToAction("AddOrEdit", "VerkiezingViewModels");
                    }
                    else
                    {
                        verkiezingContainer.AddVerkiezing(new Verkiezing(addVerkiezingViewModel.verkiezingViewModel.VerkiezingID, addVerkiezingViewModel.verkiezingViewModel.VerkiezingNaam, (int)HttpContext.Session.GetInt32("UserID")), KandidatenInVerkiezing);

                    }
                }


                return RedirectToAction(nameof(Index));
            }
            return View(addVerkiezingViewModel);
        }

        // GET: VerkiezingViewModels/Delete/5
        public IActionResult Delete(int id)
        {
            if ((int)HttpContext.Session.GetInt32("UserID") == 0) { return RedirectToAction("Index", "Home"); }

            if(verkiezingContainer.GetVerkiezing(id).UserID == (int)HttpContext.Session.GetInt32("UserID"))
            {
                Verkiezing verkiezing = verkiezingContainer.GetVerkiezing(id);
                return View(new VerkiezingViewModel(verkiezing.VerkiezingID, verkiezing.VerkiezingNaam));
            }
            else { return RedirectToAction("Index", "Home"); }
        }

        // POST: VerkiezingViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (verkiezingContainer.GetVerkiezing(id).UserID == (int)HttpContext.Session.GetInt32("UserID"))
            {
                verkiezingContainer.DeleteVerkiezing(verkiezingContainer.GetVerkiezing(id));
                return RedirectToAction(nameof(Index));

            }
            else { return RedirectToAction("Index", "Home"); }
        }

        
    }
}
